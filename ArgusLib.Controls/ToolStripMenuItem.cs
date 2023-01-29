using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A extended <see cref="System.Windows.Forms.ToolStripMenuItem"/> which fully supports user-defined
	/// back- and foregroundcolors.
	/// </summary>
	public class ToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
	{
		Color _BackColor;
		Color _ForeColor;
		bool mouseEntered = false;

		/// <summary>
		/// Creates a new instance of <see cref="ToolStripMenuItem"/>.
		/// </summary>
		public ToolStripMenuItem()
			: base()
		{
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.IsDropDownOpen = false;
		}

		public override Color BackColor
		{
			get
			{
				if (this.DesignMode == true)
					return this._BackColor;
				return base.BackColor;
			}
			set
			{
				this._BackColor = value;
				base.BackColor = value;
			}
		}

		public override Color ForeColor
		{
			get
			{
				if (this.DesignMode == true)
					return this._ForeColor;
				return base.ForeColor;
			}
			set
			{
				this._ForeColor = value;
				base.ForeColor = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the controls <see cref="System.Windows.Forms.ToolStripDropDownItem.DropDown"/>
		/// is open.
		/// </summary>
		public bool IsDropDownOpen { get; private set; }

		protected override void OnDropDownOpened(EventArgs e)
		{
			this.IsDropDownOpen = true;
			base.OnDropDownOpened(e);
			base.ForeColor = Color.Black;
			base.BackColor = Color.White;
		}

		protected override void OnDropDownClosed(EventArgs e)
		{
			this.IsDropDownOpen = false;
			base.OnDropDownClosed(e);
			base.BackColor = this._BackColor;
			if (this.mouseEntered == false)
				base.ForeColor = this._ForeColor;
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			this.mouseEntered = true;
			base.OnMouseEnter(e);
			base.ForeColor = Color.Black;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			this.mouseEntered = false;
			base.OnMouseLeave(e);
			if (this.IsDropDownOpen == false)
				base.ForeColor = this._ForeColor;
		}
	}
}
