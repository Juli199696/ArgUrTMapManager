namespace ArgUrTMapManager.Forms
{
	partial class LevelshotForm
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
			this.pbLevelshot = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbLevelshot)).BeginInit();
			this.SuspendLayout();
			// 
			// pbLevelshot
			// 
			this.pbLevelshot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbLevelshot.Location = new System.Drawing.Point(0, 0);
			this.pbLevelshot.Name = "pbLevelshot";
			this.pbLevelshot.Size = new System.Drawing.Size(284, 261);
			this.pbLevelshot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLevelshot.TabIndex = 0;
			this.pbLevelshot.TabStop = false;
			// 
			// LevelshotForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.pbLevelshot);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "LevelshotForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Levelshot";
			((System.ComponentModel.ISupportInitialize)(this.pbLevelshot)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbLevelshot;
	}
}