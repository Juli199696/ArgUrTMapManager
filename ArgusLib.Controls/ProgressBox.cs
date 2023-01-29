using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ArgusLib.Threading;

namespace ArgusLib.Controls
{
	public partial class ProgressBox : BaseForm
	{
		public ProgressBox()
		{
			InitializeComponent();
			this.lText.Text = (0.0).ToString("p");
		}

		public void SetProgress(double progress)
		{
			if (this.InvokeRequired == true)
				this.Invoke(new Action<double>(this.SetProgressHelper), progress);
			else
				this.SetProgressHelper(progress);
		}

		private void SetProgressHelper(double progress)
		{
			//if (this.IsShown == false)
			//	return;
			this.progressBar1.Value = this.progressBar1.Minimum + (int)(progress * (this.progressBar1.Maximum - this.progressBar1.Minimum));
			this.lText.Text = progress.ToString("p");
		}

		/// <summary>
		/// Executes <see cref="BackgroundWorker.RunWorkerAsync"/> and shows the progress in a modal
		/// <see cref="ProgressBox"/>. The ProgressBox is closed when the <see cref="BackgroundWorker.RunWorkerCompleted"/>-Event
		/// is raised.
		/// </summary>
		/// <param name="backgroundWorker"></param>
		/// <param name="argument"></param>
		public static void Show(BackgroundWorker backgroundWorker, object argument)
		{
			ProgressBox progressBox = new ProgressBox();
			backgroundWorker.ProgressChanged += (sender, e) =>
				{
					progressBox.SetProgressHelper(e.ProgressPercentage / 100.0);
				};
			backgroundWorker.RunWorkerCompleted += (sender, e) =>
				{
					if (progressBox.IsShown == true)
						progressBox.Close();
				};
			backgroundWorker.RunWorkerAsync(argument);
			progressBox.ShowDialog();
		}

		public static void Show(BackgroundWorker backgroundWorker)
		{
			ProgressBox.Show(backgroundWorker, null);
		}

		[RequiresAssembly(typeof(Parallelization))]
		public static void ShowForEach<T>(IEnumerable collection, Parallelization.ForEachBody<T> body)
		{
			ProgressBox progressBox = new ProgressBox();
			System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
				{
					Parallelization.ProgressChangedHandler progressChanged = new Parallelization.ProgressChangedHandler(progressBox.SetProgress);
					Parallelization.ForEach<T>(collection, body, progressChanged);
					progressBox.Invoke(new Action(() =>
						{
							if (progressBox.IsShown == true)
								progressBox.Close();
						}));
				}));
			thread.Start();
			progressBox.ShowDialog();
		}

		[RequiresAssembly(typeof(Parallelization))]
		public static void ShowFor(int count, Parallelization.ForBody body)
		{
			ProgressBox progressBox = new ProgressBox();
			System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(() =>
			{
				Parallelization.ProgressChangedHandler progressChanged = new Parallelization.ProgressChangedHandler(progressBox.SetProgress);
				Parallelization.For(count, body, progressChanged);
				progressBox.Invoke(new Action(() =>
				{
					if (progressBox.IsShown == true)
						progressBox.Close();
				}));
			}));
			thread.Start();
			progressBox.ShowDialog();
		}
	}
}
