namespace ArgusLib.Controls
{
	partial class ProcessForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.outputBox = new ArgusLib.Controls.OutputBox();
			this.inputBox = new ArgusLib.Controls.TextBox();
			this.SuspendLayout();
			// 
			// outputBox
			// 
			this.outputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.outputBox.Location = new System.Drawing.Point(12, 12);
			this.outputBox.MaxOutputLength = 1024;
			this.outputBox.Name = "outputBox";
			this.outputBox.Size = new System.Drawing.Size(560, 211);
			this.outputBox.TabIndex = 0;
			// 
			// inputBox
			// 
			this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.inputBox.BackColor = System.Drawing.Color.Black;
			this.inputBox.ForeColor = System.Drawing.Color.White;
			this.inputBox.InstructionColor = System.Drawing.Color.White;
			this.inputBox.InstructionFontStyle = System.Drawing.FontStyle.Italic;
			this.inputBox.InstructionText = "Input...";
			this.inputBox.InstructionTextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.inputBox.Location = new System.Drawing.Point(12, 229);
			this.inputBox.Name = "inputBox";
			this.inputBox.ShowInstructionText = true;
			this.inputBox.Size = new System.Drawing.Size(560, 20);
			this.inputBox.TabIndex = 1;
			this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.inputBox_KeyDown);
			// 
			// ProcessForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 261);
			this.Controls.Add(this.inputBox);
			this.Controls.Add(this.outputBox);
			this.Name = "ProcessForm";
			this.Text = "ProcessForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private OutputBox outputBox;
		private TextBox inputBox;
	}
}