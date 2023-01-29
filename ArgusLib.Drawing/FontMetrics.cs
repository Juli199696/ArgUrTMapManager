using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArgusLib.Drawing
{
	/// <summary>
	/// A static class containig methods to calculate font metrics.
	/// </summary>
	public static class FontMetrics
	{
		//public static float GetCellHeight(Font font)
		//{
		//	float cellHeight = font.FontFamily.GetCellHeight(font.Style);
		//	return FontMetrics.DesignUnitsToFontUnits(font, cellHeight);
		//}

		/// <summary>
		/// Converts design unites to font units.
		/// </summary>
		public static float DesignUnitsToFontUnits(Font font, float designUnits)
		{
			float size = font.Size;
			float emHeight = font.FontFamily.GetEmHeight(font.Style);
			return designUnits * size / emHeight;
		}
	}
}
