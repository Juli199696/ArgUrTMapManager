using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ArgUrTMapManager
{
	class EnsureDirectoryExistsAttribute : Attribute
	{
		public EnsureDirectoryExistsAttribute() { }
	}

	static class FilesystemEntries
	{
		public static string GameDirectory
		{
			get
			{
#if DEBUG
				return @"C:\Games\Urban Terror Server";
				//return @"C:\Games\World of Padman";
				//return @"C:\Games\Smokin' Guns";
				//return @"C:\Games\Open Arena";
				//return Environment.CurrentDirectory;
				//return @"C:\Games\Wolfenstein - Enemy Territory";
				//return @"C:\Games\ZEQ2 Lite";
#else
				return Environment.CurrentDirectory;
#endif
			}
		}
		public static string GameDataDirectory { get; private set; }
		public static string QuakeLiveDirectory { get; private set; }
		public static string QuakeLiveDataDirectory { get { return Path.Combine(QuakeLiveDirectory, "baseq3"); } }

		[EnsureDirectoryExists()]
		public static string MainDirectory { get { return Path.Combine(GameDirectory, "ArgUrTMapManager"); } }
		[EnsureDirectoryExists()]
		public static string CacheDirectory { get { return Path.Combine(MainDirectory, "cache"); } }
		[EnsureDirectoryExists()]
		public static string LibsDirectory { get { return Path.Combine(MainDirectory, "libs"); } }
		public static string TempDirectory { get { return Path.Combine(MainDirectory, "temp"); } }
		[EnsureDirectoryExists()]
		public static string DataDirectory { get { return Path.Combine(MainDirectory, "data"); } }
		[EnsureDirectoryExists()]
		public static string EntityConversionFilesDirectory { get { return Path.Combine(MainDirectory, "Entity Conversion Files"); } }
		[EnsureDirectoryExists()]
		public static string ExtractedEntitiesDirectory { get { return Path.Combine(MainDirectory, "Extracted Entities"); } }

		public static string CrashLogFile { get { return Path.Combine(MainDirectory, "CrashLogFile.txt"); } }

		// Data
		public static string MapExtractOptionsFile { get { return Path.Combine(DataDirectory, "MapExtractOptions.xml"); } }
		public static string SettingsFile { get { return Path.Combine(DataDirectory, "Settings.xml"); } }

		// Cache
		public static string CheckedPackagesFile { get { return Path.Combine(CacheDirectory, "CheckedPackages.xml"); } }
		public static string MapsCacheFile { get { return Path.Combine(CacheDirectory, "Maps.xml"); } }
		public static string ModelsCacheFile { get { return Path.Combine(CacheDirectory, "Models.xml"); } }
		public static string ShaderscriptEntriesCacheFile { get { return Path.Combine(CacheDirectory, "ShaderscriptEntries.xml"); } }
		public static string ShaderscriptsCacheFile { get { return Path.Combine(CacheDirectory, "Shaderscripts.xml"); } }
		public static string SoundsCacheFile { get { return Path.Combine(CacheDirectory, "Sounds.xml"); } }
		public static string TexturesCacheFile { get { return Path.Combine(CacheDirectory, "Textures.xml"); } }

		public static bool Initialize()
		{
			FilesystemEntries.EnsureDirectoriesExist();
			GameDataDirectory = FilesystemEntries.GetGameDataDirectory();
			if (GameDataDirectory == null)
				return false;
			QuakeLiveDirectory = FilesystemEntries.GetQuakeLiveDirectory();
			FilesystemEntries.CopyExamples();
			return true;
		}

		static void EnsureDirectoriesExist()
		{
			Type type = typeof(FilesystemEntries);
			PropertyInfo[] pis = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo pi in pis)
			{
				object[] attr = pi.GetCustomAttributes(typeof(EnsureDirectoryExistsAttribute), false);
				if (attr.Length < 1)
					continue;
				string path = (string)pi.GetValue(null, null);
				if (Directory.Exists(path) == false)
					Directory.CreateDirectory(path);
			}
		}

		[System.Diagnostics.Conditional("DEBUG")]
		static void CopyExamples()
		{
			string sourceDir = @".\ArgUrTMapManager\Entity Conversion Files";
			string sourcePath = Path.Combine(sourceDir, "wop2UrT example.xml");
			string targetPath = Path.Combine(EntityConversionFilesDirectory, "wop2UrT example.xml");
			File.Copy(sourcePath, targetPath, true);

			sourceDir = @".\ArgUrTMapManager\libs";
			sourcePath = Path.Combine(sourceDir, "bspc.exe");
			targetPath = Path.Combine(LibsDirectory, "bspc.exe");
			File.Copy(sourcePath, targetPath, true);

			sourceDir = @".\ArgUrTMapManager\libs";
			sourcePath = Path.Combine(sourceDir, "bspcwop.exe");
			targetPath = Path.Combine(LibsDirectory, "bspcwop.exe");
			File.Copy(sourcePath, targetPath, true);

			sourceDir = @".\ArgUrTMapManager\libs";
			sourcePath = Path.Combine(sourceDir, "libexpat.dll");
			targetPath = Path.Combine(LibsDirectory, "libexpat.dll");
			File.Copy(sourcePath, targetPath, true);
		}

		static string GetGameDataDirectory()
		{
			string[] dirs = Directory.GetDirectories(GameDirectory);
			foreach (string dir in dirs)
			{
				string[] files = Directory.GetFiles(dir, "*" + FileExtensions.Packages);
				if (files.Length > 0)
				{
					return dir;
				}
			}
			return null;
		}

		static string GetQuakeLiveDirectory()
		{
#if Mono
			string path = "~/.quakelive/quakelive/home";
			if (Directory.Exists(path) == false)
				path = @"~/Library/Application\ Support/Quakelive";
			if (Directory.Exists(path) == true)
				return path;
			return null;
#else
			string appDataLow = FilesystemEntries.GetKnownFolderPath(KnownFolderId.LocalAppDataLow);
			if (appDataLow == null)
				return null;
			string path = Path.Combine(appDataLow, @"id Software\quakelive");
			if (Directory.Exists(path) == false)
				return null;
			return path;
#endif
		}

		#region GetKnownFolderPath
		[DllImport("Shell32.dll")]
		static extern int SHGetKnownFolderPath(
			[MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
			uint dwFlags,
			IntPtr hToken,
			out IntPtr pszPath);

		static string GetKnownFolderPath(Guid FolderId)
		{
			IntPtr pszPath = IntPtr.Zero;
			int error = -1;
			try
			{
				error = SHGetKnownFolderPath(FolderId, 0, IntPtr.Zero, out pszPath);
			}
			catch
			{
				return null;
			}
			finally
			{
				if (pszPath != IntPtr.Zero)
					Marshal.FreeCoTaskMem(pszPath);
			}
			if (error >= 0)
				return Marshal.PtrToStringAuto(pszPath);
			throw Marshal.GetExceptionForHR(error);
		}

		static class KnownFolderId
		{
			public static readonly Guid LocalAppDataLow = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
		}
		#endregion
	}
}
