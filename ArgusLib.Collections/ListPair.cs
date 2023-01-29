using System;
using System.Collections;
using System.Collections.Generic;

namespace ArgusLib.Collections
{
	/// <summary>
	/// A pair of a <see cref="List{T1}"/> and a <see cref="List{T2}"/>. The lists on their own are accessible via
	/// <see cref="ListPair{T1,T2}.List1"/>/<see cref="ListPair{T1,T2}.List2"/>.
	/// </summary>
	public class ListPair<T1,T2> : IList<ItemPair<T1,T2>>
	{
		List<T1> list1;
		List<T2> list2;
		ImmutableList<T1> imList1;
		ImmutableList<T2> imList2;

		/// <summary>
		/// Gets a <see cref="ImmutableList{T1}"/> which provides restricted access to the
		/// items of the first list.
		/// </summary>
		public ImmutableList<T1> List1 { get { return this.imList1; } }
		/// <summary>
		/// Gets a <see cref="ImmutableList{T2}"/> which provides restricted access to the
		/// items of the second list.
		/// </summary>
		public ImmutableList<T2> List2 { get { return this.imList2; } }

		/// <summary>
		/// Creates a new instance of <see cref="ListPair{T1,T2}"/>.
		/// </summary>
		public ListPair()
		{
			this.list1 = new List<T1>();
			this.imList1 = new ImmutableList<T1>(this.list1);
			this.list2 = new List<T2>();
			this.imList2 = new ImmutableList<T2>(this.list2);
		}

		/// <summary>
		/// Creates a new instance of <see cref="ListPair{T1,T2}"/>.
		/// </summary>
		public ListPair(IEnumerable<ItemPair<T1, T2>> items)
			:this()
		{
			foreach (ItemPair<T1, T2> item in items)
				this.Add(item);
		}

		public ListPair(int capacity)
		{
			this.list1 = new List<T1>(capacity);
			this.imList1 = new ImmutableList<T1>(this.list1);
			this.list2 = new List<T2>(capacity);
			this.imList2 = new ImmutableList<T2>(this.list2);
		} 

		/// <summary>
		/// Creates a new instance of <see cref="ListPair{T1,T2}"/>.
		/// <paramref name="items1"/> and <paramref name="items2"/> must contain the same numbers of elements.
		/// </summary>
		/// <param name="items1">A <see cref="System.Collections.Generic.IEnumerable{T1}"/>.</param>
		/// <param name="items2">A <see cref="System.Collections.Generic.IEnumerable{T2}"/>.</param>
		public ListPair(IEnumerable<T1> items1, IEnumerable<T2> items2)
			: this()
		{
			IEnumerator<T1> e1 = items1.GetEnumerator();
			IEnumerator<T2> e2 = items2.GetEnumerator();

			while (true)
			{
				bool b1 = e1.MoveNext();
				bool b2 = e2.MoveNext();
				if (b1 != b2)
					throw new ArgumentException("items1 and items2 must contain the same number of elements.");
				if (b1 == false)
					break;

				this.Add(new ItemPair<T1, T2>(e1.Current, e2.Current));
			}
		}

		/// <summary>
		/// Copies the elements of the <see cref="ListPair{T1,T2}"/> to a new array.
		/// </summary>
		/// <returns>Type: <see cref="ItemPair{T1,T2}"/>[]</returns>
		public ItemPair<T1, T2>[] ToArray()
		{
			ItemPair<T1, T2>[] array = new ItemPair<T1, T2>[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		public void Add(T1 item1, T2 item2)
		{
			this.list1.Add(item1);
			this.list2.Add(item2);
		}

		public void Insert(int index, T1 item1, T2 item2)
		{
			this.list1.Insert(index, item1);
			this.list2.Insert(index, item2);
		}

		#region IList

		/// <summary>
		/// Implements <see cref="ICollection{T}.Add"/>.
		/// </summary>
		public void Add(ItemPair<T1, T2> item)
		{
			this.list1.Add(item.Item1);
			this.list2.Add(item.Item2);
		}

		/// <summary>
		/// Implements <see cref="ICollection{T}.Clear"/>.
		/// </summary>
		public void Clear()
		{
			this.list1.Clear();
			this.list2.Clear();
		}

		/// <summary>
		/// Implements <see cref="ICollection{T}.Contains"/>.
		/// </summary>
		public bool Contains(ItemPair<T1, T2> item)
		{
			if (this.IndexOf(item) < 0)
				return false;
			return true;
		}

		/// <summary>
		/// Implements <see cref="ICollection{T}.CopyTo"/>.
		/// </summary>
		public void CopyTo(ItemPair<T1, T2>[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>
		/// Implements <see cref="IEnumerable{T}.GetEnumerator"/>.
		/// </summary>
		public IEnumerator<ItemPair<T1,T2>> GetEnumerator()
		{
			return new Enumerator(this);
		}

		/// <summary>
		/// Implements <see cref="IEnumerable.GetEnumerator"/>.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>
		/// Implements <see cref="IList{T}.IndexOf"/>.
		/// </summary>
		public int IndexOf(ItemPair<T1, T2> item)
		{
			int index = this.list1.IndexOf(item.Item1);
			if (index < 0)
				return -1;
			if (index == this.list2.IndexOf(item.Item2))
				return index;
			return -1;
		}

		/// <summary>
		/// Implements <see cref="IList{T}.Insert"/>.
		/// </summary>
		public void Insert(int index, ItemPair<T1, T2> item)
		{
			this.list1.Insert(index, item.Item1);
			this.list2.Insert(index, item.Item2);
		}

		/// <summary>
		/// Implements <see cref="ICollection{T}.Remove"/>.
		/// </summary>
		public bool Remove(ItemPair<T1, T2> item)
		{
			int index = this.IndexOf(item);
			if (index < 0)
				return false;
			this.list1.RemoveAt(index);
			this.list2.RemoveAt(index);
			return true;
		}

		/// <summary>
		/// Implements <see cref="IList{T}.RemoveAt"/>.
		/// </summary>
		public void RemoveAt(int index)
		{
			this.list1.RemoveAt(index);
			this.list2.RemoveAt(index);
		}

		/// <summary>
		/// Gets the number of elements the <see cref="ListPair{T1,T2}"/> contains.
		/// Implements <see cref="ICollection{T}.Count"/>.
		/// </summary>
		public int Count { get { return this.list1.Count; } }

		/// <summary>
		/// Implements <see cref="ICollection{T}.IsReadOnly"/>.
		/// Always returns false.
		/// </summary>
		public bool IsReadOnly { get { return false; } }

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		public ItemPair<T1, T2> this[int index]
		{
			get
			{
				return new ItemPair<T1, T2>(this.list1[index], this.list2[index]);
			}
			set
			{
				this.list1[index] = value.Item1;
				this.list2[index] = value.Item2;
			}
		}

		#endregion

		/// <summary>
		/// This class implements <see cref="IEnumerator{T}"/> for <see cref="ListPair{T1,T2}"/>.
		/// <seealso cref="IEnumerator{T}"/>.
		/// </summary>
		public class Enumerator : IEnumerator<ItemPair<T1, T2>>
		{
			ListPair<T1, T2> listPair;
			int index;

			/// <summary>
			/// Creates a new instance of <see cref="Enumerator"/>.
			/// </summary>
			/// <param name="listPair">The <see cref="ListPair{T1,T2}"/> to get an <see cref="Enumerator"/> for.</param>
			public Enumerator(ListPair<T1, T2> listPair)
			{
				this.listPair = listPair;
				this.index = -1;
			}

			/// <summary>
			/// Move to the next element.
			/// </summary>
			public bool MoveNext()
			{
				this.index++;
				if (this.index < this.listPair.Count)
					return true;
				return false;
			}

			/// <summary>
			/// Resets the <see cref="Enumerator"/>.
			/// </summary>
			public void Reset()
			{
				this.index = -1;
			}

			/// <summary>
			/// Gets the current element.
			/// </summary>
			public ItemPair<T1, T2> Current { get { return this.listPair[this.index]; } }
			object IEnumerator.Current { get { return this.Current; } }

			/// <summary>
			/// Implements <see cref="IDisposable.Dispose"/>. 
			/// </summary>
			public void Dispose() { }
		}
	}
}
