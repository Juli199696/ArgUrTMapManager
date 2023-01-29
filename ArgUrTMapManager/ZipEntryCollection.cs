using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;

namespace ArgUrTMapManager
{
	class ZipEntryCollection
	{
		public string SourceFile { get; private set; }
		public List<ZipEntry> Entries { get; private set; }

		public ZipEntryCollection(string SourceFile, List<ZipEntry> Entries)
		{
			this.SourceFile = SourceFile;
			this.Entries = Entries;
		}

		public ZipEntryCollection(string SourceFile)
			: this(SourceFile, new List<ZipEntry>())
		{
		}

		public ZipEntryInfo this[int i]
		{
			get
			{
				return new ZipEntryInfo(this.SourceFile, this.Entries[i]);
			}
		}
	}
}
