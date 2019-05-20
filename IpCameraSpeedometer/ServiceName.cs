using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BPUtil.Forms;

namespace IpCameraSpeedometer
{
	public partial class ServiceName : UserControl
	{
		private string currentServiceName;
		private Action<string> onNameChanged;
		public ServiceName(string currentServiceName, Action<string> onNameChanged)
		{
			this.currentServiceName = currentServiceName;
			this.onNameChanged = onNameChanged;
			InitializeComponent();
			lblServiceName.Text = currentServiceName;
		}

		private void BtnChangeServiceName_Click(object sender, EventArgs e)
		{
			InputDialog txtInput = new InputDialog("Service Name", "Enter a new name:");
			if (txtInput.ShowDialog() == DialogResult.OK)
			{
				onNameChanged(txtInput.InputText);
			}
		}
	}
}
