using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace ArgUrTMapManager
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if DEBUG == false
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			string logfile = FilesystemEntries.CrashLogFile;

			using (StreamWriter file = new StreamWriter(logfile, true))
			{
				DateTime now = DateTime.Now;
				string datetimestring = now.ToShortDateString()+" "+now.ToShortTimeString();
				file.WriteLine(datetimestring);
				file.WriteLine(new string('-', datetimestring.Length));
				file.Write(e.ExceptionObject.ToString());
				file.WriteLine();
				file.WriteLine();
				file.Close();
			}
			MessageBox.Show("The Program crashed.\nLogfile: " + logfile);
			if (e.IsTerminating == true)
				Application.ExitThread();
		}
	}
}
