using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ArgUrTMapManager
{
	public abstract class XmlDataBase : IDisposable
	{
		internal void Save(string Filename)
		{
			using (FileStream file = new FileStream(Filename, FileMode.Create))
			{
				XmlSerializer xs = new XmlSerializer(this.GetType());
				xs.Serialize(file, this);
				file.Close();
			}
		}

		internal static T Load<T>(string Filename)
			where T:XmlDataBase
		{
			if (File.Exists(Filename) == false)
				return null;

			using (FileStream file = new FileStream(Filename, FileMode.Open))
			{
				XmlReader xmlReader = XmlReader.Create(file);
				XmlSerializer xs = new XmlSerializer(typeof(T));
				if (xs.CanDeserialize(xmlReader) == false)
				{
					File.Delete(Filename);
					return null;
				}
				file.Seek(0, SeekOrigin.Begin);
				T data = (T)xs.Deserialize(file);
				file.Close();
				return data;
			}
		}

		public abstract void Dispose();
	}
}
