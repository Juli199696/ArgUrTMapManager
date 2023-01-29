using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ArgusLib.Math
{
	/// <summary>
	/// A base class for wrappers for the basic value types (<see cref="int"/>, <see cref="double"/>, etc.)
	/// to allow them to be used with <see cref="Vector{T}"/> and <see cref="Matrix{T}"/>. Classes which inherit
	/// from this class should also declare a method <c>public static bool TryParse(string, out DerivenClass)</c>
	/// to allow Vectors and Matrices of the deriven class to be parsed.
	/// </summary>
	public abstract class StructWrapper<T> : ILinearScalar
		where T: struct
	{
		public T Value { get; set; }

		public StructWrapper(T value)
		{
			this.Value = value;
		}

		public StructWrapper()
		{
			this.Value = default(T);
		}

		public StructWrapper(StructWrapper<T> value)
			: this(value.Value) { }

		IScalar IScalar.Add(IScalar value)
		{
			return this.Add((StructWrapper<T>)value);
		}

		IScalar IScalar.Multiply(IScalar value)
		{
			return this.Multiply((StructWrapper<T>)value);
		}

		IScalar IScalar.Subtract(IScalar value)
		{
			return this.Subtract((StructWrapper<T>)value);
		}

		IScalar IScalar.Divide(IScalar value)
		{
			return this.Divide((StructWrapper<T>)value);
		}

		public abstract StructWrapper<T> Add(StructWrapper<T> value);
		public abstract StructWrapper<T> Multiply(StructWrapper<T> value);
		public abstract StructWrapper<T> Subtract(StructWrapper<T> value);
		public abstract StructWrapper<T> Divide(StructWrapper<T> value);

		public override string ToString()
		{
			return this.Value.ToString();
		}

		public string ToString(IFormatProvider formatProvider, string format)
		{
			return this.Value.ToFormatString(formatProvider, format);
		}

		public override bool Equals(object obj)
		{
			return this.Value.Equals(((StructWrapper<T>)obj).Value);
		}

		public bool IsEqualTo(IScalar value)
		{
			return this.IsEqualTo((StructWrapper<T>)value);
		}

		public bool IsEqualTo(StructWrapper<T> value)
		{
			return this.Value.Equals(value.Value);
		}

		bool ILinearScalar.IsGreaterThan(ILinearScalar value)
		{
			return this.IsGreaterThan((StructWrapper<T>)value);
		}

		bool ILinearScalar.IsSmallerThan(ILinearScalar value)
		{
			return this.IsSmallerThan((StructWrapper<T>)value);
		}

		public abstract bool IsGreaterThan(StructWrapper<T> value);
		public abstract bool IsSmallerThan(StructWrapper<T> value);

		public static implicit operator T(StructWrapper<T> value)
		{
			return value.Value;
		}
	}
}
