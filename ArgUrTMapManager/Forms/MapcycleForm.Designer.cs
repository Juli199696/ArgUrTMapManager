namespace ArgUrTMapManager.Forms
{
	partial class MapcycleForm
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
			this.bUp = new ArgUrTMapManager.Controls.BlackButton();
			this.bDown = new ArgUrTMapManager.Controls.BlackButton();
			this.bRemove = new ArgUrTMapManager.Controls.BlackButton();
			this.bStartServer = new ArgUrTMapManager.Controls.BlackButton();
			this.lbMaps = new ArgUrTMapManager.Controls.BlackListBox();
			this.miFile = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miFileSave = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miFileOpen = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.MainMenuStrip1 = new ArgUrTMapManager.Controls.BlackMenuStrip();
			this.MainMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bUp
			// 
			this.bUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bUp.BackColor = System.Drawing.Color.Black;
			this.bUp.ForeColor = System.Drawing.Color.White;
			this.bUp.Location = new System.Drawing.Point(76, 27);
			this.bUp.Name = "bUp";
			this.bUp.Size = new System.Drawing.Size(45, 23);
			this.bUp.TabIndex = 5;
			this.bUp.Text = "Up";
			this.bUp.UseVisualStyleBackColor = false;
			this.bUp.Click += new System.EventHandler(this.bUp_Click);
			// 
			// bDown
			// 
			this.bDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bDown.BackColor = System.Drawing.Color.Black;
			this.bDown.ForeColor = System.Drawing.Color.White;
			this.bDown.Location = new System.Drawing.Point(127, 27);
			this.bDown.Name = "bDown";
			this.bDown.Size = new System.Drawing.Size(45, 23);
			this.bDown.TabIndex = 4;
			this.bDown.Text = "Down";
			this.bDown.UseVisualStyleBackColor = false;
			this.bDown.Click += new System.EventHandler(this.bDown_Click);
			// 
			// bRemove
			// 
			this.bRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bRemove.BackColor = System.Drawing.Color.Black;
			this.bRemove.ForeColor = System.Drawing.Color.White;
			this.bRemove.Location = new System.Drawing.Point(12, 27);
			this.bRemove.Name = "bRemove";
			this.bRemove.Size = new System.Drawing.Size(57, 23);
			this.bRemove.TabIndex = 3;
			this.bRemove.Text = "Remove";
			this.bRemove.UseVisualStyleBackColor = false;
			this.bRemove.Click += new System.EventHandler(this.bRemove_Click);
			// 
			// bStartServer
			// 
			this.bStartServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bStartServer.AutoSize = true;
			this.bStartServer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.bStartServer.BackColor = System.Drawing.Color.Black;
			this.bStartServer.ForeColor = System.Drawing.Color.White;
			this.bStartServer.Location = new System.Drawing.Point(99, 526);
			this.bStartServer.Name = "bStartServer";
			this.bStartServer.Size = new System.Drawing.Size(73, 23);
			this.bStartServer.TabIndex = 1;
			this.bStartServer.Text = "Start Server";
			this.bStartServer.UseVisualStyleBackColor = false;
			this.bStartServer.Click += new System.EventHandler(this.bStartServer_Click);
			// 
			// lbMaps
			// 
			this.lbMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbMaps.BackColor = System.Drawing.Color.Black;
			this.lbMaps.ForeColor = System.Drawing.Color.White;
			this.lbMaps.FormattingEnabled = true;
			this.lbMaps.IntegralHeight = false;
			this.lbMaps.Location = new System.Drawing.Point(12, 56);
			this.lbMaps.Name = "lbMaps";
			this.lbMaps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbMaps.Size = new System.Drawing.Size(160, 464);
			this.lbMaps.TabIndex = 0;
			this.lbMaps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbMaps_KeyDown);
			// 
			// miFile
			// 
			this.miFile.BackColor = System.Drawing.Color.Black;
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFileSave,
            this.miFileOpen});
			this.miFile.ForeColor = System.Drawing.Color.White;
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(37, 20);
			this.miFile.Text = "File";
			// 
			// miFileSave
			// 
			this.miFileSave.BackColor = System.Drawing.Color.Black;
			this.miFileSave.ForeColor = System.Drawing.Color.White;
			this.miFileSave.Name = "miFileSave";
			this.miFileSave.Size = new System.Drawing.Size(103, 22);
			this.miFileSave.Text = "Save";
			this.miFileSave.Click += new System.EventHandler(this.miFileSave_Click);
			// 
			// miFileOpen
			// 
			this.miFileOpen.BackColor = System.Drawing.Color.Black;
			this.miFileOpen.ForeColor = System.Drawing.Color.White;
			this.miFileOpen.Name = "miFileOpen";
			this.miFileOpen.Size = new System.Drawing.Size(103, 22);
			this.miFileOpen.Text = "Open";
			this.miFileOpen.Click += new System.EventHandler(this.miFileOpen_Click);
			// 
			// MainMenuStrip1
			// 
			this.MainMenuStrip1.BackColor = System.Drawing.Color.Black;
			this.MainMenuStrip1.ForeColor = System.Drawing.Color.White;
			this.MainMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile});
			this.MainMenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip1.Name = "MainMenuStrip1";
			this.MainMenuStrip1.Size = new System.Drawing.Size(184, 24);
			this.MainMenuStrip1.TabIndex = 2;
			this.MainMenuStrip1.Text = "blackMenuStrip1";
			// 
			// MapcycleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(184, 561);
			this.Controls.Add(this.bUp);
			this.Controls.Add(this.bDown);
			this.Controls.Add(this.bRemove);
			this.Controls.Add(this.bStartServer);
			this.Controls.Add(this.lbMaps);
			this.Controls.Add(this.MainMenuStrip1);
			this.MainMenuStrip = this.MainMenuStrip1;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "MapcycleForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Server Control";
			this.MainMenuStrip1.ResumeLayout(false);
			this.MainMenuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.BlackListBox lbMaps;
		private Controls.BlackButton bStartServer;
		private Controls.BlackMenuStrip MainMenuStrip1;
		private Controls.BlackMenuItem miFile;
		private Controls.BlackMenuItem miFileSave;
		private Controls.BlackMenuItem miFileOpen;
		private Controls.BlackButton bRemove;
		private Controls.BlackButton bDown;
		private Controls.BlackButton bUp;
	}
}