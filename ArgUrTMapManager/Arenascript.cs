using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager
{
	class Arenascript
	{
		public string map { get; set; }
		public string longname { get; set; }
		public GameTypes[] type { get; set; }

		public void Save(Stream target)
		{
			StreamWriter writer = new StreamWriter(target);
			writer.WriteLine("{");
			writer.WriteLine("map \"" + this.map + '\"');
			writer.WriteLine("longname \"" + this.longname + '\"');
			string types = string.Empty;
			foreach (GameTypes t in this.type)
			{
				types += t.ToString() + ' ';
			}
			writer.WriteLine("type \"" + types.TrimEnd(' ') + '\"');
			writer.WriteLine("}");
			writer.Flush();
		}
	}
}
