namespace IpCameraSpeedometer
{
	partial class Configuration
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuration));
			this.pbCamPreview = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtStreamUrl = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nudPixelsPerMeter = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.btnLoadStream = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.txtOutputFormat = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.cbShowPassword = new System.Windows.Forms.CheckBox();
			this.nudPixelsPerFoot = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbCamPreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPixelsPerMeter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPixelsPerFoot)).BeginInit();
			this.SuspendLayout();
			// 
			// pbCamPreview
			// 
			this.pbCamPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pbCamPreview.Location = new System.Drawing.Point(333, 71);
			this.pbCamPreview.Name = "pbCamPreview";
			this.pbCamPreview.Size = new System.Drawing.Size(320, 180);
			this.pbCamPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCamPreview.TabIndex = 0;
			this.pbCamPreview.TabStop = false;
			this.pbCamPreview.Click += new System.EventHandler(this.PbCamPreview_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Stream URL:";
			// 
			// txtStreamUrl
			// 
			this.txtStreamUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStreamUrl.Location = new System.Drawing.Point(86, 6);
			this.txtStreamUrl.Name = "txtStreamUrl";
			this.txtStreamUrl.Size = new System.Drawing.Size(486, 20);
			this.txtStreamUrl.TabIndex = 1;
			this.txtStreamUrl.TextChanged += new System.EventHandler(this.TxtStreamUrl_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(333, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(320, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Preview";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(13, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Pixels Per Meter:";
			// 
			// nudPixelsPerMeter
			// 
			this.nudPixelsPerMeter.DecimalPlaces = 1;
			this.nudPixelsPerMeter.Location = new System.Drawing.Point(12, 110);
			this.nudPixelsPerMeter.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.nudPixelsPerMeter.Name = "nudPixelsPerMeter";
			this.nudPixelsPerMeter.Size = new System.Drawing.Size(111, 20);
			this.nudPixelsPerMeter.TabIndex = 6;
			this.nudPixelsPerMeter.ValueChanged += new System.EventHandler(this.NudPixelsPerMeter_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
			this.label4.Location = new System.Drawing.Point(12, 67);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 20);
			this.label4.TabIndex = 6;
			this.label4.Text = "Calibration";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(13, 159);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(314, 92);
			this.label9.TabIndex = 11;
			this.label9.Text = "To calibrate visually:\r\n\r\n1) Load a video stream\r\n2) Click the preview video to t" +
    "he right\r\n3) Follow the provided instructions";
			// 
			// btnLoadStream
			// 
			this.btnLoadStream.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadStream.Location = new System.Drawing.Point(578, 4);
			this.btnLoadStream.Name = "btnLoadStream";
			this.btnLoadStream.Size = new System.Drawing.Size(75, 23);
			this.btnLoadStream.TabIndex = 2;
			this.btnLoadStream.Text = "LOAD";
			this.btnLoadStream.UseVisualStyleBackColor = true;
			this.btnLoadStream.Click += new System.EventHandler(this.BtnLoadStream_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
			this.label6.Location = new System.Drawing.Point(12, 251);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(58, 20);
			this.label6.TabIndex = 13;
			this.label6.Text = "Output";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(12, 280);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(89, 13);
			this.label7.TabIndex = 15;
			this.label7.Text = "Output Template:";
			// 
			// txtOutputFormat
			// 
			this.txtOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutputFormat.Location = new System.Drawing.Point(12, 296);
			this.txtOutputFormat.Multiline = true;
			this.txtOutputFormat.Name = "txtOutputFormat";
			this.txtOutputFormat.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutputFormat.Size = new System.Drawing.Size(641, 136);
			this.txtOutputFormat.TabIndex = 8;
			// 
			// label8
			// 
			this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label8.Location = new System.Drawing.Point(12, 435);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(641, 152);
			this.label8.TabIndex = 16;
			this.label8.Text = resources.GetString("label8.Text");
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(112, 32);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(152, 20);
			this.txtUser.TabIndex = 3;
			this.txtUser.TextChanged += new System.EventHandler(this.TxtUser_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(12, 35);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(94, 13);
			this.label10.TabIndex = 18;
			this.label10.Text = "Stream Username:";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(375, 32);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(152, 20);
			this.txtPassword.TabIndex = 4;
			this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(277, 35);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(92, 13);
			this.label11.TabIndex = 20;
			this.label11.Text = "Stream Password:";
			// 
			// cbShowPassword
			// 
			this.cbShowPassword.AutoSize = true;
			this.cbShowPassword.Location = new System.Drawing.Point(533, 34);
			this.cbShowPassword.Name = "cbShowPassword";
			this.cbShowPassword.Size = new System.Drawing.Size(102, 17);
			this.cbShowPassword.TabIndex = 5;
			this.cbShowPassword.Text = "Show Password";
			this.cbShowPassword.UseVisualStyleBackColor = true;
			this.cbShowPassword.CheckedChanged += new System.EventHandler(this.CbShowPassword_CheckedChanged);
			// 
			// nudPixelsPerFoot
			// 
			this.nudPixelsPerFoot.DecimalPlaces = 1;
			this.nudPixelsPerFoot.Location = new System.Drawing.Point(200, 110);
			this.nudPixelsPerFoot.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.nudPixelsPerFoot.Name = "nudPixelsPerFoot";
			this.nudPixelsPerFoot.Size = new System.Drawing.Size(111, 20);
			this.nudPixelsPerFoot.TabIndex = 7;
			this.nudPixelsPerFoot.ValueChanged += new System.EventHandler(this.NudPixelsPerFoot_ValueChanged);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(201, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(80, 13);
			this.label5.TabIndex = 21;
			this.label5.Text = "Pixels Per Foot:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(133, 92);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(35, 13);
			this.label12.TabIndex = 23;
			this.label12.Text = "- OR -";
			// 
			// Configuration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(665, 600);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.nudPixelsPerFoot);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cbShowPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtUser);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.txtOutputFormat);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.btnLoadStream);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.nudPixelsPerMeter);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtStreamUrl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pbCamPreview);
			this.Name = "Configuration";
			this.Text = "IpCameraSpeedometer Configuration";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Configuration_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.pbCamPreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPixelsPerMeter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudPixelsPerFoot)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pbCamPreview;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtStreamUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown nudPixelsPerMeter;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnLoadStream;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtOutputFormat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.CheckBox cbShowPassword;
		private System.Windows.Forms.NumericUpDown nudPixelsPerFoot;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label12;
	}
}