using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using ArgusLib.Threading;
using SBitmap = System.Drawing.Bitmap;
using Bitmap = ArgusLib.Drawing.ExtendedBitmap;

namespace ArgusLib.Drawing
{
	public class ExtendedBitmap : IDisposable
	{
		BitmapData data;
		Rectangle rect;
		Byte[] pixel;
		SBitmap bitmap;
		bool locked;
		PixelFormat pxf;
		bool doNotDispose = false;

		public ExtendedBitmap(Image image)
		{
			this.bitmap = new SBitmap(image);
			this.rect = new Rectangle(0, 0, image.Width, image.Height);
			this.locked = false;
			this.pxf = PixelFormat.Format32bppArgb;
		}

		private ExtendedBitmap()
		{
		}

		public ExtendedBitmap(int width, int height)
		{
			this.bitmap = new SBitmap(width, height);
			this.rect = new Rectangle(0, 0, width, height);
			this.locked = false;
			this.pxf = PixelFormat.Format32bppArgb;
		}

		public static Bitmap FromBitmap(SBitmap bitmap)
		{
			Bitmap RetVal = new Bitmap();
			RetVal.bitmap = bitmap;
			RetVal.rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			RetVal.pxf = PixelFormat.Format32bppArgb;
			RetVal.doNotDispose = true;
			return RetVal;
		}

		public ExtendedBitmap(Image SourceImage, Rectangle SourceRectangle, GraphicsUnit unit)
		{
			this.bitmap = new SBitmap(SourceRectangle.Width, SourceRectangle.Height);
			this.rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
			using (Graphics g = Graphics.FromImage(this.bitmap))
			{
				g.DrawImage(SourceImage, this.rect, SourceRectangle, unit);
			}
			this.locked = false;
			this.pxf = PixelFormat.Format32bppArgb;
		}

		public bool IsLocked { get { return this.locked; } }

		public bool Lock()
		{
			if (this.locked == true)
				return false;

			this.data = this.bitmap.LockBits(this.rect, ImageLockMode.ReadWrite, this.pxf);
			this.pixel = new Byte[this.data.Stride * this.data.Height];
			Marshal.Copy(this.data.Scan0, this.pixel, 0, this.pixel.Length);
			this.locked = true;
			return true;
		}

		public void SetPixel(int x, int y, Color c)
		{
			if (this.locked == false)
			{
				this.bitmap.SetPixel(x, y, c);
				return;
			}

			int p = y * this.data.Stride + x * 4;
			//if (this.bpp == 4)
			//{
			//    this.pixel[p++] = c.A;
			//}
			this.pixel[p] = c.B;
			this.pixel[++p] = c.G;
			this.pixel[++p] = c.R;
			this.pixel[++p] = c.A;
		}

		public Color GetPixel(int x, int y)
		{
			if (this.locked == false)
			{
				return this.bitmap.GetPixel(x, y);
			}

			int p = y * this.data.Stride + x * 4;
			byte b = this.pixel[p];
			byte g = this.pixel[++p];
			byte r = this.pixel[++p];
			byte a = this.pixel[++p];
			return Color.FromArgb(a, r, g, b);
		}

		public Color this[int x, int y]
		{
			get { return this.GetPixel(x, y); }
			set { this.SetPixel(x, y, value); }
		}

		public bool Unlock()
		{
			if (this.locked == false)
				return false;

			Marshal.Copy(this.pixel, 0, this.data.Scan0, this.pixel.Length);
			this.bitmap.UnlockBits(this.data);
			this.locked = false;
			return true;
		}
		
		/// <summary>
		/// Inverts the color in the whole image.
		/// </summary>
		public void InvertColor()
		{
			for (int x = 0; x < this.Width; x++)
			{
				for (int y = 0; y < this.Height; y++)
				{
					this.InvertColor(x, y);
				}
			}
		}

		/// <summary>
		/// Inverts the color of a pixel.
		/// </summary>
		/// <param name="x">x coordinate of the pixel.</param>
		/// <param name="y">y coordinate of the pixel.</param>
		public void InvertColor(Int32 x, Int32 y)
		{
			this.SetPixel(x, y, Bitmap.InvertColor(this.GetPixel(x, y)));
		}

		/// <summary>
		/// Inverts the color of an array of pixels.
		/// </summary>
		/// <param name="Points">Point-array containing the coordinates of the pixels.</param>
		public void InvertColor(Point[] Points)
		{
			foreach (Point p in Points)
			{
				this.InvertColor(p.X, p.Y);
			}
		}

		/// <summary>
		/// Inverts pixels using a mask. Each pixel which is colored black in the mask
		/// is inverted.
		/// </summary>
		/// <param name="mask">The mask to be used to invert the colors.</param>
		/// <param name="TopLeft">The position where the mask will be applied.</param>
		public void InvertColor(Image mask, Point TopLeft)
		{
			using (Bitmap t = new Bitmap(mask))
			{
				t.Lock();
				for (int x = 0; x < mask.Width; x++)
				{
					for (int y = 0; y < mask.Height; y++)
					{
						if (t.GetPixel(x, y) != Color.Black)
							continue;

						Point p = new Point(TopLeft.X + x, TopLeft.Y + y);
						if (this.Size.Contains(p) == false)
							continue;

						this.InvertColor(x, y);
					}
				}
				t.Unlock();
			}
		}

		/// <summary>
		/// Inverts the given color.
		/// </summary>
		/// <param name="c">The color to invert.</param>
		/// <returns>The inverted color.</returns>
		public static Color InvertColor(Color c)
		{
			return Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B);
		}

		public SBitmap GetBitmap()
		{
			if (this.locked == true)
				throw new Exception("The GetBitmap-Method may only be called when the Bitmap is unlocked.");
			return new SBitmap(this.bitmap);
		}

		public Bitmap GetSection(Rectangle bounds)
		{
			Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
			bitmap.Lock();

			for (int x = 0; x < bounds.Width; x++)
			{
				for (int y = 0; y < bounds.Height; y++)
				{
					bitmap.SetPixel(x, y, this.GetPixel(x + bounds.X, y + bounds.Y));
				}
			}

			bitmap.Unlock();
			return bitmap;
		}

		public Bitmap RemoveTransparency(Color Background)
		{
			SBitmap Ret = new SBitmap(this.bitmap.Width, this.bitmap.Height);
			Graphics g = Graphics.FromImage(Ret);
			g.Clear(Background);
			g.DrawImage(this.bitmap, new Rectangle(new Point(0, 0), this.bitmap.Size));
			g.Dispose();
			return new Bitmap(Ret);
		}

		public Bitmap MakeTransparent(Color Background)
		{
			Bitmap b = this.RemoveTransparency(Background);
			b.Lock();

			for (int x = 0; x < b.Width; x++)
			{
				for (int y = 0; y < b.Height; y++)
				{
					Color c = b.GetPixel(x, y);
					double a = this.MakeTransparent_GetAlpha(c, Background);
					b.SetPixel(x, y, this.MakeTransparent_GetColor(a, c, Background));
				}
			}

			b.Unlock();
			return b;
		}

		#region MakeTransparentHelpMethods
		private double MakeTransparent_GetAlpha(Color c, Color BackColor)
		{
			double a = this.MakeTransparent_GetAlphaC(c.R, BackColor.R);
			a = System.Math.Max(a, this.MakeTransparent_GetAlphaC(c.G, BackColor.G));
			a = System.Math.Max(a, this.MakeTransparent_GetAlphaC(c.B, BackColor.B));
			return a;
		}

		private double MakeTransparent_GetAlphaC(byte c, byte c_b)
		{
			if (c == c_b)
			{
				return 0;
			}
			if (c > c_b)
			{
				return (c - c_b) / (double)(255 - c_b);
			}
			else
			{
				return (c_b - c) / (double)c_b;
			}
		}

		private byte MakeTransparent_GetColorC(double a, byte c, byte c_b)
		{
			if (a == 0)
				return 0;
			return (byte)((c - c_b) / a + c_b);
		}

		private Color MakeTransparent_GetColor(double a, Color c, Color BackColor)
		{
			return Color.FromArgb((byte)(a * 255),
				this.MakeTransparent_GetColorC(a, c.R, BackColor.R),
				this.MakeTransparent_GetColorC(a, c.G, BackColor.G),
				this.MakeTransparent_GetColorC(a, c.B, BackColor.B));
		}
		#endregion

		public int Width { get { return this.bitmap.Width; } }
		public int Height { get { return this.bitmap.Height; } }
		public Size Size { get { return this.bitmap.Size; } }

		public void Dispose()
		{
			if (this.IsLocked == true)
				this.Unlock();

			if (this.doNotDispose == false)
				this.bitmap.Dispose();
		}

		[RequiresAssembly(typeof(Parallelization))]
		public void SplitAndSave(string[,] filenames, string destFolder, ImageFormat imgFormat, string fileExt, Parallelization.ProgressChangedHandler progressChanged)
		{
			if (Directory.Exists(destFolder) == false)
				throw new IOException("Folder does not exist:\n" + destFolder);

			int length0 = filenames.GetLength(0);
			int length1 = filenames.GetLength(1);
			int sectionWidth = this.Width / length0;
			int sectionHeight = this.Height / length1;

			Parallelization.For(length0, new Parallelization.ForBody((x, threadState) =>
				{
					for (int y = 0; y < length1; y++)
					{
						if (string.IsNullOrEmpty(filenames[x, y]) == true)
							continue;
						Bitmap b = this.GetSection(new Rectangle(x * sectionWidth, y * sectionHeight, sectionWidth, sectionHeight));
						string path = Path.Combine(destFolder, filenames[x, y]+fileExt);
						b.GetBitmap().Save(path, imgFormat);
					}
				}), progressChanged);
		}

		[RequiresAssembly(typeof(Parallelization))]
		public void SplitAndSave(string[,] filenames, string destFolder, ImageFormat imgFormat)
		{
			this.SplitAndSave(filenames, destFolder, imgFormat, string.Empty, null);
		}

		public bool Multiply(Bitmap factor)
		{
			if (this.Size != factor.Size)
				return false;

			for (int x = 0; x < this.Width; x++)
			{
				for (int y = 0; y < this.Height; y++)
				{
					Color c1 = this.GetPixel(x, y);
					Color c2 = factor.GetPixel(x, y);
					Color c = Color.FromArgb(
						(byte)(c1.A * c2.A / 255.0),
						(byte)(c1.R * c2.R / 255.0),
						(byte)(c1.G * c2.G / 255.0),
						(byte)(c1.B * c2.B / 255.0));
					this.SetPixel(x, y, c);
				}
			}
			return true;
		}
	}
}
