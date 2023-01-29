using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ArgUrTMapManager.Forms
{
	public class ToolForm : BaseForm
	{
		bool close = false;

		public ToolForm()
			: base()
		{
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		}

		public new void ShowDialog()
		{
			this.Show();
		}

		public void Close(bool Terminate)
		{
			this.close = Terminate;
			this.Close();
		}

		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			if (this.close == false)
			{
				e.Cancel = true;
				this.Hide();
			}
			base.OnClosing(e);
		}
	}
}
