namespace ArgUrTMapManager.Forms
{
	partial class ExtractMapEntitiesForm
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
			this.lText = new System.Windows.Forms.Label();
			this.cbEntityConversionFile = new ArgUrTMapManager.Controls.EntityConversionComboBox();
			this.bOK = new ArgUrTMapManager.Controls.BlackButton();
			this.SuspendLayout();
			// 
			// lText
			// 
			this.lText.AutoSize = true;
			this.lText.Location = new System.Drawing.Point(12, 9);
			this.lText.Name = "lText";
			this.lText.Size = new System.Drawing.Size(133, 13);
			this.lText.TabIndex = 0;
			this.lText.Text = "Use Entity Conversion File:";
			// 
			// cbEntityConversionFile
			// 
			this.cbEntityConversionFile.BackColor = System.Drawing.Color.Black;
			this.cbEntityConversionFile.ForeColor = System.Drawing.Color.White;
			this.cbEntityConversionFile.FormattingEnabled = true;
			this.cbEntityConversionFile.Location = new System.Drawing.Point(12, 25);
			this.cbEntityConversionFile.Name = "cbEntityConversionFile";
			this.cbEntityConversionFile.Size = new System.Drawing.Size(210, 21);
			this.cbEntityConversionFile.TabIndex = 1;
			this.cbEntityConversionFile.Text = "None";
			// 
			// bOK
			// 
			this.bOK.BackColor = System.Drawing.Color.Black;
			this.bOK.ForeColor = System.Drawing.Color.White;
			this.bOK.Location = new System.Drawing.Point(147, 52);
			this.bOK.Name = "bOK";
			this.bOK.Size = new System.Drawing.Size(75, 23);
			this.bOK.TabIndex = 2;
			this.bOK.Text = "OK";
			this.bOK.UseVisualStyleBackColor = false;
			this.bOK.Click += new System.EventHandler(this.bOK_Click);
			// 
			// ExtractMapEntitiesForm
			// 
			this.AcceptButton = this.bOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 89);
			this.Controls.Add(this.bOK);
			this.Controls.Add(this.cbEntityConversionFile);
			this.Controls.Add(this.lText);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ExtractMapEntitiesForm";
			this.Text = "Extract Map Entities";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lText;
		private Controls.EntityConversionComboBox cbEntityConversionFile;
		private Controls.BlackButton bOK;
	}
}