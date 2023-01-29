namespace ArgUrTMapManager.Forms
{
	partial class MapExtractOptionsForm
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
			this.lImages = new System.Windows.Forms.Label();
			this.cbImages = new ArgUrTMapManager.Controls.BlackEnumComboBox();
			this.cbSounds = new ArgUrTMapManager.Controls.BlackEnumComboBox();
			this.lSounds = new System.Windows.Forms.Label();
			this.cbBspVersion = new ArgUrTMapManager.Controls.BlackEnumComboBox();
			this.lBspVersion = new System.Windows.Forms.Label();
			this.cbRemoveFallingDamage = new System.Windows.Forms.CheckBox();
			this.bOK = new ArgUrTMapManager.Controls.BlackButton();
			this.bCancel = new ArgUrTMapManager.Controls.BlackButton();
			this.lMapEntities = new System.Windows.Forms.Label();
			this.rbCopy = new System.Windows.Forms.RadioButton();
			this.rbUseConversionFile = new System.Windows.Forms.RadioButton();
			this.blackToolTip1 = new ArgUrTMapManager.Controls.BlackToolTip();
			this.cbImport = new System.Windows.Forms.RadioButton();
			this.rbImport = new System.Windows.Forms.RadioButton();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.cbMakeSurfacesClimbable = new System.Windows.Forms.CheckBox();
			this.tpEntities = new System.Windows.Forms.TabPage();
			this.cbConversionFile = new ArgUrTMapManager.Controls.EntityConversionComboBox();
			this.tpBotSupport = new System.Windows.Forms.TabPage();
			this.pAasCompileOptions = new System.Windows.Forms.Panel();
			this.cbTryReachFirst = new System.Windows.Forms.CheckBox();
			this.lBspcCompiler = new System.Windows.Forms.Label();
			this.cbBspcCompiler = new ArgUrTMapManager.Controls.BlackFilenameComboBox();
			this.lBspcSwitches = new System.Windows.Forms.Label();
			this.pBspcSwitches = new System.Windows.Forms.Panel();
			this.cbBspcSwitchesForceSidesVisible = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesNoCsg = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesNoLiquids = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesNoBrushMerge = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesBreadthFirst = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesNoVerbose = new System.Windows.Forms.CheckBox();
			this.cbBspcSwitchesOptimize = new System.Windows.Forms.CheckBox();
			this.nudBspcSwitchesThreads = new ArgUrTMapManager.Controls.BlackNumericUpDown();
			this.lBspcSwitchesThreads = new System.Windows.Forms.Label();
			this.cbEnumCompileAasCondition = new ArgUrTMapManager.Controls.BlackEnumComboBox();
			this.cbExtractBotfile = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tpReadMe = new System.Windows.Forms.TabPage();
			this.tbReadMeFilename = new ArgusLib.Controls.FilenameTextBox();
			this.tbReadMeText = new ArgUrTMapManager.Controls.BlackTextBox();
			this.lReadMe = new System.Windows.Forms.Label();
			this.tabControl.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.tpEntities.SuspendLayout();
			this.tpBotSupport.SuspendLayout();
			this.pAasCompileOptions.SuspendLayout();
			this.pBspcSwitches.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBspcSwitchesThreads)).BeginInit();
			this.tpReadMe.SuspendLayout();
			this.SuspendLayout();
			// 
			// lImages
			// 
			this.lImages.AutoSize = true;
			this.lImages.Location = new System.Drawing.Point(6, 9);
			this.lImages.Name = "lImages";
			this.lImages.Size = new System.Drawing.Size(44, 13);
			this.lImages.TabIndex = 0;
			this.lImages.Text = "Images:";
			// 
			// cbImages
			// 
			this.cbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbImages.BackColor = System.Drawing.Color.Black;
			this.cbImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbImages.ForeColor = System.Drawing.Color.White;
			this.cbImages.FormattingEnabled = true;
			this.cbImages.Location = new System.Drawing.Point(72, 6);
			this.cbImages.Name = "cbImages";
			this.cbImages.Size = new System.Drawing.Size(198, 21);
			this.cbImages.TabIndex = 1;
			// 
			// cbSounds
			// 
			this.cbSounds.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbSounds.BackColor = System.Drawing.Color.Black;
			this.cbSounds.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSounds.ForeColor = System.Drawing.Color.White;
			this.cbSounds.FormattingEnabled = true;
			this.cbSounds.Location = new System.Drawing.Point(72, 33);
			this.cbSounds.Name = "cbSounds";
			this.cbSounds.Size = new System.Drawing.Size(198, 21);
			this.cbSounds.TabIndex = 3;
			// 
			// lSounds
			// 
			this.lSounds.AutoSize = true;
			this.lSounds.Location = new System.Drawing.Point(6, 36);
			this.lSounds.Name = "lSounds";
			this.lSounds.Size = new System.Drawing.Size(46, 13);
			this.lSounds.TabIndex = 2;
			this.lSounds.Text = "Sounds:";
			// 
			// cbBspVersion
			// 
			this.cbBspVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbBspVersion.BackColor = System.Drawing.Color.Black;
			this.cbBspVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbBspVersion.ForeColor = System.Drawing.Color.White;
			this.cbBspVersion.FormattingEnabled = true;
			this.cbBspVersion.Location = new System.Drawing.Point(72, 60);
			this.cbBspVersion.Name = "cbBspVersion";
			this.cbBspVersion.Size = new System.Drawing.Size(198, 21);
			this.cbBspVersion.TabIndex = 5;
			// 
			// lBspVersion
			// 
			this.lBspVersion.AutoSize = true;
			this.lBspVersion.Location = new System.Drawing.Point(6, 63);
			this.lBspVersion.Name = "lBspVersion";
			this.lBspVersion.Size = new System.Drawing.Size(60, 13);
			this.lBspVersion.TabIndex = 4;
			this.lBspVersion.Text = "Mapformat:";
			// 
			// cbRemoveFallingDamage
			// 
			this.cbRemoveFallingDamage.AutoSize = true;
			this.cbRemoveFallingDamage.Location = new System.Drawing.Point(9, 87);
			this.cbRemoveFallingDamage.Name = "cbRemoveFallingDamage";
			this.cbRemoveFallingDamage.Size = new System.Drawing.Size(142, 17);
			this.cbRemoveFallingDamage.TabIndex = 6;
			this.cbRemoveFallingDamage.Text = "Remove Falling Damage";
			this.cbRemoveFallingDamage.UseVisualStyleBackColor = true;
			// 
			// bOK
			// 
			this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOK.BackColor = System.Drawing.Color.Black;
			this.bOK.ForeColor = System.Drawing.Color.White;
			this.bOK.Location = new System.Drawing.Point(197, 253);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(75, 23);
			this.bOK.TabIndex = 7;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = false;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// bCancel
			// 
			this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bCancel.BackColor = System.Drawing.Color.Black;
			this.bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bCancel.ForeColor = System.Drawing.Color.White;
			this.bCancel.Location = new System.Drawing.Point(116, 253);
			this.bCancel.Name = "bCancel";
			this.bCancel.Size = new System.Drawing.Size(75, 23);
			this.bCancel.TabIndex = 8;
			this.bCancel.Text = "Cancel";
			this.bCancel.UseVisualStyleBackColor = false;
			this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
			// 
			// lMapEntities
			// 
			this.lMapEntities.AutoSize = true;
			this.lMapEntities.Location = new System.Drawing.Point(6, 6);
			this.lMapEntities.Name = "lMapEntities";
			this.lMapEntities.Size = new System.Drawing.Size(68, 13);
			this.lMapEntities.TabIndex = 9;
			this.lMapEntities.Text = "Map Entities:";
			// 
			// rbCopy
			// 
			this.rbCopy.AutoSize = true;
			this.rbCopy.Checked = true;
			this.rbCopy.Location = new System.Drawing.Point(80, 6);
			this.rbCopy.Name = "rbCopy";
			this.rbCopy.Size = new System.Drawing.Size(49, 17);
			this.rbCopy.TabIndex = 10;
			this.rbCopy.TabStop = true;
			this.rbCopy.Text = "Copy";
			this.rbCopy.UseVisualStyleBackColor = true;
			// 
			// rbUseConversionFile
			// 
			this.rbUseConversionFile.AutoSize = true;
			this.rbUseConversionFile.Location = new System.Drawing.Point(80, 29);
			this.rbUseConversionFile.Name = "rbUseConversionFile";
			this.rbUseConversionFile.Size = new System.Drawing.Size(122, 17);
			this.rbUseConversionFile.TabIndex = 11;
			this.rbUseConversionFile.TabStop = true;
			this.rbUseConversionFile.Text = "Use Conversion File:";
			this.rbUseConversionFile.UseVisualStyleBackColor = true;
			this.rbUseConversionFile.CheckedChanged += new System.EventHandler(this.rbUseConversionFile_CheckedChanged);
			// 
			// blackToolTip1
			// 
			this.blackToolTip1.AutoPopDelay = 0;
			this.blackToolTip1.BackColor = System.Drawing.Color.Black;
			this.blackToolTip1.ForeColor = System.Drawing.Color.White;
			this.blackToolTip1.InitialDelay = 500;
			this.blackToolTip1.ReshowDelay = 100;
			this.blackToolTip1.UseAnimation = false;
			// 
			// cbImport
			// 
			this.cbImport.AutoSize = true;
			this.cbImport.Location = new System.Drawing.Point(86, 189);
			this.cbImport.Name = "cbImport";
			this.cbImport.Size = new System.Drawing.Size(131, 17);
			this.cbImport.TabIndex = 13;
			this.cbImport.TabStop = true;
			this.cbImport.Text = "Import Entites from File";
			this.cbImport.UseVisualStyleBackColor = true;
			// 
			// rbImport
			// 
			this.rbImport.AutoSize = true;
			this.rbImport.Location = new System.Drawing.Point(80, 79);
			this.rbImport.Name = "rbImport";
			this.rbImport.Size = new System.Drawing.Size(131, 17);
			this.rbImport.TabIndex = 13;
			this.rbImport.TabStop = true;
			this.rbImport.Text = "Import Entites from File";
			this.rbImport.UseVisualStyleBackColor = true;
			// 
			// tabControl
			// 
			this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl.Controls.Add(this.tpGeneral);
			this.tabControl.Controls.Add(this.tpEntities);
			this.tabControl.Controls.Add(this.tpBotSupport);
			this.tabControl.Controls.Add(this.tpReadMe);
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(284, 247);
			this.tabControl.TabIndex = 16;
			// 
			// tpGeneral
			// 
			this.tpGeneral.BackColor = System.Drawing.Color.Black;
			this.tpGeneral.Controls.Add(this.cbMakeSurfacesClimbable);
			this.tpGeneral.Controls.Add(this.cbImages);
			this.tpGeneral.Controls.Add(this.lImages);
			this.tpGeneral.Controls.Add(this.lSounds);
			this.tpGeneral.Controls.Add(this.cbSounds);
			this.tpGeneral.Controls.Add(this.lBspVersion);
			this.tpGeneral.Controls.Add(this.cbRemoveFallingDamage);
			this.tpGeneral.Controls.Add(this.cbBspVersion);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpGeneral.Size = new System.Drawing.Size(276, 221);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = "General";
			// 
			// cbMakeSurfacesClimbable
			// 
			this.cbMakeSurfacesClimbable.AutoSize = true;
			this.cbMakeSurfacesClimbable.Location = new System.Drawing.Point(9, 110);
			this.cbMakeSurfacesClimbable.Name = "cbMakeSurfacesClimbable";
			this.cbMakeSurfacesClimbable.Size = new System.Drawing.Size(167, 17);
			this.cbMakeSurfacesClimbable.TabIndex = 7;
			this.cbMakeSurfacesClimbable.Text = "Make every surface climbable";
			this.cbMakeSurfacesClimbable.UseVisualStyleBackColor = true;
			// 
			// tpEntities
			// 
			this.tpEntities.BackColor = System.Drawing.Color.Black;
			this.tpEntities.Controls.Add(this.cbConversionFile);
			this.tpEntities.Controls.Add(this.lMapEntities);
			this.tpEntities.Controls.Add(this.rbCopy);
			this.tpEntities.Controls.Add(this.rbUseConversionFile);
			this.tpEntities.Controls.Add(this.rbImport);
			this.tpEntities.Location = new System.Drawing.Point(4, 22);
			this.tpEntities.Name = "tpEntities";
			this.tpEntities.Padding = new System.Windows.Forms.Padding(3);
			this.tpEntities.Size = new System.Drawing.Size(276, 221);
			this.tpEntities.TabIndex = 1;
			this.tpEntities.Text = "Entities";
			// 
			// cbConversionFile
			// 
			this.cbConversionFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbConversionFile.BackColor = System.Drawing.Color.Black;
			this.cbConversionFile.FirstItem = "None";
			this.cbConversionFile.ForeColor = System.Drawing.Color.White;
			this.cbConversionFile.FormattingEnabled = true;
			this.cbConversionFile.Location = new System.Drawing.Point(100, 52);
			this.cbConversionFile.Name = "cbConversionFile";
			this.cbConversionFile.Size = new System.Drawing.Size(168, 21);
			this.cbConversionFile.TabIndex = 14;
			// 
			// tpBotSupport
			// 
			this.tpBotSupport.BackColor = System.Drawing.Color.Black;
			this.tpBotSupport.Controls.Add(this.pAasCompileOptions);
			this.tpBotSupport.Controls.Add(this.cbEnumCompileAasCondition);
			this.tpBotSupport.Controls.Add(this.cbExtractBotfile);
			this.tpBotSupport.Controls.Add(this.label1);
			this.tpBotSupport.Location = new System.Drawing.Point(4, 22);
			this.tpBotSupport.Name = "tpBotSupport";
			this.tpBotSupport.Size = new System.Drawing.Size(276, 221);
			this.tpBotSupport.TabIndex = 2;
			this.tpBotSupport.Text = "Bot Support";
			// 
			// pAasCompileOptions
			// 
			this.pAasCompileOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pAasCompileOptions.Controls.Add(this.cbTryReachFirst);
			this.pAasCompileOptions.Controls.Add(this.lBspcCompiler);
			this.pAasCompileOptions.Controls.Add(this.cbBspcCompiler);
			this.pAasCompileOptions.Controls.Add(this.lBspcSwitches);
			this.pAasCompileOptions.Controls.Add(this.pBspcSwitches);
			this.pAasCompileOptions.Location = new System.Drawing.Point(3, 53);
			this.pAasCompileOptions.Name = "pAasCompileOptions";
			this.pAasCompileOptions.Size = new System.Drawing.Size(270, 165);
			this.pAasCompileOptions.TabIndex = 19;
			// 
			// cbTryReachFirst
			// 
			this.cbTryReachFirst.AutoSize = true;
			this.cbTryReachFirst.Location = new System.Drawing.Point(8, 27);
			this.cbTryReachFirst.Name = "cbTryReachFirst";
			this.cbTryReachFirst.Size = new System.Drawing.Size(231, 17);
			this.cbTryReachFirst.TabIndex = 19;
			this.cbTryReachFirst.Text = "Try to only calculate reachability first (faster)";
			this.cbTryReachFirst.UseVisualStyleBackColor = true;
			// 
			// lBspcCompiler
			// 
			this.lBspcCompiler.AutoSize = true;
			this.lBspcCompiler.Location = new System.Drawing.Point(5, 3);
			this.lBspcCompiler.Name = "lBspcCompiler";
			this.lBspcCompiler.Size = new System.Drawing.Size(50, 13);
			this.lBspcCompiler.TabIndex = 18;
			this.lBspcCompiler.Text = "Compiler:";
			// 
			// cbBspcCompiler
			// 
			this.cbBspcCompiler.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbBspcCompiler.BackColor = System.Drawing.Color.Black;
			this.cbBspcCompiler.FirstItem = "<Select>";
			this.cbBspcCompiler.ForeColor = System.Drawing.Color.White;
			this.cbBspcCompiler.FormattingEnabled = true;
			this.cbBspcCompiler.Location = new System.Drawing.Point(97, 0);
			this.cbBspcCompiler.Name = "cbBspcCompiler";
			this.cbBspcCompiler.Size = new System.Drawing.Size(168, 21);
			this.cbBspcCompiler.TabIndex = 17;
			// 
			// lBspcSwitches
			// 
			this.lBspcSwitches.AutoSize = true;
			this.lBspcSwitches.Location = new System.Drawing.Point(5, 47);
			this.lBspcSwitches.Name = "lBspcSwitches";
			this.lBspcSwitches.Size = new System.Drawing.Size(80, 13);
			this.lBspcSwitches.TabIndex = 7;
			this.lBspcSwitches.Text = "Bspc-Switches:";
			// 
			// pBspcSwitches
			// 
			this.pBspcSwitches.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pBspcSwitches.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesForceSidesVisible);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesNoCsg);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesNoLiquids);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesNoBrushMerge);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesBreadthFirst);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesNoVerbose);
			this.pBspcSwitches.Controls.Add(this.cbBspcSwitchesOptimize);
			this.pBspcSwitches.Controls.Add(this.nudBspcSwitchesThreads);
			this.pBspcSwitches.Controls.Add(this.lBspcSwitchesThreads);
			this.pBspcSwitches.Location = new System.Drawing.Point(0, 63);
			this.pBspcSwitches.Name = "pBspcSwitches";
			this.pBspcSwitches.Size = new System.Drawing.Size(270, 102);
			this.pBspcSwitches.TabIndex = 0;
			// 
			// cbBspcSwitchesForceSidesVisible
			// 
			this.cbBspcSwitchesForceSidesVisible.AutoSize = true;
			this.cbBspcSwitchesForceSidesVisible.Location = new System.Drawing.Point(97, 75);
			this.cbBspcSwitchesForceSidesVisible.Name = "cbBspcSwitchesForceSidesVisible";
			this.cbBspcSwitchesForceSidesVisible.Size = new System.Drawing.Size(103, 17);
			this.cbBspcSwitchesForceSidesVisible.TabIndex = 10;
			this.cbBspcSwitchesForceSidesVisible.Text = "forcesidesvisible";
			this.cbBspcSwitchesForceSidesVisible.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesNoCsg
			// 
			this.cbBspcSwitchesNoCsg.AutoSize = true;
			this.cbBspcSwitchesNoCsg.Location = new System.Drawing.Point(97, 52);
			this.cbBspcSwitchesNoCsg.Name = "cbBspcSwitchesNoCsg";
			this.cbBspcSwitchesNoCsg.Size = new System.Drawing.Size(55, 17);
			this.cbBspcSwitchesNoCsg.TabIndex = 9;
			this.cbBspcSwitchesNoCsg.Text = "nocsg";
			this.cbBspcSwitchesNoCsg.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesNoLiquids
			// 
			this.cbBspcSwitchesNoLiquids.AutoSize = true;
			this.cbBspcSwitchesNoLiquids.Location = new System.Drawing.Point(97, 29);
			this.cbBspcSwitchesNoLiquids.Name = "cbBspcSwitchesNoLiquids";
			this.cbBspcSwitchesNoLiquids.Size = new System.Drawing.Size(67, 17);
			this.cbBspcSwitchesNoLiquids.TabIndex = 8;
			this.cbBspcSwitchesNoLiquids.Text = "noliquids";
			this.cbBspcSwitchesNoLiquids.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesNoBrushMerge
			// 
			this.cbBspcSwitchesNoBrushMerge.AutoSize = true;
			this.cbBspcSwitchesNoBrushMerge.Location = new System.Drawing.Point(97, 6);
			this.cbBspcSwitchesNoBrushMerge.Name = "cbBspcSwitchesNoBrushMerge";
			this.cbBspcSwitchesNoBrushMerge.Size = new System.Drawing.Size(93, 17);
			this.cbBspcSwitchesNoBrushMerge.TabIndex = 7;
			this.cbBspcSwitchesNoBrushMerge.Text = "nobrushmerge";
			this.cbBspcSwitchesNoBrushMerge.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesBreadthFirst
			// 
			this.cbBspcSwitchesBreadthFirst.AutoSize = true;
			this.cbBspcSwitchesBreadthFirst.Location = new System.Drawing.Point(6, 75);
			this.cbBspcSwitchesBreadthFirst.Name = "cbBspcSwitchesBreadthFirst";
			this.cbBspcSwitchesBreadthFirst.Size = new System.Drawing.Size(78, 17);
			this.cbBspcSwitchesBreadthFirst.TabIndex = 6;
			this.cbBspcSwitchesBreadthFirst.Text = "breadthfirst";
			this.cbBspcSwitchesBreadthFirst.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesNoVerbose
			// 
			this.cbBspcSwitchesNoVerbose.AutoSize = true;
			this.cbBspcSwitchesNoVerbose.Location = new System.Drawing.Point(6, 52);
			this.cbBspcSwitchesNoVerbose.Name = "cbBspcSwitchesNoVerbose";
			this.cbBspcSwitchesNoVerbose.Size = new System.Drawing.Size(76, 17);
			this.cbBspcSwitchesNoVerbose.TabIndex = 5;
			this.cbBspcSwitchesNoVerbose.Text = "noverbose";
			this.cbBspcSwitchesNoVerbose.UseVisualStyleBackColor = true;
			// 
			// cbBspcSwitchesOptimize
			// 
			this.cbBspcSwitchesOptimize.AutoSize = true;
			this.cbBspcSwitchesOptimize.Location = new System.Drawing.Point(6, 29);
			this.cbBspcSwitchesOptimize.Name = "cbBspcSwitchesOptimize";
			this.cbBspcSwitchesOptimize.Size = new System.Drawing.Size(64, 17);
			this.cbBspcSwitchesOptimize.TabIndex = 4;
			this.cbBspcSwitchesOptimize.Text = "optimize";
			this.cbBspcSwitchesOptimize.UseVisualStyleBackColor = true;
			// 
			// nudBspcSwitchesThreads
			// 
			this.nudBspcSwitchesThreads.BackColor = System.Drawing.Color.Black;
			this.nudBspcSwitchesThreads.ForeColor = System.Drawing.Color.White;
			this.nudBspcSwitchesThreads.Location = new System.Drawing.Point(58, 3);
			this.nudBspcSwitchesThreads.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
			this.nudBspcSwitchesThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudBspcSwitchesThreads.Name = "nudBspcSwitchesThreads";
			this.nudBspcSwitchesThreads.Size = new System.Drawing.Size(33, 20);
			this.nudBspcSwitchesThreads.TabIndex = 3;
			this.nudBspcSwitchesThreads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudBspcSwitchesThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// lBspcSwitchesThreads
			// 
			this.lBspcSwitchesThreads.AutoSize = true;
			this.lBspcSwitchesThreads.Location = new System.Drawing.Point(3, 5);
			this.lBspcSwitchesThreads.Name = "lBspcSwitchesThreads";
			this.lBspcSwitchesThreads.Size = new System.Drawing.Size(49, 13);
			this.lBspcSwitchesThreads.TabIndex = 2;
			this.lBspcSwitchesThreads.Text = "Threads:";
			// 
			// cbEnumCompileAasCondition
			// 
			this.cbEnumCompileAasCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cbEnumCompileAasCondition.BackColor = System.Drawing.Color.Black;
			this.cbEnumCompileAasCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEnumCompileAasCondition.ForeColor = System.Drawing.Color.White;
			this.cbEnumCompileAasCondition.FormattingEnabled = true;
			this.cbEnumCompileAasCondition.Location = new System.Drawing.Point(100, 26);
			this.cbEnumCompileAasCondition.Name = "cbEnumCompileAasCondition";
			this.cbEnumCompileAasCondition.Size = new System.Drawing.Size(168, 21);
			this.cbEnumCompileAasCondition.TabIndex = 16;
			this.cbEnumCompileAasCondition.SelectedIndexChanged += new System.EventHandler(this.cbEnumCompileAasCondition_SelectedIndexChanged);
			// 
			// cbExtractBotfile
			// 
			this.cbExtractBotfile.AutoSize = true;
			this.cbExtractBotfile.Location = new System.Drawing.Point(8, 3);
			this.cbExtractBotfile.Name = "cbExtractBotfile";
			this.cbExtractBotfile.Size = new System.Drawing.Size(178, 17);
			this.cbExtractBotfile.TabIndex = 15;
			this.cbExtractBotfile.Text = "Extract Botfile (<mapname>.aas)";
			this.cbExtractBotfile.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 29);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Compile Botmap:";
			// 
			// tpReadMe
			// 
			this.tpReadMe.BackColor = System.Drawing.Color.Black;
			this.tpReadMe.Controls.Add(this.tbReadMeFilename);
			this.tpReadMe.Controls.Add(this.tbReadMeText);
			this.tpReadMe.Controls.Add(this.lReadMe);
			this.tpReadMe.Location = new System.Drawing.Point(4, 22);
			this.tpReadMe.Name = "tpReadMe";
			this.tpReadMe.Padding = new System.Windows.Forms.Padding(3);
			this.tpReadMe.Size = new System.Drawing.Size(276, 221);
			this.tpReadMe.TabIndex = 3;
			this.tpReadMe.Text = "ReadMe";
			// 
			// tbReadMeFilename
			// 
			this.tbReadMeFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbReadMeFilename.BackColor = System.Drawing.Color.Black;
			this.tbReadMeFilename.ForeColor = System.Drawing.Color.White;
			this.tbReadMeFilename.InstructionColor = System.Drawing.Color.White;
			this.tbReadMeFilename.InstructionFontStyle = System.Drawing.FontStyle.Italic;
			this.tbReadMeFilename.InstructionText = "Filename (e.g. ReadMe.txt)";
			this.tbReadMeFilename.InstructionTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.tbReadMeFilename.Location = new System.Drawing.Point(8, 46);
			this.tbReadMeFilename.Name = "tbReadMeFilename";
			this.tbReadMeFilename.ShowInstructionText = false;
			this.tbReadMeFilename.Size = new System.Drawing.Size(260, 20);
			this.tbReadMeFilename.TabIndex = 3;
			// 
			// tbReadMeText
			// 
			this.tbReadMeText.AcceptsReturn = true;
			this.tbReadMeText.AcceptsTab = true;
			this.tbReadMeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbReadMeText.BackColor = System.Drawing.Color.Black;
			this.tbReadMeText.ForeColor = System.Drawing.Color.White;
			this.tbReadMeText.InstructionColor = System.Drawing.Color.White;
			this.tbReadMeText.InstructionFontStyle = System.Drawing.FontStyle.Italic;
			this.tbReadMeText.InstructionText = "Write the readme-text...";
			this.tbReadMeText.InstructionTextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.tbReadMeText.Location = new System.Drawing.Point(8, 72);
			this.tbReadMeText.Multiline = true;
			this.tbReadMeText.Name = "tbReadMeText";
			this.tbReadMeText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.tbReadMeText.ShowInstructionText = false;
			this.tbReadMeText.Size = new System.Drawing.Size(260, 143);
			this.tbReadMeText.TabIndex = 2;
			// 
			// lReadMe
			// 
			this.lReadMe.AutoEllipsis = true;
			this.lReadMe.Dock = System.Windows.Forms.DockStyle.Top;
			this.lReadMe.Location = new System.Drawing.Point(3, 3);
			this.lReadMe.Name = "lReadMe";
			this.lReadMe.Size = new System.Drawing.Size(270, 40);
			this.lReadMe.TabIndex = 0;
			this.lReadMe.Text = "The following text will be written into a readme-file and included into the extra" +
    "cted package. You may modify the text, but you should preserve the meaning.";
			// 
			// MapExtractOptionsForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.bCancel;
			this.ClientSize = new System.Drawing.Size(284, 288);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.bCancel);
			this.Controls.Add(this.bOK);
			this.MinimumSize = new System.Drawing.Size(300, 327);
			this.Name = "MapExtractOptionsForm";
			this.Text = "Map Extract Options";
			this.tabControl.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.tpGeneral.PerformLayout();
			this.tpEntities.ResumeLayout(false);
			this.tpEntities.PerformLayout();
			this.tpBotSupport.ResumeLayout(false);
			this.tpBotSupport.PerformLayout();
			this.pAasCompileOptions.ResumeLayout(false);
			this.pAasCompileOptions.PerformLayout();
			this.pBspcSwitches.ResumeLayout(false);
			this.pBspcSwitches.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudBspcSwitchesThreads)).EndInit();
			this.tpReadMe.ResumeLayout(false);
			this.tpReadMe.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lImages;
		private Controls.BlackEnumComboBox cbImages;
		private Controls.BlackEnumComboBox cbSounds;
		private System.Windows.Forms.Label lSounds;
		private Controls.BlackEnumComboBox cbBspVersion;
		private System.Windows.Forms.Label lBspVersion;
		private System.Windows.Forms.CheckBox cbRemoveFallingDamage;
		private Controls.BlackButton bOK;
		private Controls.BlackButton bCancel;
		private System.Windows.Forms.Label lMapEntities;
		private System.Windows.Forms.RadioButton rbCopy;
		private System.Windows.Forms.RadioButton rbUseConversionFile;
		private Controls.BlackToolTip blackToolTip1;
		private System.Windows.Forms.RadioButton cbImport;
		private System.Windows.Forms.RadioButton rbImport;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.TabPage tpEntities;
		private System.Windows.Forms.TabPage tpBotSupport;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel pBspcSwitches;
		private System.Windows.Forms.Label lBspcSwitches;
		private System.Windows.Forms.Label lBspcSwitchesThreads;
		private Controls.EntityConversionComboBox cbConversionFile;
		private System.Windows.Forms.CheckBox cbExtractBotfile;
		private Controls.BlackEnumComboBox cbEnumCompileAasCondition;
		private System.Windows.Forms.Label lBspcCompiler;
		private Controls.BlackFilenameComboBox cbBspcCompiler;
		private System.Windows.Forms.Panel pAasCompileOptions;
		private System.Windows.Forms.CheckBox cbTryReachFirst;
		private Controls.BlackNumericUpDown nudBspcSwitchesThreads;
		private System.Windows.Forms.CheckBox cbBspcSwitchesForceSidesVisible;
		private System.Windows.Forms.CheckBox cbBspcSwitchesNoCsg;
		private System.Windows.Forms.CheckBox cbBspcSwitchesNoLiquids;
		private System.Windows.Forms.CheckBox cbBspcSwitchesNoBrushMerge;
		private System.Windows.Forms.CheckBox cbBspcSwitchesBreadthFirst;
		private System.Windows.Forms.CheckBox cbBspcSwitchesNoVerbose;
		private System.Windows.Forms.CheckBox cbBspcSwitchesOptimize;
		private System.Windows.Forms.TabPage tpReadMe;
		private System.Windows.Forms.Label lReadMe;
		private Controls.BlackTextBox tbReadMeText;
		private ArgusLib.Controls.FilenameTextBox tbReadMeFilename;
		private System.Windows.Forms.CheckBox cbMakeSurfacesClimbable;
	}
}