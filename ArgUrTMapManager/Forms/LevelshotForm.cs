using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public partial class LevelshotForm : ToolForm
	{
		public LevelshotForm()
			:base()
		{
			InitializeComponent();
			this.ClientSize = new Size(400, 300);
		}

		public void SetImage(Image image, string Mapname)
		{
			this.Text = Mapname;
			this.pbLevelshot.Image = image;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (ModifierKeys == Keys.Control && e.KeyCode == Keys.C)
			{
				if (this.pbLevelshot.Image != null)
				{
					Clipboard.SetImage(this.pbLevelshot.Image);
				}
			}
		}
	}
}
