using System;
using System.Collections;
using System.Collections.Generic;

namespace ArgusLib.Collections
{
	/// <summary>
	/// A class providing read-only access to a <see cref="List{T}"/>.
	/// </summary>
	public class ImmutableList<T> : IEnumerable<T>
	{
		IList<T> list;

		/// <summary>
		/// Creates a new instance of <see cref="ImmutableList{T}"/> which will provide
		/// read-only access to <paramref name="list"/>.
		/// </summary>
		public ImmutableList(IList<T> list)
		{
			this.list = list;
		}

		/// <summary>
		/// Calls <see cref="List{T}.Contains(T)"/> on the wrapped <see cref="List{T}"/>.
		/// </summary>
		public bool Contains(T item){return this.list.Contains(item);}
		/// <summary>
		/// Calls <see cref="List{T}.IndexOf(T)"/> on the wrapped <see cref="List{T}"/>.
		/// </summary>
		public int IndexOf(T item) { return this.list.IndexOf(item); }
		/// <summary>
		/// Calls <see cref="List{T}.IndexOf(T,int)"/> on the wrapped <see cref="List{T}"/>.
		/// </summary>
		//public int IndexOf(T item, int index) { return this.list.IndexOf(item, index); }
		/// <summary>
		/// Calls <see cref="List{T}.IndexOf(T,int,int)"/> on the wrapped <see cref="List{T}"/>.
		/// </summary>
		//public int IndexOf(T item, int index, int count) { return this.list.IndexOf(item, index, count); }
		/// <summary>
		/// Returns the element at the specified index.
		/// </summary>
		public T this[int index] { get { return this.list[index]; } }
		/// <summary>
		/// Calls <see cref="List{T}.ToArray()"/> on the wrapped <see cref="List{T}"/>.
		/// </summary>
		public T[] ToArray()
		{
			T[] t = new T[this.list.Count];
			this.list.CopyTo(t, 0);
			return t;
		}

		#region ICollection
		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public void CopyTo(T[] array, int index)
		{
			this.list.CopyTo(array, index);
		}

		public int Count { get { return this.list.Count; } }
		#endregion
	}
}
