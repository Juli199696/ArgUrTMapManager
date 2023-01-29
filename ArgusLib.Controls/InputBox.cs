using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A Dialog similar to <see cref="MessageBox"/> which takes an input-<see cref="string"/>.
	/// </summary>
	public partial class InputBox : BaseForm
	{
		private string input;
		private InputBox()
		{
			InitializeComponent();
		}

		/// <summary>
		/// See <see cref="Control.OnBackColorChanged"/>.
		/// </summary>
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (this.tbInput != null)
				this.tbInput.BackColor = this.BackColor;
			if (this.bOK != null)
				this.bOK.BackColor = this.BackColor;
			base.OnBackColorChanged(e);
		}

		/// <summary>
		/// See <see cref="Control.OnForeColorChanged"/>.
		/// </summary>
		protected override void OnForeColorChanged(EventArgs e)
		{
			if (this.tbInput != null)
				this.tbInput.ForeColor = this.ForeColor;
			if (this.bOK != null)
				this.bOK.ForeColor = this.ForeColor;
			base.OnForeColorChanged(e);
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			this.input = this.tbInput.Text;
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void lText_SizeChanged(object sender, EventArgs e)
		{
			int width = 2 * 12 + this.lText.Width;
			int height = 9 + this.lText.Height + 3 + this.tbInput.Height + 6 + this.bOK.Height + 12;
			this.ClientSize = new Size(width, height);
		}

		/// <summary>
		/// Shows a <see cref="InputBox"/> as modal Dialog.
		/// </summary>
		/// <param name="text">A <see cref="string"/> specifying the text to display.</param>
		/// <param name="caption">A <see cref="string"/> specifying the title of the <see cref="InputBox"/>.</param>
		/// <returns>The value that was inputed by the user on success, otherwhise null.</returns>
		public static string Show(string text, string caption)
		{
			InputBox ib = new InputBox();
			ib.Text = caption;
			ib.lText.Text = text;
			if (ib.ShowDialog() != DialogResult.OK)
				return null;
			return ib.input;
		}

		private void tbInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				this.bOK.PerformClick();
		}
	}
}
