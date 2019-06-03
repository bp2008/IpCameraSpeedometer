using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public static class Util
	{
		public static string ToBase64(string text)
		{
			if (text == null)
				return "";
			byte[] utf8Bytes = Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(utf8Bytes);
		}
		public static string FromBase64(string base64)
		{
			if (base64 == null)
				return "";
			byte[] utf8Bytes = Convert.FromBase64String(base64);
			return Encoding.UTF8.GetString(utf8Bytes);
		}
	}
}
