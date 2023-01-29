using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public interface IVector
	{
		int Dimension { get; }
		IScalar this[int index] { get; }
	}

	public class Vector<T> : IVector, IEnumerable<T>
		where T:IScalar
	{
		T[] elements;

		public Vector(int dimension)
		{
			this.elements = new T[dimension];
		}

		public Vector(params T[] elements)
		{
			this.elements = elements;
		}

		public Vector(Vector<T> vector)
			: this(vector.ToArray()) { }

		public int Dimension { get { return this.elements.Length; } }

		public T this[int index]
		{
			get { return this.elements[index]; }
			set { this.elements[index] = value; }
		}

		IScalar IVector.this[int index]
		{
			get { return this[index]; }
		}

		public T GetSquaredEuclidNorm()
		{
			return Vector<T>.ScalarProduct(this, this);
		}

		public T[] ToArray()
		{
			T[] RetVal = new T[this.Dimension];
			this.elements.CopyTo(RetVal, 0);
			return RetVal;
		}

		public override string ToString()
		{
			return this.ToFormatString(new VectorFormatter(null, ""), "g");
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Enumerator(this);
		}

		public static T ScalarProduct(Vector<T> a, Vector<T> b)
		{
			if (a.Dimension != b.Dimension)
				throw new DimensionException();

			IScalar RetVal = a[0].Multiply(b[0]);
			for (int i = 1; i < a.Dimension; i++)
			{
				RetVal = RetVal.Add(a[i].Multiply(b[i]));
			}
			return (T)RetVal;
		}

		public static Vector<T> Add(Vector<T> a, Vector<T> b)
		{
			if (a.Dimension != b.Dimension)
				throw new DimensionException();

			Vector<T> v = new Vector<T>(a.Dimension);
			for (int i = 0; i < a.Dimension; i++)
			{
				v[i] = (T)a[i].Add(b[i]);
			}
			return v;
		}

		public static Vector<T> operator +(Vector<T> a, Vector<T> b)
		{
			return Vector<T>.Add(a, b);
		}

		public static Vector<T> Subtract(Vector<T> a, Vector<T> b)
		{
			if (a.Dimension != b.Dimension)
				throw new DimensionException();

			Vector<T> v = new Vector<T>(a.Dimension);
			for (int i = 0; i < a.Dimension; i++)
			{
				v[i] = (T)a[i].Subtract(b[i]);
			}
			return v;
		}

		public static Vector<T> operator -(Vector<T> a, Vector<T> b)
		{
			return Vector<T>.Subtract(a, b);
		}

		public static Vector<T> Multiply(Vector<T> a, T b)
		{
			Vector<T> v = new Vector<T>(a.Dimension);
			for (int i = 0; i < a.Dimension; i++)
			{
				v[i] = (T)a[i].Multiply(b);
			}
			return v;
		}

		public static Vector<T> operator *(Vector<T> a, T b)
		{
			return Vector<T>.Multiply(a, b);
		}

		public static Vector<T> operator *(T a, Vector<T> b)
		{
			return Vector<T>.Multiply(b, a);
		}

		public static T operator *(Vector<T> a, Vector<T> b)
		{
			return Vector<T>.ScalarProduct(a, b);
		}

		/// <example>
		/// <include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public static bool TryParse(string text, out Vector<T> value)
		{
			if (Vector<T>.TryParse(text, out value, null, "g") == true)
				return true;
			if (Vector<T>.TryParse(text, out value, null, "a") == true)
				return true;
			if (Vector<T>.TryParse(text, out value, null, "b") == true)
				return true;
			return false;
		}

		/// <example>
		/// <include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public static bool TryParse(string text, out Vector<T> value, IFormatProvider formatProvider, string format)
		{
			Parser.TryParseHandler<T> tryParseHandler = Parser.GetTryParseHandler<T>();
			if (tryParseHandler == null)
			{
				throw new Exception("The type of the scalar (" + typeof(T).Name + ") does not declare a methond named TryParse");
			}
			if (formatProvider != null && !(formatProvider is VectorFormatter))
			{
				throw new ArgumentException("Unexpected IFormatProvider, VectorFormatter expected.", "formatProvider");
			}

			format = VectorFormatter.GetFormat(format);
			string[] parts = format.Split('\0');
			if (parts.Length != 3)
			{
				throw new ArgumentException("Invalid format", "format");
			}

			for (int i = 0; i < parts.Length; i++)
			{
				string temp = parts[i].Trim();
				if (temp.Length > 0)
					parts[i] = temp;
			}

			if (text.StartsWith(parts[0]) == true)
				text = text.Substring(parts[0].Length);
			if (text.EndsWith(parts[2]) == true)
				text = text.Substring(0, text.Length-parts[2].Length);

			text = text.Trim();

			string[] scalars = text.Split(new string[] { parts[1] }, StringSplitOptions.None);
			if (scalars.Length < 1)
			{
				value = new Vector<T>();
				return false;
			}

			value = new Vector<T>(scalars.Length);

			for (int i = 0; i < scalars.Length; i++)
			{
				scalars[i] = scalars[i].Trim();
				T val;
				if (tryParseHandler(scalars[i], out val) == false)
					return false;
				value[i] = val;
			}
			return true;
		}

		public class Enumerator : IEnumerator<T>
		{
			int index;
			Vector<T> vector;

			public T Current { get { return this.vector[this.index]; } }
			object IEnumerator.Current { get { return this.vector[this.index]; } }

			public Enumerator(Vector<T> vector)
			{
				this.vector = vector;
				this.index = -1;
			}

			public bool MoveNext()
			{
				this.index++;
				return this.index < this.vector.Dimension;
			}

			public void Reset()
			{
				this.index = -1;
			}

			public void Dispose()
			{
				this.vector = null;
			}
		}
	}
}
