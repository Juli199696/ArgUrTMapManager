using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ArgUrTMapManager.Controls
{
	public partial class BlackOutputBox : UserControl
	{
		private delegate void InvokeOutputHandler(string line);
		private InvokeOutputHandler invokeOutputHandler;

		public BlackOutputBox()
		{
			InitializeComponent();
			this.MaxOutputLength = 1 << 10;
			this.invokeOutputHandler = new InvokeOutputHandler(this.Output);
		}

		public int MaxOutputLength { get; set; }

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

		public void InvokeOutput(string line)
		{
			this.Invoke(this.invokeOutputHandler, line);
		}

		public void Clear()
		{
			this.textBox.Text = string.Empty;
		}
	}
}
