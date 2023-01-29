using System;
using System.Collections.Generic;
using ArgusLib.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace ArgUrTMapManager
{
	public class DataArchiveCache :XmlDataBase
	{
		public ItemPair<string, DateTime>[] PackageFiles { get; set; }
		public bool FilesystemEntriesIncluded { get; set; }

		public DataArchiveCache()
			: base()
		{
			this.PackageFiles = new ItemPair<string, DateTime>[0];
			this.FilesystemEntriesIncluded = false;
		}

		public void Save()
		{
			this.Save(FilesystemEntries.CheckedPackagesFile);
		}

		public static DataArchiveCache Load()
		{
			DataArchiveCache cache = Load<DataArchiveCache>(FilesystemEntries.CheckedPackagesFile);
			if (cache == null)
				return new DataArchiveCache();
			return cache;
		}

		public override void Dispose()
		{
			this.PackageFiles = null;
		}

		public abstract class ArchiveEntries : XmlDataBase
		{
			public ItemPair<string,int>[] Entries { get; set; }

			public ArchiveEntries()
			{
				this.Entries = new ItemPair<string, int>[0];
			}

			public override void Dispose()
			{
				this.Entries = null;
			}
		}

		public abstract class ArchiveEntriesMultipleFileExt : XmlDataBase
		{
			public ItemTriple<string, string, int>[] Entries { get; set; }

			public ArchiveEntriesMultipleFileExt()
			{
				this.Entries = new ItemTriple<string,string,int>[0];
			}

			public override void Dispose()
			{
				this.Entries = null;
			}
		}

		public class ShaderscriptsCache : ArchiveEntries
		{
			public ShaderscriptsCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.ShaderscriptsCacheFile);
			}

			public static ShaderscriptsCache Load()
			{
				ShaderscriptsCache cache = Load<ShaderscriptsCache>(FilesystemEntries.ShaderscriptsCacheFile);
				if (cache == null)
					return new ShaderscriptsCache();
				return cache;
			}
		}

		public class ShaderscriptEntriesCache : ArchiveEntries
		{
			public ShaderscriptEntriesCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.ShaderscriptEntriesCacheFile);
			}

			public static ShaderscriptEntriesCache Load()
			{
				ShaderscriptEntriesCache cache = Load<ShaderscriptEntriesCache>(FilesystemEntries.ShaderscriptEntriesCacheFile);
				if (cache == null)
					return new ShaderscriptEntriesCache();
				return cache;
			}
		}

		public class ModelsCache : ArchiveEntriesMultipleFileExt
		{
			public ModelsCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.ModelsCacheFile);
			}

			public static ModelsCache Load()
			{
				ModelsCache cache = Load<ModelsCache>(FilesystemEntries.ModelsCacheFile);
				if (cache == null)
					return new ModelsCache();
				return cache;
			}
		}

		public class SoundsCache : ArchiveEntriesMultipleFileExt
		{
			public SoundsCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.SoundsCacheFile);
			}

			public static SoundsCache Load()
			{
				SoundsCache cache = Load<SoundsCache>(FilesystemEntries.SoundsCacheFile);
				if (cache == null)
					return new SoundsCache();
				return cache;
			}
		}

		public class TexturesCache : ArchiveEntriesMultipleFileExt
		{
			public TexturesCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.TexturesCacheFile);
			}

			public static TexturesCache Load()
			{
				TexturesCache cache = Load<TexturesCache>(FilesystemEntries.TexturesCacheFile);
				if (cache == null)
					return new TexturesCache();
				return cache;
			}
		}

		public class MapsCache : ArchiveEntries
		{
			public MapsCache()
				: base() { }

			public void Save()
			{
				this.Save(FilesystemEntries.MapsCacheFile);
			}

			public static MapsCache Load()
			{
				MapsCache cache = Load<MapsCache>(FilesystemEntries.MapsCacheFile);
				if (cache == null)
					return new MapsCache();
				return cache;
			}
		}
	}
}
