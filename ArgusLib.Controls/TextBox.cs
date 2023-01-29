using System;
using System.Collections.Generic;
using System.Drawing;
using WinForms = System.Windows.Forms;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A TextBox which is capable of showing an instruction Text.
	/// Directly derives from <see cref="WinForms.TextBox"/>.
	/// </summary>
	public class TextBox : WinForms.TextBox
	{
		private WinForms.Label lInstruction;

		/// <summary>
		/// Gets or sets a value indicating whether the instruction text shoud be shown.
		/// The default is false/>.
		/// </summary>
		public bool ShowInstructionText
		{
			get { return this.lInstruction.Visible; }
			set { this.lInstruction.Visible = value; }
		}
		/// <summary>
		/// Gets or sets the instruction text.
		/// The default is null/>.
		/// </summary>
		public string InstructionText
		{
			get { return this.lInstruction.Text; }
			set { this.lInstruction.Text = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="FontStyle"/> used to display the <see cref="TextBox.InstructionText"/>.
		/// The default is <see cref="FontStyle.Italic"/>.
		/// </summary>
		public FontStyle InstructionFontStyle
		{
			get { return this.lInstruction.Font.Style; }
			set { this.lInstruction.Font = new Font(this.Font, value); }
		}
		/// <summary>
		/// Gets or sets the <see cref="Color"/> used to display the <see cref="TextBox.InstructionText"/>.
		/// The default is <see cref="Color.White"/>.
		/// </summary>
		public Color InstructionColor
		{
			get { return this.lInstruction.ForeColor; }
			set { this.lInstruction.ForeColor = value; }
		}
		/// <summary>
		/// Gets or sets the <see cref="ContentAlignment"/> used to display the <see cref="TextBox.InstructionText"/>.
		/// The default is <see cref="ContentAlignment.MiddleLeft"/>.
		/// </summary>
		public ContentAlignment InstructionTextAlign
		{
			get { return this.lInstruction.TextAlign; }
			set { this.lInstruction.TextAlign = value; }
		}

		public CharSet[] AllowedChars { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="TextBox"/>.
		/// </summary>
		public TextBox()
			: base()
		{
			this.BackColor = Color.Black;
			this.ForeColor = Color.White;

			this.lInstruction = new WinForms.Label();
			this.lInstruction.AutoSize = false;
			this.lInstruction.AutoEllipsis = true;
			this.lInstruction.Dock = WinForms.DockStyle.Fill;

			this.lInstruction.Click += lInstruction_Click;

			this.InstructionText = null;
			this.InstructionFontStyle = FontStyle.Italic;
			this.InstructionColor = this.ForeColor;
			this.InstructionTextAlign = ContentAlignment.MiddleLeft;
			this.ShowInstructionText = false;

			this.AllowedChars = null;

			this.Controls.Add(this.lInstruction);
		}

		void lInstruction_Click(object sender, EventArgs e)
		{
			this.Focus();
		}

		/// <summary>
		/// See <see cref="TextBox.OnFontChanged"/>.
		/// </summary>
		protected override void OnFontChanged(EventArgs e)
		{
			this.lInstruction.Font = new System.Drawing.Font(this.Font, this.InstructionFontStyle);
			base.OnFontChanged(e);
		}

		/// <summary>
		/// See <see cref="TextBox.OnGotFocus"/>.
		/// </summary>
		protected override void OnGotFocus(EventArgs e)
		{
			this.lInstruction.Visible = false;
			base.OnGotFocus(e);
		}

		/// <summary>
		/// See <see cref="TextBox.OnLostFocus"/>.
		/// </summary>
		protected override void OnLostFocus(EventArgs e)
		{
			if (string.IsNullOrEmpty(this.Text) == true)
				this.lInstruction.Visible = true;
			base.OnLostFocus(e);
		}

		public override string Text
		{
			get { return base.Text; }
			set
			{
				if (this.Focused == false)
				{
					this.lInstruction.Visible = string.IsNullOrEmpty(value);
				}
				base.Text = value;
			}
		}

		protected override void OnKeyDown(WinForms.KeyEventArgs e)
		{
			if (e.Modifiers == WinForms.Keys.Control)
			{
				if (e.KeyCode == WinForms.Keys.A)
				{
					this.SelectAll();
				}
				else if (e.KeyCode == WinForms.Keys.C)
				{
					WinForms.Clipboard.SetText(this.SelectedText);
				}
				else if (e.KeyCode == WinForms.Keys.X)
				{
					WinForms.Clipboard.SetText(this.SelectedText);
					int start = this.SelectionStart;
					this.Text = this.Text.Remove(start, this.SelectionLength);
					this.SelectionStart = start;
				}
				else if (e.KeyCode == WinForms.Keys.V)
				{
					string copyText = CharSet.RemoveUnallowedChars(this.AllowedChars, WinForms.Clipboard.GetText());
					int start = this.SelectionStart;
					this.Text = this.Text.Remove(start, this.SelectionLength).Insert(start, copyText);
					this.SelectionStart = start + copyText.Length;
				}
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyPress(WinForms.KeyPressEventArgs e)
		{
			if (char.IsControl(e.KeyChar) == false)
			{
				e.Handled = !CharSet.Check(this.AllowedChars, e.KeyChar);
			}
			base.OnKeyPress(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			if (CharSet.ContainsUnallowedChars(this.AllowedChars, this.Text) == true)
			{
				int start = this.SelectionStart;
				this.Text = CharSet.RemoveUnallowedChars(this.AllowedChars, this.Text);
				this.SelectionStart = start;
			}
			else
			{
				base.OnTextChanged(e);
			}
		}

		public class CharSet
		{
			public char Lower { get; private set; }
			public char Upper { get; private set; }

			private CharSet() { }

			public CharSet(char lower, char upper)
			{
				if (lower <= upper)
				{
					this.Lower = lower;
					this.Upper = upper;
				}
				else
				{
					this.Lower = upper;
					this.Upper = lower;
				}
			}

			public bool Check(char c)
			{
				if (c >= this.Lower && c <= this.Upper)
					return true;
				return false;
			}

			public static CharSet[] FromChars(params Char[] chars)
			{
				Array.Sort<char>(chars);
				List<CharSet> RetVal = new List<CharSet>();
				CharSet set = new CharSet();
				set.Lower = chars[0];
				int v0 = (int)chars[0];
				int v1;
				for (int i = 1; i < chars.Length; i++)
				{
					v1 = (int)chars[i];
					if (v1 - v0 > 1)
					{
						set.Upper = chars[i - 1];
						RetVal.Add(set);
						set = new CharSet();
						set.Lower = chars[i];
					}
					v0 = v1;
				}

				set.Upper = chars[chars.Length - 1];
				RetVal.Add(set);
				return RetVal.ToArray();
			}

			public static bool Check(IEnumerable<CharSet> allowedChars, char c)
			{
				bool RetVal = true;
				if (allowedChars != null)
				{
					RetVal = false;
					foreach (CharSet set in allowedChars)
					{
						if (set.Check(c) == true)
						{
							RetVal = true;
							break;
						}
					}
				}
				return RetVal;
			}

			public static string RemoveUnallowedChars(IEnumerable<CharSet> allowedChars, string input)
			{
				List<char> chars = new List<char>(input.Length);
				for (int i = 0; i < input.Length; i++)
				{
					if (CharSet.Check(allowedChars, input[i]) == true)
						chars.Add(input[i]);
				}
				return new string(chars.ToArray());
			}

			public static bool ContainsUnallowedChars(IEnumerable<CharSet> allowedChars, string input)
			{
				for (int i = 0; i < input.Length; i++)
				{
					if (CharSet.Check(allowedChars, input[i]) == false)
						return true;
				}
				return false;
			}
		}
	}
}
