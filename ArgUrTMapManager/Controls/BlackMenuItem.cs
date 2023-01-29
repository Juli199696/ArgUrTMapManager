using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ArgUrTMapManager.Controls
{
	public class BlackMenuItem : ToolStripMenuItem
	{
		private bool IsDropDownOpen { get; set; }

		public BlackMenuItem()
			: base()
		{
#if Mono == false
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
#endif
		}

#if Mono == false
		protected override void OnDropDownOpened(EventArgs e)
		{
			this.IsDropDownOpen = true;
			base.ForeColor = Color.Black;
			base.OnDropDownOpened(e);
		}

		protected override void OnDropDownClosed(EventArgs e)
		{
			base.ForeColor = Color.White;
			this.IsDropDownOpen = false;
			base.OnDropDownClosed(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.ForeColor = Color.Black;
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			if (this.IsDropDownOpen == false)
				base.ForeColor = Color.White;
			base.OnMouseLeave(e);
		}
#endif

		#region Keep Designer from changing colors
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set { }
		}

		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set { }
		}
		#endregion
	}
}
