using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using ArgusLib.Collections;

namespace ArgUrTMapManager
{
	public class MapExtractOptions : XmlDataBase
	{
		#region Enums
		public enum Images
		{
			CopyAll,
			ConvertTgaToPng,
			ConvertPngToTga
		}

		public enum Sounds
		{
			CopyAll,
			RemoveAll,
			RemoveOgg
		}

		public enum BspVersion
		{
			Copy,
			Quake3 = 46,
			QuakeLive = 47
		}

		public enum Entities
		{
			Copy,
			UseConversionFile,
			Import
		}

		[Flags]
		public enum BspcSwitch
		{
			None = 0,
			bsp2aas = 1,
			reach = 1 << 1,
			cluster = 1 << 2,
			aasopt = 1 << 3,
			output = 1 << 4,
			threads = 1 << 5,
			cfg = 1 << 6,
			optimize = 1 << 7,
			noverbose = 1 << 8,
			breadthfirst = 1 << 9,
			nobrushmerge = 1 << 10,
			noliquids = 1 << 11,
			freetree = 1 << 12,
			nocsg = 1 << 13,
			forcesidesvisible = 1 << 14,
			grapplereach = 1 << 15
		}

		[XmlType("CompileAasCondition")]
		public enum _CompileAasCondition
		{
			Never,
			IfAasIsMissing,
			Always
		}

		//public enum Various
		//{
		//	None,
		//	ApplyNoFallingDamage
		//}
		#endregion

		public Images ImagesOptions { get; set; }
		public Sounds SoundsOptions { get; set; }
		public BspVersion BspVersionOptions { get; set; }
		public Entities EntitiesOptions { get; set; }
		public string ConversionFile { get; set; }
		public bool ExtractBotfile { get; set; }
		public BspcSwitch BspcSwitches { get; set; }
		public _CompileAasCondition CompileAasCondition { get; set; }
		public string AasCompilerFile { get; set; }
		public bool CompileAasTryReachFirst { get; set; }
		public int CompileAasThreads { get; set; }
		public string ReadMeFilename { get; set; }
		public string ReadMeText { get; set; }
		public SurfaceParms SurfaceParms { get; set; }

		public MapExtractOptions()
			: base()
		{
			this.ImagesOptions = Images.CopyAll;
			this.SoundsOptions = Sounds.CopyAll;
			this.BspVersionOptions = BspVersion.Copy;
			this.EntitiesOptions = Entities.Copy;
			this.ConversionFile = null;
			this.ExtractBotfile = true;
			this.BspcSwitches = BspcSwitch.None;
			this.CompileAasCondition = _CompileAasCondition.Never;
			this.AasCompilerFile = null;
			this.CompileAasTryReachFirst = false;
			this.CompileAasThreads = 2;
			this.ReadMeFilename = null;
			this.ReadMeText = null;
			this.SurfaceParms = ArgUrTMapManager.SurfaceParms.None;
		}

		public void Save()
		{
			this.Save(FilesystemEntries.MapExtractOptionsFile);
		}

		public static MapExtractOptions Load()
		{
			MapExtractOptions options = Load<MapExtractOptions>(FilesystemEntries.MapExtractOptionsFile);
			if (options == null)
				return new MapExtractOptions();
			return options;
		}

		public override void Dispose()
		{
		}
	}
}
