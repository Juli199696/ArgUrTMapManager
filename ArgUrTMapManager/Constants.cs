using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ArgUrTMapManager
{
	static class Constants
	{
		public const string AboutHomepage = "http://argurtmapmanager.codeplex.com/";
		// File Extensions

		// Zip Directories
		public const string ZipDirMaps = "maps/";
		public const string ZipDirLevelshots = "levelshots/";
		public const string ZipDirScripts = "scripts/";

		// Game CVars / Commands
		public const string CVarSet = "set";
		public const string CVarGear = "g_gear";
		public const string CVarAllowvote = "g_allowVote";
		public const string CVarMapcycle = "g_mapcycle";
		public const string CVarMap = "map";
		public const string CVarExec = "exec";

		public static string CombineCVars(params string[] cvars)
		{
			string text = cvars[0];
			for (int i = 1; i < cvars.Length; i++)
			{
				text += ' ' + cvars[i];
			}
			return text;
		}
	}
}
