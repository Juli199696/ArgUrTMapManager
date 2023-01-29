using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Controls
{
	public abstract class NumericTextBox<T> : TextBox
	{
		protected abstract CharSet[] GetAllowedChars();
		protected abstract bool ValidateText(string text, out T value);

		public T Value { get; private set; }

		string lastValidatedText;

		public NumericTextBox()
			: base()
		{
			this.AllowedChars = this.GetAllowedChars();
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			T value;
			if (this.ValidateText(this.Text, out value) == true)
			{
				this.lastValidatedText = this.Text;
				this.Value = value;
			}
		}

		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				this.Text = this.lastValidatedText;
				this.SelectionStart = this.lastValidatedText.Length;
			}
		}

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			this.Text = this.lastValidatedText;
		}
	}
}
