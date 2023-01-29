using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public class ModalToolForm : BaseForm
	{
		public ModalToolForm()
			: base()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		}

		public new DialogResult Show()
		{
			return this.ShowDialog();
		}
	}
}
