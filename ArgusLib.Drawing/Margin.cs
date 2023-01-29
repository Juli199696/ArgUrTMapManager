using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Drawing
{
	public struct Margin
	{
		public float Left { get; set; }
		public float Top { get; set; }
		public float Right { get; set; }
		public float Bottom { get; set; }

		public Margin(float left, float top, float right, float bottom)
			: this()
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		public Margin(float all)
			: this(all, all, all, all)
		{ }

		public float Horizontal
		{
			get { return this.Left + this.Right; }
		}

		public float Vertical
		{
			get { return this.Top + this.Bottom; }
		}
	}
}
