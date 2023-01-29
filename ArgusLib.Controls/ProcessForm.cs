using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A simple <see cref="Form"/> similar to a console window. It runs a <see cref="Process"/> and displays
	/// its standard error and standard output and takes input from the user.
	/// If the form is closed, the process is terminated.
	/// </summary>
	public partial class ProcessForm : BaseForm, IDisposable
	{
		Process process;
		bool processExited = false;
		DateTime lastOutputReceived;
		List<string> buffer;

		/// <summary>
		/// Gets or sets a <see cref="int"/> specifying the time in milliseconds between
		/// the output is refreshed.
		/// The default value is 50.
		/// </summary>
		public int TimeBetweenOutputs { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the form should automatically close when the process exits.
		/// The default is true.
		/// </summary>
		public bool CloseWhenProcessExits { get; set; }

		/// <summary>
		/// Gets a value indicating whether the underlying <see cref="Process"/> has already exited.
		/// </summary>
		public bool ProcessHasExited
		{
			get { return this.processExited; }
		}

		/// <summary>
		/// Occurs when the underlying <see cref="Process"/> exits.
		/// </summary>
		public event EventHandler ProcessExited;

		internal ProcessForm()
			: base()
		{
			InitializeComponent();
			this.buffer = new List<string>();
			this.CloseWhenProcessExits = true;
			this.lastOutputReceived = new DateTime(0);
			this.TimeBetweenOutputs = 50;
		}

		/// <summary>
		/// Creates a new <see cref="ProcessForm"/> instances which will run the <see cref="Process"/>
		/// specified by <paramref name="psi"/>.
		/// </summary>
		/// <param name="psi">A <see cref="ProcessStartInfo"/>.</param>
		/// <remarks>
		/// The underlying <see cref="Process"/> will use <see cref="Process.StartInfo.RedirectStandardError"/> = true,
		/// <see cref="Process.StartInfo.RedirectStandardInput"/> = true, <see cref="Process.StartInfo.RedirectStandardOutput"/>
		/// and <see cref="Process.StartInfo.UseShellExecute"/> = false. Therefor some properties like <see cref="Process.StartInfo.WorkingDirectory"/>
		/// or <see cref="Process.StartInfo.FileName"/> may behave differently. See <see cref="Process.StartInfo.RedirectStandardError"/>,
		/// <see cref="Process.StartInfo.RedirectStandardInput"/>, <see cref="Process.StartInfo.RedirectStandardOutput"/> and
		/// <see cref="Process.StartInfo.UseShellExecute"/> for more detailed information.
		/// </remarks>
		public ProcessForm(ProcessStartInfo psi)
			: this()
		{
			this.Text = psi.FileName;
			psi.UseShellExecute = false;
			psi.RedirectStandardError = true;
			psi.RedirectStandardInput = true;
			psi.RedirectStandardOutput = true;
			this.process = new Process();
			this.process.StartInfo = psi;
			this.process.EnableRaisingEvents = true;
			this.process.ErrorDataReceived += new DataReceivedEventHandler(this.Process_OutputDataReceived);
			this.process.OutputDataReceived += new DataReceivedEventHandler(this.Process_OutputDataReceived);
			this.process.Exited += new EventHandler(this.process_Exited);
		}

		/// <summary>
		/// Starts the underlying <see cref="Process"/> and shows the Formwindow.
		/// Each instance of <see cref="ProcessForm"/> can only be started once.
		/// </summary>
		/// <param name="modal">A <see cref="Boolean"/> indicating wheter the <see cref="ProcessForm"/> should be shown as modal dialog.</param>
		public void Start(bool modal)
		{
			if (this.processExited == true)
				throw new Exception("Create a new instance, object already disposed.");
			this.process.Start();
			this.process.BeginErrorReadLine();
			this.process.BeginOutputReadLine();

			if (modal == true)
				base.ShowDialog();
			else
				base.Show();
		}

		/// <summary>
		/// Calls <see cref="Process.WaitForExit()"/> on the underlying <see cref="Process"/>.
		/// </summary>
		public void WaitForExit()
		{
			this.process.WaitForExit();
		}

		/// <summary>
		/// Calls <see cref="Process.WaitForExit(int)"/> on the underlying <see cref="Process"/>.
		/// </summary>
		public bool WaitForExit(int ms)
		{
			return this.process.WaitForExit(ms);
		}

		/// <summary>
		/// Not supported. Use <see cref="ProcessForm.Start()"/> instead.
		/// </summary>
		public new void Show()
		{
			throw new NotImplementedException("Use ProcessForm.Start() instead.");
		}

		/// <summary>
		/// Not supported. Use <see cref="ProcessForm.Start()"/> instead.
		/// </summary>
		public new void ShowDialog()
		{
			throw new NotImplementedException("Use ProcessForm.Start() instead.");
		}

		void process_Exited(object sender, EventArgs e)
		{
			this.processExited = true;
			if (this.CloseWhenProcessExits == true)
			{
				if (this.InvokeRequired == true)
				{
					Action<object> closeAction = (a) => { this.Close(); };
					this.Invoke(closeAction, new object());
				}
				else
				{
					this.Close();
				}
			}
			if (this.ProcessExited != null)
				this.ProcessExited(this, EventArgs.Empty);
		}
		/// <summary>
		/// See <see cref="Form.OnFormClosing"/>.
		/// </summary>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (this.processExited == false)
			{
				e.Cancel = true;
				if (this.process.CloseMainWindow() == false)
					this.process.Kill();
			}
			else
			{
				this.Dispose();
			}
			base.OnFormClosing(e);
		}

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			if (this.IsShown == false)
				return;

			//this.outputBox.InvokeOutput(e.Data);

			TimeSpan interval = DateTime.Now - this.lastOutputReceived;
			if (interval.TotalMilliseconds < this.TimeBetweenOutputs)
			{
				buffer.Add(e.Data);
			}
			else
			{
				string text = string.Empty;
				for (int i = 0; i < buffer.Count; i++)
				{
					text += buffer[i] + Environment.NewLine;
				}
				text += e.Data;
				this.outputBox.InvokeOutput(text);
				buffer.Clear();
				this.lastOutputReceived = DateTime.Now;
			}
		}

		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter)
				return;

			this.outputBox.Output(">> " + this.inputBox.Text);
			this.process.StandardInput.WriteLine(this.inputBox.Text);
			this.inputBox.Text = string.Empty;
		}

		/// <summary>
		/// Calls <see cref="Component.Dispose()"/> on the underlying <see cref="Process"/>.
		/// </summary>
		void IDisposable.Dispose()
		{
			this.process.Dispose();
		}
	}
}
