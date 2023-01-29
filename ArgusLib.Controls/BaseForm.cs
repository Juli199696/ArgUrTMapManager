using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

namespace ArgusLib.Controls
{
	/// <summary>
	/// Represents a base for custom <see cref="Form"/>s.
	/// </summary>
	public class BaseForm : Form
	{
		/// <summary>
		/// Gets a value indicating whether the <see cref="BaseForm"/> is shown.
		/// </summary>
		public bool IsShown { get; private set; }

		/// <summary>
		/// This Property is set in <see cref="BaseForm.OnLoad"/> if <see cref="BaseForm.IsMdiContainer"/> is <c>true</c>.
		/// Otherwise it's <c>null</c>.
		/// </summary>
		protected MdiClient MdiClient { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the <see cref="Control.ForeColor"/> and
		/// <see cref="Control.BackColor"/> of the <see cref="Control"/>s in <see cref="Controls"/>
		/// should be changed automatically.
		/// </summary>
		/// <value>
		/// The default is false.
		/// If the value is set to true, <see cref="ResetChildrenColors"/> is called internally.
		/// </value>
		public bool AutoChangeChildrensColors
		{
			get { return this.autoChangeChildrensColors; }
			set
			{
				this.autoChangeChildrensColors = value;
				if (value == true)
					this.ResetChildrensColors();
			}
		}
		bool autoChangeChildrensColors = false;

		public int MdiChildrenDockDistance { get; set; }

		/// <summary>
		/// Creates a new instance of <see cref="BaseForm"/>.
		/// </summary>
		public BaseForm()
			: base()
		{
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.IsShown = false;
			this.MdiChildrenDockDistance = 0;
		}

		/// <summary>
		/// See <see cref="Form.OnLoad"/>.
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.IsMdiContainer == true)
			{
				this.InitializeAsMdiContainer();
			}
		}

		private void InitializeAsMdiContainer()
		{
			foreach (Control c in this.Controls)
			{
				if (c is MdiClient)
				{
					this.MdiClient = (MdiClient)c;
					break;
				}
			}

			foreach (Form form in this.MdiChildren)
			{
				form.LocationChanged += mdiChild_LocationChanged;
			}

			if (this.MdiClient != null)
			{
				this.MdiClient.ControlAdded += new ControlEventHandler((sender, args) =>
				{
					Form form = args.Control as Form;
					if (form != null)
						form.LocationChanged += this.mdiChild_LocationChanged;
				});
				this.MdiClient.ControlRemoved += new ControlEventHandler((sender, args) =>
				{
					args.Control.LocationChanged -= this.mdiChild_LocationChanged;
				});
			}
		}

		void mdiChild_LocationChanged(object sender, EventArgs e)
		{
			if (this.MdiChildrenDockDistance < 1)
				return;

			Form form = (Form)sender;

			if (form.Visible == false)
				return;

			if (Math.Abs(form.Left) < this.MdiChildrenDockDistance)
				form.Left = 0;
			else if (Math.Abs(this.MdiClient.ClientSize.Width - form.Right) < this.MdiChildrenDockDistance)
				form.Left = this.MdiClient.ClientSize.Width - form.Width;
			else
			{
				foreach (Form child in this.MdiChildren)
				{

					if (form == child)
						continue;

					if (child.Visible == false)
						continue;

					if ((form.Top <= child.Bottom && form.Top >= child.Top) || (form.Bottom <= child.Bottom && form.Bottom >= child.Top))
					{
						if (Math.Abs(form.Left - child.Right) < this.MdiChildrenDockDistance)
							form.Left = child.Right;
						else if (Math.Abs(form.Right - child.Left) < this.MdiChildrenDockDistance)
							form.Left = child.Left - form.Width;
						else
							continue;
						break;
					}
				}
			}

			if (Math.Abs(form.Top) < this.MdiChildrenDockDistance)
				form.Top = 0;
			else if (Math.Abs(this.MdiClient.ClientSize.Height - form.Bottom) < this.MdiChildrenDockDistance)
				form.Top = this.MdiClient.ClientSize.Height - form.Height;
			else
			{
				foreach (Form child in this.MdiChildren)
				{
					if (form == child)
						continue;

					if (child.Visible == false)
						continue;

					if ((form.Left <= child.Right && form.Left >= child.Left) || (form.Right <= child.Right && form.Right >= child.Left))
					{
						if (Math.Abs(form.Top - child.Bottom) < this.MdiChildrenDockDistance)
							form.Top = child.Bottom;
						else if (Math.Abs(form.Bottom - child.Top) < this.MdiChildrenDockDistance)
							form.Top = child.Top - form.Height;
						else
							continue;
						break;
					}
				}
			}
		}

		/// <summary>
		/// See <see cref="Form.OnShown"/>.
		/// </summary>
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.IsShown = true;
		}

		/// <summary>
		/// See <see cref="Form.OnFormClosed"/>.
		/// </summary>
		protected override void OnFormClosed(FormClosedEventArgs e)
		{
			this.IsShown = false;
			base.OnFormClosed(e);
		}

		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			if (this.AutoChangeChildrensColors == true)
				this.RecursiveChangeForeColor();
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			if (this.AutoChangeChildrensColors == true)
				this.RecursiveChangeBackColor();
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (this.AutoChangeChildrensColors == true)
				this.ResetChildrensColors();
		}

		/// <summary>
		/// Resets the <see cref="Control.ForeColor"/> and <see cref="Control.BackColor"/> of the <see cref="Control"/>s
		/// in <see cref="Controls"/>. Has no effect if <see cref="AutoChangeChildrensColors"/> is false.
		/// </summary>
		public void ResetChildrensColors()
		{
			if (this.AutoChangeChildrensColors == false)
				return;
			BaseForm.RecursiveChangeProperties(this, new string[] { "BackColor", "ForeColor" }, new object[] { this.BackColor, this.ForeColor });
		}

		private static void RecursiveChangeProperties(Component component, string[] propertyNames, object[] propertyValues)
		{
			ToolStripDropDownItem tsddi = component as ToolStripDropDownItem;
			if (tsddi != null)
			{
				foreach (ToolStripItem item in tsddi.DropDownItems)
				{
					BaseForm.SetProperties(item, propertyNames, propertyValues);
					BaseForm.RecursiveChangeProperties(item, propertyNames, propertyValues);
				}
				return;
			}

			ToolStrip ts = component as ToolStrip;
			if (ts != null)
			{
				foreach (ToolStripItem item in ts.Items)
				{
					BaseForm.SetProperties(item, propertyNames, propertyValues);
					BaseForm.RecursiveChangeProperties(item, propertyNames, propertyValues);
				}
				return;
			}

			Control control = component as Control;
			if (control != null)
			{
				foreach (Control c in control.Controls)
				{
					BaseForm.SetProperties(c, propertyNames, propertyValues);
					BaseForm.RecursiveChangeProperties(c, propertyNames, propertyValues);
				}
			}
		}

		private static void SetProperties(Component component, string[] propertyNames, object[] propertyValues)
		{
			Type t = component.GetType();
			for (int i = 0; i < propertyNames.Length; i++)
			{
				PropertyInfo pi = t.GetProperty(propertyNames[i]);
				if (pi == null)
					continue;
				pi.SetValue(component, propertyValues[i], null);
			}
		}

		private void RecursiveChangeForeColor()
		{
			BaseForm.RecursiveChangeProperties(this, new string[] { "ForeColor" }, new object[] { this.ForeColor });
		}

		private void RecursiveChangeBackColor()
		{
			BaseForm.RecursiveChangeProperties(this, new string[] { "BackColor" }, new object[] { this.BackColor });
		}
	}
}
