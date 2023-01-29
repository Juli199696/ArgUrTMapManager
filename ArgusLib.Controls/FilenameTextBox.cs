using System;
using System.Collections.Generic;
using System.IO;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A <see cref="TextBox"/> that will only accepts valid filenames as input.
	/// </summary>
	public class FilenameTextBox : TextBox
	{
		List<char> invalidChars;

		/// <summary>
		/// Creates a new instance of <see cref="FilenameTextBox"/>.
		/// </summary>
		public FilenameTextBox()
			: base()
		{
			this.invalidChars = new List<char>(Path.GetInvalidFileNameChars());
		}

		protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			if (this.invalidChars.Contains(e.KeyChar) == true)
				e.Handled = true;
			base.OnKeyPress(e);
		}
	}
}
