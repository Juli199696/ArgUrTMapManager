using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ArgUrTMapManager.Forms
{
	public partial class MapcycleForm : ToolForm
	{
		Process serverProcess;
		string mapcycleFilename;

		public MapcycleForm()
			:base()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.miFileSave.Image = ResourceFiles.Icons16.Save;
			this.miFileOpen.Image = ResourceFiles.Icons16.Open;
			this.bUp.Image = ResourceFiles.Icons22.Up;
			this.bDown.Image = ResourceFiles.Icons22.Down;
			this.bUp.Text = string.Empty;
			this.bDown.Text = string.Empty;
		}

		internal void InitializeMaps(string MapcycleFilename)
		{
			this.mapcycleFilename = MapcycleFilename;
			if (File.Exists(MapcycleFilename) == false)
				return;

			using (FileStream file = new FileStream(MapcycleFilename, FileMode.Open))
			{
				string[] maps = MapcycleFile.ReadFile(file);
				file.Close();
				this.lbMaps.Items.AddRange(maps);
			}
		}

		internal void AddMapItems(string[] Maps)
		{
			this.lbMaps.Items.AddRange(Maps);
			this.OnMapsChanged();
		}

		private void bUp_Click(object sender, EventArgs e)
		{
			this.lbMaps.MoveSelectedUp();
			this.OnMapsChanged();
		}

		private void bDown_Click(object sender, EventArgs e)
		{
			this.lbMaps.MoveSelectedDown();
			this.OnMapsChanged();
		}

		private void bRemove_Click(object sender, EventArgs e)
		{
			this.lbMaps.RemoveSelected();
			this.OnMapsChanged();
		}

		private void lbMaps_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.bRemove_Click(null, null);
			}
			else if (e.KeyCode == Keys.Up && this.lbMaps.SelectedItems.Count > 0)
			{
				this.bUp_Click(null, null);
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Down && this.lbMaps.SelectedItems.Count > 0)
			{
				this.bDown_Click(null, null);
				e.Handled = true;
			}
		}

		private void OnMapsChanged()
		{
			string[] maps = this.lbMaps.GetItems();
			using (FileStream file = new FileStream(this.mapcycleFilename, FileMode.Create))
			{
				MapcycleFile.WriteFile(file, maps);
				file.Close();
			}
		}

		private void bStartServer_Click(object sender, EventArgs e)
		{
			if (this.lbMaps.Items.Count < 1)
			{
				MessageBox.Show("The mapcycle must at least contain one map.");
				return;
			}

			if (this.serverProcess != null && this.serverProcess.HasExited == false)
			{
				MessageBox.Show("Server is already running.");
				return;
			}

			Settings settings = Settings.Load();

			string fileName = Path.Combine(FilesystemEntries.GameDirectory, settings.NameServerBinary);
			if (File.Exists(fileName) == false)
			{
				MessageBox.Show("Server Executable not found, check your settings.", "Error");
				return;
			}
			if (string.IsNullOrEmpty(settings.NameServerConfig) == true)
			{
				MessageBox.Show("Serverconfig File not found, check your settings.", "Error");
				return;
			}

			this.serverProcess = new Process();
			this.serverProcess.StartInfo.FileName = fileName;
			this.serverProcess.StartInfo.WorkingDirectory = FilesystemEntries.GameDirectory;
			this.serverProcess.StartInfo.Arguments = "+" + Constants.CombineCVars(Constants.CVarExec, settings.NameServerConfig) +
				" +" + Constants.CombineCVars(Constants.CVarSet, Constants.CVarMapcycle, settings.NameMapcycleFile) +
				" +" + Constants.CombineCVars(Constants.CVarMap, (string)this.lbMaps.Items[0]);
			
#if Mono
			PlatformID platform = Environment.OSVersion.Platform;
			if (platform == PlatformID.Unix || platform == PlatformID.MacOSX)
			{
				this.serverProcess.StartInfo.UseShellExecute = false;
				this.serverProcess.StartInfo.RedirectStandardInput = true;
				this.serverProcess.StartInfo.RedirectStandardOutput = true;
				this.serverProcess.StartInfo.RedirectStandardError = true;
				this.serverProcess.EnableRaisingEvents = true;
				UnixServerForm unixServerForm = new UnixServerForm(this.serverProcess);
				unixServerForm.StartProcess();
				return;
			}
#endif
			this.serverProcess.Start();
		}

		private void miFileSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = FilesystemEntries.GameDataDirectory;

			if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			using (Stream file = sfd.OpenFile())
			{
				string[] maps = this.lbMaps.GetItems();
				MapcycleFile.WriteFile(file, maps);
				file.Close();
			}
		}

		private void miFileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.InitialDirectory = FilesystemEntries.GameDataDirectory;

			if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			using (Stream file = ofd.OpenFile())
			{
				string[] maps = MapcycleFile.ReadFile(file);
				file.Close();
				this.lbMaps.Items.Clear();
				this.lbMaps.Items.AddRange(maps);
				this.OnMapsChanged();
			}
		}
	}
}
