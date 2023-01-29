using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public partial class ProgressForm : ModalToolForm
	{
		const int maxHistory = 1 << 10;
		bool showDetails;

		public ProgressForm()
		{
			InitializeComponent();
			this.ShowDetails = false;
			this.lProgress.Text = string.Empty;
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.lText.Text = value;
				this.toolTip1.SetToolTip(this.lText, value);
			}
		}

		public void SetProgress(double Progress, string Text)
		{
			if (Progress >= 0)
			{
				this.pbProgress.Value = (int)(Progress * 100);
				this.Text = Text;
				this.lProgress.Text = Progress.ToString("p");
			}
			else
			{
				this.Text = Text;
				this.lProgress.Text = string.Empty;
			}

			this.obDetails.Output(this.Text+'\t'+this.lProgress.Text);

			//this.lbDetails.Items.Add(this.lText.Text);
			//if (this.lbDetails.Items.Count > maxHistory)
			//	this.lbDetails.Items.RemoveAt(0);

			//this.lbDetails.SelectedIndex = this.lbDetails.Items.Count - 1;
		}

		internal ReportProgressHandler GetProgressHandler()
		{
			ReportProgressHandler progressHandler = (progress, Text) =>
				{
					if (this.IsShown == true)
						this.Invoke(new ReportProgressHandler(this.SetProgress), progress, Text);
				};
			return progressHandler;
		}

		private void llDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.ShowDetails = this.ShowDetails == false;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			this.pbProgress.Value = 0;
			this.lText.Text = string.Empty;
			this.lProgress.Text = string.Empty;
			this.obDetails.Clear();
			base.OnClosing(e);
		}

		private bool ShowDetails
		{
			get{return this.showDetails;}
			set
			{
				this.showDetails = value;
				if (this.showDetails == false)
				{
					this.Size = new Size(this.Size.Width, this.obDetails.Location.Y);
				}
				else
				{
					this.Size = new Size(this.Size.Width, this.obDetails.Bottom + 12);
				}
			}
		}
	}
}
