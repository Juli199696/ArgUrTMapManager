using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ArgusLib.Collections;

namespace ArgUrTMapManager
{
	static class FileIO
	{
		public static void Save(string filename, IEnumerable<MapEntity> mapEntities)
		{
			ItemPair<string, string>[][] saveVal;
			{
				List<ItemPair<string, string>[]> listEntities = new List<ItemPair<string, string>[]>();

				foreach (MapEntity entity in mapEntities)
				{
					ListPair<string, string> temp = entity.KeyValueList;
					temp.Insert(0, new ItemPair<string, string>("classname", entity.Classname));
					listEntities.Add(temp.ToArray());
				}
				saveVal = listEntities.ToArray();
			}

			using (FileStream file = new FileStream(filename, FileMode.Create))
			{
				XmlSerializer xs = new XmlSerializer(typeof(ItemPair<string, string>[][]));
				xs.Serialize(file, saveVal);
			}
		}

		public static MapEntity[] LoadMapEntities(string filename)
		{
			ItemPair<string, string>[][] loadVal;
			using (FileStream file = new FileStream(filename, FileMode.Open))
			{
				XmlReader xmlReader = XmlReader.Create(file);
				XmlSerializer xs = new XmlSerializer(typeof(ItemPair<string, string>[][]));
				if (xs.CanDeserialize(xmlReader) == false)
					return null;
				file.Seek(0, SeekOrigin.Begin);
				loadVal = (ItemPair<string, string>[][])xs.Deserialize(file);
			}
			MapEntity[] RetVal = new MapEntity[loadVal.Length];
			for (int i = 0; i < RetVal.Length; i++)
			{
				RetVal[i] = new MapEntity(loadVal[i]);
			}
			return RetVal;
		}
	}
}
