using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class Int32Wrapper : StructWrapper<Int32>
	{
		public Int32Wrapper()
			: base() { }

		public Int32Wrapper(int value)
			: base(value) { }

		public Int32Wrapper(Int32Wrapper value)
			: base(value) { }

		public override StructWrapper<int> Add(StructWrapper<int> value)
		{
			return new Int32Wrapper(this.Value + value.Value);
		}

		public override StructWrapper<int> Subtract(StructWrapper<int> value)
		{
			return new Int32Wrapper(this.Value - value.Value);
		}

		public override StructWrapper<int> Multiply(StructWrapper<int> value)
		{
			return new Int32Wrapper(this.Value * value.Value);
		}

		public override StructWrapper<int> Divide(StructWrapper<int> value)
		{
			return new Int32Wrapper(this.Value / value.Value);
		}

		public override bool IsGreaterThan(StructWrapper<int> value)
		{
			return this.Value > value.Value;
		}

		public override bool IsSmallerThan(StructWrapper<int> value)
		{
			return this.Value < value.Value;
		}

		public static implicit operator Int32Wrapper(int value)
		{
			return new Int32Wrapper(value);
		}

		public static bool TryParse(string text, out Int32Wrapper value)
		{
			value = new Int32Wrapper();
			int d;
			if (int.TryParse(text, out d) == false)
				return false;
			value.Value = d;
			return true;
		}
	}
}
