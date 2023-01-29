using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace ArgUrTMapManager
{
	public class Settings : XmlDataBase
	{
		public static Settings Default { get { return new Settings(); } }

		#region Settings
		public Rectangle BoundsMainForm = new Rectangle(0, 0, 0, 0);
		public Rectangle BoundsMapcycleForm = new Rectangle(0, 0, 0, 0);
		public Rectangle BoundsLevelshotForm = new Rectangle(0, 0, 0, 0);
		public Rectangle BoundsSettingsForm = new Rectangle(0, 0, 0, 0);
		public Rectangle BoundsGearCalculatorForm = new Rectangle(0, 0, 0, 0);
		public ulong MapExtractOptions = 0;
		public string NameGameBinary = string.Empty;
		public string NameServerBinary = string.Empty;
		//public string NameDataDirectory = string.Empty;
		public string NameMapcycleFile = "ArgUrTMapcycle.txt";
		public string NameServerConfig = string.Empty;
		public bool sv_pure = true;
		#endregion

		internal static Settings Load()
		{
			Settings settings = Load<Settings>(FilesystemEntries.SettingsFile);
			if (settings == null)
				return Settings.Default;
			return settings;
		}

		internal void Save()
		{
			this.Save(FilesystemEntries.SettingsFile);
		}

		public override void Dispose()
		{
		}
	}
}
