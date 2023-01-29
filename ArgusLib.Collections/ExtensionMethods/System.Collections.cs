using System;

namespace System.Collections
{
	/// <summary>
	/// Defines Extension-Methods for classes in the <see cref="System.Collections"/> namespace.
	/// </summary>
	public static class ArgusLibExtensions
	{
		/// <summary>
		/// Get the object at the specified index in an <see cref="IEnumerable"/>.
		/// </summary>
		public static object At(this IEnumerable enumerable, int index)
		{
			int i = 0;
			IEnumerator e = enumerable.GetEnumerator();
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