namespace ArgusLib.Controls
{
	partial class ProgressBox
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
			this.pBorder = new System.Windows.Forms.Panel();
			this.progressBar1 = new ArgusLib.Controls.ProgressBar();
			this.lText = new System.Windows.Forms.Label();
			this.pBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBorder
			// 
			this.pBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pBorder.Controls.Add(this.lText);
			this.pBorder.Controls.Add(this.progressBar1);
			this.pBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pBorder.Location = new System.Drawing.Point(0, 0);
			this.pBorder.Name = "pBorder";
			this.pBorder.Size = new System.Drawing.Size(400, 47);
			this.pBorder.TabIndex = 0;
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(10, 10);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(322, 23);
			this.progressBar1.TabIndex = 0;
			// 
			// lText
			// 
			this.lText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lText.Location = new System.Drawing.Point(338, 10);
			this.lText.Name = "lText";
			this.lText.Size = new System.Drawing.Size(48, 23);
			this.lText.TabIndex = 1;
			this.lText.Text = "100.00%";
			this.lText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ProgressBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(400, 47);
			this.Controls.Add(this.pBorder);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ProgressBox";
			this.Text = "ProgressBox";
			this.pBorder.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pBorder;
		private ProgressBar progressBar1;
		private System.Windows.Forms.Label lText;

	}
}