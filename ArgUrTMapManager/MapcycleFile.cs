using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager
{
	static class MapcycleFile
	{
		public static void WriteFile(Stream Target, string[] Maps)
		{
			StreamWriter writer = new StreamWriter(Target);
			for (int i = 0; i < Maps.Length; i++)
			{
				writer.WriteLine(Maps[i]);
			}
			writer.Flush();
		}

		public static string[] ReadFile(Stream Source)
		{
			StreamReader reader = new StreamReader(Source);
			List<string> Maps = new List<string>();
			while (reader.EndOfStream == false)
			{
				Maps.Add(reader.ReadLine());
			}
			return Maps.ToArray();
		}
	}
}
