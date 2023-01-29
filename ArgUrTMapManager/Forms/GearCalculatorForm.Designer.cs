namespace ArgUrTMapManager.Forms
{
	partial class GearCalculatorForm
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
			this.lDisabledGuns = new System.Windows.Forms.Label();
			this.fcDisabledGuns = new ArgUrTMapManager.Controls.FlagsControl();
			this.tbDisabledGunsValue = new ArgUrTMapManager.Controls.BlackTextBox();
			this.pDisabledGuns = new System.Windows.Forms.Panel();
			this.pAllowvote = new System.Windows.Forms.Panel();
			this.lAllowvote = new System.Windows.Forms.Label();
			this.fcAllowvote = new ArgUrTMapManager.Controls.FlagsControl();
			this.tbAllowvoteValue = new ArgUrTMapManager.Controls.BlackTextBox();
			this.pDisabledGuns.SuspendLayout();
			this.pAllowvote.SuspendLayout();
			this.SuspendLayout();
			// 
			// lDisabledGuns
			// 
			this.lDisabledGuns.AutoSize = true;
			this.lDisabledGuns.Location = new System.Drawing.Point(3, 0);
			this.lDisabledGuns.Name = "lDisabledGuns";
			this.lDisabledGuns.Size = new System.Drawing.Size(79, 13);
			this.lDisabledGuns.TabIndex = 0;
			this.lDisabledGuns.Text = "Disabled Guns:";
			// 
			// fcDisabledGuns
			// 
			this.fcDisabledGuns.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fcDisabledGuns.BackColor = System.Drawing.Color.Black;
			this.fcDisabledGuns.CheckOnClick = true;
			this.fcDisabledGuns.ForeColor = System.Drawing.Color.White;
			this.fcDisabledGuns.FormattingEnabled = true;
			this.fcDisabledGuns.IntegralHeight = false;
			this.fcDisabledGuns.Location = new System.Drawing.Point(3, 16);
			this.fcDisabledGuns.Name = "fcDisabledGuns";
			this.fcDisabledGuns.Size = new System.Drawing.Size(150, 175);
			this.fcDisabledGuns.TabIndex = 1;
			this.fcDisabledGuns.ValueChanged += new System.EventHandler(this.fcDisabledGuns_ValueChanged);
			// 
			// tbDisabledGunsValue
			// 
			this.tbDisabledGunsValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbDisabledGunsValue.BackColor = System.Drawing.Color.Black;
			this.tbDisabledGunsValue.ForeColor = System.Drawing.Color.White;
			this.tbDisabledGunsValue.Location = new System.Drawing.Point(3, 197);
			this.tbDisabledGunsValue.Name = "tbDisabledGunsValue";
			this.tbDisabledGunsValue.ReadOnly = true;
			this.tbDisabledGunsValue.Size = new System.Drawing.Size(150, 20);
			this.tbDisabledGunsValue.TabIndex = 2;
			// 
			// pDisabledGuns
			// 
			this.pDisabledGuns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pDisabledGuns.Controls.Add(this.lDisabledGuns);
			this.pDisabledGuns.Controls.Add(this.fcDisabledGuns);
			this.pDisabledGuns.Controls.Add(this.tbDisabledGunsValue);
			this.pDisabledGuns.Location = new System.Drawing.Point(12, 12);
			this.pDisabledGuns.Name = "pDisabledGuns";
			this.pDisabledGuns.Size = new System.Drawing.Size(156, 220);
			this.pDisabledGuns.TabIndex = 4;
			// 
			// pAllowvote
			// 
			this.pAllowvote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pAllowvote.Controls.Add(this.lAllowvote);
			this.pAllowvote.Controls.Add(this.fcAllowvote);
			this.pAllowvote.Controls.Add(this.tbAllowvoteValue);
			this.pAllowvote.Location = new System.Drawing.Point(174, 12);
			this.pAllowvote.Name = "pAllowvote";
			this.pAllowvote.Size = new System.Drawing.Size(156, 220);
			this.pAllowvote.TabIndex = 5;
			// 
			// lAllowvote
			// 
			this.lAllowvote.AutoSize = true;
			this.lAllowvote.Location = new System.Drawing.Point(3, 0);
			this.lAllowvote.Name = "lAllowvote";
			this.lAllowvote.Size = new System.Drawing.Size(60, 13);
			this.lAllowvote.TabIndex = 0;
			this.lAllowvote.Text = "Allow Vote:";
			// 
			// fcAllowvote
			// 
			this.fcAllowvote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.fcAllowvote.BackColor = System.Drawing.Color.Black;
			this.fcAllowvote.CheckOnClick = true;
			this.fcAllowvote.ForeColor = System.Drawing.Color.White;
			this.fcAllowvote.FormattingEnabled = true;
			this.fcAllowvote.IntegralHeight = false;
			this.fcAllowvote.Location = new System.Drawing.Point(3, 16);
			this.fcAllowvote.Name = "fcAllowvote";
			this.fcAllowvote.Size = new System.Drawing.Size(150, 175);
			this.fcAllowvote.TabIndex = 1;
			this.fcAllowvote.ValueChanged += new System.EventHandler(this.fcAllowvote_ValueChanged);
			// 
			// tbAllowvoteValue
			// 
			this.tbAllowvoteValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbAllowvoteValue.BackColor = System.Drawing.Color.Black;
			this.tbAllowvoteValue.ForeColor = System.Drawing.Color.White;
			this.tbAllowvoteValue.Location = new System.Drawing.Point(3, 197);
			this.tbAllowvoteValue.Name = "tbAllowvoteValue";
			this.tbAllowvoteValue.ReadOnly = true;
			this.tbAllowvoteValue.Size = new System.Drawing.Size(150, 20);
			this.tbAllowvoteValue.TabIndex = 2;
			// 
			// GearCalculatorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(340, 244);
			this.Controls.Add(this.pAllowvote);
			this.Controls.Add(this.pDisabledGuns);
			this.MinimumSize = new System.Drawing.Size(350, 280);
			this.Name = "GearCalculatorForm";
			this.Text = "Gear/Allowvote Calculator";
			this.pDisabledGuns.ResumeLayout(false);
			this.pDisabledGuns.PerformLayout();
			this.pAllowvote.ResumeLayout(false);
			this.pAllowvote.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lDisabledGuns;
		private Controls.FlagsControl fcDisabledGuns;
		private Controls.BlackTextBox tbDisabledGunsValue;
		private System.Windows.Forms.Panel pDisabledGuns;
		private System.Windows.Forms.Panel pAllowvote;
		private System.Windows.Forms.Label lAllowvote;
		private Controls.FlagsControl fcAllowvote;
		private Controls.BlackTextBox tbAllowvoteValue;
	}
}