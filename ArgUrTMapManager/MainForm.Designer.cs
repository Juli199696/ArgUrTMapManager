namespace ArgUrTMapManager
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.bPlay = new ArgUrTMapManager.Controls.BlackButton();
			this.bAddToMapcycle = new ArgUrTMapManager.Controls.BlackButton();
			this.lbMaps = new ArgUrTMapManager.Controls.BlackListBox();
			this.MainMenuStrip1 = new ArgUrTMapManager.Controls.BlackMenuStrip();
			this.miExtract = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miExtractSelectedMaps = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miTools = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miToolsServerControl = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miToolsExportQuakeLive = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miToolsGearCalculator = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miToolsCreateArenaScripts = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.separatorMenuTools = new System.Windows.Forms.ToolStripSeparator();
			this.miToolsSettings = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miHelp = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.miHelpAbout = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.tbSearch = new ArgUrTMapManager.Controls.BlackTextBox();
			this.miExtractMapEntities = new ArgUrTMapManager.Controls.BlackMenuItem();
			this.MainMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// bPlay
			// 
			this.bPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bPlay.BackColor = System.Drawing.Color.Black;
			this.bPlay.ForeColor = System.Drawing.Color.White;
			this.bPlay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bPlay.Location = new System.Drawing.Point(135, 526);
			this.bPlay.Name = "bPlay";
			this.bPlay.Size = new System.Drawing.Size(37, 23);
			this.bPlay.TabIndex = 3;
			this.bPlay.Text = "Play";
			this.bPlay.UseVisualStyleBackColor = false;
			this.bPlay.Click += new System.EventHandler(this.bPlay_Click);
			// 
			// bAddToMapcycle
			// 
			this.bAddToMapcycle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bAddToMapcycle.BackColor = System.Drawing.Color.Black;
			this.bAddToMapcycle.ForeColor = System.Drawing.Color.White;
			this.bAddToMapcycle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.bAddToMapcycle.Location = new System.Drawing.Point(32, 526);
			this.bAddToMapcycle.Name = "bAddToMapcycle";
			this.bAddToMapcycle.Size = new System.Drawing.Size(97, 23);
			this.bAddToMapcycle.TabIndex = 2;
			this.bAddToMapcycle.Text = "Add to Mapcycle";
			this.bAddToMapcycle.UseVisualStyleBackColor = false;
			this.bAddToMapcycle.Click += new System.EventHandler(this.bAddToMapcycle_Click);
			// 
			// lbMaps
			// 
			this.lbMaps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbMaps.BackColor = System.Drawing.Color.Black;
			this.lbMaps.ForeColor = System.Drawing.Color.White;
			this.lbMaps.FormattingEnabled = true;
			this.lbMaps.Location = new System.Drawing.Point(12, 53);
			this.lbMaps.Name = "lbMaps";
			this.lbMaps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lbMaps.Size = new System.Drawing.Size(160, 472);
			this.lbMaps.TabIndex = 1;
			this.lbMaps.DoubleClick += new System.EventHandler(this.lbMaps_DoubleClick);
			this.lbMaps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbMaps_KeyDown);
			// 
			// MainMenuStrip1
			// 
			this.MainMenuStrip1.BackColor = System.Drawing.Color.Black;
			this.MainMenuStrip1.ForeColor = System.Drawing.Color.White;
			this.MainMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExtract,
            this.miTools,
            this.miHelp});
			this.MainMenuStrip1.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip1.Name = "MainMenuStrip1";
			this.MainMenuStrip1.Size = new System.Drawing.Size(184, 24);
			this.MainMenuStrip1.TabIndex = 0;
			this.MainMenuStrip1.Text = "menuStrip1";
			// 
			// miExtract
			// 
			this.miExtract.BackColor = System.Drawing.Color.Black;
			this.miExtract.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miExtractSelectedMaps,
            this.miExtractMapEntities});
			this.miExtract.ForeColor = System.Drawing.Color.Black;
			this.miExtract.Name = "miExtract";
			this.miExtract.Size = new System.Drawing.Size(54, 20);
			this.miExtract.Text = "Extract";
			// 
			// miExtractSelectedMaps
			// 
			this.miExtractSelectedMaps.BackColor = System.Drawing.Color.Black;
			this.miExtractSelectedMaps.ForeColor = System.Drawing.Color.White;
			this.miExtractSelectedMaps.Name = "miExtractSelectedMaps";
			this.miExtractSelectedMaps.Size = new System.Drawing.Size(152, 22);
			this.miExtractSelectedMaps.Text = "Selected Maps";
			this.miExtractSelectedMaps.Click += new System.EventHandler(this.miFileExtractMaps_Click);
			// 
			// miTools
			// 
			this.miTools.BackColor = System.Drawing.Color.Black;
			this.miTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToolsServerControl,
            this.miToolsExportQuakeLive,
            this.miToolsGearCalculator,
            this.miToolsCreateArenaScripts,
            this.separatorMenuTools,
            this.miToolsSettings});
			this.miTools.ForeColor = System.Drawing.Color.White;
			this.miTools.Name = "miTools";
			this.miTools.Size = new System.Drawing.Size(48, 20);
			this.miTools.Text = "Tools";
			// 
			// miToolsServerControl
			// 
			this.miToolsServerControl.BackColor = System.Drawing.Color.Black;
			this.miToolsServerControl.ForeColor = System.Drawing.Color.White;
			this.miToolsServerControl.Name = "miToolsServerControl";
			this.miToolsServerControl.Size = new System.Drawing.Size(213, 22);
			this.miToolsServerControl.Text = "Server Control";
			this.miToolsServerControl.Click += new System.EventHandler(this.miToolsServerControl_Click);
			// 
			// miToolsExportQuakeLive
			// 
			this.miToolsExportQuakeLive.BackColor = System.Drawing.Color.Black;
			this.miToolsExportQuakeLive.ForeColor = System.Drawing.Color.White;
			this.miToolsExportQuakeLive.Name = "miToolsExportQuakeLive";
			this.miToolsExportQuakeLive.Size = new System.Drawing.Size(213, 22);
			this.miToolsExportQuakeLive.Text = "Export Quake Live Archive";
			this.miToolsExportQuakeLive.Click += new System.EventHandler(this.miToolsExportQuakeLive_Click);
			// 
			// miToolsGearCalculator
			// 
			this.miToolsGearCalculator.BackColor = System.Drawing.Color.Black;
			this.miToolsGearCalculator.ForeColor = System.Drawing.Color.White;
			this.miToolsGearCalculator.Name = "miToolsGearCalculator";
			this.miToolsGearCalculator.Size = new System.Drawing.Size(213, 22);
			this.miToolsGearCalculator.Text = "Gear/Allowvote Calculator";
			this.miToolsGearCalculator.Click += new System.EventHandler(this.miToolsGearCalculator_Click);
			// 
			// miToolsCreateArenaScripts
			// 
			this.miToolsCreateArenaScripts.BackColor = System.Drawing.Color.Black;
			this.miToolsCreateArenaScripts.ForeColor = System.Drawing.Color.White;
			this.miToolsCreateArenaScripts.Name = "miToolsCreateArenaScripts";
			this.miToolsCreateArenaScripts.Size = new System.Drawing.Size(213, 22);
			this.miToolsCreateArenaScripts.Text = "Create Arena Scripts";
			this.miToolsCreateArenaScripts.Click += new System.EventHandler(this.miToolsCreateArenaScripts_Click);
			// 
			// separatorMenuTools
			// 
			this.separatorMenuTools.BackColor = System.Drawing.Color.Black;
			this.separatorMenuTools.ForeColor = System.Drawing.Color.White;
			this.separatorMenuTools.Name = "separatorMenuTools";
			this.separatorMenuTools.Size = new System.Drawing.Size(210, 6);
			// 
			// miToolsSettings
			// 
			this.miToolsSettings.BackColor = System.Drawing.Color.Black;
			this.miToolsSettings.ForeColor = System.Drawing.Color.White;
			this.miToolsSettings.Name = "miToolsSettings";
			this.miToolsSettings.Size = new System.Drawing.Size(213, 22);
			this.miToolsSettings.Text = "Settings";
			this.miToolsSettings.Click += new System.EventHandler(this.miToolsSettings_Click);
			// 
			// miHelp
			// 
			this.miHelp.BackColor = System.Drawing.Color.Black;
			this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelpAbout});
			this.miHelp.ForeColor = System.Drawing.Color.White;
			this.miHelp.Name = "miHelp";
			this.miHelp.Size = new System.Drawing.Size(44, 20);
			this.miHelp.Text = "Help";
			// 
			// miHelpAbout
			// 
			this.miHelpAbout.BackColor = System.Drawing.Color.Black;
			this.miHelpAbout.ForeColor = System.Drawing.Color.White;
			this.miHelpAbout.Name = "miHelpAbout";
			this.miHelpAbout.Size = new System.Drawing.Size(107, 22);
			this.miHelpAbout.Text = "About";
			this.miHelpAbout.Click += new System.EventHandler(this.miHelpAbout_Click);
			// 
			// tbSearch
			// 
			this.tbSearch.BackColor = System.Drawing.Color.Black;
			this.tbSearch.ForeColor = System.Drawing.Color.White;
			this.tbSearch.InstructionColor = System.Drawing.Color.White;
			this.tbSearch.InstructionFontStyle = System.Drawing.FontStyle.Italic;
			this.tbSearch.InstructionText = "Search...";
			this.tbSearch.InstructionTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbSearch.Location = new System.Drawing.Point(12, 27);
			this.tbSearch.Name = "tbSearch";
			this.tbSearch.ShowInstructionText = true;
			this.tbSearch.Size = new System.Drawing.Size(160, 20);
			this.tbSearch.TabIndex = 4;
			this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
			// 
			// miExtractMapEntities
			// 
			this.miExtractMapEntities.BackColor = System.Drawing.Color.Black;
			this.miExtractMapEntities.ForeColor = System.Drawing.Color.Black;
			this.miExtractMapEntities.Name = "miExtractMapEntities";
			this.miExtractMapEntities.Size = new System.Drawing.Size(152, 22);
			this.miExtractMapEntities.Text = "Map Entities";
			this.miExtractMapEntities.Click += new System.EventHandler(this.miExtractMapEntities_Click);
			// 
			// MainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(184, 561);
			this.Controls.Add(this.tbSearch);
			this.Controls.Add(this.bPlay);
			this.Controls.Add(this.bAddToMapcycle);
			this.Controls.Add(this.lbMaps);
			this.Controls.Add(this.MainMenuStrip1);
			this.MainMenuStrip = this.MainMenuStrip1;
			this.MaximizeBox = false;
			this.MinimumSize = new System.Drawing.Size(200, 200);
			this.Name = "MainForm";
			this.Text = "ArgUrT Map Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.MainMenuStrip1.ResumeLayout(false);
			this.MainMenuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ArgUrTMapManager.Controls.BlackMenuStrip MainMenuStrip1;
		private Controls.BlackMenuItem miExtract;
		private Controls.BlackMenuItem miTools;
		private Controls.BlackMenuItem miHelp;
		private Controls.BlackListBox lbMaps;
		private Controls.BlackButton bAddToMapcycle;
		private Controls.BlackButton bPlay;
		private Controls.BlackMenuItem miExtractSelectedMaps;
		private Controls.BlackMenuItem miToolsSettings;
		private Controls.BlackMenuItem miHelpAbout;
		private Controls.BlackMenuItem miToolsExportQuakeLive;
		private System.Windows.Forms.ToolStripSeparator separatorMenuTools;
		private Controls.BlackMenuItem miToolsGearCalculator;
		private Controls.BlackMenuItem miToolsCreateArenaScripts;
		private System.Windows.Forms.ToolTip toolTip1;
		private Controls.BlackMenuItem miToolsServerControl;
		private Controls.BlackTextBox tbSearch;
		private Controls.BlackMenuItem miExtractMapEntities;
	}
}

