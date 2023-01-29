using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArgUrTMapManager.Controls
{
	class BlackTextBox : ArgusLib.Controls.TextBox
	{
		public BlackTextBox()
			: base()
		{
#if Mono
			base.BackColor = Color.White;
			base.ForeColor = Color.Black;
#else
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
#endif
		}

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
