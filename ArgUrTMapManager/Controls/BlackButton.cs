using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ArgUrTMapManager.Controls
{
	class BlackButton : Button
	{
		public BlackButton()
			: base()
		{
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
			//this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
