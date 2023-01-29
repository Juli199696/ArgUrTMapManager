using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class DoubleWrapper : StructWrapper<Double>
	{
		public DoubleWrapper()
			: base() { }

		public DoubleWrapper(double value)
			: base(value) { }

		public DoubleWrapper(DoubleWrapper value)
			: base(value) { }

		public override StructWrapper<double> Add(StructWrapper<double> value)
		{
			return new DoubleWrapper(this.Value + value.Value);
		}

		public override StructWrapper<double> Subtract(StructWrapper<double> value)
		{
			return new DoubleWrapper(this.Value - value.Value);
		}

		public override StructWrapper<double> Multiply(StructWrapper<double> value)
		{
			return new DoubleWrapper(this.Value * value.Value);
		}

		public override StructWrapper<double> Divide(StructWrapper<double> value)
		{
			return new DoubleWrapper(this.Value / value.Value);
		}

		public override bool IsSmallerThan(StructWrapper<double> value)
		{
			return this.Value < value.Value;
		}

		public override bool IsGreaterThan(StructWrapper<double> value)
		{
			return this.Value > value.Value;
		}

		public static implicit operator DoubleWrapper(double value)
		{
			return new DoubleWrapper(value);
		}

		public static bool TryParse(string text, out DoubleWrapper value)
		{
			value = new DoubleWrapper();
			double d;
			if (double.TryParse(text, out d) == false)
				return false;
			value.Value = d;
			return true;
		}
	}
}
