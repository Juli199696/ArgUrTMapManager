using System;
using System.Collections;
using System.Collections.Generic;

namespace ArgusLib.Collections
{
	public class ListTriple<T1,T2,T3> : IList<ItemTriple<T1, T2, T3>>
	{
		List<T1> list1;
		List<T2> list2;
		List<T3> list3;
		ImmutableList<T1> imList1;
		ImmutableList<T2> imList2;
		ImmutableList<T3> imList3;

		public ImmutableList<T1> List1 { get { return this.imList1; } }
		public ImmutableList<T2> List2 { get { return this.imList2; } }
		public ImmutableList<T3> List3 { get { return this.imList3; } }

		public ListTriple()
		{
			this.list1 = new List<T1>();
			this.imList1 = new ImmutableList<T1>(this.list1);
			this.list2 = new List<T2>();
			this.imList2 = new ImmutableList<T2>(this.list2);
			this.list3 = new List<T3>();
			this.imList3 = new ImmutableList<T3>(this.list3);
		}

		public ListTriple(IEnumerable<ItemTriple<T1, T2, T3>> items)
			:this()
		{
			foreach (ItemTriple<T1, T2, T3> item in items)
				this.Add(item);
		}

		public ListTriple(IEnumerable<T1> items1, IEnumerable<T2> items2, IEnumerable<T3> items3)
			: this()
		{
			IEnumerator<T1> e1 = items1.GetEnumerator();
			IEnumerator<T2> e2 = items2.GetEnumerator();
			IEnumerator<T3> e3 = items3.GetEnumerator();

			while (true)
			{
				bool b1 = e1.MoveNext();
				bool b2 = e2.MoveNext();
				bool b3 = e3.MoveNext();
				if (b1 != b2)
					throw new ArgumentException("items1, items2 and items3 must contain the same number of elements.");
				if (b1 != b3)
					throw new ArgumentException("items1, items2 and items3 must contain the same number of elements.");
				if (b1 == false)
					break;

				this.Add(new ItemTriple<T1, T2, T3>(e1.Current, e2.Current, e3.Current));
			}
		}

		public ItemTriple<T1, T2, T3>[] ToArray()
		{
			ItemTriple<T1, T2, T3>[] array = new ItemTriple<T1, T2, T3>[this.Count];
			this.CopyTo(array, 0);
			return array;
		}

		public void Add(T1 item1, T2 item2, T3 item3)
		{
			this.list1.Add(item1);
			this.list2.Add(item2);
			this.list3.Add(item3);
		}

		public void Insert(int index, T1 item1, T2 item2, T3 item3)
		{
			this.list1.Insert(index, item1);
			this.list2.Insert(index, item2);
			this.list3.Insert(index, item3);
		}

		#region IList

		public void Add(ItemTriple<T1, T2, T3> item)
		{
			this.list1.Add(item.Item1);
			this.list2.Add(item.Item2);
			this.list3.Add(item.Item3);
		}

		public void Clear()
		{
			this.list1.Clear();
			this.list2.Clear();
			this.list3.Clear();
		}

		public bool Contains(ItemTriple<T1, T2, T3> item)
		{
			if (this.IndexOf(item) < 0)
				return false;
			return true;
		}

		public void CopyTo(ItemTriple<T1, T2, T3>[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		public IEnumerator<ItemTriple<T1, T2, T3>> GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public int IndexOf(ItemTriple<T1, T2, T3> item)
		{
			int index = this.list1.IndexOf(item.Item1);
			if (index < 0)
				return -1;
			if (index != this.list2.IndexOf(item.Item2))
				return -1;
			if (index != this.list3.IndexOf(item.Item3))
				return -1;
			return index;
		}

		public void Insert(int index, ItemTriple<T1, T2, T3> item)
		{
			this.list1.Insert(index, item.Item1);
			this.list2.Insert(index, item.Item2);
			this.list3.Insert(index, item.Item3);
		}

		public bool Remove(ItemTriple<T1, T2, T3> item)
		{
			int index = this.IndexOf(item);
			if (index < 0)
				return false;
			this.list1.RemoveAt(index);
			this.list2.RemoveAt(index);
			this.list3.RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			this.list1.RemoveAt(index);
			this.list2.RemoveAt(index);
			this.list3.RemoveAt(index);
		}

		public int Count { get { return this.list1.Count; } }

		public bool IsReadOnly { get { return false; } }

		public ItemTriple<T1, T2, T3> this[int index]
		{
			get
			{
				return new ItemTriple<T1, T2, T3>(this.list1[index], this.list2[index], this.list3[index]);
			}
			set
			{
				this.list1[index] = value.Item1;
				this.list2[index] = value.Item2;
				this.list3[index] = value.Item3;
			}
		}

		#endregion

		public class Enumerator : IEnumerator<ItemTriple<T1, T2, T3>>
		{
			ListTriple<T1, T2, T3> ListTriple;
			int index;

			public Enumerator(ListTriple<T1, T2, T3> ListTriple)
			{
				this.ListTriple = ListTriple;
				this.index = -1;
			}

			public bool MoveNext()
			{
				this.index++;
				if (this.index < this.ListTriple.Count)
					return true;
				return false;
			}

			public void Reset()
			{
				this.index = -1;
			}

			public ItemTriple<T1, T2, T3> Current { get { return this.ListTriple[this.index]; } }
			object IEnumerator.Current { get { return this.Current; } }

			public void Dispose() { }
		}
	}
}
