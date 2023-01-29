using System;
using System.Collections.Generic;
using System.Text;

namespace ArgUrTMapManager
{
	static class FileExtensions
	{
		public const string Packages = ".pk3";
		public const string Maps = ".bsp";
		public const string BotMaps = ".aas";
		public const string ConfigFiles = ".cfg";
		public const string ShaderscriptFiles = ".shader";
		public const string ArenascriptFiles = ".arena";
		public const string Xml = ".xml";

		public static readonly string[] Textures = new string[] { ".jpg", ".png", ".tga" };
		public static readonly string[] Models = new string[] { ".md3" };
		public static readonly string[] Sounds = new string[] { ".wav", ".ogg" };
#if Mono
		public const string Executables = "";
#else
		public const string Executables = ".exe";
#endif
	}
}
