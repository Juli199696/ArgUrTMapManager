using System;
using System.Collections.Generic;
using System.Drawing;
using ArgusLib.Controls;

namespace ArgUrTMapManager.Controls
{
	class BlackFilenameComboBox : FilenameComboBox
	{
		public BlackFilenameComboBox()
			: base()
		{
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
		}
	}
}
