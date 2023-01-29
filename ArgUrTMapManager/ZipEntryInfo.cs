using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace ArgUrTMapManager
{
	class ZipEntryInfo
	{
		public string SourceFile { get; private set; }
		public ZipEntry Entry { get; private set; }

		public ZipEntryInfo(string SourceFile, ZipEntry Entry)
		{
			this.SourceFile = SourceFile;
			this.Entry = Entry;
		}

		public FileStream OpenFile()
		{
			return new FileStream(this.SourceFile, FileMode.Open);
		}
	}
}
