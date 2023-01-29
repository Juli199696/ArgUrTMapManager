using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArgUrTMapManager.Controls
{
	class BlackMenuStrip : MenuStrip
	{
		public BlackMenuStrip()
			: base()
		{
#if Mono == false
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
#endif
		}

		#region Keep Designer from changing colors
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set { }
		}

		public new Color ForeColor
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
