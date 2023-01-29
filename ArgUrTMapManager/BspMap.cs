using System;
using System.Collections.Generic;
using System.IO;
using ArgusLib.Collections;
using MapEntityKeyValuePair = ArgusLib.Collections.ItemPair<string, string>;

namespace ArgUrTMapManager
{
	// http://www.mralligator.com/q3/
	class BspMap
	{
		private const int HeaderSize = 8 + 8 * NumberOfLumps;
		private const int NumberOfLumps = 17;
		private const int LumpSizeTextures = 72;
		private const int FieldSizeTextureName = 64;
		private const int LumpSizeEffects = 72;
		private const int FieldSizeEffectName = 64;
		private const string CVarNoise = "noise";
		private const string CVarModel = "model";
		private static readonly char[] SplitChars = new char[]{' ', '\n', '\t'};
		private static readonly char[] EntityBrackets = new char[] { '{', '}' };
		private const string MapEntityUtSpawn = "info_ut_spawn";
		private const string MapEntityCAHPoit = "team_cah_capturepoint";
		private const string MapEntityBombSite = "info_ut_bombsite";
		private const string MapEntityQuakeSpawn = "info_player_start";
		private const string MapEntityQuakeSpawnDeathmatch = "info_player_deathmatch";
		private const string MapEntityRedFlag = "team_ctf_redflag";
		private const string MapEntityBlueFlag = "team_ctf_blueflag";
		private const string MapEntityKeyGametype = "g_gametype";

		BinaryReader stream;
		Header header;

		private BspMap(Stream stream)
		{
			this.stream = new BinaryReader(stream);
		}

		public static BspMap Open(Stream stream)
		{
			if (stream.CanRead == false)
				return null;
			if (stream.CanSeek == false)
				return null;
			BspMap reader = new BspMap(stream);
			reader.header = BspMap.ReadHeader(reader.stream);
			return reader;
		}

		public int BspVersion { get { return this.header.Version; } }

		public string[] GetTextureNames()
		{
			Lump textureLump = this.header.LumpDirectories[(int)Lumps.Textures];
			int itemNo = textureLump.Length / BspMap.LumpSizeTextures;
			string[] textures = new string[itemNo];
			this.stream.BaseStream.Seek(textureLump.Offset, SeekOrigin.Begin);
			for (int i = 0; i < itemNo; i++)
			{
				textures[i] = BspMap.ReadTexturesEntry(this.stream).Name;
			}
			return textures;
		}

		public string[] GetEffectshaderNames()
		{
			Lump effectLump = this.header.LumpDirectories[(int)Lumps.Effects];
			int itemNo = effectLump.Length / BspMap.LumpSizeEffects;
			string[] effects = new string[itemNo];
			this.stream.BaseStream.Seek(effectLump.Offset, SeekOrigin.Begin);
			for (int i = 0; i < itemNo; i++)
			{
				effects[i] = BspMap.ReadEffectsEntry(this.stream).Name;
			}
			return effects;
		}

		public List<MapEntity> GetEntities()
		{
			//Lump entitiesLump = this.header.LumpDirectories[(int)Lumps.Entities];
			//this.stream.BaseStream.Seek(entitiesLump.Offset, SeekOrigin.Begin);
			//string RetVal = string.Empty;
			//for (int i = 0; i < entitiesLump.Length; i++)
			//	RetVal += (char)this.stream.ReadByte();
			//return RetVal;

			List<MapEntity> entities = new List<MapEntity>();
			List<string> currentEntity = new List<string>();
			string str = string.Empty;
			bool read = false;
			Lump entitiesLump = this.header.LumpDirectories[(int)Lumps.Entities];
			this.stream.BaseStream.Seek(entitiesLump.Offset, SeekOrigin.Begin);
			for (int i = 0; i < entitiesLump.Length; i++)
			{
				char c = (char)this.stream.ReadByte();
				if (c == BspMap.EntityBrackets[0] && read == false)
				{
					currentEntity = new List<string>();
				}
				else if (c == BspMap.EntityBrackets[1] && read == false)
				{
					entities.Add(new MapEntity(currentEntity));
				}
				else if (c == '\"')
				{
					read = read == false;
					if (read == false)
					{
						currentEntity.Add(str);
						str = string.Empty;
					}
				}
				else if (read == true)
				{
					str += c;
				}
			}
			return entities;
		}

		private List<string> GetEntityReferences(string Keyword)
		{
			List<MapEntity> entities = this.GetEntities();
			List<string> values = MapEntity.GetValues(entities, new string[] { Keyword });
			while (values.Remove(null) == true) ;
			return values;
		}

		public List<string> GetReferencedSounds()
		{
			return this.GetEntityReferences(BspMap.CVarNoise);
		}

		public List<string> GetReferencedModels()
		{
			return this.GetEntityReferences(BspMap.CVarModel);
		}

		private static Header ReadHeader(BinaryReader stream)
		{
			Header header = new Header();
			header.Magic = stream.ReadString(4);
			header.Version = stream.ReadInt32();
			header.LumpDirectories = new Lump[BspMap.NumberOfLumps];
			for (int i = 0; i < BspMap.NumberOfLumps; i++)
			{
				header.LumpDirectories[i] = BspMap.ReadLump(stream);
			}
			return header;
		}

		private static void WriteHeader(BinaryWriter stream, Header header)
		{
			stream.WriteString(header.Magic);
			stream.Write(header.Version);
			for (int i = 0; i < BspMap.NumberOfLumps; i++)
			{
				BspMap.WriteLump(stream, header.LumpDirectories[i]);
			}
		}

		private static Lump ReadLump(BinaryReader stream)
		{
			Lump lDir = new Lump();
			lDir.Offset = stream.ReadInt32();
			lDir.Length = stream.ReadInt32();
			return lDir;
		}

		private static void WriteLump(BinaryWriter stream, Lump lump)
		{
			stream.Write(lump.Offset);
			stream.Write(lump.Length);
		}

		private static TexturesEntry ReadTexturesEntry(BinaryReader stream)
		{
			TexturesEntry entry = new TexturesEntry();
			entry.Name = stream.ReadString(BspMap.FieldSizeTextureName).ToLowerInvariant();
			entry.Flags = (SurfaceParms)stream.ReadInt32();
			entry.Contents = stream.ReadInt32();
			return entry;
		}

		private static void WriteTextureEntry(BinaryWriter stream, TexturesEntry textureEntry)
		{
			stream.WriteString(textureEntry.Name, BspMap.FieldSizeTextureName);
			stream.Write((int)textureEntry.Flags);
			stream.Write(textureEntry.Contents);
		}

		private static EffectsEntry ReadEffectsEntry(BinaryReader stream)
		{
			EffectsEntry entry = new EffectsEntry();
			entry.Name = stream.ReadString(BspMap.FieldSizeEffectName).ToLowerInvariant();
			entry.Brush = stream.ReadInt32();
			entry.Unknown = stream.ReadInt32();
			return entry;
		}

		private byte[] ReadLump(Lumps lumpId)
		{
			Lump lump = this.header.LumpDirectories[(int)lumpId];
			this.stream.BaseStream.Seek(lump.Offset, SeekOrigin.Begin);
			byte[] data = new byte[lump.Length];
			this.stream.Read(data, 0, data.Length);
			return data;
		}

		public void Save(Stream target)
		{
			this.Save(target, null);
		}

		public void Save(Stream target, ListPair<Lumps, byte[]> LumpReplacements)
		{
			this.Save(target, this.header.Version, LumpReplacements);
		}

		public void Save(Stream target, int Version, ListPair<Lumps, byte[]> LumpReplacements)
		{
			Header newHeader = new Header();
			newHeader.Version = Version;
			newHeader.Magic = this.header.Magic;
			newHeader.LumpDirectories = new Lump[BspMap.NumberOfLumps];

			if (LumpReplacements == null)
				LumpReplacements = new ListPair<Lumps, byte[]>();

			long startPos = target.Position;
			target.Seek(BspMap.HeaderSize, SeekOrigin.Current);
			for (int i = 0; i < BspMap.NumberOfLumps; i++)
			{
				Lumps lumpEnum = (Lumps)i;
				if (LumpReplacements.List1.Contains(lumpEnum) == true)
					continue;

				byte[] lumpData = this.ReadLump(lumpEnum);
				newHeader.LumpDirectories[i] = new Lump()
				{
					Length = lumpData.Length,
					Offset = (int)(target.Position - startPos)
				};
				target.Write(lumpData, 0, lumpData.Length);
			}

			foreach (ItemPair<Lumps, byte[]> lump in LumpReplacements)
			{
				newHeader.LumpDirectories[(int)lump.Item1] = new Lump()
				{
					Length = lump.Item2.Length,
					Offset = (int)(target.Position - startPos)
				};
				target.Write(lump.Item2, 0, lump.Item2.Length);
			}
			target.Seek(startPos, SeekOrigin.Begin);
			using (BinaryWriter writer = new BinaryWriter(target))
			{
				BspMap.WriteHeader(writer, newHeader);
			}
		}

		public ItemPair<Lumps, byte[]> GetLumpReplacementApplySurfaceParm(SurfaceParms surfaceParm)
		{
			ItemPair<Lumps, byte[]> RetVal = new ItemPair<Lumps, byte[]>();
			RetVal.Item1 = Lumps.Textures;

			Lump textureLump = this.header.LumpDirectories[(int)Lumps.Textures];
			int itemNo = textureLump.Length / BspMap.LumpSizeTextures;
			this.stream.BaseStream.Seek(textureLump.Offset, SeekOrigin.Begin);
			using (MemoryStream ms = new MemoryStream(BspMap.LumpSizeTextures))
			{
				BinaryWriter writer = new BinaryWriter(ms);
				for (int i = 0; i < itemNo; i++)
				{
					TexturesEntry entry = BspMap.ReadTexturesEntry(this.stream);
					entry.Flags |= surfaceParm;
					BspMap.WriteTextureEntry(writer, entry);
				}
				ms.Seek(0, SeekOrigin.Begin);
				RetVal.Item2 = ms.GetBytes();
			}
			return RetVal;
		}

		public static ItemPair<Lumps, byte[]> GetLumpReplacementEntities(IEnumerable<MapEntity> mapEntities)
		{
			ItemPair<Lumps, byte[]> RetVal = new ItemPair<Lumps, byte[]>();
			RetVal.Item1 = Lumps.Entities;

			using (MemoryStream ms = new MemoryStream())
			{
				StreamWriter writer = new StreamWriter(ms);
				foreach (MapEntity entity in mapEntities)
				{
					writer.WriteLine("{");
					writer.WriteLine("\t\"classname\" \"" + entity.Classname + '\"');
					foreach (ItemPair<string, string> keyvalupair in entity.KeyValueList)
					{
						writer.WriteLine("\t\"" + keyvalupair.Item1 + "\" \"" + keyvalupair.Item2 + '\"');
					}
					writer.WriteLine("}");
				}
				writer.Flush();
				RetVal.Item2 = ms.GetBytes();
			}
			return RetVal;
		}

		public List<GameTypes> GetSupportedGametypes()
		{
			List<MapEntity> entities = this.GetEntities();
			int capturePoints = 0;
			int bombSites = 0;
			int infoPlayerStart = 0;
			int blueflag = 0;
			int redflag = 0;
			List<GameTypes> RetVal = new List<GameTypes>();
			foreach (MapEntity entity in entities)
			{
				string classname = entity.Classname.ToLowerInvariant();
				if (classname == BspMap.MapEntityUtSpawn)
				{
					string g_gametypes = entity.GetValue(BspMap.MapEntityKeyGametype);
					if (string.IsNullOrEmpty(g_gametypes) == false)
					{
						string[] parts = g_gametypes.Split(',');
						for (int i = 0; i < parts.Length; i++)
						{
							parts[i] = parts[i].Trim();
							GameTypes gameType = (GameTypes)Enum.Parse(typeof(GameTypes), parts[i]);
							if (RetVal.Contains(gameType) == false)
								RetVal.Add(gameType);
						}
					}
				}
				else if (classname == BspMap.MapEntityBombSite)
				{
					bombSites++;
				}
				else if (classname == BspMap.MapEntityCAHPoit)
				{
					capturePoints++;
				}
				else if (classname == BspMap.MapEntityQuakeSpawn)
				{
					infoPlayerStart++;
				}
				else if (classname == BspMap.MapEntityQuakeSpawnDeathmatch)
				{
					infoPlayerStart++;
				}
				else if (classname == BspMap.MapEntityBlueFlag)
				{
					blueflag++;
				}
				else if (classname == BspMap.MapEntityRedFlag)
				{
					redflag++;
				}
			}

			if (bombSites < 2)
			{
				RetVal.Remove(GameTypes.ut_bomb);
			}
			if (capturePoints < 1)
			{
				RetVal.Remove(GameTypes.ut_cah);
			}
			if (infoPlayerStart > 0)
			{
				if (RetVal.Contains(GameTypes.ut_ffa) == false)
					RetVal.Add(GameTypes.ut_ffa);
			}
			if (redflag < 1)
			{
				RetVal.Remove(GameTypes.ut_ctf);
			}
			if (blueflag < 1)
			{
				RetVal.Remove(GameTypes.ut_ctf);
			}
			return RetVal;
		}

		#region Structs
		struct Header
		{
			public string Magic;
			public int Version;
			public Lump[] LumpDirectories;
		}

		struct Lump
		{
			public int Offset;
			public int Length;
		}

		struct TexturesEntry
		{
			public string Name;		// Length = 64
			public SurfaceParms Flags;		// Surface Flags
			public int Contents;	// Content Flags
		}

		struct EffectsEntry
		{
			public string Name;	// Effect shader.
			public int Brush;	// Brush that generated this effect.
			public int Unknown;	// Always 5, except in q3dm8, which has one effect with -1.
		}
		#endregion

		#region Enums
		public enum Lumps : int
		{
			Entities = 0,	// Game-related object descriptions.
			Textures,		// Surface descriptions.
			Planes,			// Planes used by map geometry.
			Nodes,			// BSP tree nodes.
			Leafs,			// BSP tree leaves.
			Leaffaces,		// Lists of face indices, one list per leaf.
			Leafbrushes,	// Lists of brush indices, one list per leaf.
			Models,			// Descriptions of rigid world geometry in map.
			Brushes,		// Convex polyhedra used to describe solid space.
			Brushsides,		// Brush surfaces.
			Vertexes,		// Vertices used to describe faces.
			Meshverts,		// Lists of offsets, one list per mesh.
			Effects,		// List of special map effects.
			Faces,			// Surface geometry.
			Lightmaps,		// Packed lightmap data.
			Lightvols,		// Local illumination data.
			Visdata,		// Cluster-cluster visibility data.
		}
		#endregion
	}
}
