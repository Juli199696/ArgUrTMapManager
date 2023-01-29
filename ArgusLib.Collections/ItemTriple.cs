using System;
using System.Collections.Generic;

namespace ArgusLib.Collections
{
	/// <summary>
	/// Defines a triple of items.
	/// </summary>
	public struct ItemTriple<T1, T2, T3>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set; }
		public T3 Item3 { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="ItemTriple{T1,T2,T3}"/>
		/// </summary>
		public ItemTriple(T1 Item1, T2 Item2, T3 Item3)
			: this()
		{
			this.Item1 = Item1;
			this.Item2 = Item2;
			this.Item3 = Item3;
		}
	}
}
