﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IpCameraSpeedometer
{
	public partial class IpCameraSpeedometer : ServiceBase
	{
		public IpCameraSpeedometer()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			ServiceWrapper.Initialize();
		}

		protected override void OnStop()
		{
			ServiceWrapper.Shutdown();
		}
	}
}
