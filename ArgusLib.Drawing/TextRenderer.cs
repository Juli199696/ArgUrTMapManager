using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ArgusLib.Drawing
{
	/// <summary>
	/// A static class containig methods to measure and draw text to a <see cref="Bitmap"/>.
	/// </summary>
	/// <example>
	/// <code>
	/// protected override void OnLoad(EventArgs e)
	///	{
	///		base.OnLoad(e);
	///
	///		string text = "Test, test,\n1, 2, 3...";
	///		DrawTextInfo drawTextInfo = new DrawTextInfo();
	///		drawTextInfo.FontFamily = this.Font.FontFamily;
	///		drawTextInfo.FontStyle = FontStyle.Bold;
	///		drawTextInfo.FontSize = 200;
	///		drawTextInfo.BorderThickness = 5;
	///		drawTextInfo.ShadowOffset = 10;
	///		drawTextInfo.ShadowAngle = 45;
	///		GraphicsPath TextPath, BorderPath, ShadowPath;
	///		SizeF size = TextRend.MeasureText(text, drawTextInfo, out TextPath, out BorderPath, out ShadowPath);
	///		RectangleF rect = new RectangleF(new PointF(0,0), size);
	///		drawTextInfo.BackgroundBrush = new LinearGradientBrush(rect, Color.Black, Color.Red, LinearGradientMode.Horizontal);
	///		drawTextInfo.TextBrush = new LinearGradientBrush(rect, Color.Blue, Color.Black, LinearGradientMode.Horizontal);
	///		drawTextInfo.BorderBrush = new LinearGradientBrush(rect, Color.Yellow, Color.White, LinearGradientMode.Horizontal);
	///		drawTextInfo.ShadowBrush = new LinearGradientBrush(rect, Color.White, Color.Black, LinearGradientMode.Horizontal);
	///		this.ClientSize = size.ToSize();
	///		this.BackgroundImage = TextRend.DrawText(text, drawTextInfo, TextPath, BorderPath, ShadowPath, size);
	///	}
	///	</code>
	/// </example>
	public static class TextRenderer
	{
		/// <summary>
		/// Measures the <see cref="SizeF"/> required to draw <paramref name="text"/> using <see cref="Graphics.MeasureString(string,Font)"/>.
		/// </summary>
		/// <param name="text">A <see cref="string"/> specifying the text to measure.</param>
		/// <param name="font">A <see cref="Font"/> specifying the <see cref="FontStyle"/>, <see cref="FontFamily"/>
		/// and the fontsize of the text to be measured.</param>
		//private static SizeF MeasureText(string text, Font font)
		//{
		//	using (Graphics g = Graphics.FromImage(new Bitmap(1, 1)))
		//	{
		//		SizeF size = g.MeasureString(text, font);
		//		g.Dispose();
		//		return size;
		//	}
		//}

		/// <summary>
		/// Calculates the size of the bouding box of the drawn text. This is usually smaller
		/// than the <see cref="Font.SizeInPoints"/>.
		/// </summary>
		//public static SizeF MeasureText(string text, Font font, StringFormat stringFormat)
		//{
		//	using (GraphicsPath path = new GraphicsPath())
		//	{
		//		path.AddString(text, font.FontFamily, (int)font.Style, font.SizeInPoints, new Point(0, 0), stringFormat);
		//		return path.GetBounds().Size;
		//	}
		//}

		/// <summary>
		/// Calculate the maximal Font.SizeInPoints for <paramref name="text"/> to fit <paramref name="bounds"/>.
		/// </summary>
		public static float MeasureText(string text, FontFamily fontFamily, FontStyle fontStyle, StringFormat stringFormat, ref SizeF bounds)
		{
			Point pZero = new Point(0,0);
			if (bounds.Width <= 0)
				throw new ArgumentException("bounds.Width must be greater than 0 (zero).", "bounds");
			if (bounds.Height <= 0)
				throw new ArgumentException("bounds.Height must be greater than 0 (zero).", "bounds");

			using (GraphicsPath path = new GraphicsPath())
			{
				SizeF currentBounds;
				float emSize = bounds.Height;
				path.AddString(text, fontFamily, (int)fontStyle, emSize, pZero, stringFormat);
				currentBounds = path.GetBounds().Size;
				float factorH = bounds.Height / currentBounds.Height;
				float factorW = bounds.Width / currentBounds.Width;
				float factor = Math.Min(factorH, factorW);
				emSize *= factor;
				path.Reset();
				path.AddString(text, fontFamily, (int)fontStyle, emSize, pZero, stringFormat);
				currentBounds = path.GetBounds().Size;

				bounds = currentBounds;
				return emSize;
			}
		}

		/// <summary>
		/// Draws a text to a <see cref="Bitmap"/>.
		/// Calls <see cref="TextRenderer.MeasureText(string,DrawTextInfo,GraphicsPath,GraphicsPath,GraphicsPath)"/> and
		/// <see cref="TextRenderer.DrawText(string,DrawTextInfo,GraphicsPath,GraphicsPath,GraphicsPath,SizeF)"/> internally.
		/// </summary>
		/// <param name="text">A <see cref="string"/> specifying the text to draw.</param>
		/// <param name="drawTextInfo">A <see cref="DrawTextInfo"/> specifying various parameters used to draw the text.</param>
		/// <returns>A <see cref="Bitmap"/> exactly containing the drawn text.</returns>
		public static Bitmap DrawText(string text, Margin margin, DrawTextInfo drawTextInfo)
		{
			GraphicsPath TextPath;
			GraphicsPath BorderPath;
			GraphicsPath ShadowPath;
			SizeF size = TextRenderer.MeasureText(text, margin, drawTextInfo, out TextPath, out BorderPath, out ShadowPath);
			return TextRenderer.DrawText(text, drawTextInfo, TextPath, BorderPath, ShadowPath, size);
		}

		public static Bitmap DrawText(string text, DrawTextInfo drawTextInfo)
		{
			return TextRenderer.DrawText(text, new Margin(0), drawTextInfo);
		}

		/// <summary>
		/// Calculates the size of the bouding box of the drawn text. This is usually smaller
		/// than the <see cref="Font.SizeInPoints"/>.
		/// </summary>
		public static SizeF MeasureText(string text, DrawTextInfo drawTextInfo)
		{
			return TextRenderer.MeasureText(text, new Margin(0), drawTextInfo, null, null, null);
		}

		public static SizeF MeasureText(string text, Margin margin, DrawTextInfo drawTextInfo, out GraphicsPath TextPath, out GraphicsPath BorderPath, out GraphicsPath ShadowPath)
		{
			TextPath = new GraphicsPath();
			BorderPath = new GraphicsPath();
			ShadowPath = new GraphicsPath();
			return TextRenderer.MeasureText(text, margin, drawTextInfo, TextPath, BorderPath, ShadowPath);
		}

		private static SizeF MeasureText(string text, Margin margin, DrawTextInfo drawTextInfo, GraphicsPath TextPath, GraphicsPath BorderPath, GraphicsPath ShadowPath)
		{
			bool onlyMeasure = false;
			if (TextPath == null)
			{
				TextPath = new GraphicsPath();
				onlyMeasure = true;
			}
			if (BorderPath == null)
			{
				BorderPath = new GraphicsPath();
				onlyMeasure = true;
			}
			if (ShadowPath == null)
			{
				ShadowPath = new GraphicsPath();
				onlyMeasure = true;
			}

			Font font = drawTextInfo.GetFont();
			int lineSpacingDesingUnits = font.FontFamily.GetLineSpacing(font.Style);
			float lineSpacing = FontMetrics.DesignUnitsToFontUnits(font, lineSpacingDesingUnits);
			lineSpacing *= drawTextInfo.LineSpacing;

			string[] lines = text.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				PointF offset = new PointF(0, i * lineSpacing);

				TextPath.AddString(
					lines[i],
					drawTextInfo.FontFamily,
					(int)drawTextInfo.FontStyle,
					drawTextInfo.FontSize/* - 2 * drawTextInfo.BorderThickness*/,
					offset,
					drawTextInfo.StringFormat);
			}

			BorderPath.AddPath(TextPath, false);
			using (Pen pen = new Pen(Color.Black, drawTextInfo.BorderThickness))
			{
				BorderPath.Widen(pen);
			}

			ShadowPath.AddPath(TextPath, false);
			ShadowPath.AddPath(BorderPath, false);
			Matrix translateMatrix = new Matrix();
			PointF shadowOffset = drawTextInfo.GetShadowOffset();
			translateMatrix.Translate(shadowOffset.X, shadowOffset.Y);
			ShadowPath.Transform(translateMatrix);

			RectangleF bounds;
			using (GraphicsPath measurePath = new GraphicsPath())
			{
				measurePath.AddPath(BorderPath, false);
				measurePath.AddPath(ShadowPath, false);
				bounds = measurePath.GetBounds();
			}

			if (onlyMeasure == true)
				return bounds.Size;

			translateMatrix = new Matrix();
			translateMatrix.Translate(-bounds.X+margin.Left, -bounds.Y+margin.Top);
			TextPath.Transform(translateMatrix);
			BorderPath.Transform(translateMatrix);
			ShadowPath.Transform(translateMatrix);
			TextPath.FillMode = FillMode.Winding;
			BorderPath.FillMode = FillMode.Winding;
			ShadowPath.FillMode = FillMode.Winding;
			return new SizeF(bounds.Width + margin.Horizontal, bounds.Height + margin.Vertical);
		}

		/// <summary>
		/// Draws a <see cref="string"/> and returns the containing <see cref="Bitmap"/>.
		/// </summary>
		/// <param name="text">The text to draw.</param>
		/// <param name="drawTextInfo">A <see cref="DrawTextInfo"/>.</param>
		/// <param name="TextPath">GraphicsPath obtained by <see cref="MeasureText"/></param>
		/// <param name="BorderPath"></param>
		/// <param name="ShadowPath"></param>
		/// <param name="BitmapSize"></param>
		/// <returns></returns>
		public static Bitmap DrawText(string text, DrawTextInfo drawTextInfo, GraphicsPath TextPath, GraphicsPath BorderPath, GraphicsPath ShadowPath, SizeF BitmapSize)
		{
			Bitmap bitmap = new Bitmap((int)(BitmapSize.Width + 1), (int)(BitmapSize.Height + 1));
			using (Graphics g = Graphics.FromImage(bitmap))
			{
				g.SmoothingMode = SmoothingMode.AntiAlias;

				// Draw Background First
				if (drawTextInfo.BackgroundBrush != null)
				{
					g.FillRectangle(drawTextInfo.BackgroundBrush, bitmap.GetRectangle());
				}

				// Draw Shadow Second
				if (drawTextInfo.ShadowBrush != null)
				{
					g.FillPath(drawTextInfo.ShadowBrush, ShadowPath);
				}

				// Draw Text Third
				if (drawTextInfo.TextBrush != null)
				{
					g.FillPath(drawTextInfo.TextBrush, TextPath);
				}

				// Draw Border Last
				if (drawTextInfo.BorderBrush != null)
				{
					g.FillPath(drawTextInfo.BorderBrush, BorderPath);
				}
			}
			return bitmap;
		}
	}
}
