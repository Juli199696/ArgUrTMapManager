using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ArgusLib.Math
{
	public class Vector3<T> : Vector<T>
		where T : IScalar
	{
		public T X { get { return this[0]; } set { this[0] = value; } }
		public T Y { get { return this[1]; } set { this[1] = value; } }
		public T Z { get { return this[2]; } set { this[2] = value; } }

		public Vector3()
			: base(3) { }

		public Vector3(T x, T y, T z)
			: base(new T[] { x, y, z }) { }

		public Vector3(Vector<T> vector)
			: this()
		{
			if (vector.Dimension != 3)
				throw new DimensionException();
			this.X = vector[0];
			this.Y = vector[1];
			this.Z = vector[2];
		}

		public T GetSquaredLength()
		{
			return this.GetSquaredEuclidNorm();
		}

		public static Vector3<T> CrossProduct(Vector3<T> a, Vector3<T> b)
		{
			return new Vector3<T>(
				(T)a.Y.Multiply(b.Z).Subtract(a.Z.Multiply(b.Y)),
				(T)a.Z.Multiply(b.X).Subtract(a.X.Multiply(b.Z)),
				(T)a.X.Multiply(b.Y).Subtract(a.Y.Multiply(b.X)));
		}
	}
}
