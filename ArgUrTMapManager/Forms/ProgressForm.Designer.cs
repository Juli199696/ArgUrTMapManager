namespace ArgUrTMapManager.Forms
{
	partial class ProgressForm
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
			this.components = new System.ComponentModel.Container();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.pBorder = new System.Windows.Forms.Panel();
			this.lProgress = new System.Windows.Forms.Label();
			this.obDetails = new ArgUrTMapManager.Controls.BlackOutputBox();
			this.llDetails = new System.Windows.Forms.LinkLabel();
			this.lText = new System.Windows.Forms.Label();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.pBorder.SuspendLayout();
			this.SuspendLayout();
			// 
			// pBorder
			// 
			this.pBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pBorder.Controls.Add(this.lProgress);
			this.pBorder.Controls.Add(this.obDetails);
			this.pBorder.Controls.Add(this.llDetails);
			this.pBorder.Controls.Add(this.lText);
			this.pBorder.Controls.Add(this.pbProgress);
			this.pBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pBorder.Location = new System.Drawing.Point(0, 0);
			this.pBorder.Name = "pBorder";
			this.pBorder.Size = new System.Drawing.Size(400, 250);
			this.pBorder.TabIndex = 2;
			// 
			// lProgress
			// 
			this.lProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lProgress.Location = new System.Drawing.Point(333, 7);
			this.lProgress.Name = "lProgress";
			this.lProgress.Size = new System.Drawing.Size(53, 13);
			this.lProgress.TabIndex = 4;
			this.lProgress.Text = "100.00%";
			this.lProgress.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// obDetails
			// 
			this.obDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.obDetails.Location = new System.Drawing.Point(10, 68);
			this.obDetails.MaxOutputLength = 1024;
			this.obDetails.Name = "obDetails";
			this.obDetails.Size = new System.Drawing.Size(376, 168);
			this.obDetails.TabIndex = 3;
			// 
			// llDetails
			// 
			this.llDetails.ActiveLinkColor = System.Drawing.Color.Gold;
			this.llDetails.AutoSize = true;
			this.llDetails.LinkColor = System.Drawing.Color.White;
			this.llDetails.Location = new System.Drawing.Point(10, 49);
			this.llDetails.Margin = new System.Windows.Forms.Padding(3);
			this.llDetails.Name = "llDetails";
			this.llDetails.Size = new System.Drawing.Size(48, 13);
			this.llDetails.TabIndex = 2;
			this.llDetails.TabStop = true;
			this.llDetails.Text = "Details...";
			this.llDetails.VisitedLinkColor = System.Drawing.Color.White;
			this.llDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDetails_LinkClicked);
			// 
			// lText
			// 
			this.lText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lText.AutoEllipsis = true;
			this.lText.Location = new System.Drawing.Point(10, 7);
			this.lText.Name = "lText";
			this.lText.Size = new System.Drawing.Size(317, 13);
			this.lText.TabIndex = 1;
			this.lText.Text = "Work in progress...";
			// 
			// pbProgress
			// 
			this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbProgress.Location = new System.Drawing.Point(10, 23);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(376, 23);
			this.pbProgress.TabIndex = 0;
			// 
			// ProgressForm
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(400, 250);
			this.Controls.Add(this.pBorder);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ProgressForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "ProgressForm";
			this.pBorder.ResumeLayout(false);
			this.pBorder.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ProgressBar pbProgress;
		private System.Windows.Forms.Label lText;
		private System.Windows.Forms.Panel pBorder;
		private System.Windows.Forms.LinkLabel llDetails;
		private System.Windows.Forms.ToolTip toolTip1;
		private Controls.BlackOutputBox obDetails;
		private System.Windows.Forms.Label lProgress;
	}
}