using System;

namespace System.Collections.Generic
{
	/// <summary>
	/// Defines Extension-Methods for classes in the <see cref="System.Collections.Generic"/> namespace.
	/// </summary>
	public static class ArgusLibExtensions
	{
		/// <summary>
		/// Get the object at the specified index in an <see cref="IEnumerable{T}"/>.
		/// </summary>
		public static T At<T>(this IEnumerable<T> enumerable, int index)
		{
			int i = 0;
			IEnumerator<T> e = enumerable.GetEnumerator();
			while (e.MoveNext() == true)
			{
				if (i == index)
					return e.Current;
				i++;
			}
			throw new ArgumentOutOfRangeException("index");
		}
	}
}