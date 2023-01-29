using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public interface IScalar
	{
		IScalar Add(IScalar value);
		IScalar Multiply(IScalar value);
		IScalar Subtract(IScalar value);
		IScalar Divide(IScalar value);
		bool IsEqualTo(IScalar value);
		string ToString(IFormatProvider formatProvider, string format);
	}

	public interface ILinearScalar : IScalar
	{
		bool IsGreaterThan(ILinearScalar value);
		bool IsSmallerThan(ILinearScalar value);
	}
}
