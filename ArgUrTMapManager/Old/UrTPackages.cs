#if false
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FreeImageAPI;
using ZipEntryCollection = System.Collections.Generic.List<ArgUrTMapManager.ZipEntryInfo>;

namespace ArgUrTMapManager
{
	enum SortZipEntriesOptions
	{
		SearchPattern,
		ZipFilename
	}

	static class UrTPackages
	{
		private static readonly string[] EmptyStringArray = new string[] { };
		//private static ImageSource LevelshotUnkownMap;
		private const CompressionLevel CompressionLevel = System.IO.Compression.CompressionLevel.Optimal;

		private static string[] GetPackages()
		{
			List<string> packages = new List<string>();
			foreach (string gameDataDir in Constants.GameDataDirectories)
			{
				string[] files = Directory.GetFiles(gameDataDir, "*" + FileExtensions.Packages);
				packages.AddRange(files);
			}

			return packages.ToArray();
		}

		public static ZipEntryCollection GetEntries(string searchPattern, bool IgnoreCase)
		{
			return UrTPackages.GetEntries(searchPattern, IgnoreCase, -1);
		}

		public static ZipEntryCollection GetEntries(string searchPattern, bool IgnoreCase, int MaxEntries)
		{
			return UrTPackages.GetEntries(searchPattern, IgnoreCase, MaxEntries, SortZipEntriesOptions.SearchPattern)[0];
		}

		public static ZipEntryCollection[] GetEntries(string searchPattern, bool IgnoreCase, int MaxEntries, SortZipEntriesOptions SortOptions)
		{
			return UrTPackages.GetEntries(new string[] { searchPattern }, IgnoreCase, MaxEntries, SortOptions);
		}

		public static ZipEntryCollection[] GetEntries(string[] SearchPatterns, bool IgnoreCase, int MaxEntries, SortZipEntriesOptions SortOptions)
		{
			bool[] ignoreCase = new bool[SearchPatterns.Length];
			int[] maxEntries = new int[SearchPatterns.Length];
			for (int i = 0; i < SearchPatterns.Length; i++)
			{
				ignoreCase[i] = IgnoreCase;
				maxEntries[i] = MaxEntries;
			}
			return UrTPackages.GetEntries(SearchPatterns, ignoreCase, maxEntries, SortOptions);
		}

		/// <summary>
		/// Returns all Entries which match the Search pattern as <see cref="ZipEntryInfo"/>.
		/// </summary>
		/// <param name="searchPatterns"></param>
		/// <param name="IgnoreCase"></param>
		/// <param name="MaxEntriesPerSearchPattern"></param>
		/// <returns></returns>
		private static ZipEntryCollection[] GetEntries(string[] searchPatterns, bool[] IgnoreCase, int[] MaxEntriesPerSearchPattern, SortZipEntriesOptions SortOptions)
		{
			if (searchPatterns.Length != IgnoreCase.Length || searchPatterns.Length != MaxEntriesPerSearchPattern.Length)
				throw new ArgumentException("Dimension Error", "searchPatterns, IgnoreCase, MaxEntriesPerSearchPattern");

			string[] packages = UrTPackages.GetPackages();

			ZipEntryCollection[] RetVal = null;
			int[] searchPatternCount = new int[searchPatterns.Length];
			if (SortOptions == SortZipEntriesOptions.SearchPattern)
				RetVal = new ZipEntryCollection[searchPatterns.Length];
			else if (SortOptions == SortZipEntriesOptions.ZipFilename)
				RetVal = new ZipEntryCollection[packages.Length];
			else
				throw new ArgumentOutOfRangeException("SortOptions");
			//List<string> zipFilenamesIndices = new List<string>();
			//List<int> searchPatternCount = new List<int>();
			for (int i = 0; i < searchPatterns.Length; i++)
			{
				if (IgnoreCase[i] == true)
					searchPatterns[i] = searchPatterns[i].ToLower();
				if (MaxEntriesPerSearchPattern[i] < 0)
					MaxEntriesPerSearchPattern[i] = int.MaxValue;
				searchPatternCount[i] = 0;
			}

			for (int i = 0; i < RetVal.Length; i++)
			{
				RetVal[i] = new ZipEntryCollection();
			}

			bool abort = false;
			for (int k = 0; k < packages.Length; k++)
			{
				string package = packages[k];
				if (abort == true)
					break;
				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
					{
						abort = true;
						for (int i = 0; i < searchPatterns.Length; i++)
						{
							if (searchPatternCount[i] >= MaxEntriesPerSearchPattern[i])
								continue;
							string[] search = searchPatterns[i].Split('*');
							if (search.Length < 1)
								continue;

							foreach (ZipArchiveEntry entry in zip.Entries)
							{
								string name = entry.FullName;
								if (IgnoreCase[i] == true)
									name = name.ToLower();

								if (name.StartsWith(search[0]) == false)
									continue;
								if (search.Length > 1 && name.EndsWith(search[search.Length - 1]) == false)
									continue;
								bool bContains = true;
								for (int j = 1; j < search.Length-1; j++)
								{
									if (name.Contains(search[j]) == false)
									{
										bContains = false;
										break;
									}
								}
								if (bContains == false)
									continue;

								ZipEntryInfo zipEntryInfo = new ZipEntryInfo
										{
											Name = entry.FullName,
											ZipFilename = package
										};


								if (SortOptions == SortZipEntriesOptions.SearchPattern)
								{
									RetVal[i].Add(zipEntryInfo);
								}
								else if (SortOptions == SortZipEntriesOptions.ZipFilename)
								{
									RetVal[k].Add(zipEntryInfo);
								}
								searchPatternCount[i]++;
								if (searchPatternCount[i] >= MaxEntriesPerSearchPattern[i])
									break;
							}

							if (searchPatternCount[i] < MaxEntriesPerSearchPattern[i])
								abort = false;
						}
					}
				}
			}
			return RetVal;
		}

		private static void ExecuteActionOnEntries(ZipEntryCollection zipEntryCollection, Action<ZipArchiveEntry> EntryAction, ZipArchiveMode ArchiveMode)
		{
			if (zipEntryCollection.Count < 1)
				return;
			string Filename = zipEntryCollection[0].ZipFilename;
			FileStream file = new FileStream(Filename, FileMode.Open);
			ZipArchive zip = new ZipArchive(file, ArchiveMode);
			
			for (int i = 0; i < zipEntryCollection.Count; i++)
			{
				if (Filename != zipEntryCollection[i].ZipFilename)
				{
					file.Close();
					Filename = zipEntryCollection[i].ZipFilename;
					file = new FileStream(Filename, FileMode.Open);
					zip = new ZipArchive(file, ArchiveMode);
				}

				ZipArchiveEntry entry = zip.GetEntry(zipEntryCollection[i].Name);
				EntryAction(entry);
			}
			file.Close();
		}

#if false
		private static ZipEntryCollection[] SortEntriesByZipFilename(ZipEntryCollection[] Entries)
		{
			List<ZipEntryCollection> RetVal = new List<ZipEntryCollection>();
			foreach (ZipEntryCollection collection in Entries)
			{
				foreach (ZipEntryInfo entry in collection)
				{
					bool added = false;
					for (int i = 0; i < RetVal.Count; i++)
					{
						if (RetVal[i][0].ZipFilename == entry.ZipFilename)
						{
							RetVal[i].Add(entry);
							added = true;
							break;
						}
					}
					if (added == false)
					{
						ZipEntryCollection newCollection = new ZipEntryCollection();
						newCollection.Add(entry);
						RetVal.Add(newCollection);
					}
				}
			}
			return RetVal.ToArray();
		}
#endif

		public static string[] GetLevelNames()
		{
#if false
			string[] packages = UrTPackages.GetPackages();
			List<string> levelNames = new List<string>();
			foreach (string package in packages)
			{
				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
					{
						foreach (ZipArchiveEntry entry in zip.Entries)
						{
							string fullName = entry.FullName.ToLower();
							if (fullName.StartsWith(Constants.ZipMaps) == false)
								continue;
							if (fullName.EndsWith(FileExtensions.Maps) == false)
								continue;
							levelNames.Add(entry.Name.Substring(0, entry.Name.Length - FileExtensions.Maps.Length));
						}
					}
				}
			}
			return levelNames.ToArray();
#endif

			ZipEntryCollection levelNames = UrTPackages.GetEntries(Constants.ZipMaps + "*" + FileExtensions.Maps, true);
			string[] RetVal = new string[levelNames.Count];
			for (int i = 0; i < levelNames.Count; i++)
			{
				string text = levelNames[i].Name;
				RetVal[i] = text.Substring(Constants.ZipMaps.Length, text.Length - Constants.ZipMaps.Length - FileExtensions.Maps.Length);
			}
			return RetVal;
		}

		public static ImageSource GetLevelShot(string LevelName)
		{
			ZipEntryCollection levelshotEntry = UrTPackages.GetEntries(Constants.ZipLevelshots + LevelName + ".*", true, 1);
			if (levelshotEntry.Count < 1)
				return null;

			using (FileStream file = new FileStream(levelshotEntry[0].ZipFilename, FileMode.Open))
			{
				using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
				{
					ZipArchiveEntry entry = zip.GetEntry(levelshotEntry[0].Name);
					MemoryStream ms = null;
					using (Stream sEntry = entry.Open())
					{
						if (levelshotEntry[0].Name.ToLower().EndsWith(".tga") == true)
						{
							FIBITMAP fiImage = FreeImage.LoadFromStream(sEntry);
							if (fiImage.IsNull == true)
								return null;
							ms = new MemoryStream();
							FreeImage.SaveToStream(fiImage, ms, FREE_IMAGE_FORMAT.FIF_PNG);
							FreeImage.UnloadEx(ref fiImage);
						}
						else
						{
							byte[] data = new byte[entry.Length];
							sEntry.Read(data, 0, data.Length);
							ms = new MemoryStream(data);
						}
					}
					BitmapImage image = new BitmapImage();
					image.BeginInit();
					image.CacheOption = BitmapCacheOption.OnLoad;
					image.StreamSource = ms;
					image.EndInit();
					ms.Close();
					zip.Dispose();
					file.Close();
					return image;
				}
			}
			return null;

#if false
			string[] packages = UrTPackages.GetPackages();
			LevelName = LevelName.ToLower();
			foreach (string package in packages)
			{
				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
					{
						if (UrTPackages.LevelshotUnkownMap == null)
						{
							ZipArchiveEntry a = zip.GetEntry(Constants.LevelshotUnkownMap);
							if (a != null)
							{
								Stream s = a.Open();
								byte[] data = new byte[a.Length];
								s.Read(data, 0, data.Length);
								s.Close();
								MemoryStream ms = new MemoryStream(data);
								BitmapImage image = new BitmapImage();
								image.BeginInit();
								image.CacheOption = BitmapCacheOption.OnLoad;
								image.StreamSource = ms;
								image.EndInit();
								ms.Close();
								UrTPackages.LevelshotUnkownMap = image;
							}
						}

						foreach (ZipArchiveEntry entry in zip.Entries)
						{
							string name = entry.Name.ToLower();
							string fullname = entry.FullName.ToLower();
							if (name.Contains(LevelName + ".") == false)
								continue;
							if (fullname.StartsWith(Constants.ZipLevelshots) == false)
								continue;
							if (name.EndsWith(".jpg") == false && name.EndsWith(".jepg") == false)
								continue;

							Stream sEntry = entry.Open();
							byte[] data = new byte[entry.Length];
							sEntry.Read(data, 0, data.Length);
							sEntry.Close();
							MemoryStream ms = new MemoryStream(data);
							BitmapImage image = new BitmapImage();
							image.BeginInit();
							image.CacheOption = BitmapCacheOption.OnLoad;
							image.StreamSource = ms;
							image.EndInit();
							ms.Close();
							zip.Dispose();
							file.Close();
							return image;
						}
					}
				}
			}
			return UrTPackages.LevelshotUnkownMap;
#endif
		}

		public static void RemoveMap(string MapName)
		{
#if false
			string[] mapPackages = Directory.GetFiles(Constants.GameDataDir, MapName + FileExtensions.Packages);
			foreach (string mapPackage in mapPackages)
			{
				File.Delete(mapPackage);
			}
			string[] packages = UrTPackages.GetPackages();
			MapName = MapName.ToLower() + ".";
			foreach (string package in packages)
			{
				List<string> removeEntries = new List<string>();
				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
					{
						foreach (ZipArchiveEntry entry in zip.Entries)
						{
							if (entry.Name.ToLower().StartsWith(MapName) == true)
							{
								removeEntries.Add(entry.FullName);
							}
						}
					}
				}
				
				if (removeEntries.Count < 1)
					continue;

				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Update))
					{
						foreach (String entryName in removeEntries)
						{
							ZipArchiveEntry entry = zip.GetEntry(entryName);
							if (entry != null)
								entry.Delete();
						}
					}
				}
			}
#endif
		}

		private static bool GetTextureAndSoundNames(ZipEntryInfo MapEntry, out string[] texNames, out string[] soundNames)
		{
			texNames = null;
			soundNames = null;

			string[] textures = null;
			string[] sounds = null;

			MapEntry.OpenEntry(
				entry =>
				{
					using (Stream mapEntryStream = entry.Open())
					{
						byte[] data = new byte[entry.Length];
						mapEntryStream.Read(data, 0, data.Length);

						using (MemoryStream ms = new MemoryStream(data))
						{
							BspReader bspReader = BspReader.Open(ms);
							// BspReader returns the names in lower case.
							textures = bspReader.GetTextureNames();
							sounds = bspReader.GetNoises();
							//effectNames.AddRange(bspReader.GetEffectshaderNames());
						}
					}
				}, ZipArchiveMode.Read);

			texNames = textures;
			soundNames = sounds;
			return true;
		}

		/// <ToDo>
		/// Levelshots
		/// Shaders (Done)
		/// Sounds (Done)
		/// </ToDo>
		public static void ExportMap(string MapName, Stream Target, MapExportOptions ExportOptions)
		{
			string[] texNames;
			string[] soundNames;
			ZipEntryCollection tempEntryCollection = UrTPackages.GetEntries(Constants.ZipMaps + MapName + FileExtensions.Maps, true, 1);
			if (tempEntryCollection.Count < 1)
				return;
			ZipEntryInfo MapEntryInfo = tempEntryCollection[0];
			UrTPackages.GetTextureAndSoundNames(MapEntryInfo, out texNames, out soundNames);
			ZipEntryCollection[] shaderZipEntries = UrTPackages.GetEntries(Constants.ZipScripts + "*" + FileExtensions.ShaderscriptFiles, true, -1, SortZipEntriesOptions.ZipFilename);
			List<ShaderscriptEntry> shaderscriptEntries = new List<ShaderscriptEntry>();
			List<string> texList = new List<string>(texNames);
			texNames = null;
			foreach (ZipEntryCollection entryCollection in shaderZipEntries)
			{
				shaderscriptEntries.AddRange(UrTPackages.GetShaderscriptEntries(entryCollection, ref texList));
			}
			shaderZipEntries = null;
			string[] refTextures = Shaderscript.GetReferencedTextures(shaderscriptEntries.ToArray());
			foreach (string refTex in refTextures)
			{
				if (texList.Contains(refTex) == false)
					texList.Add(refTex);
			}
			refTextures = null;
			string[] searchPatterns = new string[texList.Count + soundNames.Length + 2];
			//bool[] ignoreCase = new bool[searchPatterns.Length];
			//int[] maxEntries = new int[searchPatterns.Length];
			int i;
			for (i = 0; i < texList.Count; i++)
			{
				searchPatterns[i] = texList[i] + ".*";
				//ignoreCase[i] = true;
				//maxEntries[i] = 1;
			}
			for (i = texList.Count; i < texList.Count + soundNames.Length; i++)
			{
				searchPatterns[i] = soundNames[i - texList.Count] + ".*";
				//ignoreCase[i] = true;
				//maxEntries[i] = 1;
			}
			searchPatterns[i] = Constants.ZipLevelshots + MapName + ".*";
			//ignoreCase[i] = true;
			//maxEntries[i] = 1;
			i++;
			searchPatterns[i] = Constants.ZipMaps + MapName + FileExtensions.BotMaps;
			//ignoreCase[i] = true;
			//maxEntries[i] = 1;
			texList = null;
			soundNames = null;
			ZipEntryCollection[] zipEntryCollections = UrTPackages.GetEntries(searchPatterns, true, 1, SortZipEntriesOptions.ZipFilename);
			using (ZipArchive TargetZip = new ZipArchive(Target, ZipArchiveMode.Update))
			{
				MapEntryInfo.OpenEntry(
					entry =>
					{
						UrTPackages.CopyEntry(entry, TargetZip);
					},
					ZipArchiveMode.Read);
				ZipArchiveEntry scriptEntry = TargetZip.CreateEntry(Constants.ZipScripts + MapName.ToLower() + FileExtensions.ShaderscriptFiles, UrTPackages.CompressionLevel);
				using (Stream stream = scriptEntry.Open())
				{
					Shaderscript.WriteEntries(stream, shaderscriptEntries.ToArray());
				}

				Action<ZipArchiveEntry> elseAction = zipEntry =>
					{
						UrTPackages.CopyEntry(zipEntry, TargetZip);
					};
				byte[] EmptyWavData = null;
				if (ExportOptions.HasFlag(MapExportOptions.RemoveSounds) == true)
				{
					using (Stream stream = Resources.Sounds.EmptyWav)
					{
						EmptyWavData = stream.GetBytes();
					}
				}
				foreach (ZipEntryCollection collection in zipEntryCollections)
				{
					UrTPackages.ExecuteActionOnEntries(
						collection,
						zipEntry =>
						{
							if (ExportOptions.HasFlag(MapExportOptions.ConvertTgaToPng) == true && zipEntry.Name.ToLower().EndsWith(".tga") == true)
							{
								using (Stream streamSource = zipEntry.Open())
								{
									FIBITMAP fiImage = FreeImage.LoadFromStream(streamSource);
									if (fiImage.IsNull == true)
									{
										elseAction(zipEntry);
									}
									else
									{
										ZipArchiveEntry targetEntry = TargetZip.CreateEntry(zipEntry.FullName.ToLower().Replace(".tga", ".png"), UrTPackages.CompressionLevel);
										using (Stream streamTarget = targetEntry.Open())
										{
											FreeImage.SaveToStream(fiImage, streamTarget, FREE_IMAGE_FORMAT.FIF_PNG, FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_COMPRESSION);
										}
										FreeImage.UnloadEx(ref fiImage);
									}
								}
							}
							else if (ExportOptions.HasFlag(MapExportOptions.RemoveSounds) == true)
							{
								string tempName = zipEntry.FullName.ToLower();
								if (tempName.EndsWith(".wav") == true || tempName.EndsWith(".ogg") == true)
								{
									tempName = tempName.Replace(".ogg", ".wav");
									ZipArchiveEntry targetEntry = TargetZip.CreateEntry(tempName, UrTPackages.CompressionLevel);
									using (Stream stream = targetEntry.Open())
									{
										stream.WriteBytes(EmptyWavData);
									}
								}
								else
								{
									elseAction(zipEntry);
								}
							}
							else
							{
								elseAction(zipEntry);
							}
						},
						ZipArchiveMode.Read);
				}
			}
#if false
			string[] packages = UrTPackages.GetPackages();
			string MapEntryName = Constants.ZipMaps+MapName+FileExtensions.Maps;
			List<string> texNames = new List<string>();
			List<string> soundFiles = new List<string>();
			//List<string> effectNames = new List<string>();

			using (ZipArchive TargetZip = new ZipArchive(Target, ZipArchiveMode.Update))
			{
				foreach (string package in packages)
				{
					using (FileStream file = new FileStream(package, FileMode.Open))
					{
						using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
						{
							ZipArchiveEntry mapEntry = zip.GetEntry(MapEntryName);
							if (mapEntry == null)
								continue;

							using (Stream mapEntryStream = mapEntry.Open())
							{
								byte[] data = new byte[mapEntry.Length];
								mapEntryStream.Read(data, 0, data.Length);

								using (MemoryStream ms = new MemoryStream(data))
								{
									BspReader bspReader = BspReader.Open(ms);
									texNames.AddRange(bspReader.GetTextureNames());
									soundFiles.AddRange(bspReader.GetNoises());
									//effectNames.AddRange(bspReader.GetEffectshaderNames());
								}

								ZipArchiveEntry targetMapEntry = TargetZip.CreateEntry(mapEntry.FullName, UrTPackages.CompressionLevel);
								using (Stream stream = targetMapEntry.Open())
								{
									stream.Write(data, 0, data.Length);
								}
							}
							break;
						}
					}
				}

				List<ShaderscriptEntry> shaderscriptEntries = new List<ShaderscriptEntry>(UrTPackages.GetAllShaderscriptEntries());
				List<ShaderscriptEntry> usedShaderscriptEntires = new List<ShaderscriptEntry>();
				for (int i = 0; i < texNames.Count; i++)
				{
					string tex = texNames[i];
					for (int j = 0; j < shaderscriptEntries.Count; j++)
					{
						if (tex == shaderscriptEntries[j].Name)
						{
							usedShaderscriptEntires.Add(shaderscriptEntries[j]);
							shaderscriptEntries.RemoveAt(j);
							texNames.RemoveAt(i);
							i--;
							break;
						}
					}
				}
				string[] referencedTextures = Shaderscript.GetReferencedTextures(usedShaderscriptEntires.ToArray());
				foreach (string refTex in referencedTextures)
				{
					//string tex = refTex;
					//int i = refTex.LastIndexOf('.');
					//if (i > -1)
					//	tex = refTex.Substring(0, i);
					if (texNames.Contains(refTex) == false)
						texNames.Add(refTex);
				}

				//ShaderscriptEntry[] testArray = usedShaderscriptEntires.ToArray();
				//for (int i = 0; i < usedShaderscriptEntires.Count / 10; i++)
				//{
					ZipArchiveEntry shaderscriptZip = TargetZip.CreateEntry(Constants.ZipScripts + MapName.ToLower()/*+i.ToString()*/ + FileExtensions.ShaderscriptFiles, UrTPackages.CompressionLevel);
					using (Stream stream = shaderscriptZip.Open())
					{
						Shaderscript.WriteEntries(stream, usedShaderscriptEntires.ToArray()/*, i*10, 10*/);
					}
				//}
				/// ToDo:
				/// - Search each package for shader-scripts which are referenced by texNames (To be tested)
				/// - Search all found shader-scripts for additional Texture-files and add them to texNames (to be tested)
				/// - Add the shader-scripts to the TargetZip (to be tested)

				foreach (string package in packages)
				{
					using (FileStream file = new FileStream(package, FileMode.Open))
					{
						using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
						{
							for (int i = 0; i < soundFiles.Count; i++)
							{
								foreach (ZipArchiveEntry entry in zip.Entries)
								{
									if (entry.FullName.StartsWith(soundFiles[i] + ".") == true)
									{
										UrTPackages.CopyEntry(entry, TargetZip);
										soundFiles.RemoveAt(i);
										i--;
										break;
									}
								}
							}
							for (int i = 0; i < texNames.Count; i++)
							{
								string tex = texNames[i];
								ZipArchiveEntry sourceEntry = zip.GetEntry(tex + FileExtensions.Textures);
								if (sourceEntry == null)
									sourceEntry = zip.GetEntry(tex + FileExtensions.Textures2);
								if (sourceEntry == null)
									continue;
								UrTPackages.CopyEntry(sourceEntry, TargetZip);
								texNames.RemoveAt(i);
								i--;
							}

							//for (int i = 0; i < effectNames.Count; i++)
							//{
							//	string eff = effectNames[i];
							//	ZipArchiveEntry sourceEntry = zip.GetEntry(eff + Constants.EffectFileExt);
							//	if (sourceEntry == null)
							//		continue;
							//	UrTPackages.CopyEntry(sourceEntry, TargetZip);
							//	effectNames.RemoveAt(i);
							//	i--;
							//}

							if (texNames.Count < 1/* && effectNames.Count < 1*/)
								break;
						}
					}
				}
			}
#endif
		}

		private static void CopyEntry(ZipArchiveEntry entry, ZipArchive TargetArchive)
		{
			ZipArchiveEntry targetEntry = TargetArchive.CreateEntry(entry.FullName, UrTPackages.CompressionLevel);
			byte[] data = new byte[entry.Length]; 
			using (Stream stream = entry.Open())
			{
				stream.Read(data, 0, data.Length);
			}
			using (Stream stream = targetEntry.Open())
			{
				stream.Write(data, 0, data.Length);
			}
		}

		private static ShaderscriptEntry[] GetAllShaderscriptEntries()
		{
			List<ShaderscriptEntry> scripts = new List<ShaderscriptEntry>();
			string[] packages = UrTPackages.GetPackages();
			foreach (string package in packages)
			{
				using (FileStream file = new FileStream(package, FileMode.Open))
				{
					using (ZipArchive zip = new ZipArchive(file, ZipArchiveMode.Read))
					{
						foreach (ZipArchiveEntry entry in zip.Entries)
						{
							if (entry.FullName.ToLower().StartsWith(Constants.ZipScripts) == false)
								continue;
							if (entry.Name.ToLower().EndsWith(FileExtensions.ShaderscriptFiles) == false)
								continue;

							using (Stream stream = entry.Open())
							{
								scripts.AddRange(Shaderscript.ReadEntries(stream));
							}
						}
					}
				}
			}
			return scripts.ToArray();
		}

		/// <summary>
		/// Returns all Shaderscriptentries which names are listed in ShaderNames and found in the ZipEntryCollection.
		/// The Shaderscriptentries that were found are removed from ShaderNames, thus the list containts the names of
		/// the Shaderscripts which were not found.
		/// </summary>
		/// <param name="zipEntryCollection"></param>
		/// <param name="ShaderNames"></param>
		/// <returns></returns>
		private static ShaderscriptEntry[] GetShaderscriptEntries(ZipEntryCollection zipEntryCollection, ref List<string> ShaderNames)
		{
			List<ShaderscriptEntry> RetVal = new List<ShaderscriptEntry>();
			List<string> shaderNames = ShaderNames;
			UrTPackages.ExecuteActionOnEntries(
				zipEntryCollection,
				entry =>
				{
					using (Stream stream = entry.Open())
					{
						ShaderscriptEntry[] shaderEntries = Shaderscript.ReadEntries(stream);
						foreach (ShaderscriptEntry shaderEntry in shaderEntries)
						{
							int i = shaderNames.IndexOf(shaderEntry.Name);
							if (i > -1)
							{
								RetVal.Add(shaderEntry);
								shaderNames.RemoveAt(i);
							}
						}
					}
				},
				ZipArchiveMode.Read);
			ShaderNames = shaderNames;
			return RetVal.ToArray();
		}
	}
}
#endif