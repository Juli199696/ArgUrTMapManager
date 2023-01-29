using System;
using System.Collections.Generic;

namespace ArgusLib.Collections
{
	/// <summary>
	/// Defines a pair of items.
	/// </summary>
	public struct ItemPair<T1, T2>
	{
		/// <summary>
		/// Gets or sets a value of <see cref="Type"/> <typeparamref name="T1"/>.
		/// </summary>
		public T1 Item1 { get; set; }
		/// <summary>
		/// Gets or sets a value of <see cref="Type"/> <typeparamref name="T2"/>.
		/// </summary>
		public T2 Item2 { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="ItemPair{T1,T2}"/>.
		/// </summary>
		public ItemPair(T1 Item1, T2 Item2)
			: this()
		{
			this.Item1 = Item1;
			this.Item2 = Item2;
		}
	}
}
