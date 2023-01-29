using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public class BaseForm : Form
	{
		public bool IsShown { get; private set; }

		public BaseForm()
			: base()
		{
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.IsShown = false;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.IsShown = true;
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			this.IsShown = false;
			base.OnFormClosing(e);
		}

		//protected abstract void LocalizeText();
	}
}
