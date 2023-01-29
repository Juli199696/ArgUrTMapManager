using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ArgusLib.Controls
{
	public class ListBox : System.Windows.Forms.ListBox
	{
		public ListBox()
			: base()
		{
			base.BackColor = Color.Black;
			base.ForeColor = Color.White;
		}

		public void MoveSelectedUp()
		{
			if (this.SelectedIndices.Count < 1)
				return;

			object[] items = new object[this.Items.Count];
			List<object> unselected = new List<object>();
			List<int> isSelected = new List<int>();

			if (this.GetSelected(0) == false)
			{
				unselected.Add(this.Items[0]);
			}
			else
			{
				items[0] = this.Items[0];
				isSelected.Add(0);
			}

			for (int i = 1; i < this.Items.Count; i++)
			{
				if (this.GetSelected(i) == false)
				{
					unselected.Add(this.Items[i]);
					continue;
				}

				if (items[i - 1] == null)
				{
					items[i - 1] = this.Items[i];
					isSelected.Add(i - 1);
				}
				else
				{
					items[i] = this.Items[i];
					isSelected.Add(i);
				}
			}

			int index = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
					continue;
				items[i] = unselected[index];
				index++;
			}
			this.Items.Clear();
			this.Items.AddRange(items);
			foreach (int s in isSelected)
			{
				this.SetSelected(s, true);
			}
		}

		public void MoveSelectedDown()
		{
			if (this.SelectedIndices.Count < 1)
				return;

			object[] items = new object[this.Items.Count];
			List<object> unselected = new List<object>();
			List<int> isSelected = new List<int>();

			if (this.GetSelected(this.Items.Count-1) == false)
			{
				unselected.Add(this.Items[this.Items.Count - 1]);
			}
			else
			{
				items[this.Items.Count - 1] = this.Items[this.Items.Count - 1];
				isSelected.Add(this.Items.Count - 1);
			}

			for (int i = this.Items.Count - 2; i > -1; i--)
			{
				if (this.GetSelected(i) == false)
				{
					unselected.Add(this.Items[i]);
					continue;
				}

				if (items[i + 1] == null)
				{
					items[i + 1] = this.Items[i];
					isSelected.Add(i + 1);
				}
				else
				{
					items[i] = this.Items[i];
					isSelected.Add(i);
				}
			}

			int index = 0;
			for (int i = items.Length-1; i > -1; i--)
			{
				if (items[i] != null)
					continue;
				items[i] = unselected[index];
				index++;
			}
			this.Items.Clear();
			this.Items.AddRange(items);
			foreach (int s in isSelected)
			{
				this.SetSelected(s, true);
			}
		}

		public void RemoveSelected()
		{
			while (this.SelectedIndices.Count > 0)
			{
				this.Items.RemoveAt(this.SelectedIndices[0]);
			}
		}

		//public string[] GetItems()
		//{
		//	string[] items = new string[this.Items.Count];
		//	this.Items.CopyTo(items, 0);
		//	return items;
		//}

		//public string[] GetSelectedItems()
		//{
		//	string[] items = new string[this.SelectedItems.Count];
		//	this.SelectedItems.CopyTo(items, 0);
		//	return items;
		//}
	}
}
