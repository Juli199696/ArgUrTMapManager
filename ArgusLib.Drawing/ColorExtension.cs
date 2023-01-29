using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ArgusLib.Drawing
{
	public static class ColorExtension
	{
		public static Color FromHSV(double Hue, double Saturation, double Value)
		{
			int h = (int)(Hue / 60);
			double f = Hue / 60 - h;
			double p = Value * (1 - Saturation);
			double q = Value * (1 - Saturation * f);
			double t = Value * (1 - Saturation * (1 - f));
			double[] rgb = null;
			if (h == 0 || h == 6)
			{
				rgb = new double[] { Value, t, p };
			}
			else if (h == 1)
			{
				rgb = new double[] { q, Value, p };
			}
			else if (h == 2)
			{
				rgb = new double[] { p, Value, t };
			}
			else if (h == 3)
			{
				rgb = new double[] { p, q, Value };
			}
			else if (h == 4)
			{
				rgb = new double[] { t, p, Value };
			}
			else if (h == 5)
			{
				rgb = new double[] { Value, p, q };
			}
			return Color.FromArgb(255, (byte)(255 * rgb[0]), (byte)(255 * rgb[1]), (byte)(255 * rgb[2]));
		}
	}
}
