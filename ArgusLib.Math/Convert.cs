using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public static class Convert
	{
		public static double RadToDeg(double rad)
		{
			return rad * 180 / System.Math.PI;
		}

		public static double DegToRad(double deg)
		{
			return deg * System.Math.PI / 180;
		}
	}
}
