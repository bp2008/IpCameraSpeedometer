using System;
using BPUtil;

namespace IpCameraSpeedometer
{
	public static class ServiceWrapper
	{
		public static Settings settings = new Settings();
		/// <summary>
		/// If this is true when the service manager form is closed, it will be set to false and the service manager form will be re-opened.
		/// </summary>
		public static bool restartServiceManager = false;
		static ServiceWrapper()
		{
			settings.Load(Globals.ConfigFilePath);
		}

		public static void SaveSettings()
		{
			settings.Save(Globals.ConfigFilePath);
		}
	}
}