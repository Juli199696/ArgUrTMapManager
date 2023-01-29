using System;
using System.Collections.Generic;
using System.Text;
using ArgusLib.Collections;
using MapEntityKeyValuePair = ArgusLib.Collections.ItemPair<string, string>;

namespace ArgUrTMapManager
{
	/// <summary>
	/// Classname and Keys are guaranteed to be in lower case.
	/// </summary>
	class MapEntity
	{
		const string KeyClassname = "classname";

		public string Classname { get; private set; }
		public ListPair<string, string> KeyValueList { get; private set; }

		public MapEntity(string classname)
		{
			this.Classname = classname.ToLowerInvariant();
			this.KeyValueList = new ListPair<string, string>();
		}

		public void Add(MapEntityKeyValuePair KeyValuePair)
		{
			KeyValuePair.Item1 = KeyValuePair.Item1.ToLowerInvariant();
			this.KeyValueList.Add(KeyValuePair);
		}

		public void AddRange(IEnumerable<MapEntityKeyValuePair> KeyValuePairs)
		{
			foreach (MapEntityKeyValuePair keyValuePair in KeyValuePairs)
			{
				this.Add(keyValuePair);
			}
		}

		public MapEntity(IEnumerable<MapEntityKeyValuePair> KeyValuePairs)
			: this(string.Empty)
		{
			foreach (MapEntityKeyValuePair keyValuePair in KeyValuePairs)
			{
				if (keyValuePair.Item1.ToLowerInvariant() == MapEntity.KeyClassname)
				{
					this.Classname = keyValuePair.Item2;
				}
				else
				{
					this.Add(keyValuePair);
				}
			}
		}

		public MapEntity(IEnumerable<string> KeyValuePairs)
			: this(string.Empty)
		{
			int i = 0;
			string key = string.Empty;
			foreach (string str in KeyValuePairs)
			{
				if (i % 2 == 0)
				{
					key = str.ToLowerInvariant();
				}
				else
				{
					if (key == MapEntity.KeyClassname)
					{
						this.Classname = str;
					}
					else
					{
						this.KeyValueList.Add(new MapEntityKeyValuePair(key, str));
					}
				}
				i++;
			}

			if (i % 2 != 0)
				throw new ArgumentException("Even number of elements expected (Key-Value-Pairs).", "KeyValuePairs");
		}

		public string GetValue(string Key)
		{
			int i = this.KeyValueList.List1.IndexOf(Key.ToLowerInvariant());
			if (i < 0)
				return null;
			return this.KeyValueList.List2[i];
		}

		public List<string> GetValues(IEnumerable<string> Keys)
		{
			List<string> RetVal = new List<string>();
			foreach (string key in Keys)
			{
				int i = this.KeyValueList.List1.IndexOf(key.ToLowerInvariant());
				if (i < 0)
					RetVal.Add(null);
				else
					RetVal.Add(this.KeyValueList.List2[i]);
			}
			return RetVal;
		}

		public static List<string> GetValues(IEnumerable<MapEntity> Entities, IEnumerable<string> Keys)
		{
			List<string> RetVal = new List<string>();
			foreach (MapEntity entity in Entities)
			{
				RetVal.AddRange(entity.GetValues(Keys));
			}
			return RetVal;
		}
	}
}
