//#define ShaderTextureDetectFileExt

using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager
{
	static class Shaderscript
	{
		const string ScriptComment = "//";
		static readonly char[] ContentBrackets = new char[] { '{', '}' };
		static readonly char[] NameTrimChars = new char[] { ' ', '\n', '\t' };
		static readonly List<string> KeywordsTextures = new List<string>(new string[] { "skyparms", "animmap", /*"qer_editorimage", */"map", "q3map_lightimage", "clampmap" });
		static readonly string[] SkyboxFaces = new string[] { "_rt", "_lf", "_ft", "_bk", "_up", "_dn" };

		public static List<string> GetEntryNames(Stream stream)
		{
			List<string> entryNames = new List<string>();
			StreamReader reader = new StreamReader(stream);
			int openBrackes = 0;
			List<char> currentName = new List<char>();

			while (reader.EndOfStream == false)
			{
				string line = reader.ReadLine().Trim(Shaderscript.NameTrimChars).ToLowerInvariant();
				int commentIndex = line.IndexOf(Shaderscript.ScriptComment);
				if (commentIndex > -1)
				{
					line = line.Substring(0, commentIndex).Trim(Shaderscript.NameTrimChars);
				}

				if (string.IsNullOrEmpty(line) == true)
					continue;
				if (line.StartsWith(Shaderscript.ScriptComment) == true)
					continue;

				for (int i = 0; i < line.Length; i++)
				{
					char ch = line[i];

					if (ch == Shaderscript.ContentBrackets[0])
					{
						if (openBrackes < 1)
						{
							string text = new string(currentName.ToArray());
							entryNames.Add(text.Trim(Shaderscript.NameTrimChars));
							currentName = new List<char>();
						}
						openBrackes++;
					}
					else if (ch == Shaderscript.ContentBrackets[1])
					{
						openBrackes--;
					}
					else if (openBrackes < 1)
					{
						currentName.Add(ch);
					}
				}
			}
			return entryNames;
		}

		/// <summary>
		/// Read ShaderscriptEntries from a stream.
		/// </summary>
		/// <param name="stream">A Stream providing access to a Shaderscript file.</param>
		/// <param name="EntryNames">Names of the entries to read. Expected in lower-case.</param>
		/// <returns></returns>
		public static List<ShaderscriptEntry> ReadEntries(Stream stream, IEnumerable<string> EntryNames)
		{
			return Shaderscript.ReadEntries(stream, EntryNames, null);
		}

		/// <summary>
		/// Read ShaderscriptEntries from a stream.
		/// </summary>
		/// <param name="stream">A Stream providing access to a Shaderscript file.</param>
		/// <param name="EntryNames">Names of the entries to read. Expected in lower-case.</param>
		/// <param name="NotFound">Will contain the names of the entries that where not found.</param>
		/// <returns></returns>
		public static List<ShaderscriptEntry> ReadEntries(Stream stream, IEnumerable<string> EntryNames, out List<string> NotFound)
		{
			NotFound = new List<string>();
			return Shaderscript.ReadEntries(stream, EntryNames, NotFound);
		}

		/// <summary>
		/// Read ShaderscriptEntries from a stream.
		/// </summary>
		/// <param name="stream">A Stream providing access to a Shaderscript file.</param>
		/// <param name="EntryNames">Names of the entries to read. Expected in lower-case.</param>
		/// <param name="NotFound">Will contain the names of the entries that where not found.</param>
		/// <returns></returns>
		private static List<ShaderscriptEntry> ReadEntries(Stream stream, IEnumerable<string> EntryNames, List<string> NotFound)
		{
			List<ShaderscriptEntry> entries = new List<ShaderscriptEntry>();
			ShaderscriptEntry currentEntry = new ShaderscriptEntry();
			List<string> nameList = new List<string>(EntryNames);
			StreamReader reader = new StreamReader(stream);
			int openBrackets = 0;
			string text = string.Empty;

			while (reader.EndOfStream == false)
			{
				string line = reader.ReadLine().ToLowerInvariant()+'\n';

				if (line.StartsWith(Shaderscript.ScriptComment) == true)
					continue;

				int commendIndex = line.IndexOf(Shaderscript.ScriptComment);
				if (commendIndex > -1)
					line = line.Substring(0, commendIndex) + '\n';

				for (int i = 0; i < line.Length; i++)
				{
					char ch = line[i];
					if (ch == ContentBrackets[0])
					{
						openBrackets++;
						if (openBrackets < 2)
						{
							currentEntry.Name = text.Trim(NameTrimChars);
							text = string.Empty;
							continue;
						}
					}
					else if (ch == ContentBrackets[1])
					{
						openBrackets--;
						if (openBrackets < 1)
						{
							int nameIndex = nameList.IndexOf(currentEntry.Name);
							if (nameIndex > -1)
							{
								//text = text.Replace("implicitmap -", "{\nmap " + currentEntry.Name+".tga\n}");
								//text = text.Replace("implicitmask -", "{\nmap " + currentEntry.Name + ".tga\n}");
								//text = text.Replace("implicitmap ", "map ");
								//int impIndex = text.IndexOf("implicitmap");
								//while (impIndex > -1)
								//{
								//	text.remo
								//}
								currentEntry.Content = text;
								entries.Add(currentEntry);
								nameList.RemoveAt(nameIndex);
							}
							if (nameList.Count < 1)
							{
								if (reader.BaseStream.CanSeek == true)
									reader.BaseStream.Seek(0, SeekOrigin.End);
								break;
							}
							currentEntry = new ShaderscriptEntry();
							text = string.Empty;
							continue;
						}
					}
					text += ch;
				}
			}
			if (NotFound != null)
				NotFound.AddRange(nameList);
			return entries;
		}

		public static void WriteEntries(Stream stream, IEnumerable<ShaderscriptEntry> Entries)
		{
			StreamWriter writer = new StreamWriter(stream);
			foreach (ShaderscriptEntry entry in Entries)
			{
				writer.WriteLine(entry.Name);
				writer.Write(Shaderscript.ContentBrackets[0].ToString());
				writer.Write(entry.Content);
				writer.WriteLine(Shaderscript.ContentBrackets[1]);
				writer.WriteLine();
			}
			writer.Flush();
		}

		/// <summary>
		/// Returns all the Texturefiles which are referenced by the ShaderscriptEntries.
		/// The Texturefilenames are returned without the fileextension.
		/// </summary>
		/// <param name="Entries"></param>
		/// <returns></returns>
		public static List<string> GetReferencedTextures(IEnumerable<ShaderscriptEntry> Entries)
		{
			List<string> textures = new List<string>();

			foreach (ShaderscriptEntry entry in Entries)
			{
				string[] parts = entry.Content.Split(Shaderscript.NameTrimChars);
				parts[0] = parts[0].Trim().Trim('\"');
				for (int i = 0; i < parts.Length - 1; i++)
				{
					parts[i + 1] = parts[i + 1].Trim().Trim('\"');

					//if (parts[i] == "implicitmap")
					//{
					//	textures.Add(entry.Name);
					//	continue;
					//}

					int index = Shaderscript.KeywordsTextures.IndexOf(parts[i]);
					if (index < 0)
						continue;

					if (index == 0)
					{
						foreach (string postfix in Shaderscript.SkyboxFaces)
						{
							string tex = parts[i + 1] + postfix;
							if (textures.Contains(tex) == false)
								textures.Add(tex);
						}
					}
					else if (index == 1)
					{
						int end = i+10;
						for (i = i + 2; i < end; i++)
						{
							if (i >= parts.Length)
								break;

							string tex = parts[i].Substring(0, parts[i].Length - Path.GetExtension(parts[i]).Length);

							if (textures.Contains(tex) == false)
								textures.Add(tex);
						}
					}
					else
					{
						string tex = parts[i + 1].Substring(0, parts[i + 1].Length - Path.GetExtension(parts[i + 1]).Length);

						if (textures.Contains(tex) == false)
							textures.Add(tex);
					}
				}
			}
			return textures;
		}
	}
}
