using BPUtil;
using OpenH264Lib;
using RtspClientSharp;
using RtspClientSharp.RawFrames.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpCameraSpeedometer
{
	public partial class Configuration : Form
	{
		BackgroundWorker bwStreamer;
		CancellationTokenSource streamingCancelTokenSource;
		CalibrationWindow calibrationWindow;
		public Configuration()
		{
			InitializeComponent();
			txtStreamUrl.Text = ServiceWrapper.settings.StreamUrl;
			nudPixelsPerMeter.Value = ServiceWrapper.settings.PixelsPerMeter;
			txtUser.Text = ServiceWrapper.settings.StreamUser;
			txtPassword.Text = FromBase64(ServiceWrapper.settings.StreamPass);
		}
		private void Configuration_FormClosing(object sender, FormClosingEventArgs e)
		{
			streamingCancelTokenSource?.Cancel();
			pbCamPreview.Image?.Dispose();
		}

		#region Input Change Events
		private void TxtStreamUrl_TextChanged(object sender, EventArgs e)
		{
			ServiceWrapper.settings.StreamUrl = txtStreamUrl.Text;
			ServiceWrapper.SaveSettings();
		}

		private bool isAutoSettingPPM = false;
		private void NudPixelsPerMeter_ValueChanged(object sender, EventArgs e)
		{
			ServiceWrapper.settings.PixelsPerMeter = nudPixelsPerMeter.Value;
			ServiceWrapper.SaveSettings();

			if (!isAutoSettingPPM)
				nudPixelsPerFoot.Value = nudPixelsPerMeter.Value / 3.28084m;
		}
		private void NudPixelsPerFoot_ValueChanged(object sender, EventArgs e)
		{
			isAutoSettingPPM = true;
			nudPixelsPerMeter.Value = nudPixelsPerFoot.Value * 3.28084m;
			isAutoSettingPPM = false;
		}

		private void TxtUser_TextChanged(object sender, EventArgs e)
		{
			ServiceWrapper.settings.StreamUser = txtUser.Text;
			ServiceWrapper.SaveSettings();
		}

		private void TxtPassword_TextChanged(object sender, EventArgs e)
		{
			ServiceWrapper.settings.StreamPass = ToBase64(txtPassword.Text);
			ServiceWrapper.SaveSettings();
		}

		private void CbShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (cbShowPassword.Checked)
				txtPassword.PasswordChar = (char)0;
			else
				txtPassword.PasswordChar = '*';
		}

		private string ToBase64(string text)
		{
			if (text == null)
				return "";
			byte[] utf8Bytes = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(utf8Bytes);
		}
		private string FromBase64(string base64)
		{
			if (base64 == null)
				return "";
			byte[] utf8Bytes = Convert.FromBase64String(base64);
			return Encoding.UTF8.GetString(utf8Bytes);
		}
		#endregion

		#region Streaming Preview
		private void BtnLoadStream_Click(object sender, EventArgs e)
		{
			if (streamingCancelTokenSource != null)
			{
				streamingCancelTokenSource?.Cancel();
				streamingCancelTokenSource = null;
				btnLoadStream.Text = "LOAD";
			}
			else
			{
				bwStreamer = new BackgroundWorker();
				bwStreamer.WorkerReportsProgress = true;
				bwStreamer.WorkerSupportsCancellation = true;
				bwStreamer.DoWork += BwStreamer_DoWork;
				bwStreamer.ProgressChanged += BwStreamer_ProgressChanged;

				streamingCancelTokenSource = new CancellationTokenSource();
				streamingCancelTokenSource.Token.Register(bwStreamer.CancelAsync);

				bwStreamer.RunWorkerAsync(new
				{
					bw = bwStreamer,
					cancellationToken = streamingCancelTokenSource.Token
				});
				btnLoadStream.Text = "STOP";
			}
		}

		private void BwStreamer_DoWork(object sender, DoWorkEventArgs e)
		{
			try
			{
				dynamic args = e.Argument;
				BackgroundWorker bw = (BackgroundWorker)args.bw;
				CancellationToken cancellationToken = (CancellationToken)args.cancellationToken;

				Uri serverUri = new Uri(ServiceWrapper.settings.StreamUrl);

				ConnectionParameters connectionParameters;
				if (!string.IsNullOrWhiteSpace(ServiceWrapper.settings.StreamUser))
					connectionParameters = new ConnectionParameters(serverUri, new NetworkCredential(ServiceWrapper.settings.StreamUser, FromBase64(ServiceWrapper.settings.StreamPass)));
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
							DecodeFrame(bw, decoder, iFrame.SpsPpsSegment.ToArray());
							DecodeFrame(bw, decoder, iFrame.FrameSegment.ToArray());
						}
						else if (frame is RawH264PFrame)
						{
							DecodeFrame(bw, decoder, frame.FrameSegment.ToArray());
						}
					};
					if (!bw.CancellationPending)
						rtspClient.ConnectAsync(cancellationToken).Wait();

					if (!bw.CancellationPending)
						rtspClient.ReceiveAsync(cancellationToken).Wait();
				}
			}
			catch (TaskCanceledException) { }
			catch (Exception ex)
			{
				if (ex is AggregateException)
				{
					AggregateException aex = ex as AggregateException;
					if (aex.InnerExceptions.Count == 1 && aex.InnerException is TaskCanceledException)
						return;
				}
				Logger.Debug(ex);
				MessageBox.Show(ex.ToString());
			}
		}

		private void DecodeFrame(BackgroundWorker bw, OpenH264Lib.Decoder decoder, byte[] frame)
		{
			Bitmap bmp = decoder.Decode(frame, frame.Length);
			if (bmp != null)
				bw.ReportProgress(0, bmp);
		}

		private void BwStreamer_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			Image newImg = (Image)e.UserState;
			if (calibrationWindow != null)
				calibrationWindow.FeedImage(newImg);
			else
			{
				Image oldImg = pbCamPreview.Image;
				pbCamPreview.Image = newImg;
				oldImg?.Dispose();
			}
		}
		#endregion

		#region Calibration Window
		private void PbCamPreview_Click(object sender, EventArgs e)
		{
			if (calibrationWindow != null)
			{
				calibrationWindow.Focus();
				return;
			}
			calibrationWindow = new CalibrationWindow();
			calibrationWindow.CalibrationFinished += CalibrationWindow_CalibrationFinished;
			calibrationWindow.FormClosed += (sender2, e2) => { calibrationWindow = null; };
			calibrationWindow.Show();

			pbCamPreview.Image?.Dispose();
			pbCamPreview.Image = null;
		}

		private void CalibrationWindow_CalibrationFinished(object sender, decimal pixelsPerMeter)
		{
			nudPixelsPerMeter.Value = pixelsPerMeter;
		}
		#endregion
	}
}
