using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ArgusLib.Collections;

namespace ArgusLib.Controls
{
	public partial class DockPanel : UserControl
	{
		const int ButtonRotation = 3;

		ListPair<Button, Control> listPages;

		public DockPanel()
		{
			InitializeComponent();
			this.listPages = new ListPair<Button, Control>();
			this.pHeader.AutoSize = true;
		}

		#region HeaderDock
		public event EventHandler HeaderDockChanged;

		public HeaderDockStyle HeaderDock
		{
			get { return this.headerDock; }
			set
			{
				if (this.headerDock == value)
					return;
				this.headerDock = value;
				this.OnHeaderDockChanged(EventArgs.Empty);
			}
		}
		HeaderDockStyle headerDock;

		protected virtual void OnHeaderDockChanged(EventArgs e)
		{
			this.pHeader.SuspendLayout();
			this.pHeader.Dock = (DockStyle)this.HeaderDock;
			int rotation = 0;
			if (this.HeaderDock == HeaderDockStyle.Left || this.HeaderDock == HeaderDockStyle.Right)
			{
				this.pHeader.FlowDirection = FlowDirection.TopDown;
				rotation = DockPanel.ButtonRotation;
			}
			else if (this.HeaderDock == HeaderDockStyle.Top || this.HeaderDock == HeaderDockStyle.Bottom)
			{
				this.pHeader.FlowDirection = FlowDirection.LeftToRight;
			}

			foreach (Button button in this.pHeader.Controls)
			{
				button.Rotation = rotation;
			}
			this.pHeader.ResumeLayout();

			this.PerformLayout();

			if (this.HeaderDockChanged != null)
				this.HeaderDockChanged(this, e);
		}
		#endregion

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);

			if (this.HeaderDock == HeaderDockStyle.Left)
			{
				this.pContent.Location = new Point(this.pHeader.Right, 0);
				this.pContent.Size = new Size(this.Width - this.pHeader.Width, this.Height);
			}
			else if (this.HeaderDock == HeaderDockStyle.Top)
			{
				this.pContent.Location = new Point(0, this.pHeader.Bottom);
				this.pContent.Size = new Size(this.Width, this.Height - this.pHeader.Height);
			}
			else if (this.HeaderDock == HeaderDockStyle.Right)
			{
				this.pContent.Location = new Point(0, 0);
				this.pContent.Size = new Size(this.Width - this.pHeader.Width, this.Height);
			}
			else if (this.HeaderDock == HeaderDockStyle.Bottom)
			{
				this.pContent.Location = new Point(0, 0);
				this.pContent.Size = new Size(this.Width, this.Height - this.pHeader.Height);
			}
		}

		public enum HeaderDockStyle
		{
			Left = DockStyle.Left,
			Top = DockStyle.Top,
			Right = DockStyle.Right,
			Bottom = DockStyle.Bottom
		}
	}
}
