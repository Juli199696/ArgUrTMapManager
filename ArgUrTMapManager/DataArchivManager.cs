using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using Ionic.Zip;
using FreeImageAPI;
using ArgusLib.Collections;
using ArgusLib.Drawing;
using ArgusLib.Math;

namespace ArgUrTMapManager
{
	internal delegate void ReportProgressHandler(double Progress, string Text);

	class DataArchiveManager
	{
		List<string> packages;

		EncryptedStream EncryptedStream { get; set; }
		public event ReportProgressHandler ReportProgress;
		private ReportProgressHandler progressHandler;

		public DataArchiveManager()
		{
			this.packages = new List<string>();
			this.EncryptedStream = null;
			this.progressHandler = new ReportProgressHandler(this.OnReportProgress);
			this.IncludeFilesystemEntries = false;
		}

		public ImmutableList<string> Packages { get { return new ImmutableList<string>(this.packages); } }
		public bool IncludeFilesystemEntries { get; set; }

		public static DataArchiveManager LoadQuake3Archive(string GameDirectory)
		{
			DataArchiveManager dam = new DataArchiveManager();
			string[] dirs = Directory.GetDirectories(GameDirectory);
			dam.AddDataDirectories(dirs);

			string virtualstore = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "VirtualStore");
			string gameDir = GameDirectory.Substring(Path.GetPathRoot(GameDirectory).Length);
			gameDir = Path.Combine(virtualstore, gameDir);
			if (Directory.Exists(gameDir) == true)
			{
				dirs = Directory.GetDirectories(gameDir);
				dam.AddDataDirectories(dirs);
			}
			dam.EncryptedStream = new NotEncryptedStream();
			return dam;
		}

		public static DataArchiveManager LoadQuakeLiveArchive()
		{
			DataArchiveManager dam = new DataArchiveManager();
			dam.AddDataDirectory(FilesystemEntries.QuakeLiveDataDirectory);
			dam.EncryptedStream = new EncryptedXorStream(ResourceFiles.Resources.QuakeLiveXorKey);
			return dam;
		}

		private void OnReportProgress(double Progress, string Text)
		{
			if (this.ReportProgress != null)
				this.ReportProgress(Progress, Text);
		}

		public void AddDataDirectory(string DataDirectory)
		{
			string[] packageFiles = Directory.GetFiles(DataDirectory, "*" + FileExtensions.Packages);
			this.packages.AddRange(packageFiles);
		}

		public void AddDataDirectories(string[] DataDirectories)
		{
			foreach (string dir in DataDirectories)
			{
				this.AddDataDirectory(dir);
			}
		}

		private ZipEntryInfo GetEntry(string entryName)
		{
			if (this.IncludeFilesystemEntries == true)
			{
				string name = Path.GetFileName(entryName);
				string[] files = Directory.GetFiles(FilesystemEntries.GameDataDirectory, name, SearchOption.AllDirectories);
				if (files.Length > 0)
				{
					return new ZipEntryInfo(files[0], null);
				}
			}

			for (int i = 0; i < this.packages.Count; i++)
			{
				using (FileStream file = new FileStream(this.packages[i], FileMode.Open))
				{
					this.EncryptedStream.SetUnderlyingStream(file);
					using (ZipFile zip = ZipFile.Read(this.EncryptedStream))
					{
						ZipEntry entry = zip[entryName];
						if (entry != null)
						{
							return new ZipEntryInfo(this.packages[i], entry);
						}
					}
				}
			}
			return null;
		}

		private ZipEntryInfo SearchEntry(string SearchPattern, ReportProgressHandler reportProgress)
		{
			List<ZipEntryCollection> entries = this.GetEntries(SearchPattern, 1, reportProgress);
			if (entries.Count > 0)
				return entries[0][0];
			if (this.IncludeFilesystemEntries == true)
			{
				string[] file;
				try
				{
					file = Directory.GetFiles(FilesystemEntries.GameDirectory, SearchPattern, SearchOption.AllDirectories);
				}
				catch (Exception e)
				{
					return null;
				}
				if (file.Length > 0)
					return new ZipEntryInfo(file[0], null);
			}
			return null;
		}

		private List<ZipEntryCollection> GetEntries(string SearchPattern, ReportProgressHandler reportProgress)
		{
			return this.GetEntries(SearchPattern, 0, reportProgress);
		}

		private List<ZipEntryCollection> GetEntries(string SearchPattern, int MaxEntries, ReportProgressHandler reportProgress)
		{
			return this.GetEntries(new string[] { SearchPattern }, new int[] { MaxEntries }, reportProgress);
		}

		private List<ZipEntryCollection> GetEntries(ICollection<string> SearchPatterns, int MaxEntries, ReportProgressHandler reportProgress)
		{
			int[] maxEntries = new int[SearchPatterns.Count];
			for (int i = 0; i < maxEntries.Length; i++)
			{
				maxEntries[i] = MaxEntries;
			}
			return this.GetEntries(SearchPatterns, maxEntries, reportProgress);
		}

		private List<ZipEntryCollection> GetEntries(ICollection<string> SearchPatterns, int[] MaxEntries, ReportProgressHandler reportProgress)
		{
			if (SearchPatterns.Count != MaxEntries.Length)
				throw new ArgumentException("SearchPatterns and MaxEntries must contain the same number of elements.");

			Action<double> progressHandler = p =>
				{
					if (reportProgress != null)
						reportProgress(p, string.Empty);
				};

			int[] EntryCount = new int[SearchPatterns.Count];
			string[][] SearchParts = new string[SearchPatterns.Count][];
			List<string> lSearchPatterns = SearchPatterns as List<string>;
			if (lSearchPatterns == null)
				lSearchPatterns = new List<string>(SearchPatterns);

			for (int i = 0; i < EntryCount.Length; i++)
			{
				if (MaxEntries[i] < 1)
					MaxEntries[i] = int.MaxValue;
				EntryCount[i] = 0;
				SearchParts[i] = lSearchPatterns[i].ToLower().Split('*');
			}

			List<ZipEntryCollection> Entries = new List<ZipEntryCollection>();
			bool allFound = false;
			for (int i = 0; i < this.packages.Count; i++)
			{
				progressHandler((double)i / this.packages.Count);

				if (allFound == true)
					break;

				using (FileStream file = new FileStream(this.packages[i], FileMode.Open))
				{
					ZipEntryCollection zipEntryCollection = new ZipEntryCollection(this.packages[i]);
					this.EncryptedStream.SetUnderlyingStream(file);
					using (ZipFile zip = ZipFile.Read(this.EncryptedStream))
					{
						foreach (ZipEntry zipEntry in zip.Entries)
						{
							allFound = true;
							for (int j = 0; j < SearchPatterns.Count; j++)
							{
								if (EntryCount[j] >= MaxEntries[j])
									continue;

								allFound = false;

								string zipEntryName = zipEntry.FileName.ToLower();
								int index = zipEntryName.IndexOf(SearchParts[j][0]);
								if (index != 0)
									continue;
								for (int e = 1; e < SearchParts[j].Length-1; e++)
								{
									index = zipEntryName.IndexOf(SearchParts[j][e], index);
									if (index < 0)
										break;
								}
								if (index < 0)
									continue;
								if (zipEntryName.EndsWith(SearchParts[j][SearchParts[j].Length - 1]) == false)
									continue;

								zipEntryCollection.Entries.Add(zipEntry);
								EntryCount[j]++;
								break;
							}
						}
					}
					if (zipEntryCollection.Entries.Count > 0)
						Entries.Add(zipEntryCollection);
				}
			}
			progressHandler(1);
			return Entries;
		}

		private List<string> CheckIntegrity(ReportProgressHandler reportProgress)
		{
			List<string> corrupted = new List<string>();

			ReportProgressHandler rProgress = (p, t) =>
				{
					if (reportProgress != null)
						reportProgress(p, t);
				};

			DataArchiveCache cache = DataArchiveCache.Load();
			ListPair<string, DateTime> AlreadyChecked = new ListPair<string, DateTime>(cache.PackageFiles);
			bool updateCache = cache.FilesystemEntriesIncluded != this.IncludeFilesystemEntries;
			ListPair<string, DateTime> newChecked = new ListPair<string, DateTime>();

			for (int i = 0; i < this.packages.Count; i++)
			{
				rProgress((double)i / this.packages.Count, "Checking " + this.packages[i] + "...");

				int index = AlreadyChecked.List1.IndexOf(this.packages[i]);
				DateTime lastWriteTime = File.GetLastWriteTime(this.packages[i]);
				if (index > -1)
				{
					if (lastWriteTime == AlreadyChecked.List2[index])
					{
						newChecked.Add(new ItemPair<string, DateTime>(this.packages[i], lastWriteTime));
						continue;
					}
				}

				bool corrupt = false;
				updateCache = true;

				try
				{
					corrupt = ZipFile.CheckZip(this.packages[i]) == false;
				}
				catch (Ionic.Zip.ZipException exception)
				{
					corrupt = true;
				}

				if (corrupt == true)
				{
					corrupted.Add(this.packages[i]);
					this.packages.RemoveAt(i);
					i--;
					rProgress(-1, "File is corrupt.");
				}
				else
				{
					newChecked.Add(new ItemPair<string, DateTime>(this.packages[i], lastWriteTime));
				}
			}
			rProgress(1, "All files checked.");
			cache.PackageFiles = newChecked.ToArray();
			if (updateCache == true)
			{
				this.CreateArchivEntriesCache(cache, reportProgress);
			}

			rProgress(-1, "Save Checked-Packages-Cachefile.");
			cache.PackageFiles = newChecked.ToArray();
			cache.FilesystemEntriesIncluded = this.IncludeFilesystemEntries;
			cache.Save();
			rProgress(-1, "Operation completed.");
			return corrupted;
		}

		private void CreateArchivEntriesCache(DataArchiveCache cache, ReportProgressHandler reportProgress)
		{
			ListTriple<string, string, int> texturesCache = new ListTriple<string, string, int>();
			ListTriple<string, string, int> modelsCache = new ListTriple<string, string, int>();
			ListTriple<string, string, int> soundsCache = new ListTriple<string, string, int>();
			ListPair<string, int> shaderscriptsCache = new ListPair<string, int>();
			ListPair<string, int> shaderscriptEntriesCache = new ListPair<string, int>();
			ListPair<string, int> mapsCache = new ListPair<string, int>();

			ReportProgressHandler progressHandler;
			if (reportProgress != null)
				progressHandler = reportProgress;
			else
				progressHandler = (p, t) => { };

			for (int i = 0; i < cache.PackageFiles.Length; i++)
			{
				progressHandler((double)i / this.packages.Count, "Retrieve archive entry names...");
				ItemPair<string, DateTime> packageFile = cache.PackageFiles[i];
				using (FileStream file = new FileStream(packageFile.Item1, FileMode.Open))
				{
					this.EncryptedStream.SetUnderlyingStream(file);
					using (ZipFile zip = ZipFile.Read(this.EncryptedStream))
					{
						foreach (ZipEntry entry in zip.Entries)
						{
							string entryName = entry.FileName.ToLowerInvariant();

							if (entryName.EndsWith(FileExtensions.Maps) == true)
							{
								ItemPair<string, int> item = new ItemPair<string, int>(entryName, i);
								mapsCache.Add(item);
								continue;
							}
							if (entryName.EndsWith(FileExtensions.ShaderscriptFiles) == true)
							{
								ItemPair<string, int> item = new ItemPair<string, int>(entryName, i);
								shaderscriptsCache.Add(item);
								using (MemoryStream ms = new MemoryStream())
								{
									entry.Extract(ms);
									ms.Seek(0, SeekOrigin.Begin);
									List<string> shaderEntries = Shaderscript.GetEntryNames(ms);
									ms.Dispose();
									int index = shaderscriptsCache.Count - 1;
									for (int j = 0; j < shaderEntries.Count; j++)
									{
										shaderscriptEntriesCache.Add(new ItemPair<string, int>(shaderEntries[j], index));
									}
								}
								continue;
							}

							bool contin = false;

							foreach (string texExt in FileExtensions.Textures)
							{
								if (entryName.EndsWith(texExt) == true)
								{
									ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
									item.Item1 = entryName.Substring(0, entryName.Length - texExt.Length);
									item.Item2 = texExt;
									item.Item3 = i;
									texturesCache.Add(item);
									contin = true;
									break;
								}
							}

							if (contin == true)
								continue;

							foreach (string soundExt in FileExtensions.Sounds)
							{
								if (entryName.EndsWith(soundExt) == true)
								{
									ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
									item.Item1 = entryName.Substring(0, entryName.Length - soundExt.Length);
									item.Item2 = soundExt;
									item.Item3 = i;
									soundsCache.Add(item);
									contin = true;
									break;
								}
							}

							if (contin == true)
								continue;

							foreach (string modelExt in FileExtensions.Models)
							{
								if (entryName.EndsWith(modelExt) == true)
								{
									ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
									item.Item1 = entryName.Substring(0, entryName.Length - modelExt.Length);
									item.Item2 = modelExt;
									item.Item3 = i;
									modelsCache.Add(item);
									contin = true;
									break;
								}
							}
						}
					}
				}
			}

			reportProgress(1, "Retrieve archive entry names...");

			if (this.IncludeFilesystemEntries == true)
			{
				reportProgress(-1, "Retrieve filesystem entry names...");

				string gameDir = Path.GetFullPath(FilesystemEntries.GameDirectory);

				reportProgress(-1, "Retrieve Map-Files...");
				string[] files = Directory.GetFiles(gameDir, "*" + FileExtensions.Maps, SearchOption.AllDirectories);
				foreach (string file in files)
				{
					mapsCache.Add(new ItemPair<string, int>(file.Substring(gameDir.Length).ToLowerInvariant(), -1));
				}

				reportProgress(-1, "Retrieve Model-Files...");
				foreach (string ext in FileExtensions.Models)
				{
					files = Directory.GetFiles(gameDir, "*" + ext, SearchOption.AllDirectories);
					foreach (string file in files)
					{
						ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
						item.Item1 = file.Substring(gameDir.Length, file.Length - gameDir.Length - ext.Length).ToLowerInvariant();
						item.Item2 = ext;
						item.Item3 = -1;
						modelsCache.Add(item);
					}
				}

				reportProgress(-1, "Retrieve Texture-Files...");
				foreach (string ext in FileExtensions.Textures)
				{
					files = Directory.GetFiles(gameDir, "*" + ext, SearchOption.AllDirectories);
					foreach (string file in files)
					{
						ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
						item.Item1 = file.Substring(gameDir.Length, file.Length - gameDir.Length - ext.Length).ToLowerInvariant();
						item.Item2 = ext;
						item.Item3 = -1;
						texturesCache.Add(item);
					}
				}

				reportProgress(-1, "Retrieve Sound-Files...");
				foreach (string ext in FileExtensions.Sounds)
				{
					files = Directory.GetFiles(gameDir, "*" + ext, SearchOption.AllDirectories);
					foreach (string file in files)
					{
						ItemTriple<string, string, int> item = new ItemTriple<string, string, int>();
						item.Item1 = file.Substring(gameDir.Length, file.Length - gameDir.Length - ext.Length).ToLowerInvariant();
						item.Item2 = ext;
						item.Item3 = -1;
						soundsCache.Add(item);
					}
				}

				reportProgress(-1, "Retrieve Shaderscript-Files...");
				files = Directory.GetFiles(gameDir, "*" + FileExtensions.ShaderscriptFiles, SearchOption.AllDirectories);
				foreach (string file in files)
				{
					shaderscriptsCache.Add(new ItemPair<string, int>(file.Substring(gameDir.Length).ToLowerInvariant(), -1));
					using (FileStream stream = new FileStream(file, FileMode.Open))
					{
						List<string> shaderEntries = Shaderscript.GetEntryNames(stream);
						for (int j = 0; j < shaderEntries.Count; j++)
						{
							shaderscriptEntriesCache.Add(new ItemPair<string, int>(shaderEntries[j], -1));
						}
					}
				}
			}

			reportProgress(-1, "Saving Maps-Cachefile...");
			DataArchiveCache.MapsCache maps = new DataArchiveCache.MapsCache() { Entries = mapsCache.ToArray() };
			mapsCache = null;
			maps.Save();
			maps = null;

			reportProgress(-1, "Saving Models-Cachefile...");
			DataArchiveCache.ModelsCache models = new DataArchiveCache.ModelsCache() { Entries = modelsCache.ToArray() };
			modelsCache = null;
			models.Save();
			models = null;

			reportProgress(-1, "Saving Textures-Cachefile...");
			DataArchiveCache.TexturesCache textures = new DataArchiveCache.TexturesCache() { Entries = texturesCache.ToArray() };
			texturesCache = null;
			textures.Save();
			textures = null;

			reportProgress(-1, "Saving Sounds-Cachefile...");
			DataArchiveCache.SoundsCache sounds = new DataArchiveCache.SoundsCache() { Entries = soundsCache.ToArray() };
			soundsCache = null;
			sounds.Save();
			sounds = null;

			reportProgress(-1, "Saving Shaderscripts-Cachefile...");
			DataArchiveCache.ShaderscriptsCache shaders = new DataArchiveCache.ShaderscriptsCache() { Entries = shaderscriptsCache.ToArray() };
			shaderscriptsCache = null;
			shaders.Save();
			shaders = null;

			reportProgress(-1, "Saving Shadersciptentries-Cachefile...");
			DataArchiveCache.ShaderscriptEntriesCache shaderscriptEntries = new DataArchiveCache.ShaderscriptEntriesCache() { Entries = shaderscriptEntriesCache.ToArray() };
			shaderscriptEntriesCache = null;
			shaderscriptEntries.Save();
			shaderscriptEntries = null;

			reportProgress(-1, "Cache updated.");
		}

		public List<string> CheckIntegrity()
		{
			return this.CheckIntegrity(this.progressHandler);
		}

		public string[] GetMapNames(ReportProgressHandler reportProgress)
		{
			ReportProgressHandler progressHandler = (p, text) =>
				{
					if (reportProgress != null)
						reportProgress(p, text);
				};

			progressHandler(-1, "Loading Maps-Cachefile...");
			DataArchiveCache.MapsCache mapsCache = DataArchiveCache.MapsCache.Load();

			progressHandler(-1, "Get map names...");
			string[] mapNames = new string[mapsCache.Entries.Length];
			for (int i = 0; i < mapNames.Length; i++)
			{
				mapNames[i] = Path.GetFileNameWithoutExtension(mapsCache.Entries[i].Item1);
			}
			progressHandler(-1, "Operation completed.");
			return mapNames;
		}

		public string[] GetMapNames()
		{
			return this.GetMapNames(this.progressHandler);
		}

		public void CopyArchive(string TargetDirectory, ReportProgressHandler reportProgress)
		{
			if (Directory.Exists(TargetDirectory) == false)
				throw new ArgumentException(TargetDirectory + " does not exist");


			for (int i = 0; i < this.packages.Count; i++)
			{
				string filename = Path.GetFileName(this.packages[i]);
				if (reportProgress != null)
					reportProgress((double)i / this.packages.Count, "Copying "+filename+"...");
				using (FileStream source = new FileStream(this.packages[i], FileMode.Open))
				{
					this.EncryptedStream.SetUnderlyingStream(source);
					string newFile = Path.Combine(TargetDirectory, filename);
					using (FileStream target = new FileStream(newFile, FileMode.Create))
					{
						target.ReadFromStream(this.EncryptedStream, 1024);
					}
				}
			}
			if (reportProgress != null)
				reportProgress(1, "Finished");
		}

		public void CopyArchive(string TargetDirectory)
		{
			this.CopyArchive(TargetDirectory, this.progressHandler);
		}

		public Image GetLevelshotImage(string Mapname)
		{
#if Mono
			return null;
#else
			string SearchPattern = Constants.ZipDirLevelshots + Mapname +".*";
			ZipEntryInfo levelshotEntry = this.SearchEntry(SearchPattern, null);
			if (levelshotEntry == null)
				return null;

			if (levelshotEntry.Entry == null)
			{
				using (FreeImageBitmap fim = new FreeImageBitmap(levelshotEntry.SourceFile))
				{
					return fim.ToBitmap();
				}
			}

			using (FileStream file = new FileStream(levelshotEntry.SourceFile, FileMode.Open))
			{
				this.EncryptedStream.SetUnderlyingStream(file);
				MemoryStream ms = new MemoryStream();
				levelshotEntry.Entry.Extract(ms);
				ms.Seek(0, SeekOrigin.Begin);
				using (FreeImageBitmap fim = new FreeImageBitmap(ms))
				{
					return fim.ToBitmap();
				}
			}
#endif
		}

#if false
		public void ExtractMap(string Mapname, Stream targetStream, MapExtractOptions mapExtractOptions, ReportProgressHandler reportProgress)
		{
			string tempFolder = this.GetTempFolder();
			string progressText = "Searching Map "+Mapname+"...";
			bool useProgressText = true;
			ReportProgressHandler progressHandler = (progress, Text) =>
				{
					if (reportProgress != null)
					{
						if (useProgressText == true)
							reportProgress(progress, progressText);
						else
							reportProgress(progress, Text);
					}
				};
			ZipEntryInfo bspMapEntry = this.GetEntry(Constants.ZipDirMaps + Mapname + FileExtensions.Maps, progressHandler);
			if (bspMapEntry == null)
				throw new ArgumentException("Map does not exist: " + Mapname, "Mapname");

			useProgressText = false;
			progressHandler(-1, "Extract " + bspMapEntry.Entry.FileName + "...");
			this.ExtractZipEntry(bspMapEntry, tempFolder);
			progressHandler(-1, "Extraction complete");

			Directory.Delete(tempFolder, true);
		}
#endif
		public bool ExtractMap(Stream target, string Mapname, string NewMapname, MapExtractOptions mapExtractOptions, ListPair<BspMap.Lumps, byte[]> BspLumpReplacements)
		{
			return this.ExtractMap(target, Mapname, NewMapname, mapExtractOptions, BspLumpReplacements, this.progressHandler);
		}

		public bool ExtractMap(Stream target, string Mapname, string NewMapname, MapExtractOptions mapExtractOptions, ListPair<BspMap.Lumps, byte[]> BspLumpReplacements, ReportProgressHandler progressHandler)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);

			ZipEntryInfo bspEntry = this.GetEntry(Constants.ZipDirMaps + Mapname + FileExtensions.Maps);

			if (bspEntry == null)
			{
				reportProgress(-1, "Map not found.");
				return false;
			}

			if (BspLumpReplacements == null)
				BspLumpReplacements = new ListPair<BspMap.Lumps, byte[]>();

			string tempDir = this.GetTempFolder();
			string bspFilePath;
			//string tempBspMap = Path.GetTempFileName();

			ZipEntryInfo arenascriptEntry = this.GetEntry(Constants.ZipDirScripts + Mapname + FileExtensions.ArenascriptFiles);
			if (arenascriptEntry != null)
			{
				reportProgress(-1, "Extract " + arenascriptEntry.Entry.FileName + "...");
				string path = Path.Combine(tempDir, Constants.ZipDirScripts + NewMapname + FileExtensions.ArenascriptFiles);
				DataArchiveManager.EnsureDirectoryExists(path);
				using (FileStream file = new FileStream(path, FileMode.Create))
				{
					this.ExtractZipEntry(arenascriptEntry, file);
				}
			}

			using (MemoryStream ms = new MemoryStream())
			{
				this.ExtractZipEntry(bspEntry, ms);
				ms.Seek(0, SeekOrigin.Begin);

				BspMap bspMap = BspMap.Open(ms);

				if (mapExtractOptions.SurfaceParms != SurfaceParms.None)
					BspLumpReplacements.Add(bspMap.GetLumpReplacementApplySurfaceParm(mapExtractOptions.SurfaceParms));

				int version = (int)mapExtractOptions.BspVersionOptions;
				if (mapExtractOptions.BspVersionOptions == MapExtractOptions.BspVersion.Copy)
					version = bspMap.BspVersion;

				bspFilePath = Path.Combine(tempDir, Constants.ZipDirMaps + NewMapname + FileExtensions.Maps).Replace('/', '\\');
				DataArchiveManager.EnsureDirectoryExists(bspFilePath);

				using (FileStream file = new FileStream(bspFilePath, FileMode.Create))
				{
					reportProgress(-1, "Extract " + bspEntry.Entry.FileName + "...");
					bspMap.Save(file, version, BspLumpReplacements);
				}

				if (arenascriptEntry == null)
				{
					using (FileStream file = new FileStream(bspFilePath, FileMode.Open))
					{
						file.Seek(0, SeekOrigin.Begin);
						bspMap = BspMap.Open(file);
						List<GameTypes> gameTypes = bspMap.GetSupportedGametypes();
						file.Close();
						Arenascript arenascript = new Arenascript();
						arenascript.map = NewMapname;
						arenascript.longname = NewMapname;
						arenascript.type = gameTypes.ToArray();
						string arenapath = Path.Combine(tempDir, Constants.ZipDirScripts);
						if (Directory.Exists(arenapath) == false)
							Directory.CreateDirectory(arenapath);
						arenapath = Path.Combine(arenapath, NewMapname + FileExtensions.ArenascriptFiles);
						using (FileStream arenaFile = new FileStream(arenapath, FileMode.Create))
						{
							arenascript.Save(arenaFile);
						}
					}
				}

#if DEBUG
				{
					List<MapEntity> entities;
					using (FileStream file = new FileStream(bspFilePath, FileMode.Open))
					{
						bspMap = BspMap.Open(file);
						entities = bspMap.GetEntities();
					}
					string spawnpointMapPath = Path.Combine(tempDir, Constants.ZipDirMaps + NewMapname + "_Spawnpoints.png");
					using (Bitmap bitmap = DataArchiveManager.DrawSpawnpointMap(entities))
					{
						bitmap.Save(spawnpointMapPath, System.Drawing.Imaging.ImageFormat.Png);
					}
				}
#endif
			}

			ZipEntryInfo aasEntry = null;
			if (mapExtractOptions.ExtractBotfile == true)
			{
				aasEntry = this.GetEntry(Constants.ZipDirMaps + Mapname + FileExtensions.BotMaps);
				if (aasEntry != null)
				{
					reportProgress(-1, "Extract " + aasEntry.Entry.FileName + "...");
					string path = Path.Combine(tempDir, Constants.ZipDirMaps + NewMapname + FileExtensions.BotMaps);
					DataArchiveManager.EnsureDirectoryExists(path);
					using (FileStream file = new FileStream(path, FileMode.Create))
					{
						this.ExtractZipEntry(aasEntry, file);
					}
					aasEntry = null;
				}
			}

			//Process bspcProcess = null;
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			DateTime bspcProcessStart = DateTime.Now;
			ArgusLib.Controls.ProcessForm processForm = null;
			if (File.Exists(mapExtractOptions.AasCompilerFile) == true)
			{
				if (mapExtractOptions.CompileAasCondition == MapExtractOptions._CompileAasCondition.Always ||
					(mapExtractOptions.CompileAasCondition == MapExtractOptions._CompileAasCondition.IfAasIsMissing &&
					aasEntry == null))
				{
					Thread thread = new Thread(new ThreadStart(() =>
						{
							processStartInfo.CreateNoWindow = true;
							processStartInfo.FileName = mapExtractOptions.AasCompilerFile;
							processStartInfo.WorkingDirectory = FilesystemEntries.LibsDirectory;
							processStartInfo.Arguments = '-' + MapExtractOptions.BspcSwitch.threads.ToString() + ' ' + mapExtractOptions.CompileAasThreads.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.optimize) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.optimize.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.noverbose) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.noverbose.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.breadthfirst) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.breadthfirst.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.nobrushmerge) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.nobrushmerge.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.noliquids) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.noliquids.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.nocsg) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.nocsg.ToString();
							if (mapExtractOptions.BspcSwitches.HasFlag(MapExtractOptions.BspcSwitch.forcesidesvisible) == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.forcesidesvisible.ToString();

							string args = processStartInfo.Arguments;

							if (mapExtractOptions.CompileAasTryReachFirst == true)
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.reach.ToString();
							else
								processStartInfo.Arguments += " -" + MapExtractOptions.BspcSwitch.bsp2aas.ToString();
							processStartInfo.Arguments += " \"" + bspFilePath + '\"';

							DateTime aasLastWriteTime = new DateTime(0);
							string aasPath = Path.Combine(tempDir, Constants.ZipDirMaps + NewMapname + FileExtensions.BotMaps).Replace('/', '\\');
							if (File.Exists(aasPath) == true)
							{
								aasLastWriteTime = File.GetLastWriteTime(aasPath);
							}

							//bspcProcess = new Process();
							//bspcProcess.StartInfo = processStartInfo;
							//bspcProcess.EnableRaisingEvents = true;
							processForm = new ArgusLib.Controls.ProcessForm(processStartInfo);
							if (mapExtractOptions.CompileAasTryReachFirst == true)
							{
								processForm.ProcessExited += (sender, e) =>
									{
										if (File.Exists(aasPath) == false || aasLastWriteTime == File.GetLastWriteTime(aasPath))
										{
											processStartInfo.Arguments = args + " -" + MapExtractOptions.BspcSwitch.bsp2aas.ToString() + " \"" + bspFilePath + '\"';
											//bspcProcess = new Process();
											//bspcProcess.StartInfo = processStartInfo;
											//bspcProcess.Start();
											processForm = new ArgusLib.Controls.ProcessForm(processStartInfo);
											processForm.Start(true);
										}
									};
							}
							//bspcProcess.Start();
							//bspcProcessStart = DateTime.Now;
							processForm.Start(true);
						}));
					thread.Start();
					//System.Threading.ThreadPool.QueueUserWorkItem(waitCallback);
				}
			}

			this.ExtractMapAssets(Mapname, NewMapname, bspFilePath, tempDir, mapExtractOptions, progressHandler);

			if (processForm != null && processForm.ProcessHasExited == false)
			{
				while (true)
				{
					TimeSpan elapsed = DateTime.Now - bspcProcessStart;
					reportProgress(-1, "Waiting for Botmap-compilation to finish... Elapsed time: " + elapsed.ToString());
					if (processForm.WaitForExit(500) == true)
						break;
				}
			}

			{
				string readMePath = Path.Combine(tempDir, mapExtractOptions.ReadMeFilename);
				using (FileStream file = new FileStream(readMePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					StreamReader reader = new StreamReader(file);
					string content = reader.ReadToEnd();
					file.Seek(0, SeekOrigin.Begin);
					StreamWriter writer = new StreamWriter(file);
					writer.Write(mapExtractOptions.ReadMeText.Replace("\n", Environment.NewLine));
					writer.WriteLine();
					writer.WriteLine();
					writer.Write(content);
					writer.Flush();
				}
			}

			using (ZipFile zip = new ZipFile())
			{
				zip.AddDirectory(tempDir);
				zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
				zip.CompressionMethod = CompressionMethod.Deflate;
				reportProgress(0, "Create Package...");
				zip.SaveProgress += (saveSender, saveArgs) =>
					{
						double progress = (double)saveArgs.EntriesSaved / saveArgs.EntriesTotal;
						if (progress >= 0)
							reportProgress(progress, "Create Package...");
					};
				zip.Save(target);
			}

			Directory.Delete(tempDir, true);

			reportProgress(-1, "Map successfully extracted.");
			return true;
		}

		private void ExtractMapAssets(string oldMapname, string newMapname, string tempBspMap, string TempDir, MapExtractOptions mapExtractOptions, ReportProgressHandler progressHandler)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			using (DataArchiveCache cache = DataArchiveCache.Load())
			{
				reportProgress(-1, "Extract Map Assets");
				this.ExtractMapAssetsTextures(oldMapname, newMapname, tempBspMap, TempDir, mapExtractOptions, progressHandler, cache);
				this.ExtractMapAssetsSounds(newMapname, tempBspMap, TempDir, mapExtractOptions, progressHandler, cache);
				this.ExtractMapAssetsModels(newMapname, tempBspMap, TempDir, mapExtractOptions, progressHandler, cache);
			}
		}

		private void ExtractMapAssetsTextures(string oldMapname, string newMapname, string tempBspMap, string TempDir, MapExtractOptions mapExtractOptions, ReportProgressHandler progressHandler, DataArchiveCache cache)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			List<string> textureNames;
			List<string> levelshotNames = new List<string>();
			using (FileStream file = new FileStream(tempBspMap, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				BspMap bspMap = BspMap.Open(file);
				textureNames = new List<string>(bspMap.GetTextureNames());
			}

			List<ShaderscriptEntry> shaderscriptEntries = new List<ShaderscriptEntry>();
			List<ShaderscriptEntry> lsShaderscriptEntries = new List<ShaderscriptEntry>();

			// Limit number of local variables.
			{
				string levelshotName = Constants.ZipDirLevelshots + oldMapname;
				ItemPair<string, int> lsShaderEntry = new ItemPair<string, int>(null, 0);
				ListPair<List<string>, int> shaderEntries = new ListPair<List<string>, int>();
				using (DataArchiveCache.ShaderscriptEntriesCache shaderscriptEntriesCache = DataArchiveCache.ShaderscriptEntriesCache.Load())
				{
					int currentIndex = -2;
					List<string> shaders = new List<string>();
					int startCount = textureNames.Count;
					foreach (ItemPair<string, int> item in shaderscriptEntriesCache.Entries)
					{
						if (lsShaderEntry.Item1 == null)
						{
							if (item.Item1 == levelshotName)
							{
								lsShaderEntry = item;
								continue;
							}
						}

						int index = textureNames.IndexOf(item.Item1);
						if (index < 0)
							continue;

						reportProgress((double)(startCount-textureNames.Count) / startCount, "Get shaderscripts...");

						if (item.Item2 == currentIndex)
						{
							shaders.Add(item.Item1);
						}
						else
						{
							if (shaders.Count > 0)
								shaderEntries.Add(new ItemPair<List<string>, int>(shaders, currentIndex));
							shaders = new List<string>();
							shaders.Add(item.Item1);
							currentIndex = item.Item2;
						}
						textureNames.RemoveAt(index);
						if (textureNames.Count < 1)
							break;
					}
					if (shaders.Count > 0)
						shaderEntries.Add(new ItemPair<List<string>, int>(shaders, currentIndex));

					reportProgress(1, "Get Shaderscript-Files...");
				}

				using (DataArchiveCache.ShaderscriptsCache shaderscriptCache = DataArchiveCache.ShaderscriptsCache.Load())
				{
					for (int i = 0; i < shaderEntries.Count; i++)
					{
						reportProgress((double)i / shaderEntries.Count, "Get Shaderscripts...");
						ItemPair<List<string>, int> item = shaderEntries[i];
						ItemPair<string, int> shaderscriptArchiveEntry = shaderscriptCache.Entries[item.Item2];
						string package = cache.PackageFiles[shaderscriptArchiveEntry.Item2].Item1;
						using (ZipFile zip = ZipFile.Read(package))
						{
							ZipEntry entry = zip[shaderscriptArchiveEntry.Item1];
							if (entry == null)
								continue;
							using (Stream stream = entry.OpenReader())
							{
								shaderscriptEntries.AddRange(Shaderscript.ReadEntries(stream, item.Item1));
							}
						}
					}
					if (lsShaderEntry.Item1 != null)
					{
						ItemPair<string, int> lsShaderscriptEntry = shaderscriptCache.Entries[lsShaderEntry.Item2];
						string package = cache.PackageFiles[lsShaderscriptEntry.Item2].Item1;
						using (ZipFile zip = ZipFile.Read(package))
						{
							ZipEntry entry = zip[lsShaderscriptEntry.Item1];
							if (entry != null)
							{
								using (Stream stream = entry.OpenReader())
								{
									lsShaderscriptEntries = new List<ShaderscriptEntry>(Shaderscript.ReadEntries(stream, new string[] { levelshotName }));
									if (lsShaderscriptEntries.Count > 0)
									{
										ShaderscriptEntry temp = lsShaderscriptEntries[0];
										temp.Name = Constants.ZipDirLevelshots + newMapname;
										lsShaderscriptEntries[0] = temp;
									}
									levelshotNames = new List<string>(Shaderscript.GetReferencedTextures(lsShaderscriptEntries));
								}
							}
						}
					}
					else
					{
						levelshotNames = new List<string>();
						levelshotNames.Add(levelshotName);
					}
					reportProgress(1, "Get Shaderscripts...");
				}
			}

			reportProgress(-1, "Get Texture-files...");
			textureNames.AddRange(Shaderscript.GetReferencedTextures(shaderscriptEntries));

			{
				reportProgress(-1, "Write Shaderscripts...");
				string dirPath = Path.Combine(TempDir, Constants.ZipDirScripts);
				string path = Path.Combine(dirPath, newMapname + FileExtensions.ShaderscriptFiles);
				if (Directory.Exists(dirPath) == false)
					Directory.CreateDirectory(dirPath);
				using (FileStream file = new FileStream(path, FileMode.Create))
				{
					shaderscriptEntries.AddRange(lsShaderscriptEntries);
					Shaderscript.WriteEntries(file, shaderscriptEntries);
				}
				shaderscriptEntries = null;
			}

			using (DataArchiveCache.TexturesCache texturesCache = DataArchiveCache.TexturesCache.Load())
			{
				ListTriple<string,string,int> texCache = new ListTriple<string,string,int>(texturesCache.Entries);
				texturesCache.Dispose();

				textureNames.AddRange(levelshotNames);
				textureNames.Add(Constants.ZipDirMaps + oldMapname);

				reportProgress(0, "Extract Textures...");
				for (int i = 0; i < textureNames.Count; i++)
				{
					string tex = textureNames[i];
					reportProgress((double)i / textureNames.Count, "Extract " + tex + "...");
					int index = texCache.List1.IndexOf(tex);
					if (index < 0)
						continue;

					ItemTriple<string,string,int> item = texCache[index];

					string name = item.Item1+item.Item2;
					string path;
					if (item.Item1 == Constants.ZipDirLevelshots + oldMapname)
						path = Path.Combine(TempDir, Constants.ZipDirLevelshots + newMapname);
					else if (item.Item1 == Constants.ZipDirMaps + oldMapname)
						path = Path.Combine(TempDir, Constants.ZipDirMaps + newMapname);
					else
					path = Path.Combine(TempDir, item.Item1);
					DataArchiveManager.EnsureDirectoryExists(path);

					string package = cache.PackageFiles[item.Item3].Item1;
					FREE_IMAGE_FORMAT imageFormat = FREE_IMAGE_FORMAT.FIF_UNKNOWN;
					FREE_IMAGE_SAVE_FLAGS imageFlags = FREE_IMAGE_SAVE_FLAGS.DEFAULT;

					bool isLevelshot = levelshotNames.Remove(item.Item1);

					if (mapExtractOptions.ImagesOptions == MapExtractOptions.Images.ConvertPngToTga && item.Item2 == FileExtensions.Textures[1])
					{
						path += FileExtensions.Textures[2];
						imageFormat = FREE_IMAGE_FORMAT.FIF_TARGA;
					}
					else if (mapExtractOptions.ImagesOptions == MapExtractOptions.Images.ConvertTgaToPng && item.Item2 == FileExtensions.Textures[2])
					{
						path += FileExtensions.Textures[1];
						imageFormat = FREE_IMAGE_FORMAT.FIF_PNG;
						imageFlags = FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_COMPRESSION;
					}
					else
					{
						path += item.Item2;
						if (isLevelshot == false)
						{
							using (FileStream file = new FileStream(path, FileMode.Create))
							{
								this.ExtractZipEntry(package, name, file);
							}
							continue;
						}
					}

					using (MemoryStream ms = new MemoryStream())
					{
						this.ExtractZipEntry(package, name, ms);
						ms.Seek(0, SeekOrigin.Begin);
						using (FreeImageBitmap fim = new FreeImageBitmap(ms))
						{
							if (imageFormat == FREE_IMAGE_FORMAT.FIF_UNKNOWN)
								imageFormat = fim.ImageFormat;

							if (isLevelshot == true)
							{
								Bitmap bitmap = fim.ToBitmap();
								fim.Dispose();
								DataArchiveManager.AddWatermark(bitmap);
								if (lsShaderscriptEntries.Count < 1)
								{
									path = Path.Combine(TempDir, Constants.ZipDirLevelshots + newMapname + ".jpg");
									DataArchiveManager.EnsureDirectoryExists(path);
								}
								using (FreeImageBitmap fim2 = new FreeImageBitmap(bitmap))
								{
									fim2.Save(path, imageFormat, imageFlags);
									continue;
								}
							}

							fim.Save(path, imageFormat, imageFlags);
							continue;
						}
					}
				}
			}

			if (levelshotNames.Count > 0)
			{
				Bitmap bitmap = new Bitmap(1024, 768);
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					g.Clear(Color.Black);
				}
				DataArchiveManager.AddWatermark(bitmap);
				using (FreeImageBitmap fim = new FreeImageBitmap(bitmap))
				{
					if (lsShaderscriptEntries.Count < 1)
					{
						string path = Path.Combine(TempDir, Constants.ZipDirLevelshots + newMapname + ".jpg");
						DataArchiveManager.EnsureDirectoryExists(path);
						fim.Save(path, FREE_IMAGE_FORMAT.FIF_JPEG, FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB);
					}
					else
					{
						foreach (string s in levelshotNames)
						{
							string path = Path.Combine(TempDir, s + ".jpg");
							DataArchiveManager.EnsureDirectoryExists(path);
							fim.Save(path, FREE_IMAGE_FORMAT.FIF_JPEG, FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB);
						}
					}
				}
			}

			reportProgress(1, "Extract Textures...");
		}

		private void ExtractMapAssetsSounds(string newMapname, string tempBspMap, string TempDir, MapExtractOptions mapExtractOptions, ReportProgressHandler progressHandler, DataArchiveCache cache)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			List<string> soundNames;
			using (FileStream file = new FileStream(tempBspMap, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				BspMap bspMap = BspMap.Open(file);
				soundNames = bspMap.GetReferencedSounds();
			}

			Action<string> writeEmptyWav = (filename) =>
				{
					using (FileStream file = new FileStream(filename, FileMode.Create))
					{
						using (UnmanagedMemoryStream ms = ResourceFiles.Resources.EmptyWav)
						{
							ms.WriteToStream(file, 8 * 1024);
						}
					}
				};

			using (DataArchiveCache.SoundsCache soundsCacheTemp = DataArchiveCache.SoundsCache.Load())
			{
				ListTriple<string, string, int> soundsCache = new ListTriple<string, string, int>(soundsCacheTemp.Entries);
				soundsCacheTemp.Dispose();
				reportProgress(0, "Extract Sounds...");
				for (int i = 0; i < soundNames.Count; i++)
				{
					string sound = soundNames[i];
					if (sound.StartsWith("*") == true)
						continue;
					if (sound.StartsWith("/") == true)
						sound = sound.Substring(1);
					reportProgress((double)i / soundNames.Count, "Extract " + sound + "...");
					string name = sound.Substring(0, sound.Length - Path.GetExtension(sound).Length);
					string path = Path.Combine(TempDir, name);
					DataArchiveManager.EnsureDirectoryExists(path);

					if (mapExtractOptions.SoundsOptions == MapExtractOptions.Sounds.RemoveAll)
					{
						path += FileExtensions.Sounds[0];
						writeEmptyWav(path);
						continue;
					}
				
					int index = soundsCache.List1.IndexOf(name);
					if (index < 0)
					{
						path += FileExtensions.Sounds[0];
						writeEmptyWav(path);
						continue;
					}

					ItemTriple<string, string, int> item = soundsCache[index];
					if (mapExtractOptions.SoundsOptions == MapExtractOptions.Sounds.RemoveOgg && item.Item2 == FileExtensions.Sounds[1])
					{
						path += FileExtensions.Sounds[0];
						writeEmptyWav(path);
						continue;
					}

					name = item.Item1 + item.Item2;
					string package = cache.PackageFiles[item.Item3].Item1;
					path = Path.Combine(TempDir, name);
					using (FileStream file = new FileStream(path, FileMode.Create))
					{
						this.ExtractZipEntry(package, name, file);
					}
				}
			}
			reportProgress(1, "Extract Sounds...");
		}

		private void ExtractMapAssetsModels(string newMapname, string tempBspMap, string TempDir, MapExtractOptions mapExtractOptions, ReportProgressHandler progressHandler, DataArchiveCache cache)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			List<string> modelNames;
			using (FileStream file = new FileStream(tempBspMap, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				BspMap bspMap = BspMap.Open(file);
				modelNames = bspMap.GetReferencedModels();
			}

			using (DataArchiveCache.ModelsCache modelsCacheTemp = DataArchiveCache.ModelsCache.Load())
			{
				ListTriple<string, string, int> modelsCache = new ListTriple<string, string, int>(modelsCacheTemp.Entries);
				modelsCacheTemp.Dispose();

				reportProgress(0, "Extract Models...");

				for (int i = 0; i < modelNames.Count; i++)
				{
					string model = modelNames[i];
					if (model.StartsWith("*") == true)
						continue;
					if (model.StartsWith("/") == true)
						model = model.Substring(1);
					reportProgress((double)i / modelNames.Count, "Extract " + model + "...");
					string name = model.Substring(0, model.Length - Path.GetExtension(model).Length);
					string path = Path.Combine(TempDir, name);
					string dirPath = Path.GetDirectoryName(path);
					if (Directory.Exists(dirPath) == false)
						Directory.CreateDirectory(dirPath);

					int index = modelsCache.List1.IndexOf(name);
					if (index < 0)
						continue;

					ItemTriple<string, string, int> item = modelsCache[index];
					name = item.Item1 + item.Item2;
					string package = cache.PackageFiles[item.Item3].Item1;
					path = Path.Combine(TempDir, name);
					using (FileStream file = new FileStream(path, FileMode.Create))
					{
						this.ExtractZipEntry(package, name, file);
					}
				}
			}
			reportProgress(1, "Extract Models...");
		}

		private void ExtractZipEntry(ZipEntryInfo zipEntryInfo, Stream Target)
		{
			using (FileStream file = zipEntryInfo.OpenFile())
			{
				this.EncryptedStream.SetUnderlyingStream(file);
				zipEntryInfo.Entry.Extract(Target);
			}
		}

		private bool ExtractZipEntry(string package, string entryName, Stream target)
		{
			using (FileStream file = new FileStream(package, FileMode.Open))
			{
				this.EncryptedStream.SetUnderlyingStream(file);
				using (ZipFile zip = ZipFile.Read(this.EncryptedStream))
				{
					ZipEntry entry = zip[entryName];
					if (entry == null)
						return false;
					entry.Extract(target);
				}
			}
			return true;
		}

		private void ExtractZipEntry(ZipEntryInfo zipEntryInfo, string BaseDirectory)
		{
			using (FileStream file = zipEntryInfo.OpenFile())
			{
				this.EncryptedStream.SetUnderlyingStream(file);
				zipEntryInfo.Entry.Extract(BaseDirectory);
			}
		}

		private string GetTempFolder()
		{
			string temp = FilesystemEntries.TempDirectory;
			if (Directory.Exists(temp) == true)
				Directory.Delete(temp, true);

			Directory.CreateDirectory(temp);
			return temp;
		}

		private static ReportProgressHandler GetSaveProgressHandler(ReportProgressHandler progressHandler)
		{
			ReportProgressHandler reportProgress;
			if (progressHandler != null)
				reportProgress = progressHandler;
			else
				reportProgress = (p, t) => { };
			return reportProgress;
		}

		public void CreateArenaScripts(Stream targetPackage)
		{
			this.CreateArenaScripts(targetPackage, this.progressHandler);
		}

		public void CreateArenaScripts(Stream targetPackage, ReportProgressHandler progressHandler)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			string tempDir = this.GetTempFolder();
			using (ZipFile zip = new ZipFile())
			{
				zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
				zip.CompressionMethod = CompressionMethod.Deflate;

				using (DataArchiveCache cache = DataArchiveCache.Load())
				{
					using (DataArchiveCache.MapsCache mapsCache = DataArchiveCache.MapsCache.Load())
					{
						reportProgress(0, "Create arenasctipts...");
						for (int i = 0; i < mapsCache.Entries.Length; i++)
						{
							ItemPair<string, int> item = mapsCache.Entries[i];
							reportProgress((double)i/mapsCache.Entries.Length, "Create arenascript for "+item.Item1+"...");
							string package = cache.PackageFiles[item.Item2].Item1;
							Arenascript arena = new Arenascript();
							arena.map = Path.GetFileNameWithoutExtension(item.Item1);
							arena.longname = arena.map;

							using (MemoryStream ms = new MemoryStream())
							{
								this.ExtractZipEntry(package, item.Item1, ms);
								ms.Seek(0, SeekOrigin.Begin);
								BspMap bspMap = BspMap.Open(ms);
								arena.type = bspMap.GetSupportedGametypes().ToArray();
							}

							string path = Path.Combine(tempDir, arena.map + FileExtensions.ArenascriptFiles);
							using (FileStream file = new FileStream(path, FileMode.Create))
							{
								arena.Save(file);
							}

						}
						reportProgress(1, "Create arenasctipts...");
					}
				}
				reportProgress(0, "Create package...");
				zip.AddDirectory(tempDir, Constants.ZipDirScripts);
				zip.SaveProgress += (saveSender, saveArgs) =>
				{
					double progress = (double)saveArgs.EntriesSaved / saveArgs.EntriesTotal;
					if (progress >= 0)
						reportProgress(progress, "Create Package...");
				};
				zip.Save(targetPackage);
			}

			Directory.Delete(tempDir, true);
		}

		private static void EnsureDirectoryExists(string Filename)
		{
			string dir = Path.GetDirectoryName(Filename);
			if (Directory.Exists(dir) == false)
				Directory.CreateDirectory(dir);
		}

		private static void AddWatermark(Bitmap image)
		{
			SizeF bounds = new SizeF(image.Width * 0.49f, image.Height * 0.2f);
			string text = ResourceFiles.LocalizedText.Watermark;
			DrawTextInfo drawTextInfo = new DrawTextInfo();
			drawTextInfo.FontFamily = new FontFamily(System.Drawing.Text.GenericFontFamilies.SansSerif);
			drawTextInfo.FontStyle = FontStyle.Bold;
			drawTextInfo.BorderBrush = new SolidBrush(Color.Black);

			drawTextInfo.FontSize = TextRenderer.MeasureText(text, drawTextInfo.FontFamily, drawTextInfo.FontStyle, drawTextInfo.StringFormat, ref bounds);

			drawTextInfo.BorderThickness = drawTextInfo.FontSize / 20;
			drawTextInfo.ShadowOffset = drawTextInfo.FontSize / 10;
			drawTextInfo.ShadowAngle = 45;
			drawTextInfo.TextBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
				new RectangleF(new PointF(0, 0), bounds),
				Color.Yellow,
				Color.Red,
				System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
			drawTextInfo.ShadowBrush = new SolidBrush(Color.White);
			using (Bitmap watermark = TextRenderer.DrawText(text, drawTextInfo))
			{
				watermark.SetResolution(image.HorizontalResolution, image.VerticalResolution);
				using (Graphics g = Graphics.FromImage(image))
				{
					int x = (int)(image.Width * 0.01);
					int y = (int)(image.Height * 0.01);
					g.DrawImageUnscaled(watermark, x, y);
				}
			}
		}

#if DEBUG
		private static Bitmap DrawSpawnpointMap(List<MapEntity> entities)
		{
			List<Vector3> vSpawn = new List<Vector3>();
			List<Vector3> vSpawnRed = new List<Vector3>();
			List<Vector3> vSpawnBlue = new List<Vector3>();
			List<Vector3> vEntity = new List<Vector3>();
			Vector3 vMin = new Vector3(double.PositiveInfinity, double.PositiveInfinity, 0);
			Vector3 vMax = new Vector3(double.NegativeInfinity, double.NegativeInfinity, 0);
			foreach (MapEntity entity in entities)
			{
				string value = entity.GetValue("origin");
				if (value == null)
					continue;
				string[] origin = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				double x, y;
				if (double.TryParse(origin[0], out x) == false)
					continue;
				if (double.TryParse(origin[1], out y) == false)
					continue;
				Vector3 v = new Vector3(x, y, 0);
				if (x < vMin.X)
					vMin.X = x;
				if (x > vMax.X)
					vMax.X = x;
				if (y < vMin.Y)
					vMin.Y = y;
				if (y > vMax.Y)
					vMax.Y = y;

				if (entity.Classname == "info_player_start")
					vSpawn.Add(v);
				else if (entity.Classname == "info_player_deathmatch")
					vSpawn.Add(v);
				else if (entity.Classname == "info_ut_spawn")
				{
					value = entity.GetValue("team");
					if (value != null)
					{
						value = value.ToLowerInvariant();
						if (value == "red")
							vSpawnRed.Add(v);
						else if (value == "blue")
							vSpawnBlue.Add(v);
					}
				}
				else
					vEntity.Add(v);
			}

			double width = vMax.X - vMin.X;
			double height = vMax.Y - vMin.Y;
			double ratio = width / height;
			int maxWidthHeight = 600;
			Size imSize;
			if (ratio < 1)
				imSize = new Size((int)(maxWidthHeight * ratio), maxWidthHeight);
			else
				imSize = new Size(maxWidthHeight, (int)(maxWidthHeight / ratio));

			double scale = imSize.Width / width;

			float radius = 10;
			float radiusBlueRed = 7.5f;
			float radiusEntity = 1;

			Bitmap bitmap = new Bitmap(imSize.Width, imSize.Height);
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				g.Clear(Color.Black);
				Brush brush = new SolidBrush(Color.White);
				foreach (Vector3 vec in vEntity)
				{
					Vector3 v = vec - vMin;
					v *= scale;
					g.FillEllipse(brush, (float)(v.X - radiusEntity), (float)(v.Y - radiusEntity), 2 * radiusEntity, 2 * radiusEntity);
				}
				foreach (Vector3 vec in vSpawn)
				{
					Vector3 v = vec - vMin;
					v *= scale;
					g.FillEllipse(brush, (float)(v.X - radius), (float)(v.Y - radius), 2 * radius, 2 * radius);
				}
				brush = new SolidBrush(Color.Red);
				foreach (Vector3 vec in vSpawnRed)
				{
					Vector3 v = vec - vMin;
					v *= scale;
					g.FillEllipse(brush, (float)(v.X - radiusBlueRed), (float)(v.Y - radiusBlueRed), 2 * radiusBlueRed, 2 * radiusBlueRed);
				}
				brush = new SolidBrush(Color.Blue);
				foreach (Vector3 vec in vSpawnBlue)
				{
					Vector3 v = vec - vMin;
					v *= scale;
					g.FillEllipse(brush, (float)(v.X - radiusBlueRed), (float)(v.Y - radiusBlueRed), 2 * radiusBlueRed, 2 * radiusBlueRed);
				}
			}
			return bitmap;
		}
#endif

		public List<MapEntity> GetMapEntities(string Mapname, ReportProgressHandler progressHandler)
		{
			ReportProgressHandler reportProgress = DataArchiveManager.GetSaveProgressHandler(progressHandler);
			ZipEntryInfo mapEntry = this.GetEntry(Constants.ZipDirMaps + Mapname + FileExtensions.Maps);
			if (mapEntry == null)
				return null;

			using (MemoryStream ms = new MemoryStream())
			{
				this.ExtractZipEntry(mapEntry, ms);
				ms.Seek(0, SeekOrigin.Begin);
				BspMap bspMap = BspMap.Open(ms);
				return bspMap.GetEntities();
			}
		}

		public List<MapEntity> GetMapEntities(string Mapname)
		{
			return this.GetMapEntities(Mapname, this.progressHandler);
		}
	}
}