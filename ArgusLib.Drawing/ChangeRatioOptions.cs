using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Drawing
{
	public enum ChangeRatioOptions : byte
	{
		/// <summary>
		/// Keeps the length of one side, lenghtens the other, thus increases the size of the area.
		/// </summary>
		IncreaseAreaSize,
		/// <summary>
		/// Keeps the length of one side, shortens the other, thus dencreases the size of the area.
		/// </summary>
		DecreaseAreaSize,
		/// <summary>
		/// Adjusts both sides. The size of the area stays the same.
		/// </summary>
		KeepAreaSize
	}
}
