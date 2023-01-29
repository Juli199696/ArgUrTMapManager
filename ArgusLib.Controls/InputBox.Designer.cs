namespace ArgusLib.Controls
{
	partial class InputBox
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
			this.tbInput = new System.Windows.Forms.TextBox();
			this.bOK = new System.Windows.Forms.Button();
			this.lText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbInput
			// 
			this.tbInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbInput.BackColor = System.Drawing.Color.Black;
			this.tbInput.ForeColor = System.Drawing.Color.White;
			this.tbInput.Location = new System.Drawing.Point(12, 25);
			this.tbInput.Name = "tbInput";
			this.tbInput.Size = new System.Drawing.Size(260, 20);
			this.tbInput.TabIndex = 0;
			this.tbInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbInput_KeyDown);
			// 
			// bOK
			// 
			this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.bOK.Location = new System.Drawing.Point(197, 51);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(75, 23);
			this.bOK.TabIndex = 1;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = false;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// lText
			// 
			this.lText.AutoSize = true;
			this.lText.Location = new System.Drawing.Point(12, 9);
			this.lText.Name = "lText";
			this.lText.Size = new System.Drawing.Size(35, 13);
			this.lText.TabIndex = 2;
			this.lText.Text = "label1";
			this.lText.SizeChanged += new System.EventHandler(this.lText_SizeChanged);
			// 
			// InputBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 87);
			this.Controls.Add(this.lText);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.tbInput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MinimumSize = new System.Drawing.Size(300, 126);
			this.Name = "InputBox";
			this.Text = "InputBox";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.Button bOK;
		private System.Windows.Forms.Label lText;
	}
}