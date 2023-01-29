namespace ArgusLib.Controls
{
	partial class DockPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pContent = new System.Windows.Forms.Panel();
			this.pHeader = new ArgusLib.Controls.StackPanel();
			this.SuspendLayout();
			// 
			// pContent
			// 
			this.pContent.Location = new System.Drawing.Point(63, 0);
			this.pContent.Name = "pContent";
			this.pContent.Size = new System.Drawing.Size(413, 285);
			this.pContent.TabIndex = 1;
			// 
			// pHeader
			// 
			this.pHeader.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
			this.pHeader.Location = new System.Drawing.Point(0, 0);
			this.pHeader.Name = "pHeader";
			this.pHeader.Size = new System.Drawing.Size(57, 285);
			this.pHeader.TabIndex = 2;
			// 
			// DockPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.pHeader);
			this.Controls.Add(this.pContent);
			this.Name = "DockPanel";
			this.Size = new System.Drawing.Size(476, 285);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pContent;
		private StackPanel pHeader;
	}
}
