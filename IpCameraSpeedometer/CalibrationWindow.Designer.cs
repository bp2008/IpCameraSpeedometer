namespace IpCameraSpeedometer
{
	partial class CalibrationWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrationWindow));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.rbSetLeftSide = new System.Windows.Forms.RadioButton();
			this.rbSetRightSide = new System.Windows.Forms.RadioButton();
			this.btnFreezeVideo = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudObjectSize = new System.Windows.Forms.NumericUpDown();
			this.cbObjectSizeUnit = new System.Windows.Forms.ComboBox();
			this.btnSaveCalibration = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudObjectSize)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 158);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(510, 285);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseUp);
			// 
			// rbSetLeftSide
			// 
			this.rbSetLeftSide.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbSetLeftSide.AutoSize = true;
			this.rbSetLeftSide.Checked = true;
			this.rbSetLeftSide.Location = new System.Drawing.Point(211, 102);
			this.rbSetLeftSide.Name = "rbSetLeftSide";
			this.rbSetLeftSide.Size = new System.Drawing.Size(78, 23);
			this.rbSetLeftSide.TabIndex = 3;
			this.rbSetLeftSide.TabStop = true;
			this.rbSetLeftSide.Text = "Set Left Side";
			this.rbSetLeftSide.UseVisualStyleBackColor = true;
			// 
			// rbSetRightSide
			// 
			this.rbSetRightSide.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbSetRightSide.AutoSize = true;
			this.rbSetRightSide.Location = new System.Drawing.Point(312, 102);
			this.rbSetRightSide.Name = "rbSetRightSide";
			this.rbSetRightSide.Size = new System.Drawing.Size(85, 23);
			this.rbSetRightSide.TabIndex = 4;
			this.rbSetRightSide.Text = "Set Right Side";
			this.rbSetRightSide.UseVisualStyleBackColor = true;
			// 
			// btnFreezeVideo
			// 
			this.btnFreezeVideo.Location = new System.Drawing.Point(12, 102);
			this.btnFreezeVideo.Name = "btnFreezeVideo";
			this.btnFreezeVideo.Size = new System.Drawing.Size(128, 23);
			this.btnFreezeVideo.TabIndex = 2;
			this.btnFreezeVideo.Text = "Freeze Video";
			this.btnFreezeVideo.UseVisualStyleBackColor = true;
			this.btnFreezeVideo.Click += new System.EventHandler(this.BtnFreezeVideo_Click);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.SystemColors.Window;
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(510, 84);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 133);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Object Width:";
			// 
			// nudObjectSize
			// 
			this.nudObjectSize.DecimalPlaces = 3;
			this.nudObjectSize.Location = new System.Drawing.Point(90, 131);
			this.nudObjectSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.nudObjectSize.Name = "nudObjectSize";
			this.nudObjectSize.Size = new System.Drawing.Size(115, 20);
			this.nudObjectSize.TabIndex = 6;
			// 
			// cbObjectSizeUnit
			// 
			this.cbObjectSizeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbObjectSizeUnit.FormattingEnabled = true;
			this.cbObjectSizeUnit.Items.AddRange(new object[] {
            "meters",
            "feet",
            "inches"});
			this.cbObjectSizeUnit.Location = new System.Drawing.Point(211, 130);
			this.cbObjectSizeUnit.Name = "cbObjectSizeUnit";
			this.cbObjectSizeUnit.Size = new System.Drawing.Size(121, 21);
			this.cbObjectSizeUnit.TabIndex = 7;
			// 
			// btnSaveCalibration
			// 
			this.btnSaveCalibration.Location = new System.Drawing.Point(359, 128);
			this.btnSaveCalibration.Name = "btnSaveCalibration";
			this.btnSaveCalibration.Size = new System.Drawing.Size(163, 23);
			this.btnSaveCalibration.TabIndex = 8;
			this.btnSaveCalibration.Text = "Save Calibration";
			this.btnSaveCalibration.UseVisualStyleBackColor = true;
			this.btnSaveCalibration.Click += new System.EventHandler(this.BtnSaveCalibration_Click);
			// 
			// CalibrationWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(534, 455);
			this.Controls.Add(this.btnSaveCalibration);
			this.Controls.Add(this.cbObjectSizeUnit);
			this.Controls.Add(this.nudObjectSize);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.btnFreezeVideo);
			this.Controls.Add(this.rbSetRightSide);
			this.Controls.Add(this.rbSetLeftSide);
			this.Controls.Add(this.pictureBox1);
			this.Name = "CalibrationWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "CalibrationWindow";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudObjectSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.RadioButton rbSetLeftSide;
		private System.Windows.Forms.RadioButton rbSetRightSide;
		private System.Windows.Forms.Button btnFreezeVideo;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudObjectSize;
		private System.Windows.Forms.ComboBox cbObjectSizeUnit;
		private System.Windows.Forms.Button btnSaveCalibration;
	}
}