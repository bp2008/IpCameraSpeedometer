using BPUtil;
using BPUtil.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpCameraSpeedometer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			Globals.Initialize(Application.ExecutablePath);
			PrivateAccessor.SetStaticFieldValue(typeof(Globals), "configFilePath", Globals.WritableDirectoryBase + "IpCameraSpeedometerSettings.xml");

			if (Environment.UserInteractive)
			{
				Settings settings = new Settings();
				settings.Load(Globals.ConfigFilePath);
				settings.SaveIfNoExist(Globals.ConfigFilePath);
				string Title = "IpCameraSpeedometer " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " Service Manager";
				ButtonDefinition btnConfiguration = new ButtonDefinition("Configuration", btnConfiguration_Click);
				ServiceManager serviceManager = new ServiceManager(Title, ServiceWrapper.settings.ServiceName, new ButtonDefinition[] { btnConfiguration }, new ServiceName(ServiceWrapper.settings.ServiceName, ServiceNameChange));
				Application.Run(serviceManager);
				while (ServiceWrapper.restartServiceManager)
				{
					ServiceWrapper.restartServiceManager = false;
					serviceManager = new ServiceManager(Title, ServiceWrapper.settings.ServiceName, new ButtonDefinition[] { btnConfiguration }, new ServiceName(ServiceWrapper.settings.ServiceName, ServiceNameChange));
					Application.Run(serviceManager);
				}
			}
			else
			{
				ServiceBase[] ServicesToRun;
				ServicesToRun = new ServiceBase[]
				{
					new IpCameraSpeedometer()
				};
				ServiceBase.Run(ServicesToRun);
			}
		}

		private static void btnConfiguration_Click(object sender, EventArgs e)
		{
			new Configuration().ShowDialog();
		}

		private static void ServiceNameChange(string newName)
		{
			ServiceWrapper.settings.ServiceName = newName;
			ServiceWrapper.SaveSettings();
			ServiceWrapper.restartServiceManager = true;
			Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
			foreach (Form f in forms)
				f.Close();
		}
	}
}
