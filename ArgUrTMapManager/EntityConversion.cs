using System;
using System.Collections.Generic;
using ArgusLib.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using Vector3 = ArgusLib.Math.Vector3;

namespace ArgUrTMapManager
{
	public class EntityConversion : XmlDataBase
	{
		public _EntityGrouping[] EntityGroupings { get; set; }
		public _EntityReplacement[] EntityReplacements { get; set; }

		public override void Dispose()
		{
			this.EntityReplacements = null;
		}

		internal List<MapEntity> Convert(IEnumerable<MapEntity> entities)
		{
			List<MapEntity> RetVal = new List<MapEntity>();
			List<string> classnamesReplacements = this.GetOriginalClassnamesReplacements();

			/// Item1 [int]: The index of the _EntityGrouping in which the classname was found.
			/// Item2 [int]: The index of the _OriginalEntity in this.EntityGroupings[Item1].OriginalEntities
			///	which defines the classname.
			///	Item3 [string]: The original classname in lower case.
			ListTriple<int, int, string> classnamesGroups = this.GetOriginalClassnamesGroups();

			List<MapEntity>[] groupEntities = new List<MapEntity>[this.EntityGroupings.Length];

			foreach (MapEntity entity in entities)
			{
				string entityClassname = entity.Classname.ToLowerInvariant();

				int index = classnamesGroups.List3.IndexOf(entityClassname);

				if (index > -1)
				{
					ItemTriple<int, int, string> item = classnamesGroups[index];
					if (groupEntities[item.Item1] == null)
						groupEntities[item.Item1] = new List<MapEntity>();

					List<string> keysToRemove = this.GetKeysToRemove(this.EntityGroupings[item.Item1].OriginalEntities[item.Item2]);
					groupEntities[item.Item1].Add(RemoveKeys(entity, keysToRemove));
					continue;
				}

				index = classnamesReplacements.IndexOf(entityClassname);
				if (index < 0)
				{
					RetVal.Add(entity);
					continue;
				}

				{
					_EntityReplacement def = this.EntityReplacements[index];

					if (def.NewEntities == null || def.NewEntities.Length < 1)
						continue;

					List<string> keysToRemove = this.GetKeysToRemove(def.OriginalEntity);
					List<ItemPair<string, string>> keyValuePairsCopy = new List<ItemPair<string, string>>();
					foreach (ItemPair<string, string> keyvaluepair in entity.KeyValueList)
					{
						if (keysToRemove.Contains(keyvaluepair.Item1) == false)
							keyValuePairsCopy.Add(keyvaluepair);
					}

					foreach (_NewEntity newEntityDef in def.NewEntities)
					{
						MapEntity temp = new MapEntity(newEntityDef.Classname);
						temp.AddRange(keyValuePairsCopy);
						temp.AddRange(newEntityDef.KeyValuePairsToAdd);
						RetVal.Add(temp);
					}
				}
			}

			for (int groupingNo = 0; groupingNo < this.EntityGroupings.Length; groupingNo++)
			{
				_EntityGrouping grouping = this.EntityGroupings[groupingNo];
				List<MapEntity> groupingEntities = groupEntities[groupingNo];

				if (grouping.GroupingMethod == _GroupingMethod.GroupNearest)
				{
					if (groupingEntities.Count < 2 * grouping.NewGroups.Length)
					{
						RetVal.AddRange(groupingEntities);
						continue;
					}

					List<int>[] entityIndices = GroupNearest(groupingEntities, grouping.NewGroups.Length);
					for (int groupNo = 0; groupNo < entityIndices.Length; groupNo++)
					{
						_NewGroup newGroup = grouping.NewGroups[groupNo];
						foreach (int index in entityIndices[groupNo])
						{
							MapEntity entity = groupingEntities[index];
							foreach (_NewEntity newEntity in newGroup.NewEntites)
							{
								MapEntity temp = new MapEntity(newEntity.Classname);
								temp.AddRange(entity.KeyValueList);
								temp.AddRange(newEntity.KeyValuePairsToAdd);
								RetVal.Add(temp);
							}
						}
					}
				}
			}

			return RetVal;
		}

		/// <summary>
		/// Groups the <see cref="MapEntity"/>'s in <paramref name=" entities"/> into <paramref name="numberOfGroups"/>
		/// groups with the condition, that the elements of one group are as near as possible.
		/// </summary>
		/// <returns>
		/// An array of <see cref="List<int>"/>, one list for each group, containig the indices of the groupmembers in
		/// <paramref name="entites"/>.
		/// </returns>
		private static List<int>[] GroupNearest(IEnumerable<MapEntity> entities, int numberOfGroups)
		{
			if (numberOfGroups < 2)
				throw new ArgumentException("There must be at least 2 groups.", "numberOfGroups");

			List<Vector3> vectors = new List<Vector3>();
			foreach (MapEntity entity in entities)
			{
				string origin = entity.GetValue("origin");
				if (origin == null)
					continue;
				string[] originK = origin.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (originK.Length != 3)
					continue;
				double x, y, z;
				if (double.TryParse(originK[0], out x) == false)
					continue;
				if (double.TryParse(originK[1], out y) == false)
					continue;
				if (double.TryParse(originK[2], out z) == false)
					continue;

				//vectors.Add(new Vector3(x,y,0));
				vectors.Add(new Vector3(x, y, z));
			}

			List<int>[] RetVal = new List<int>[numberOfGroups];
			int countPerGroup = vectors.Count / numberOfGroups;
			List<Vector3> tempVectors = new List<Vector3>(vectors);
			for (int groupNo = 0; groupNo < numberOfGroups - 1; groupNo++)
			{
				RetVal[groupNo] = new List<int>();

				//if (tempVectors.Count < 2)
				//{
				//	break;
				//}

				int index1, index2;
				Vector3.GetGreatestDistance(tempVectors, out index1, out index2);
				Vector3 p = tempVectors[index1];
				tempVectors.RemoveAt(index1);
				RetVal[groupNo].Add(vectors.IndexOf(p));
				List<int> indices;
				Vector3.GetSmallestDistance(p, tempVectors, out indices);
				for (int i = 0; i < countPerGroup - 1; i++)
				{
					RetVal[groupNo].Add(vectors.IndexOf(tempVectors[indices[i]]));
				}
				{
					List<Vector3> temp = new List<Vector3>();
					for (int i = 0; i < tempVectors.Count; i++)
					{
						int e = indices.IndexOf(i);
						if (e > -1 && e < countPerGroup - 1)
							continue;
						temp.Add(tempVectors[i]);
					}
					tempVectors = temp;
				}
			}
			RetVal[numberOfGroups - 1] = new List<int>();
			foreach (Vector3 v in tempVectors)
			{
				RetVal[numberOfGroups - 1].Add(vectors.IndexOf(v));
			}

#if false // DEBUG // Draw image to visualize the grouping
			{
				double xMin = double.PositiveInfinity;
				double yMin = double.PositiveInfinity;
				double xMax = double.NegativeInfinity;
				double yMax = double.NegativeInfinity;
				int imWidth = 800;
				int imHeight = 600;
				foreach (Vector3 v in vectors)
				{
					if (v.X < xMin)
						xMin = v.X;
					if (v.Y < yMin)
						yMin = v.Y;
					if (v.X > xMax)
						xMax = v.X;
					if (v.Y > yMax)
						yMax = v.Y;
				}

				Vector3 trans = new Vector3(xMin, yMin, 0);
				float scale = imWidth * 0.9f / (float)(xMax - xMin);
				scale = Math.Min(scale, imHeight * 0.9f / (float)(yMax - yMin));
				using (Bitmap bitmap = new Bitmap(imWidth, imHeight))
				{
					using (Graphics g = Graphics.FromImage(bitmap))
					{
						g.Clear(Color.Black);
						g.DrawRectangle(
							new Pen(Color.White, 2),
							imWidth * 0.05f, imHeight * 0.05f,
							(float)(xMax - xMin) * scale, (float)(yMax - yMin) * scale);

						for (int groupNo = 0; groupNo < RetVal.Length; groupNo++)
						{
							Color color = Color.FromArgb(255, Color.FromArgb(0x00ffffff / RetVal.Length * (groupNo + 1)));
							Brush brush = new SolidBrush(color);
							float radius = 10;

							foreach (int index in RetVal[groupNo])
							{
								Vector3 v = vectors[index];
								v -= trans;
								v *= scale;
								float x = (float)(v.X-radius+0.05*imWidth);
								float y = (float)(v.Y-radius+0.05*imHeight);
								g.FillEllipse(brush, x, y, 2 * radius, 2 * radius);
							}
						}
					}
					bitmap.Save("Test Entity Grouping.png", System.Drawing.Imaging.ImageFormat.Png);
				}
			}
#endif

			return RetVal;
		}

		/// <summary>
		/// Returns the MapEntity with the KeyValuePairs specified by the Keys in <paramref name="KeysToRemove"/>
		/// removed.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="KeysToRemove">List of the Keys to remove. Keys are expected to be in lower case.</param>
		/// <returns></returns>
		private static MapEntity RemoveKeys(MapEntity entity, List<string> KeysToRemove)
		{
			MapEntity RetVal = new MapEntity(entity.Classname);
			foreach (ItemPair<string,string> keyValuePair in entity.KeyValueList)
			{
				if (KeysToRemove.Contains(keyValuePair.Item1) == true)
					continue;
				RetVal.Add(keyValuePair);
			}
			return RetVal;
		}

		private List<string> GetOriginalClassnamesReplacements()
		{
			List<string> RetVal = new List<string>(this.EntityReplacements.Length);
			for (int i = 0; i < this.EntityReplacements.Length; i++)
			{
				RetVal.Add(this.EntityReplacements[i].OriginalEntity.Classname.ToLowerInvariant());
			}
			return RetVal;
		}

		/// <summary>
		/// Get all orignal classnames in this.EntityGroupings.
		/// </summary>
		/// <returns>
		/// A <see cref="ListTriple<int,int,string>"/>.
		/// Item1 [int]: The index of the <see cref="_EntityGrouping"/> in which the classname was found.
		/// Item2 [int]: The index of the <see cref="_OriginalEntity"/> in this.EntityGroupings[Item1].OriginalEntities
		///	which defines the classname.
		///	Item3 [string]: The original classname in lower case.
		/// </returns>
		private ListTriple<int, int, string> GetOriginalClassnamesGroups()
		{
			ListTriple<int, int, string> RetVal = new ListTriple<int, int, string>();
			for (int i = 0; i < this.EntityGroupings.Length; i++)
			{
				for (int j = 0; j < this.EntityGroupings[i].OriginalEntities.Length; j++)
				{
					RetVal.Add(new ItemTriple<int,int,string>(i, j, this.EntityGroupings[i].OriginalEntities[j].Classname));
				}
			}
			return RetVal;
		}

		private List<string> GetKeysToRemove(_OriginalEntity originalEntity)
		{
			List<string> RetVal = new List<string>(originalEntity.KeysToRemove.Length);
			for (int i = 0; i < originalEntity.KeysToRemove.Length; i++)
			{
				RetVal.Add(originalEntity.KeysToRemove[i].ToLowerInvariant());
			}
			return RetVal;
		}

		#region Subtypes

		[XmlType("EntityGrouping")]
		public class _EntityGrouping
		{
			[XmlElement("GroupingMethod")]
			public _GroupingMethod GroupingMethod { get; set; }

			[XmlArray("OriginalEntities")]
			[XmlArrayItem("OriginalEntity")]
			public _OriginalEntity[] OriginalEntities { get; set; }

			[XmlArray("Groups")]
			[XmlArrayItem("Group")]
			public _NewGroup[] NewGroups { get; set; }
		}

		[XmlType("EntityReplacement")]
		public class _EntityReplacement
		{
			[XmlElement("OriginalEntity")]
			public _OriginalEntity OriginalEntity { get; set; }

			[XmlArray("NewEntities")]
			[XmlArrayItem("Entity")]
			public _NewEntity[] NewEntities { get; set; }
		}

		[XmlType("OriginalEntity")]
		public class _OriginalEntity
		{
			public string Classname { get; set; }
			public string[] KeysToRemove { get; set; }
		}

		[XmlType("NewEntity")]
		public class _NewEntity
		{
			public string Classname { get; set; }
			public ItemPair<string, string>[] KeyValuePairsToAdd { get; set; }
		}

		[XmlType("NewGroup")]
		public class _NewGroup
		{
			[XmlArray("NewEntities")]
			[XmlArrayItem("Entity")]
			public _NewEntity[] NewEntites { get; set; }
		}

		[XmlType("GroupingMethod")]
		public enum _GroupingMethod
		{
			GroupNearest = 1
		}

		#endregion
	}
}
