using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public class SpeedometerFrame
	{
		public Bitmap Frame;
		public DateTime Timestamp;

		public SpeedometerFrame(Bitmap Frame, DateTime Timestamp)
		{
			this.Frame = Frame;
			this.Timestamp = Timestamp;
		}
	}
}
