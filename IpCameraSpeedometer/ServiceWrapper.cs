using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
			if (string.IsNullOrWhiteSpace(settings.ServiceName))
				settings.ServiceName = "IpCameraSpeedometer";
			if (settings.OutputTemplate == null)
				settings.OutputTemplate = "";
			settings.OutputTemplate = settings.OutputTemplate.Replace("\r\n", "\n").Replace("\n", "\r\n"); // Restore lost \r characters.
		}

		public static void SaveSettings()
		{
			settings.Save(Globals.ConfigFilePath);
		}

		public static void WriteSpeedFile(decimal kph)
		{
			string filePath = Globals.ApplicationDirectoryBase + "speed.txt";
			string speedText = GenerateSpeedText(kph);
			for (int i = 0; i < 5; i++)
			{
				try
				{
					File.WriteAllText(filePath, speedText, Encoding.UTF8);
					return;
				}
				catch (IOException)
				{
					Thread.Sleep(10);
				}
			}
		}

		private static Regex rxMeter = new Regex("%HMETER-(\\d+)-([0-9\\.]+)%", RegexOptions.Compiled);
		private static string GenerateSpeedText(decimal kph)
		{
			string txt = settings.OutputTemplate;
			if (string.IsNullOrEmpty(txt))
				return "";
			txt = txt.Replace("%KPH%", kph.ToString("0"));
			txt = txt.Replace("%MPH%", (kph * 0.621371m).ToString("0"));
			txt = txt.Replace("%KPH1%", kph.ToString("0.0"));
			txt = txt.Replace("%MPH1%", (kph * 0.621371m).ToString("0.0"));
			txt = txt.Replace("%KPH2%", kph.ToString("0.00"));
			txt = txt.Replace("%MPH2%", (kph * 0.621371m).ToString("0.00"));

			Match m = rxMeter.Match(txt);
			while (m.Success)
			{
				int maxLength = int.Parse(m.Groups[1].Value);
				decimal filledAtKph = decimal.Parse(m.Groups[2].Value);
				decimal filled = (kph / filledAtKph) * maxLength;
				StringBuilder meter = new StringBuilder(maxLength);
				for (int i = 0; i < maxLength; i++)
				{
					decimal overshoot = filled - i;
					if (overshoot > 0.75m)
					{
						meter.Append('█'); // 219
					}
					else if (overshoot > 0.5m)
					{
						meter.Append('▓'); // 178
					}
					else if (overshoot > 0.25m)
					{
						meter.Append('▒'); // 177
					}
					else if (overshoot > 0.001m)
					{
						meter.Append('░'); // 176
					}
					else
						meter.Append(' ');
				}
				txt = txt.Remove(m.Index, m.Length).Insert(m.Index, meter.ToString());
				m = rxMeter.Match(txt);
			}
			return txt;
		}

		#region Service
		static Speedometer speedometer;
		static volatile bool shuttingDown = false;
		static decimal? lastWrittenSpeed = null;
		static Averager speedAverager = new Averager(1000);
		/// <summary>
		/// Starts the service's background threads. Should only be called once.
		/// </summary>
		public static void Initialize()
		{
			if (speedometer != null)
				return;
			speedometer = new Speedometer(settings, null);
			speedometer.OnStop += Speedometer_OnStop;
			speedometer.SpeedUpdated += Speedometer_SpeedUpdated;
			speedometer.Start();
		}
		/// <summary>
		/// Stops the service's background threads. Should only be called once.
		/// </summary>
		public static void Shutdown()
		{
			shuttingDown = true;
			speedometer?.Stop();
		}

		private static void Speedometer_OnStop(object sender, EventArgs e)
		{
			if (!shuttingDown)
				speedometer.Start();
		}

		private static void Speedometer_SpeedUpdated(object sender, decimal kph)
		{
			kph = speedAverager.AddSample(kph);
			if (lastWrittenSpeed == null || lastWrittenSpeed.Value != kph)
			{
				lastWrittenSpeed = kph;
				WriteSpeedFile(kph);
			}
		}
		#endregion
	}
}