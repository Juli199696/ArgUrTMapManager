using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArgUrTMapManager.Controls
{
	class BlackEnumComboBox : ArgusLib.Controls.EnumComboBox
	{
		public BlackEnumComboBox()
			: base()
		{
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
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
