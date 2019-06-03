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
		Speedometer meter;
		CalibrationWindow calibrationWindow;
		bool kph = true;
		decimal lastSpeedKPH = 0;
		Averager speedAverager = new Averager(1000);

		public Configuration()
		{
			InitializeComponent();
			txtStreamUrl.Text = ServiceWrapper.settings.StreamUrl;
			nudPixelsPerMeter.Value = ServiceWrapper.settings.PixelsPerMeter;
			txtUser.Text = ServiceWrapper.settings.StreamUser;
			txtPassword.Text = Util.FromBase64(ServiceWrapper.settings.StreamPass);
			txtOutputFormat.Text = ServiceWrapper.settings.OutputTemplate;
		}
		private void Configuration_FormClosing(object sender, FormClosingEventArgs e)
		{
			meter?.Stop();
			pbCamPreview.Image?.Dispose();
			calibrationWindow?.Close();
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
			ServiceWrapper.settings.StreamPass = Util.ToBase64(txtPassword.Text);
			ServiceWrapper.SaveSettings();
		}

		private void CbShowPassword_CheckedChanged(object sender, EventArgs e)
		{
			if (cbShowPassword.Checked)
				txtPassword.PasswordChar = (char)0;
			else
				txtPassword.PasswordChar = '*';
		}

		private void CbPreviewObjectTracking_CheckedChanged(object sender, EventArgs e)
		{
			if (meter != null)
				meter.HighlightTrackedObjects = cbPreviewObjectTracking.Checked;
		}

		private void TxtOutputFormat_TextChanged(object sender, EventArgs e)
		{
			ServiceWrapper.settings.OutputTemplate = txtOutputFormat.Text;
			ServiceWrapper.SaveSettings();
		}

		private void LblSpeedometerPreview_Click(object sender, EventArgs e)
		{
			kph = !kph;
			UpdateSpeedPreview();
		}
		#endregion

		#region Streaming Preview
		private void BtnLoadStream_Click(object sender, EventArgs e)
		{
			if (meter != null)
			{
				meter.Stop();
				meter = null;
				btnLoadStream.Text = "LOAD";
			}
			else
			{
				meter = new Speedometer(ServiceWrapper.settings, RenderFrame);
				meter.OnError += Meter_OnError;
				meter.OnStop += Meter_OnStop;
				meter.SpeedUpdated += Meter_SpeedUpdated;
				meter.HighlightTrackedObjects = cbPreviewObjectTracking.Checked;
				meter.Start();

				btnLoadStream.Text = "STOP";
			}
		}

		private void Meter_OnStop(object sender, EventArgs e)
		{
			btnLoadStream.Text = "LOAD";
			meter = null;
		}

		private void Meter_SpeedUpdated(object sender, decimal speed)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<object, decimal>)Meter_SpeedUpdated, sender, speed);
			else
			{
				decimal kph = speedAverager.AddSample(speed);
				if (lastSpeedKPH != kph)
				{
					lastSpeedKPH = kph;
					UpdateSpeedPreview();
					ServiceWrapper.WriteSpeedFile(kph);
				}
			}
		}

		private void UpdateSpeedPreview()
		{
			if (kph)
				lblSpeedometerPreview.Text = lastSpeedKPH.ToString("0.0") + " KPH";
			else
				lblSpeedometerPreview.Text = (lastSpeedKPH * 0.621371m).ToString("0.0") + " MPH";
		}

		private void Meter_OnError(object sender, Exception ex)
		{
			MessageBox.Show(ex.ToString());
		}

		private void RenderFrame(SpeedometerFrame frame)
		{
			if (this.InvokeRequired)
				this.Invoke((Action<SpeedometerFrame>)RenderFrame, frame);
			else
			{
				if (calibrationWindow != null)
					calibrationWindow.FeedImage(frame.Frame);
				else
				{
					Image oldImg = pbCamPreview.Image;
					pbCamPreview.Image = frame.Frame;
					oldImg?.Dispose();
				}
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
