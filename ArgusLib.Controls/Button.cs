using System;
using System.Collections.Generic;
using System.Drawing;

namespace ArgusLib.Controls
{
	public class Button : System.Windows.Forms.Button
	{
		private Size autoSizeValue;
		public Button()
			: base()
		{
			this.Padding = new System.Windows.Forms.Padding(3);
			this.autoSizeValue = this.Size;
		}

		public event EventHandler RotationChanged;

		/// <summary>
		/// Gets or sets a value specifying the angle the displayed text is rotated.
		/// </summary>
		/// <value>
		/// 0 = 0° Rotation,
		/// 1 = 90° Rotation,
		/// 2 = 190° Rotation,
		/// 3 = 270° Rotation
		/// </value>
		public int Rotation
		{
			get { return this._Rotation; }
			set
			{
				bool changed = this._Rotation != value;
				this._Rotation = value;
				if (changed == true)
					this.OnRotationChanged(EventArgs.Empty);
			}
		}
		int _Rotation;

		protected virtual void OnRotationChanged(EventArgs e)
		{
			if (this.AutoSize == true)
				this.autoSizeValue = this.GetAutoSize();
			if (this.RotationChanged != null)
				this.RotationChanged(this, e);
			this.OnResize(e);
		}

		protected override void OnPaddingChanged(EventArgs e)
		{
			if (this.AutoSize == true)
				this.autoSizeValue = this.GetAutoSize();
			base.OnPaddingChanged(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (this.AutoSize == true)
				this.autoSizeValue = this.GetAutoSize();
			base.OnTextChanged(e);
		}

		protected override void OnAutoSizeChanged(EventArgs e)
		{
			if (this.AutoSize == true)
				this.autoSizeValue = this.GetAutoSize();
			base.OnAutoSizeChanged(e);
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.AutoSize == true)
				this.Size = this.autoSizeValue;
		}

		private Size GetAutoSize()
		{
			using (Graphics g = this.CreateGraphics())
			{
				SizeF size = g.MeasureString(this.Text, this.Font);
				if (this.Rotation % 2 == 1)
					size = new SizeF(size.Height, size.Width);
				return new Size((int)(size.Width + this.Padding.Horizontal), (int)(size.Height + this.Padding.Vertical));
			}
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			Graphics g = pevent.Graphics;
			g.FillRectangle(
				new SolidBrush(this.BackColor),
				this.ClientRectangle.X + this.Padding.Left,
				this.ClientRectangle.Y + this.Padding.Top,
				this.ClientRectangle.Width - this.Padding.Horizontal,
				this.ClientRectangle.Height - this.Padding.Vertical);
			g.ResetTransform();
			SizeF size = g.MeasureString(this.Text, this.Font);
			if (size.Width > this.ClientSize.Width - this.Padding.Horizontal)
				size.Width = this.ClientSize.Width - this.Padding.Horizontal;
			g.TranslateTransform(-size.Width / 2, -size.Height / 2);
			g.RotateTransform(this.Rotation * 90, System.Drawing.Drawing2D.MatrixOrder.Append);
			g.TranslateTransform(size.Width / 2, size.Height / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
			
			g.TranslateTransform((this.ClientSize.Width - size.Width) / 2.0f, (this.ClientSize.Height - size.Height) / 2.0f, System.Drawing.Drawing2D.MatrixOrder.Append);
			g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), 0, 0);
		}
	}
}
