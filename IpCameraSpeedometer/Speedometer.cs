using BPUtil;
using RtspClientSharp;
using RtspClientSharp.RawFrames.Video;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public class Speedometer
	{
		/// <summary>
		/// The URL for the RTSP video stream.
		/// </summary>
		public string Url { get; protected set; }
		/// <summary>
		/// The user name for the video stream.
		/// </summary>
		public string Username { get; protected set; }
		/// <summary>
		/// The password for the video stream.
		/// </summary>
		public string Password { get; protected set; }
		/// <summary>
		/// If true, the speedometer is running.
		/// </summary>
		public bool Running { get; protected set; }
		/// <summary>
		/// The last measured speed in kilometers per hour.
		/// </summary>
		public decimal SpeedKph { get; protected set; } = 0;

		protected decimal? lastReportedSpeedKPH = null;

		private bool highlightTrackedObjects = false;
		/// <summary>
		/// Gets or sets a value that specifies if tracked objects should be visually highlighted in frames.
		/// </summary>
		public bool HighlightTrackedObjects
		{
			get
			{
				return highlightTrackedObjects;
			}
			set
			{
				highlightTrackedObjects = value;
				lock (myLock)
				{
					if (speedMeasurer != null)
						speedMeasurer.HighlightTrackedObjects = value;
				}
			}
		}

		/// <summary>
		/// An event which is raised when the detected speed has changed. The event argument is the number of kilometers per hour.
		/// </summary>
		public event EventHandler<decimal> SpeedChanged = delegate { };
		/// <summary>
		/// An event which is raised when the detected speed has been updated (it may be the same as last time). The event argument is the number of kilometers per hour.
		/// </summary>
		public event EventHandler<decimal> SpeedUpdated = delegate { };
		/// <summary>
		/// An event which is raised when an unrecoverable error occurs.
		/// </summary>
		public event EventHandler<Exception> OnError = delegate { };
		/// <summary>
		/// An event which is raised when the Speedometer stops.
		/// </summary>
		public event EventHandler OnStop = delegate { };

		protected object myLock = new object();
		protected Thread StreamingThread;
		protected Thread ImageProcessingThread;
		protected ConcurrentQueue<SpeedometerFrame> FrameQueue = new ConcurrentQueue<SpeedometerFrame>();
		protected CancellationTokenSource streamingCancelTokenSource;
		protected SpeedMeasurer speedMeasurer;

		protected Settings settings;
		/// <summary>
		/// An callback which is called when this class is finished with a video frame. If an action is assigned to this callback, the Speedometer class will no longer be responsible for disposing the Bitmap.
		/// </summary>
		protected Action<SpeedometerFrame> FrameFinished = null;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="settings">A settings object containing the URL, user name, and password.</param>
		/// <param name="FrameFinished"> An callback which is called when this class is finished with a video frame. Bitmaps sent to this callback will not be the responsibility of the Speedometer to dispose.</param>
		public Speedometer(Settings settings, Action<SpeedometerFrame> FrameFinished)
		{
			this.settings = settings;
			this.Url = settings.StreamUrl;
			this.Username = settings.StreamUser;
			this.Password = Util.FromBase64(settings.StreamPass);
			this.FrameFinished = FrameFinished;
		}

		public void Start()
		{
			if (Running)
				return;
			lock (myLock)
			{
				if (Running)
					return;

				Running = true;

				lastReportedSpeedKPH = null;

				while (!FrameQueue.IsEmpty && FrameQueue.TryDequeue(out SpeedometerFrame f))
					f.Frame.Dispose();

				streamingCancelTokenSource = new CancellationTokenSource();

				speedMeasurer = new SpeedMeasurer();
				speedMeasurer.HighlightTrackedObjects = HighlightTrackedObjects;

				StreamingThread = new Thread(streamingThreadLoop);
				StreamingThread.IsBackground = true;
				StreamingThread.Name = "Stream";
				StreamingThread.Start();

				ImageProcessingThread = new Thread(imageProcessingThreadLoop);
				ImageProcessingThread.IsBackground = true;
				ImageProcessingThread.Name = "ImgProc";
				ImageProcessingThread.Start();
			}
		}

		public void Stop()
		{
			if (!Running)
				return;
			lock (myLock)
			{
				if (!Running)
					return;

				Running = false;

				streamingCancelTokenSource?.Cancel();
				StreamingThread?.Abort();
				ImageProcessingThread?.Abort();

				while (!FrameQueue.IsEmpty && FrameQueue.TryDequeue(out SpeedometerFrame f))
					f.Frame.Dispose();

				speedMeasurer = null;

				OnStop(this, null);
			}
		}

		protected void streamingThreadLoop()
		{
			try
			{
				CancellationToken cancellationToken = streamingCancelTokenSource.Token;

				Uri serverUri = new Uri(Url);

				ConnectionParameters connectionParameters;
				if (!string.IsNullOrWhiteSpace(Username))
					connectionParameters = new ConnectionParameters(serverUri, new NetworkCredential(Username, Password));
				else
					connectionParameters = new ConnectionParameters(serverUri);
				connectionParameters.RtpTransport = RtpTransportProtocol.TCP;

				string openH264DllPath = Globals.ApplicationDirectoryBase + "openh264-1.8.0-win" + (Environment.Is64BitProcess ? "64" : "32") + ".dll";
				using (OpenH264Lib.Decoder decoder = new OpenH264Lib.Decoder(openH264DllPath))
				using (RtspClient rtspClient = new RtspClient(connectionParameters))
				{
					rtspClient.FrameReceived += (sender2, frame) =>
					{
						//process (e.g. decode/save to file) encoded frame here or 
						//make deep copy to use it later because frame buffer (see FrameSegment property) will be reused by client
						if (frame is RawH264IFrame)
						{
							RawH264IFrame iFrame = frame as RawH264IFrame;
							DecodeFrame(decoder, iFrame.SpsPpsSegment.ToArray(), frame.Timestamp);
							DecodeFrame(decoder, iFrame.FrameSegment.ToArray(), frame.Timestamp);
						}
						else if (frame is RawH264PFrame)
						{
							DecodeFrame(decoder, frame.FrameSegment.ToArray(), frame.Timestamp);
						}
					};
					rtspClient.ConnectAsync(cancellationToken).Wait();
					rtspClient.ReceiveAsync(cancellationToken).Wait();
				}
			}
			catch (ThreadAbortException) { }
			catch (TaskCanceledException)
			{
				Stop();
			}
			catch (Exception ex)
			{
				if (ex is AggregateException)
				{
					AggregateException aex = ex as AggregateException;
					if (aex.InnerExceptions.Count == 1 && aex.InnerException is TaskCanceledException)
						return;
				}
				Logger.Debug(ex);
				OnError(this, ex);
				Stop();
			}
		}
		protected void DecodeFrame(OpenH264Lib.Decoder decoder, byte[] frame, DateTime timestamp)
		{
			Bitmap bmp = decoder.Decode(frame, frame.Length);
			if (bmp != null)
				FrameQueue.Enqueue(new SpeedometerFrame(bmp, timestamp));
		}

		protected void imageProcessingThreadLoop()
		{
			try
			{
				while (true)
				{
					while (FrameQueue.TryDequeue(out SpeedometerFrame frame))
					{
						try
						{
							ProcessFrame(frame);

							if (FrameFinished != null)
								FrameFinished(frame);
						}
						finally
						{
							if (FrameFinished == null)
								frame.Frame?.Dispose();
						}
					}
					Thread.Sleep(1);
				}
			}
			catch (ThreadAbortException) { }
			catch (Exception ex)
			{
				Logger.Debug(ex);
				OnError(this, ex);
				Stop();
			}
		}

		protected void ProcessFrame(SpeedometerFrame frame)
		{
			SpeedMeasurement result = speedMeasurer.Measure(frame);
			decimal PixelsPerHour = result == null ? 0 : result.SpeedPPH;
			decimal MetersPerHour = settings.PixelsPerMeter <= 0 ? 0 : PixelsPerHour / settings.PixelsPerMeter;
			SpeedKph = MetersPerHour / 1000;

			SpeedUpdated(this, SpeedKph);
			if (lastReportedSpeedKPH == null || SpeedKph != lastReportedSpeedKPH)
			{
				lastReportedSpeedKPH = SpeedKph;
				SpeedChanged(this, SpeedKph);
			}
		}
	}
}
