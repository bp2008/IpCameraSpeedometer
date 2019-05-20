namespace IpCameraSpeedometer
{
	partial class ServiceName
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnChangeServiceName = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblServiceName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnChangeServiceName
			// 
			this.btnChangeServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnChangeServiceName.Location = new System.Drawing.Point(6, 41);
			this.btnChangeServiceName.Name = "btnChangeServiceName";
			this.btnChangeServiceName.Size = new System.Drawing.Size(172, 23);
			this.btnChangeServiceName.TabIndex = 0;
			this.btnChangeServiceName.Text = "Change Name";
			this.btnChangeServiceName.UseVisualStyleBackColor = true;
			this.btnChangeServiceName.Click += new System.EventHandler(this.BtnChangeServiceName_Click);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(179, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Service Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblServiceName
			// 
			this.lblServiceName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblServiceName.AutoEllipsis = true;
			this.lblServiceName.Location = new System.Drawing.Point(3, 21);
			this.lblServiceName.Name = "lblServiceName";
			this.lblServiceName.Size = new System.Drawing.Size(175, 13);
			this.lblServiceName.TabIndex = 2;
			this.lblServiceName.Text = "Unset";
			this.lblServiceName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ServiceName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.lblServiceName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnChangeServiceName);
			this.Name = "ServiceName";
			this.Size = new System.Drawing.Size(181, 70);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnChangeServiceName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblServiceName;
	}
}
