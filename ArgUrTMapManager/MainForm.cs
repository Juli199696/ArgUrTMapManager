//#define DebugBackgroundWorker
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using ArgusLib.Collections;

namespace ArgUrTMapManager
{
	public partial class MainForm : Forms.BaseForm
	{
		Forms.MapcycleForm mapcycleForm;
		Forms.LevelshotForm levelshotForm;
		Forms.SettingsForm settingsForm;
		Forms.GearCalculatorForm gearCalculatorForm;
		Forms.ProgressForm progressForm;
		DataArchiveManager dataArchiveManager;
		Process gameProcess;
		bool ignoreActivation = false;
		string[] mapNames;
		bool initialized = false;

		public MainForm()
		{
			InitializeComponent();
		}

		private void InitializeIcons()
		{
			this.miExtractSelectedMaps.Image = ResourceFiles.Icons16.Export;
			this.miToolsExportQuakeLive.Image = ResourceFiles.Icons16.Export;
			this.miToolsGearCalculator.Image = ResourceFiles.Icons16.Calculator;
			this.miToolsSettings.Image = ResourceFiles.Icons16.Settings;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.initialized = FilesystemEntries.Initialize();
			if (this.initialized == false)
			{
				MessageBox.Show("Initialization failed. Make sure that the program is run from the game's main directory.");
				this.Close();
				return;
			}

			// Temp
			//{
			//	EntityConversion e1 = new EntityConversion();
			//	EntityConversion.Definition d1 = new EntityConversion.Definition();
			//	d1.OriginalEntity = new EntityConversion.Definition.c_OriginalEntity();
			//	d1.OriginalEntity.Classname = "classname";
			//	d1.OriginalEntity.KeysToRemove = new string[0];
			//	EntityConversion.Definition.c_NewEntity newE = new EntityConversion.Definition.c_NewEntity();
			//	newE.Classname = "newclassname";
			//	newE.KeyValuePairsToAdd = new ItemPair<string, string>[]{
			//		new ItemPair<string,string>("key1","value1"),
			//		new ItemPair<string,string>("key1","value1")};
			//	d1.NewEntities = new EntityConversion.Definition.c_NewEntity[] { newE };
			//	e1.Definitions = new EntityConversion.Definition[] { d1 };
			//	e1.Save("EntityReplacement.xml");
			//}

			this.InitializeIcons();

			this.mapcycleForm = new Forms.MapcycleForm();
			this.levelshotForm = new Forms.LevelshotForm();
			this.settingsForm = new Forms.SettingsForm();
			this.gearCalculatorForm = new Forms.GearCalculatorForm();
			this.progressForm = new Forms.ProgressForm();

			// Load Settings
			Settings settings = Settings.Load();
			if (settings.BoundsMainForm.GetArea() > 0)
				this.Bounds = settings.BoundsMainForm;
			if (settings.BoundsMapcycleForm.GetArea() > 0)
				this.mapcycleForm.Bounds = settings.BoundsMapcycleForm;
			if (settings.BoundsLevelshotForm.GetArea() > 0)
				this.levelshotForm.Bounds = settings.BoundsLevelshotForm;
			if (settings.BoundsSettingsForm.GetArea() > 0)
				this.settingsForm.Bounds = settings.BoundsSettingsForm;
			if (settings.BoundsGearCalculatorForm.GetArea() > 0)
				this.gearCalculatorForm.Bounds = settings.BoundsGearCalculatorForm;

			string mapcycleFilename = Path.Combine(FilesystemEntries.GameDataDirectory, settings.NameMapcycleFile);
			this.mapcycleForm.InitializeMaps(mapcycleFilename);

			// Check for Quake Live
			this.miToolsExportQuakeLive.Enabled = FilesystemEntries.QuakeLiveDirectory != null;

			// Load Map Names
			this.dataArchiveManager = DataArchiveManager.LoadQuake3Archive(FilesystemEntries.GameDirectory);
			this.dataArchiveManager.IncludeFilesystemEntries = false; // = settings.sv_pure == false;
			this.dataArchiveManager.ReportProgress += this.progressForm.GetProgressHandler();

			this.RunBackgroundWorker(
				(wSender, wE) =>
				{
					wE.Result = this.dataArchiveManager.CheckIntegrity();
				},
			(wSender, wE) =>
			{
				List<string> corruptedFiles = (List<string>)wE.Result;
				if (corruptedFiles.Count < 1)
					return;
				string text = "Do you want to delete corrupted files?\nThe following files are corrupted:\n";
				foreach (string file in corruptedFiles)
					text += file + '\n';
				if (MessageBox.Show(text, "Corrupted Files", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
				{
					foreach (string file in corruptedFiles)
						File.Delete(file);
				}
			});

			this.RunBackgroundWorker(
				(wSender, wE) =>
				{
					wE.Result = this.dataArchiveManager.GetMapNames();
					//wE.Result = new List<string>();
				},
			(wSender, wE) =>
			{
				this.mapNames = (string[])wE.Result;
				this.lbMaps.Items.AddRange(this.mapNames);
			});
		}

		private void RunBackgroundWorker(DoWorkEventHandler DoWork, RunWorkerCompletedEventHandler WorkerCompleted)
		{
			BackgroundWorker worker = new BackgroundWorker();
			worker.DoWork += DoWork;
			worker.RunWorkerCompleted += (wSender, wE) =>
				{
					if (wE.Error == null)
					{
						WorkerCompleted(wSender, wE);
						this.progressForm.Close();
					}
					else
					{
						this.progressForm.Close();
						throw wE.Error;
					}
				};
			//worker.RunWorkerCompleted += WorkerCompleted;
			//worker.RunWorkerCompleted += (wSender, wE) => this.progressForm.Close();
			worker.RunWorkerAsync();
			this.progressForm.ShowDialog();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.initialized == false)
				return;
			// Save Settings
			Settings settings = Settings.Load();
			settings.BoundsMainForm = this.Bounds;
			settings.BoundsMapcycleForm = this.mapcycleForm.Bounds;
			settings.BoundsLevelshotForm = this.levelshotForm.Bounds;
			settings.BoundsSettingsForm = this.settingsForm.Bounds;
			settings.BoundsGearCalculatorForm = this.gearCalculatorForm.Bounds;
			settings.Save();
		}

		private void miToolsSettings_Click(object sender, EventArgs e)
		{
			this.settingsForm.ShowDialog();
		}

		private void miToolsExportQuakeLive_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "Where do you want to export the files to?";
			fbd.ShowNewFolderButton = true;
			if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

			this.RunBackgroundWorker(
				(wSender, wE) =>
				{
					DataArchiveManager quakeLiveArchive = DataArchiveManager.LoadQuakeLiveArchive();
					quakeLiveArchive.ReportProgress += this.progressForm.GetProgressHandler();
					quakeLiveArchive.CopyArchive(fbd.SelectedPath);
				}, null);
		}

		private void lbMaps_DoubleClick(object sender, EventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count < 1)
				return;

			string mapname = (string)this.lbMaps.SelectedItem;

			Image im = this.dataArchiveManager.GetLevelshotImage(mapname);
			if (im == null)
				return;

			this.levelshotForm.SetImage(im, mapname);
			this.levelshotForm.Show();
		}

		private void lbMaps_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count < 1)
				return;

			if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C)
			{
				string text = (string)this.lbMaps.SelectedItems[0];
				for (int i = 1; i < this.lbMaps.SelectedItems.Count; i++)
				{
					text += '\n' + (string)this.lbMaps.SelectedItems[i];
				}
				Clipboard.SetText(text);
			}
			else if (e.KeyCode == Keys.Add)
			{
				this.bAddToMapcycle_Click(null, EventArgs.Empty);
			}
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			if (this.ignoreActivation == true)
			{
				this.ignoreActivation = false;
				return;
			}

			this.ignoreActivation = true;
			this.levelshotForm.Activate();
			this.Activate();
			//this.ignoreActivation = false;
		}

		private void miHelpAbout_Click(object sender, EventArgs e)
		{
			Process.Start(Constants.AboutHomepage);
		}

		private void miFileExtractMaps_Click(object sender, EventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count < 1)
			{
				MessageBox.Show("No Map selected.");
				return;
			}

			Forms.MapExtractOptionsForm w = new Forms.MapExtractOptionsForm();
			if (w.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;
			MapExtractOptions mapExtractOptions = w.MapExtractOptions;

			ListTriple<string, string, string> maps = new ListTriple<string, string, string>();

			if (this.lbMaps.SelectedItems.Count < 2)
			{
				string Mapname = (string)this.lbMaps.SelectedItems[0];
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.AddExtension = true;
				sfd.FileName = Mapname;
				sfd.DefaultExt = FileExtensions.Packages;
				sfd.Filter = "PK3 Archive|*.pk3";
				sfd.FilterIndex = 0;
				if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;
				maps.Add(new ItemTriple<string, string, string>(sfd.FileName, Mapname, Path.GetFileNameWithoutExtension(sfd.FileName)));
			}
			else
			{
				string prefix = ArgusLib.Controls.InputBox.Show("Prefix for extracted maps:", "Prefix");
				string postfix = ArgusLib.Controls.InputBox.Show("Postfix for extracted maps:", "Postfix");
				FolderBrowserDialog fbd = new FolderBrowserDialog();
				fbd.SelectedPath = FilesystemEntries.GameDataDirectory;
				fbd.ShowNewFolderButton = true;
				if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
					return;
				string folder = fbd.SelectedPath;
				foreach (string mapname in this.lbMaps.SelectedItems)
				{
					string newName = prefix + mapname + postfix;
					string path = Path.Combine(folder, newName) + FileExtensions.Packages;
					int postNo = 1;
					while (File.Exists(path) == true)
					{
						path = Path.Combine(folder, newName) + postNo.ToString() + FileExtensions.Packages;
						postNo++;
					}
					maps.Add(new ItemTriple<string, string, string>(path, mapname, newName));
				}
			}

			foreach (ItemTriple<string, string, string> item in maps)
			{
				//item.Item1 = Target Filename;
				//item.Item2 = Mapname;
				//item.Item3 = New Mapname;

#if DebugBackgroundWorker
				using (FileStream file = new FileStream(item.Item1, FileMode.Create))
				{
					this.dataArchiveManager.ExtractMap(file, item.Item2, item.Item3, mapExtractOptions, null);
				}
#else
				this.RunBackgroundWorker(
					(wSender, wE) =>
					{
						ListPair<BspMap.Lumps, byte[]> lumpReplacements = new ListPair<BspMap.Lumps, byte[]>();
						if (mapExtractOptions.EntitiesOptions == MapExtractOptions.Entities.Import)
						{
							string importEntityPath = Path.Combine(FilesystemEntries.ExtractedEntitiesDirectory, item.Item2 + FileExtensions.Xml);
							if (File.Exists(importEntityPath) == true)
							{
								MapEntity[] entities = FileIO.LoadMapEntities(importEntityPath);
								lumpReplacements.Add(BspMap.GetLumpReplacementEntities(entities));
							}
						}
						else if (mapExtractOptions.EntitiesOptions == MapExtractOptions.Entities.UseConversionFile)
						{
							List<MapEntity> entities = this.dataArchiveManager.GetMapEntities(item.Item2);
							EntityConversion entConversion = EntityConversion.Load<EntityConversion>(mapExtractOptions.ConversionFile);
							entities = entConversion.Convert(entities);
							lumpReplacements.Add(BspMap.GetLumpReplacementEntities(entities));
						}

						using (FileStream file = new FileStream(item.Item1, FileMode.Create))
						{
							this.dataArchiveManager.ExtractMap(file, item.Item2, item.Item3, mapExtractOptions, lumpReplacements);
						}
					},
					(wSender, wE) =>
					{
					});
#endif
			}
		}

		private void miToolsGearCalculator_Click(object sender, EventArgs e)
		{
			this.gearCalculatorForm.Show();
		}

		private void bPlay_Click(object sender, EventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count < 1)
			{
				MessageBox.Show("Select a Map to play.");
				return;
			}

			Settings settings = Settings.Load();

			string bin = Path.Combine(FilesystemEntries.GameDirectory, settings.NameGameBinary);
			
			if (File.Exists(bin) == false)
			{
				MessageBox.Show("Game Binary not found. Check your Settings.", "Error");
				return;
			}

			if (this.gameProcess != null && this.gameProcess.HasExited == false)
			{
				this.gameProcess.CloseMainWindow();
				this.gameProcess.WaitForExit(1000);
				if (this.gameProcess.HasExited == false)
					this.gameProcess.Kill();
				this.gameProcess.Close();
			}

			ProcessStartInfo psi = new ProcessStartInfo(bin);
			psi.Arguments = "+" + Constants.CombineCVars(Constants.CVarMap, (string)this.lbMaps.SelectedItems[0]);
			psi.WorkingDirectory = FilesystemEntries.GameDirectory;
			this.gameProcess = new Process();
			this.gameProcess.StartInfo = psi;
			this.gameProcess.Start();
		}

		private void bAddToMapcycle_Click(object sender, EventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count < 1)
			{
				MessageBox.Show("No Maps selected.");
				return;
			}

			string[] maps = this.lbMaps.GetSelectedItems();
			this.mapcycleForm.AddMapItems(maps);
			this.mapcycleForm.Show();
		}

		private void miToolsServerControl_Click(object sender, EventArgs e)
		{
			this.mapcycleForm.Show();
		}

		private void miToolsCreateArenaScripts_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog();
			sfd.AddExtension = true;
			sfd.FileName = "Arenascripts";
			sfd.DefaultExt = FileExtensions.Packages;
			sfd.Filter = "PK3 Archive|*.pk3";
			sfd.FilterIndex = 0;
			if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
				return;

#if DebugBackgroundWorker
			using (FileStream file = new FileStream(sfd.FileName, FileMode.Create))
			{
				this.dataArchiveManager.CreateArenaScripts(file);
			}
#else
			this.RunBackgroundWorker(
				(wSender, wE) =>
				{
					using (FileStream file = new FileStream(sfd.FileName, FileMode.Create))
					{
						this.dataArchiveManager.CreateArenaScripts(file);
					}
				},
				(wSender, wE) =>
				{
				});
#endif
		}

		private void tbSearch_TextChanged(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if (string.IsNullOrEmpty(tb.Text) == true)
			{
				this.lbMaps.Items.Clear();
				this.lbMaps.Items.AddRange(this.mapNames);
				return;
			}

			string searchText = tb.Text.ToLowerInvariant();
			this.lbMaps.Items.Clear();
			for (int i = 0; i < this.mapNames.Length; i++)
			{
				string mapname = this.mapNames[i].ToLowerInvariant();
				if (mapname.Contains(searchText) == true)
				{
					this.lbMaps.Items.Add(mapname);
				}
			}
		}

		private void miExtractMapEntities_Click(object sender, EventArgs e)
		{
			if (this.lbMaps.SelectedIndices.Count != 1)
			{
				MessageBox.Show("Select one map.");
				return;
			}

			string mapname = (string)this.lbMaps.SelectedItem;

			SaveFileDialog sfd = new SaveFileDialog();
			sfd.InitialDirectory = FilesystemEntries.ExtractedEntitiesDirectory;
			sfd.FileName = mapname + FileExtensions.Xml;
			sfd.Filter = "Xml File (*.xml)|*.xml";
			sfd.FilterIndex = 0;

			if (sfd.ShowDialog() != DialogResult.OK)
				return;

			Forms.ExtractMapEntitiesForm entForm = new Forms.ExtractMapEntitiesForm();
			if (entForm.ShowDialog() != DialogResult.OK)
				return;

			List<MapEntity> entities = this.dataArchiveManager.GetMapEntities(mapname);
			if (string.IsNullOrEmpty(entForm.EntityConversionFile) == false)
			{
				EntityConversion entConversion = EntityConversion.Load<EntityConversion>(entForm.EntityConversionFile);
				if (entConversion != null)
				{
					entities = entConversion.Convert(entities);
				}
			}
			FileIO.Save(sfd.FileName, entities);
		}
	}
}
