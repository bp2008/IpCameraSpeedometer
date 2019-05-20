using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpCameraSpeedometer
{
	public partial class CalibrationWindow : Form
	{
		bool frozen = false;
		int leftEdgePx = 20;
		int rightEdgePx = 40;
		/// <summary>
		/// An event which is raised when calibration is finished. The event argument is the number of pixels per meter.
		/// </summary>
		public event EventHandler<decimal> CalibrationFinished = delegate { };
		public CalibrationWindow()
		{
			InitializeComponent();
			cbObjectSizeUnit.SelectedIndex = 0;
			textBox1.SelectionStart = textBox1.SelectionLength = 0;
		}

		public void FeedImage(Image newImg)
		{
			if (!frozen)
			{
				Image oldImg = pictureBox1.Image;
				pictureBox1.Image = newImg;
				if (pictureBox1.Size != newImg.Size)
					pictureBox1.Size = newImg.Size;
				oldImg?.Dispose();
			}
		}

		private void BtnFreezeVideo_Click(object sender, EventArgs e)
		{
			frozen = !frozen;
			if (frozen)
				btnFreezeVideo.Text = "Unfreeze Video";
			else
				btnFreezeVideo.Text = "Freeze Video";
		}

		private void BtnSaveCalibration_Click(object sender, EventArgs e)
		{
			decimal s = nudObjectSize.Value;
			if (cbObjectSizeUnit.SelectedItem?.ToString() == "feet")
				s /= 3.28084m;
			else if (cbObjectSizeUnit.SelectedItem?.ToString() == "inches")
				s /= 39.3701m;
			if (s <= 0)
			{
				MessageBox.Show("Please enter a positive nonzero value for object size.");
				return;
			}
			// s is now a number of meters
			int pixels = Math.Abs(rightEdgePx - leftEdgePx);
			if (pixels == 0)
			{
				MessageBox.Show("The left and right edges are located at exactly the same place! Please re-read the instructions.");
				return;
			}

			decimal ppm = pixels / s;
			decimal ppf = ppm / 3.28084m;
			CalibrationFinished(this, ppm);
			MessageBox.Show("Calibration result:" + Environment.NewLine + Environment.NewLine
				+ pixels + "px / " + s.ToString("0.##") + "m" + Environment.NewLine + Environment.NewLine
				+ ppm.ToString("0.#") + " Pixels Per Meter" + Environment.NewLine
				+ ppf.ToString("0.#") + " Pixels Per Foot");
		}
		#region Drawing edge lines
		bool mouseDown = false;
		private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDown = true;
			UpdateLine(e);
		}

		private void UpdateLine(MouseEventArgs e)
		{
			if (rbSetLeftSide.Checked)
				leftEdgePx = e.X;
			if (rbSetRightSide.Checked)
				rightEdgePx = e.X;
			pictureBox1.Invalidate();
		}

		private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			if (mouseDown)
				UpdateLine(e);
		}

		private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
		}

		private static SolidBrush redBrush = new SolidBrush(Color.FromArgb(255, 0, 0));
		private static SolidBrush greenBrush = new SolidBrush(Color.FromArgb(0, 255, 0));
		private static Pen redPen = new Pen(redBrush, 1);
		private static Pen greenPen = new Pen(greenBrush, 1);
		private static Font drawFont = new Font("Arial", 12, FontStyle.Bold);
		private void PictureBox1_Paint(object sender, PaintEventArgs e)
		{
			DrawLine(e.Graphics, true, leftEdgePx, "Left Edge");
			DrawLine(e.Graphics, false, rightEdgePx, "Right Edge");
		}
		private void DrawLine(Graphics g, bool red, int x, string name)
		{
			g.DrawLine(red ? redPen : greenPen, new Point(x, 0), new Point(x, pictureBox1.Height - 1));

			StringFormat drawFormat = new StringFormat(StringFormatFlags.DirectionVertical);
			g.DrawString(name, drawFont, red ? redBrush : greenBrush, x, 20, drawFormat);
		}
		#endregion
	}
}
