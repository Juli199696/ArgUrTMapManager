using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Collections
{
	/// <summary>
	/// A base class read-only wrappers for arrays.
	/// </summary>
	public class ReadOnlyArrayBase : System.Collections.IEnumerable
	{
		Array array;

		public ReadOnlyArrayBase(Array array)
		{
			this.array = array;
		}

		public int Length { get { return this.array.Length; } }
		public long LongLength { get { return this.array.LongLength; } }
		public int Rank { get { return this.array.Rank; } }
		public int GetLength(int dimension) { return this.array.GetLength(dimension); }

		public System.Collections.IEnumerator GetEnumerator() { return this.array.GetEnumerator(); }
	}

	/// <summary>
	/// Read-Only wrapper for a one-dimensional array.
	/// </summary>
	public class ReadOnlyArray<T> : ReadOnlyArrayBase
	{
		T[] array;

		public ReadOnlyArray(T[] array)
			:base(array)
		{
			this.array = array;
		}

		public T this[int i] { get { return this.array[i]; } }
	}

	/// <summary>
	/// Read-Only wrapper for a two-dimensional array.
	/// </summary>
	public class ReadOnlyArray2<T> : ReadOnlyArrayBase
	{
		T[,] array;

		public ReadOnlyArray2(T[,] array)
			:base(array)
		{
			this.array = array;
		}

		public T this[int i, int j] { get { return this.array[i, j]; } }
	}

	/// <summary>
	/// Read-Only wrapper for a three-dimensional array.
	/// </summary>
	public class ReadOnlyArray3<T> : ReadOnlyArrayBase
	{
		T[,,] array;

		public ReadOnlyArray3(T[,,] array)
			:base(array)
		{
			this.array = array;
		}

		public T this[int i, int j, int k] { get { return this.array[i, j, k]; } }
	}
}
