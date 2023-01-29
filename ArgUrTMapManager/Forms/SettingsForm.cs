using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ArgUrTMapManager.Forms
{
	public partial class SettingsForm : ModalToolForm
	{
		const string TextSelectGameBin = "<Select Game Binary>";
		const string TextSelectServerBin = "<Select Server Binary>";
		const string TextSelectServerConfig = "<Select Serverconfig File>";

		Settings settings;

		public SettingsForm()
			:base()
		{
			InitializeComponent();
		}

		private void InitializeGameExecutableList(Settings settings)
		{
			// Initialize Binary-ComboBoxes
			this.cbGeneralGameBin.Items.Clear();
			this.cbGeneralServerBin.Items.Clear();
			string thisBin = Path.GetFileName(Application.ExecutablePath).ToLower();

			string[] bins = Directory.GetFiles(
				FilesystemEntries.GameDirectory,
				"*"+FileExtensions.Executables);

			this.cbGeneralGameBin.Items.Add(SettingsForm.TextSelectGameBin);
			this.cbGeneralServerBin.Items.Add(SettingsForm.TextSelectServerBin);
			foreach (string bin in bins)
			{
				string binName = Path.GetFileName(bin);
				if (binName.ToLower() == thisBin)
					continue;
				this.cbGeneralGameBin.Items.Add(binName);
				this.cbGeneralServerBin.Items.Add(binName);
			}

			if (string.IsNullOrEmpty(settings.NameGameBinary) == true)
				this.cbGeneralGameBin.SelectedItem = SettingsForm.TextSelectGameBin;
			else
				this.cbGeneralGameBin.SelectedItem = settings.NameGameBinary;

			if (string.IsNullOrEmpty(settings.NameServerBinary) == true)
				this.cbGeneralServerBin.SelectedItem = SettingsForm.TextSelectServerBin;
			else
				this.cbGeneralServerBin.SelectedItem = settings.NameServerBinary;
		}

		private void InitializeServerConfigList(Settings settings)
		{
			this.cbServerconfigFile.Items.Clear();

			this.cbServerconfigFile.Items.Add(TextSelectServerConfig);

			string[] configFiles = Directory.GetFiles(
				FilesystemEntries.GameDataDirectory,
				"*" + FileExtensions.ConfigFiles);

			foreach (string file in configFiles)
			{
				this.cbServerconfigFile.Items.Add(Path.GetFileName(file).ToLowerInvariant());
			}

			if (string.IsNullOrEmpty(settings.NameServerConfig) == true)
				this.cbServerconfigFile.SelectedItem = SettingsForm.TextSelectServerConfig;
			else
				this.cbServerconfigFile.SelectedItem = settings.NameServerConfig;
		
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.settings = Settings.Load();
			this.InitializeGameExecutableList(settings);
			this.InitializeServerConfigList(settings);
			this.tbMapcycleFile.Text = settings.NameMapcycleFile;
			this.cbSv_Pure.Checked = settings.sv_pure;
		}

		private void bCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			if (this.cbGeneralGameBin.Text == TextSelectGameBin)
				this.settings.NameGameBinary = string.Empty;
			else
				this.settings.NameGameBinary = this.cbGeneralGameBin.Text;

			if (this.cbGeneralServerBin.Text == TextSelectServerBin)
				this.settings.NameServerBinary = string.Empty;
			else
				this.settings.NameServerBinary = this.cbGeneralServerBin.Text;

			if (this.cbServerconfigFile.Text == TextSelectServerConfig)
				this.settings.NameServerConfig = string.Empty;
			else
				this.settings.NameServerConfig = this.cbServerconfigFile.Text;

			string mapcycle = Path.Combine(FilesystemEntries.GameDataDirectory, this.tbMapcycleFile.Text);
			if (File.Exists(mapcycle) == true)
			{
				string text = this.tbMapcycleFile.Text+" already exists and will be overwritten.\nDo you want to continue?";
				if (MessageBox.Show(text, "Warning", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
					return;
			}

			this.settings.sv_pure = this.cbSv_Pure.Checked;

			this.settings.Save();
			this.Close();
		}
	}
}
