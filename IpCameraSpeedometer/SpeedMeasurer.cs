using Accord.Imaging;
using Accord.Vision.Motion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace IpCameraSpeedometer
{
	public class SpeedMeasurer
	{
		public bool HighlightTrackedObjects
		{
			get
			{
				return blobCounter.HighlightMotionRegions;
			}
			set
			{
				blobCounter.HighlightMotionRegions = value;
			}
		}

		MotionDetector detector;
		BlobCountingObjectsProcessing blobCounter;
		Rectangle? lastLargest = null;
		DateTime? lastFrameTime = null;

		public SpeedMeasurer()
		{
			blobCounter = new BlobCountingObjectsProcessing(50, 50, false);
			blobCounter.HighlightColor = Color.FromArgb(0, 255, 0);
			SimpleBackgroundModelingDetector bmd = new SimpleBackgroundModelingDetector(true, true);
			bmd.FramesPerBackgroundUpdate = 1;
			detector = new MotionDetector(bmd, blobCounter);
		}
		private Rectangle? GetLargestRectangle(IEnumerable<Rectangle> rects)
		{
			Rectangle? largest = null;
			int largestSize = 0;
			foreach (Rectangle r in rects)
			{
				int size = r.Width * r.Height;
				if (size > largestSize)
				{
					largest = r;
					largestSize = size;
				}
			}
			return largest;
		}
		/// <summary>
		/// Returns the speed of the largest moving object between this frame and the last. May return null.
		/// </summary>
		/// <param name="frame"></param>
		/// <returns></returns>
		public SpeedMeasurement Measure(SpeedometerFrame frame)
		{
			float motion;
			BitmapData data = frame.Frame.LockBits(ImageLockMode.ReadWrite);
			try
			{
				using (UnmanagedImage unmanagedImage = new UnmanagedImage(data))
					motion = detector.ProcessFrame(unmanagedImage);
			}
			finally
			{
				frame.Frame.UnlockBits(data);
			}

			Rectangle? largest = GetLargestRectangle(blobCounter.ObjectRectangles);
			SpeedMeasurement result = null;
			if (largest != null && lastLargest != null)
			{
				decimal distLeftPx = largest.Value.Left - lastLargest.Value.Left;
				decimal distRightPx = largest.Value.Right - lastLargest.Value.Right;

				decimal absDistLeftPx = Math.Abs(distLeftPx);
				decimal absDistRightPx = Math.Abs(distRightPx);
				decimal greater = absDistLeftPx > absDistRightPx ? distLeftPx : distRightPx;
				if (greater != 0)
				{
					result = new SpeedMeasurement(greater > 0 ? MovementDirection.Left : MovementDirection.Right, Math.Abs(greater), frame.Timestamp - lastFrameTime.Value);
				}
				else
				{
					decimal dx = largest.Value.Center().X - lastLargest.Value.Center().X;
					if (dx > 0)
						result = new SpeedMeasurement(MovementDirection.Right, dx, frame.Timestamp - lastFrameTime.Value);
					else if (dx < 0)
						result = new SpeedMeasurement(MovementDirection.Left, -dx, frame.Timestamp - lastFrameTime.Value);
				}
			}
			lastFrameTime = frame.Timestamp;
			lastLargest = largest;
			return result;
		}
	}
}