using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	/// <summary>
	/// Defines Extension-Methods for classes in the <see cref="System"/> namespace.
	/// </summary>
	public static class ArgusLibExtensions
	{
		public static void Initialize(this Array array, object value)
		{
			if (array.GetType().GetElementType() != value.GetType())
				throw new ArgumentException("Type mismatch!");

			int[] indices = new int[array.Rank];
			int[] length = new int[array.Rank];
			for (int dim = 0; dim < array.Rank; dim++)
			{
				indices[dim] = 0;
				length[dim] = array.GetLength(dim);
			}

			while (indices[0] < length[0])
			{
				array.SetValue(value, indices);

				indices[array.Rank - 1]++;
				for (int i = array.Rank - 1; i > 0; i--)
				{
					if (indices[i] < length[i])
						break;

					indices[i] = 0;
					indices[i - 1]++;
				}
			}
		}

		public static void CopyTo<T>(this T[,] source, T[,] destination)
		{
			for (int i = 0; i < source.GetLength(0); i++)
			{
				for (int j = 0; j < source.GetLength(1); j++)
				{
					destination[i, j] = source[i, j];
				}
			}
		}
	}
}
