namespace ArgUrTMapManager.Forms
{
	partial class SettingsForm
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
			this.bOK = new ArgUrTMapManager.Controls.BlackButton();
			this.bCancel = new ArgUrTMapManager.Controls.BlackButton();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.cbSv_Pure = new System.Windows.Forms.CheckBox();
			this.cbServerconfigFile = new ArgUrTMapManager.Controls.BlackComboBox();
			this.lServerconfigFile = new System.Windows.Forms.Label();
			this.tbMapcycleFile = new ArgUrTMapManager.Controls.BlackTextBox();
			this.lMapcycleFile = new System.Windows.Forms.Label();
			this.cbGeneralServerBin = new ArgUrTMapManager.Controls.BlackComboBox();
			this.lGeneralServerBin = new System.Windows.Forms.Label();
			this.cbGeneralGameBin = new ArgUrTMapManager.Controls.BlackComboBox();
			this.lGeneralGameBin = new System.Windows.Forms.Label();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.tpGeneral.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// bOK
			// 
			this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOK.AutoSize = true;
			this.bOK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bOK.BackColor = System.Drawing.Color.Black;
			this.bOK.ForeColor = System.Drawing.Color.White;
			this.bOK.Location = new System.Drawing.Point(240, 226);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(32, 23);
			this.bOK.TabIndex = 1;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = false;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.AutoSize = true;
			this.bCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bCancel.BackColor = System.Drawing.Color.Black;
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.ForeColor = System.Drawing.Color.White;
			this.bCancel.Location = new System.Drawing.Point(184, 226);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(50, 23);
			this.bCancel.TabIndex = 2;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = false;
			this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
			// 
			// tpGeneral
			// 
			this.tpGeneral.BackColor = System.Drawing.Color.Black;
			this.tpGeneral.Controls.Add(this.cbSv_Pure);
			this.tpGeneral.Controls.Add(this.cbServerconfigFile);
			this.tpGeneral.Controls.Add(this.lServerconfigFile);
			this.tpGeneral.Controls.Add(this.tbMapcycleFile);
			this.tpGeneral.Controls.Add(this.lMapcycleFile);
			this.tpGeneral.Controls.Add(this.cbGeneralServerBin);
			this.tpGeneral.Controls.Add(this.lGeneralServerBin);
			this.tpGeneral.Controls.Add(this.cbGeneralGameBin);
			this.tpGeneral.Controls.Add(this.lGeneralGameBin);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpGeneral.Size = new System.Drawing.Size(276, 194);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			// 
			// cbSv_Pure
			// 
			this.cbSv_Pure.AutoSize = true;
			this.cbSv_Pure.Enabled = false;
			this.cbSv_Pure.Location = new System.Drawing.Point(9, 123);
			this.cbSv_Pure.Name = "cbSv_Pure";
			this.cbSv_Pure.Size = new System.Drawing.Size(82, 17);
			this.cbSv_Pure.TabIndex = 8;
			this.cbSv_Pure.Text = "sv_pure = 1";
			this.cbSv_Pure.UseVisualStyleBackColor = true;
			// 
			// cbServerconfigFile
			// 
			this.cbServerconfigFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbServerconfigFile.BackColor = System.Drawing.Color.Black;
			this.cbServerconfigFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbServerconfigFile.ForeColor = System.Drawing.Color.White;
			this.cbServerconfigFile.FormattingEnabled = true;
			this.cbServerconfigFile.Location = new System.Drawing.Point(101, 86);
			this.cbServerconfigFile.Name = "cbServerconfigFile";
			this.cbServerconfigFile.Size = new System.Drawing.Size(169, 21);
			this.cbServerconfigFile.TabIndex = 7;
			// 
			// lServerconfigFile
			// 
			this.lServerconfigFile.AutoSize = true;
			this.lServerconfigFile.Location = new System.Drawing.Point(6, 89);
			this.lServerconfigFile.Name = "lServerconfigFile";
			this.lServerconfigFile.Size = new System.Drawing.Size(89, 13);
			this.lServerconfigFile.TabIndex = 6;
			this.lServerconfigFile.Text = "Serverconfig File:";
			// 
			// tbMapcycleFile
			// 
			this.tbMapcycleFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbMapcycleFile.BackColor = System.Drawing.Color.Black;
			this.tbMapcycleFile.ForeColor = System.Drawing.Color.White;
			this.tbMapcycleFile.InstructionColor = System.Drawing.SystemColors.WindowText;
			this.tbMapcycleFile.InstructionFontStyle = System.Drawing.FontStyle.Italic;
			this.tbMapcycleFile.InstructionText = "";
			this.tbMapcycleFile.InstructionTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbMapcycleFile.Location = new System.Drawing.Point(101, 60);
			this.tbMapcycleFile.Name = "tbMapcycleFile";
			this.tbMapcycleFile.ShowInstructionText = false;
			this.tbMapcycleFile.Size = new System.Drawing.Size(169, 20);
			this.tbMapcycleFile.TabIndex = 5;
			// 
			// lMapcycleFile
			// 
			this.lMapcycleFile.AutoSize = true;
			this.lMapcycleFile.Location = new System.Drawing.Point(6, 63);
			this.lMapcycleFile.Name = "lMapcycleFile";
			this.lMapcycleFile.Size = new System.Drawing.Size(75, 13);
			this.lMapcycleFile.TabIndex = 4;
			this.lMapcycleFile.Text = "Mapcycle File:";
			// 
			// cbGeneralServerBin
			// 
			this.cbGeneralServerBin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbGeneralServerBin.BackColor = System.Drawing.Color.Black;
			this.cbGeneralServerBin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGeneralServerBin.ForeColor = System.Drawing.Color.White;
			this.cbGeneralServerBin.FormattingEnabled = true;
			this.cbGeneralServerBin.Location = new System.Drawing.Point(101, 33);
			this.cbGeneralServerBin.Name = "cbGeneralServerBin";
			this.cbGeneralServerBin.Size = new System.Drawing.Size(169, 21);
			this.cbGeneralServerBin.TabIndex = 3;
			// 
			// lGeneralServerBin
			// 
			this.lGeneralServerBin.AutoSize = true;
			this.lGeneralServerBin.Location = new System.Drawing.Point(6, 36);
			this.lGeneralServerBin.Name = "lGeneralServerBin";
			this.lGeneralServerBin.Size = new System.Drawing.Size(73, 13);
			this.lGeneralServerBin.TabIndex = 2;
			this.lGeneralServerBin.Text = "Server Binary:";
			// 
			// cbGeneralGameBin
			// 
			this.cbGeneralGameBin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbGeneralGameBin.BackColor = System.Drawing.Color.Black;
			this.cbGeneralGameBin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGeneralGameBin.ForeColor = System.Drawing.Color.White;
			this.cbGeneralGameBin.FormattingEnabled = true;
			this.cbGeneralGameBin.Location = new System.Drawing.Point(101, 6);
			this.cbGeneralGameBin.Name = "cbGeneralGameBin";
			this.cbGeneralGameBin.Size = new System.Drawing.Size(169, 21);
			this.cbGeneralGameBin.TabIndex = 1;
			// 
			// lGeneralGameBin
			// 
			this.lGeneralGameBin.AutoSize = true;
			this.lGeneralGameBin.Location = new System.Drawing.Point(6, 9);
			this.lGeneralGameBin.Name = "lGeneralGameBin";
			this.lGeneralGameBin.Size = new System.Drawing.Size(70, 13);
			this.lGeneralGameBin.TabIndex = 0;
			this.lGeneralGameBin.Text = "Game Binary:";
			// 
			// MainTabControl
			// 
			this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MainTabControl.Controls.Add(this.tpGeneral);
			this.MainTabControl.HotTrack = true;
			this.MainTabControl.Location = new System.Drawing.Point(0, 0);
			this.MainTabControl.Multiline = true;
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(284, 220);
			this.MainTabControl.TabIndex = 0;
			// 
			// SettingsForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.MainTabControl);
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "SettingsForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Settings";
			this.tpGeneral.ResumeLayout(false);
			this.tpGeneral.PerformLayout();
			this.MainTabControl.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.BlackButton bOK;
		private Controls.BlackButton bCancel;
		private System.Windows.Forms.TabPage tpGeneral;
		private Controls.BlackTextBox tbMapcycleFile;
		private System.Windows.Forms.Label lMapcycleFile;
		private Controls.BlackComboBox cbGeneralServerBin;
		private System.Windows.Forms.Label lGeneralServerBin;
		private Controls.BlackComboBox cbGeneralGameBin;
		private System.Windows.Forms.Label lGeneralGameBin;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.Label lServerconfigFile;
		private Controls.BlackComboBox cbServerconfigFile;
		private System.Windows.Forms.CheckBox cbSv_Pure;
	}
}