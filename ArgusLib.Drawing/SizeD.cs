using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Drawing
{
	public struct SizeD
	{
		public double Width { get; set; }
		public double Height { get; set; }

		public SizeD(double width, double height)
			: this()
		{
			this.Width = width;
			this.Height = height;
		}

		public double GetRatio()
		{
			return this.Width / this.Height;
		}

		public SizeD ChangeRatio(double ratio, ChangeRatioOptions options)
		{
			double r = this.GetRatio();
			SizeD RetVal = this;

			if (options == ChangeRatioOptions.IncreaseAreaSize)
			{
				if (r < ratio)
					RetVal.Width = this.Height * ratio;
				else
					RetVal.Height = this.Width / ratio;
				return RetVal;
			}
			if (options == ChangeRatioOptions.DecreaseAreaSize)
			{
				if (r > ratio)
					RetVal.Width = this.Height * ratio;
				else
					RetVal.Height = this.Width / ratio;
				return RetVal;
			}

			// if (opt == ChangeRatioOptions.KeepAreaSize)
			double a = System.Math.Sqrt((this.Width * this.Height) / ratio);
			RetVal.Height = a;
			RetVal.Width = a * ratio;
			return RetVal;
		}
	}
}
