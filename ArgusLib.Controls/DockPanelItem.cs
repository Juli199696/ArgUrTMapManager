using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ArgusLib.Controls
{
	public class DockPanelItem : Panel
	{
		Button bHeader;

		public event EventHandler HeaderClick;

		public DockPanelItem()
			: base()
		{
			this.bHeader = new Button();
			this.bHeader.AutoSize = true;
			this.bHeader.Text = "header";
			this.bHeader.Click += bHeader_Click;
		}

		void bHeader_Click(object sender, EventArgs e)
		{
			if (this.HeaderClick != null)
				this.HeaderClick(this, e);
		}

		public string Header
		{
			get { return this.bHeader.Text; }
			set { this.bHeader.Text = value; }
		}

		internal Button HeaderButton { get { return this.bHeader; } }
	}
}
