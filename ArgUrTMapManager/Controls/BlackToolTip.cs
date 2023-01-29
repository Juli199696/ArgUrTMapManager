using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ArgUrTMapManager.Controls
{
	class BlackToolTip : ToolTip
	{
		public BlackToolTip()
			: base()
		{
			this.UseAnimation = false;
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
		}
	}
}
