using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace ArgusLib.Controls
{
	public class ColorComboBox : ComboBox
	{
		public enum SortModes
		{
			Alphabetical,
			Color
		}

		List<Color> colors;

		public ColorComboBox()
			: base()
		{
			base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.InitializeItems();
		}

		private void InitializeItems()
		{
			PropertyInfo[] pis = typeof(Color).GetProperties(BindingFlags.Public | BindingFlags.Static);
			base.Items.Clear();
			base.Items.Add("choose...");
			Color[] colors = new Color[pis.Length + 1];
			colors[0] = Color.Transparent;
			for (int i = 0; i < pis.Length; i++)
			{
				colors[i+1] = (Color)pis[i].GetValue(null, null);
				base.Items.Add(pis[i].Name);
			}
			this.colors = new List<Color>(colors);
			this.SelectColor(Color.Black);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DrawMode DrawMode
		{
			get { return base.DrawMode; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ObjectCollection Items
		{
			get { return null; }
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Color SelectedColor
		{
			get { return this.colors[this.SelectedIndex]; }
		}

		public bool SelectColor(Color color)
		{
			int i = this.colors.LastIndexOf(color);
			if (i < 0)
				return false;
			this.SelectedIndex = i;
			return true;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			base.OnDrawItem(e);
			if (e.Index >= this.colors.Count)
				return;

			int index = e.Index;
			if (index < 0)
				index = this.SelectedIndex;
			if (index < 0)
				return;

			Color back = this.colors[index];
			//Color fore = Color.FromArgb((byte)(255 - back.R), (byte)(255 - back.G), (byte)(255 - back.B));

			Brush bgBrush = new SolidBrush(back);
			Brush fgBrush = new SolidBrush(e.ForeColor);
			e.DrawBackground();
			e.Graphics.FillRectangle(bgBrush, e.Bounds.X, e.Bounds.Y, this.ItemHeight, this.ItemHeight);
			//string text = back.GetHue().ToString() + " " + back.GetSaturation().ToString() + " " + back.GetBrightness().ToString();
			e.Graphics.DrawString((string)base.Items[index], e.Font, fgBrush, e.Bounds.X+2+this.ItemHeight, e.Bounds.Y);
			//e.Graphics.DrawString(text, e.Font, fgBrush, e.Bounds.X + 2 + this.ItemHeight, e.Bounds.Y);
		}

		bool inSelectedIndexChanged = false;
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			if (this.inSelectedIndexChanged == true)
				return;

			if (this.SelectedIndex != 0)
			{
				base.OnSelectedIndexChanged(e);
				return;
			}

			ColorDialog colorDialog = new ColorDialog();
			if (colorDialog.ShowDialog() != DialogResult.OK)
			{
				base.OnSelectedIndexChanged(e);
				return;
			}

			this.colors[0] = colorDialog.Color;
			this.inSelectedIndexChanged = true;
			base.Items[0] = colorDialog.Color.Name + " (choose...)";
			this.inSelectedIndexChanged = false;
			this.Refresh();
			base.OnSelectedIndexChanged(e);
		}

		SortModes sortMode;
		public SortModes SortMode
		{
			get { return this.sortMode; }
			set
			{
				if (this.sortMode == value)
					return;
				this.sortMode = value;
				this.OnSortModeChanged(EventArgs.Empty);
			}
		}

		private void OnSortModeChanged(EventArgs e)
		{
			this.SuspendLayout();
			if (this.SortMode == SortModes.Alphabetical)
			{
				this.InitializeItems();
			}
			else if (this.SortMode == SortModes.Color)
			{
				this.Sort_Color();
			}
			this.ResumeLayout();
		}

		private void Sort_Color()
		{
			object item0 = base.Items[0];
			base.Items.Clear();
			base.Items.Add(item0);
			Color color0 = this.colors[0];
			this.colors.RemoveAt(0);

			this.colors.Sort(new Comparison<Color>((left, right) =>
				{
					if (left == right)
						return 0;
					if (left == Color.Transparent)
						return -1;
					if (right == Color.Transparent)
						return 1;

					float leftSat = left.GetSaturation();
					float rightSat = right.GetSaturation();
					float satLimit = 0.1f;
					if (leftSat < satLimit && rightSat < satLimit)
						return (int)(100 * (left.GetBrightness() - right.GetBrightness()));
					if (leftSat < satLimit || rightSat < satLimit)
						return (int)(100 * (leftSat - rightSat));

					int leftP = 6;
					int rightP = 6;
					float leftH = left.GetHue();
					float rightH = right.GetHue();
					for (int i = 0; i < 6; i++)
					{
						if (Math.Abs(leftH - i * 60) <= 30)
							leftP = i;
						if (Math.Abs(rightH - i * 60) <= 30)
							rightP = i;
					}

					if (leftP != rightP)
						return leftP - rightP;

					return (left.R + left.G + left.B - right.R - right.G - right.B);

					//return (int)(100 * (left.GetBrightness() - right.GetBrightness()));

					//float leftB = 0.5f - Math.Abs(0.5f - left.GetBrightness());
					//float rightB = 0.5f - Math.Abs(0.5f - right.GetBrightness());
					//float bLimit = 0.15f;
					//if (leftB < bLimit || rightB < bLimit)
					//	return (int)(100 * (leftB - rightB));

					//return (int)(left.GetHue()- right.GetHue());
				}));

			this.colors.Insert(0, color0);
			for (int i = 1; i < this.colors.Count; i++)
			{
				base.Items.Add(this.colors[i].Name);
			}
		}
	}
}
