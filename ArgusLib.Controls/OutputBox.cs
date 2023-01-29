using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A <see cref="Control"/> which is intended to be used to display outputtext.
	/// </summary>
	public partial class OutputBox : UserControl
	{
		private delegate void InvokeOutputHandler(string line);
		private InvokeOutputHandler invokeOutputHandler;

		/// <summary>
		/// Creates a new instance of <see cref="OutputBox"/>.
		/// </summary>
		public OutputBox()
		{
			InitializeComponent();
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;
			this.MaxOutputLength = 1 << 10;
			this.invokeOutputHandler = new InvokeOutputHandler(this.Output);
		}

		/// <summary>
		/// Gets or sets the maximal amount of characters displayed.
		/// </summary>
		public int MaxOutputLength { get; set; }

		/// <summary>
		/// Appends a new line of text specified by <paramref name="line"/> to the output text.
		/// If the amount of characters in the <see cref="OutputBox"/> exceeds <see cref="OutputBox.MaxOutputLength"/>,
		/// the first outputed text is removed.
		/// </summary>
		/// <param name="line">A <see cref="string"/> specifying the text to output.</param>
		public void Output(string line)
		{
			string text = this.textBox.Text + line + Environment.NewLine;
			int i = 0;
			if (text.Length > this.MaxOutputLength)
			{
				i = text.IndexOf(Environment.NewLine, text.Length - this.MaxOutputLength);
				if (i < 0)
					i = 0;
			}
			this.textBox.Text = text.Substring(i);
			this.textBox.SelectionLength = 0;
			this.textBox.SelectionStart = this.textBox.Text.Length;
			this.textBox.ScrollToCaret();
		}

		/// <summary>
		/// Invokes <see cref="OutputBox.Output(string)"/>.
		/// </summary>
		public bool InvokeOutput(string line)
		{
			if (this.IsHandleCreated == false)
				return false;
			if (this.InvokeRequired == true)
				this.Invoke(this.invokeOutputHandler, line);
			else
				this.Output(line);
			return true;
		}

		/// <summary>
		/// Clears the output by removing all text.
		/// </summary>
		public void Clear()
		{
			this.textBox.Text = string.Empty;
		}

		/// <summary>
		/// Gets or sets the Background<see cref="Color"/> of the <see cref="OutputBox"/>.
		/// The default is <see cref="Color.Black"/>.
		/// </summary>
		public override Color BackColor
		{
			get { return this.textBox.BackColor; }
			set { this.textBox.BackColor = value; }
		}

		/// <summary>
		/// Gets or sets the Foreground<see cref="Color"/> of the <see cref="OutputBox"/>.
		/// The default is <see cref="Color.White"/>.
		/// </summary>
		public override Color ForeColor
		{
			get { return this.textBox.ForeColor; }
			set { this.textBox.ForeColor = value; }
		}
	}
}
