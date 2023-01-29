using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArgusLib.Drawing
{
	/// <summary>
	/// A class containig all information to draw text.
	/// </summary>
	public class DrawTextInfo
	{
		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.FontFamily"/> used to draw the text.
		/// </summary>
		public FontFamily FontFamily { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.FontStyle"/> used to draw the text.
		/// </summary>
		public FontStyle FontStyle { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="float"/> specifying the fontsize used to draw the text.
		/// </summary>
		public float FontSize { get; set; }

		/// <summary>
		/// Gets or sets a linespacingfactor used to manipulate linespacing.
		/// </summary>
		public float LineSpacing { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> used to draw the text.
		/// </summary>
		public Brush TextBrush { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="float"/> specifying the width of the textborder.
		/// </summary>
		public float BorderThickness { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> used to draw the textborder.
		/// </summary>
		public Brush BorderBrush { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="float"/> specifying the offset of the textshadow.
		/// </summary>
		public float ShadowOffset { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="float"/> specifying the direction of the textshadow.
		/// </summary>
		public float ShadowAngle { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> used to draw the textshadow.
		/// </summary>
		public Brush ShadowBrush { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="Brush"/> used to draw the background of the rectangle
		/// containig the text.
		/// </summary>
		public Brush BackgroundBrush { get; set; }

		/// <summary>
		/// Gets or sets the <see cref="System.Drawing.StringFormat"/> used to draw the text.
		/// </summary>
		public StringFormat StringFormat { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="DrawTextInfo"/>.
		/// </summary>
		public DrawTextInfo()
		{
			this.LineSpacing = 1;
			this.TextBrush = new SolidBrush(Color.Black);
			this.BorderThickness = 0;
			this.BorderBrush = null;
			this.ShadowOffset = 0;
			this.ShadowAngle = 0;
			this.ShadowBrush = null;
			this.BackgroundBrush = null;
			this.StringFormat = StringFormat.GenericDefault;
		}

		/// <summary>
		/// Creates a new instance of <see cref="DrawTextInfo"/>. The properties <see cref="DrawTextInfo.FontFamily"/>,
		/// <see cref="DrawTextInfo.FontStyle"/> and <see cref="DrawTextInfo.FontSize"/> are initialized with <paramref name="font"/>.
		/// </summary>
		public DrawTextInfo(Font font)
			: this()
		{
			this.FontFamily = font.FontFamily;
			this.FontStyle = font.Style;
			this.FontSize = font.Size;
		}

		/// <summary>
		/// Gets a <see cref="Font"/> with <see cref="System.Drawing.FontFamily"/> specified by <see cref="DrawTextInfo.FontFamily"/>,
		/// <see cref="System.Drawing.FontStyle"/> specified by <see cref="DrawTextInfo.FontStyle"/>,
		/// fontsize specified by <see cref="DrawTextInfo.FontSize"/> and <see cref="Font.Unit"/> = <see cref="GraphicsUnit.Pixel"/>.
		/// </summary>
		public Font GetFont()
		{
			return new Font(this.FontFamily, this.FontSize, this.FontStyle, GraphicsUnit.Pixel);
		}

		/// <summary>
		/// Gets the x- and the y-offset of the textshadow.
		/// </summary>
		public PointF GetShadowOffset()
		{
			double angleRad = this.ShadowAngle * Math.PI / 180;
			double x = this.ShadowOffset * Math.Cos(angleRad);
			double y = this.ShadowOffset * Math.Sin(angleRad);
			return new PointF((float)x, (float)y);
		}
	}
}
