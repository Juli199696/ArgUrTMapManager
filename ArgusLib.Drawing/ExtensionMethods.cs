using System;
using ArgusLib.Drawing;

namespace System.Drawing
{
	/// <summary>
	/// Defines Extension-Methods for classes in the <see cref="System.Drawing"/> namespace.
	/// </summary>
	public static class ArgusLibExtensions
	{
		/// <summary>
		/// Gets a <see cref="Rectangle"/>:
		/// <code>
		/// return new Rectangle(new Point(0, 0), image.Size);
		/// </code>
		/// </summary>
		public static Rectangle GetRectangle(this Image image)
		{
			return new Rectangle(new Point(0, 0), image.Size);
		}

		/// <summary>
		/// Gets a <see cref="RectangleF"/>:
		/// <code>
		/// return new RectangleF(0, 0, image.Width, image.Height);
		/// </code>
		/// </summary>
		public static RectangleF GetRectangleF(this Image image)
		{
			return new RectangleF(0, 0, image.Width, image.Height);
		}

		/// <summary>
		/// Calls <see cref="FontMetrics.DesignUnitsToFontUnits(Font,float)"/>.
		/// </summary>
		public static float ToUnits(this Font font, float designUnits)
		{
			return FontMetrics.DesignUnitsToFontUnits(font, designUnits);
		}

		public static PointF Add(this PointF p1, PointF p2)
		{
			return new PointF(p1.X + p2.X, p1.Y + p2.Y);
		}


		//public static float GetCellHeight(this FontFamily fontFamily, FontStyle fontStyle)
		//{
		//	return fontFamily.GetCellAscent(fontStyle) + fontFamily.GetCellDescent(fontStyle);
		//}

		/// <summary>
		/// Checks if the <see cref="Point"/> p lies within the boundaries of the <see cref="Rectangle"/> with TopLeft-coordinates (0, 0) and <see cref="Size"/>s.
		/// </summary>
		/// <param name="s">Size of the rectangle.</param>
		/// <param name="p">Point to check.</param>
		/// <returns></returns>
		public static bool Contains(this Size s, Point p)
		{
			return s.Contains(p.X, p.Y);
		}

		/// <summary>
		/// Checks if the (x, y) lies within the boundaries of the <see cref="Rectangle"/> with TopLeft-coordinates (0, 0) and <see cref="Size"/>s.
		/// </summary>
		/// <param name="s"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public static bool Contains(this Size s, int x, int y)
		{
			if (x < 0 || x >= s.Width)
				return false;
			if (y < 0 || y >= s.Height)
				return false;
			return true;
		}

		/// <summary>
		/// Gets the ratio between width and height (width/height).
		/// </summary>
		/// <param name="s"></param>
		/// <returns>width/height.</returns>
		public static double GetRatio(this Size s)
		{
			return s.Width / (double)s.Height;
		}

		/// <summary>
		/// Converts the Size struct to a Rectangle.
		/// </summary>
		/// <param name="s">Size to convert.</param>
		/// <param name="p">Point used as edge or Center of Rectangle.</param>
		/// <param name="op">Convertoption: Defines the relation between the Point p and the Rectangle.</param>
		/// <returns>The converted Rectangle. (0, 0, 0, 0) if failed.</returns>
		public static Rectangle ToRectangle(this Size s, Point p, RectangleConversion op)
		{
			if (op == RectangleConversion.TopLeft)
				return new Rectangle(p, s);
			if (op == RectangleConversion.TopRight)
				return new Rectangle(new Point(p.X - s.Width, p.Y), s);
			if (op == RectangleConversion.BottomLeft)
				return new Rectangle(new Point(p.X, p.Y - s.Height), s);
			if (op == RectangleConversion.BottomRight)
				return new Rectangle(new Point(p.X - s.Width, p.Y - s.Height), s);
			if (op == RectangleConversion.Center)
				return new Rectangle(new Point(p.X - s.Width / 2, p.Y - s.Height / 2), s);
			return new Rectangle(0, 0, 0, 0);
		}

		public enum RectangleConversion : byte
		{
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight,
			Center
		}

		//public static Size ChangeRatio_Bigger(this Size s, double Ratio)
		//{
		//	double r = s.GetRatio();
		//	Size RetVal = s;
		//	if (r < Ratio)
		//		RetVal.Width = (int)System.Math.Abs(s.Height * Ratio);
		//	else
		//		RetVal.Height = (int)System.Math.Abs(s.Width / Ratio);
		//	return RetVal;
		//}

		public static Size ChangeRatio(this Size s, double Ratio, ChangeRatioOptions opt)
		{
			double r = s.GetRatio();
			Size RetVal = s;

			if (opt == ChangeRatioOptions.IncreaseAreaSize)
			{
				if (r < Ratio)
					RetVal.Width = (int)System.Math.Round(s.Height * Ratio);
				else
					RetVal.Height = (int)System.Math.Round(s.Width / Ratio);
				return RetVal;
			}
			if (opt == ChangeRatioOptions.DecreaseAreaSize)
			{
				if (r > Ratio)
					RetVal.Width = (int)System.Math.Round(s.Height * Ratio);
				else
					RetVal.Height = (int)System.Math.Round(s.Width / Ratio);
				return RetVal;
			}

			// if (opt == ChangeRatioOptions.KeepAreaSize)
			double a = System.Math.Sqrt((s.Width * s.Height) / Ratio);
			RetVal.Height = (int)System.Math.Round(a);
			RetVal.Width = (int)System.Math.Round(a * Ratio);
			return RetVal;
		}
	}
}