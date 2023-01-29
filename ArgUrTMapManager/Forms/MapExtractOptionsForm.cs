using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace ArgUrTMapManager.Forms
{
	public partial class MapExtractOptionsForm : ModalToolForm
	{
		public MapExtractOptions MapExtractOptions { get; private set; }

		public MapExtractOptionsForm()
		{
			InitializeComponent();
			this.tbReadMeText.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace), this.tbReadMeText.Font.Size);
			this.MapExtractOptions = new MapExtractOptions();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.MapExtractOptions = MapExtractOptions.Load();

			// General Tab
			this.cbImages.Initialize(typeof(MapExtractOptions.Images));
			this.cbSounds.Initialize(typeof(MapExtractOptions.Sounds));
			this.cbBspVersion.Initialize(typeof(MapExtractOptions.BspVersion));
#if Mono
			this.cbImages.Select(MapExtractOptions.Images.CopyAll.ToUInt64());
			this.cbImages.Enabled = false;
#else
			this.cbImages.Select(this.MapExtractOptions.ImagesOptions.ToUInt64());
#endif
			this.cbSounds.Select(this.MapExtractOptions.SoundsOptions.ToUInt64());
			this.cbBspVersion.Select(this.MapExtractOptions.BspVersionOptions.ToUInt64());
			this.cbRemoveFallingDamage.Checked = this.MapExtractOptions.SurfaceParms.HasFlag(SurfaceParms.SURF_NODAMAGE);
			this.cbMakeSurfacesClimbable.Checked = this.MapExtractOptions.SurfaceParms.HasFlag(SurfaceParms.SURF_LADDER);
			
			// Entities Tab
			if (this.MapExtractOptions.EntitiesOptions == ArgUrTMapManager.MapExtractOptions.Entities.Copy)
				this.rbCopy.Checked = true;
			else if (this.MapExtractOptions.EntitiesOptions == ArgUrTMapManager.MapExtractOptions.Entities.Import)
				this.rbImport.Checked = true;
			else if (this.MapExtractOptions.EntitiesOptions == ArgUrTMapManager.MapExtractOptions.Entities.UseConversionFile)
				this.rbUseConversionFile.Checked = true;

			string conversionfile = this.MapExtractOptions.ConversionFile;
			if (string.IsNullOrEmpty(conversionfile) == false)
				this.cbConversionFile.Select(Path.GetFileName(conversionfile));

			this.blackToolTip1.SetToolTip(this.rbImport,
				"Import Entities from <mapname>.xml placed in\n" + FilesystemEntries.ExtractedEntitiesDirectory + '.');

			// Bot Support Tab
			this.cbExtractBotfile.Checked = this.MapExtractOptions.ExtractBotfile;
			this.cbEnumCompileAasCondition.Initialize(typeof(MapExtractOptions._CompileAasCondition));
			this.cbEnumCompileAasCondition.Select(this.MapExtractOptions.CompileAasCondition.ToUInt64());
			this.cbTryReachFirst.Checked = this.MapExtractOptions.CompileAasTryReachFirst;
			this.cbBspcCompiler.DirectoryPath = FilesystemEntries.LibsDirectory;
			this.cbBspcCompiler.FileExtension = ".exe";
			if (string.IsNullOrEmpty(this.MapExtractOptions.AasCompilerFile) == false)
				this.cbBspcCompiler.SelectedItem = Path.GetFileName(this.MapExtractOptions.AasCompilerFile);
			this.nudBspcSwitchesThreads.Value = this.MapExtractOptions.CompileAasThreads;
			MapExtractOptions.BspcSwitch bspcSwitches = this.MapExtractOptions.BspcSwitches;
			this.cbBspcSwitchesBreadthFirst.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.breadthfirst);
			this.cbBspcSwitchesForceSidesVisible.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.forcesidesvisible);
			this.cbBspcSwitchesNoBrushMerge.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.nobrushmerge);
			this.cbBspcSwitchesNoCsg.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.nocsg);
			this.cbBspcSwitchesNoLiquids.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.noliquids);
			this.cbBspcSwitchesNoVerbose.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.noverbose);
			this.cbBspcSwitchesOptimize.Checked = bspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.optimize);

			// ReadMe Tab
			if (string.IsNullOrEmpty(this.MapExtractOptions.ReadMeFilename) == true)
				this.MapExtractOptions.ReadMeFilename = ResourceFiles.Resources.MapExtractReadMeStandardFilename;
			if (string.IsNullOrEmpty(this.MapExtractOptions.ReadMeText) == true)
				this.MapExtractOptions.ReadMeText = ResourceFiles.Resources.MapExtractReadMeStandardText;
			this.tbReadMeFilename.Text = this.MapExtractOptions.ReadMeFilename;
			this.tbReadMeText.Text = this.MapExtractOptions.ReadMeText;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
			{
				MapExtractOptions options = this.MapExtractOptions;
				// General Tab
				options.ImagesOptions = (MapExtractOptions.Images)this.cbImages.SelectedEnum;
				options.SoundsOptions = (MapExtractOptions.Sounds)this.cbSounds.SelectedEnum;
				options.BspVersionOptions = (MapExtractOptions.BspVersion)this.cbBspVersion.SelectedEnum;
				options.SurfaceParms = (SurfaceParms)0;
				if (this.cbRemoveFallingDamage.Checked == true)
					options.SurfaceParms |= SurfaceParms.SURF_NODAMAGE;
				if (this.cbMakeSurfacesClimbable.Checked == true)
					options.SurfaceParms |= SurfaceParms.SURF_LADDER;

				// Entities Tab
				options.ConversionFile = this.cbConversionFile.EntityConversionFile;
				if (this.rbCopy.Checked == true)
					options.EntitiesOptions = ArgUrTMapManager.MapExtractOptions.Entities.Copy;
				else if (this.rbImport.Checked == true)
					options.EntitiesOptions = ArgUrTMapManager.MapExtractOptions.Entities.Import;
				else if (this.rbUseConversionFile.Checked == true)
					options.EntitiesOptions = ArgUrTMapManager.MapExtractOptions.Entities.UseConversionFile;

				// Bot Support Tab
				options.AasCompilerFile = this.cbBspcCompiler.FullFilename;
				options.ExtractBotfile = this.cbExtractBotfile.Checked;
				options.CompileAasCondition = (MapExtractOptions._CompileAasCondition)this.cbEnumCompileAasCondition.SelectedEnum;
				options.CompileAasTryReachFirst = this.cbTryReachFirst.Checked;
				options.CompileAasThreads = (int)this.nudBspcSwitchesThreads.Value;
				options.BspcSwitches = ArgUrTMapManager.MapExtractOptions.BspcSwitch.None;
				if (this.cbBspcSwitchesBreadthFirst.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.breadthfirst;
				if (this.cbBspcSwitchesForceSidesVisible.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.forcesidesvisible;
				if (this.cbBspcSwitchesNoBrushMerge.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.nobrushmerge;
				if (this.cbBspcSwitchesNoCsg.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.nocsg;
				if (this.cbBspcSwitchesNoLiquids.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.noliquids;
				if (this.cbBspcSwitchesNoVerbose.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.noverbose;
				if (this.cbBspcSwitchesOptimize.Checked == true)
					options.BspcSwitches |= ArgUrTMapManager.MapExtractOptions.BspcSwitch.optimize;

				// ReadMe Tab
				if (string.IsNullOrEmpty(this.tbReadMeFilename.Text) == true ||
					string.IsNullOrEmpty(this.tbReadMeText.Text) == true)
				{
					MessageBox.Show("The readme-filename and the readme-text must not be empty.");
					e.Cancel = true;
					base.OnClosing(e);
				}
				options.ReadMeFilename = this.tbReadMeFilename.Text;
				options.ReadMeText = this.tbReadMeText.Text;

				this.MapExtractOptions = options;
				this.MapExtractOptions.Save();
			}
			base.OnClosing(e);
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void rbUseConversionFile_CheckedChanged(object sender, EventArgs e)
		{
			this.cbConversionFile.Enabled = this.rbUseConversionFile.Checked;
		}

		private void cbEnumCompileAasCondition_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.pAasCompileOptions.Enabled = this.cbEnumCompileAasCondition.SelectedEnum != (UInt64)MapExtractOptions._CompileAasCondition.Never;
		}
	}
}
