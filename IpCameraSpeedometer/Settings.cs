using BPUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public class Settings : SerializableObjectBase
	{
		public string ServiceName = "IpCameraSpeedometer";
		public string StreamUrl;
		public string StreamUser;
		public string StreamPass;
		public decimal PixelsPerMeter;
		public string OutputTemplate;
	}
}
