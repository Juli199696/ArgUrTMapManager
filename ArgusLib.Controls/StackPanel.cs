using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ArgusLib.Controls
{
	public class StackPanel : System.Windows.Forms.Panel
	{
		Control[] sortedControls;

		public StackPanel()
			:base()
		{
			this.sortedControls = new Control[] { };
		}

		public event EventHandler FlowDirectionChanged;

		public FlowDirection FlowDirection
		{
			get { return this.flowDirection; }
			set
			{
				bool changed = this.flowDirection != value;
				this.flowDirection = value;
				if (changed == true)
					this.OnFlowDirectionChanged(EventArgs.Empty);
			}
		}
		FlowDirection flowDirection;

		protected virtual void OnFlowDirectionChanged(EventArgs e)
		{
			this.PerformLayout();
			if (this.FlowDirectionChanged != null)
				this.FlowDirectionChanged(this, EventArgs.Empty);
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.SuspendLayout();

			if (this.FlowDirection == System.Windows.Forms.FlowDirection.TopDown)
				this.OnLayoutFlowDirectionTopDown(levent);
			else if (this.FlowDirection == System.Windows.Forms.FlowDirection.BottomUp)
				this.OnLayoutFlowDirectionBottomUp(levent);
			else if (this.FlowDirection == System.Windows.Forms.FlowDirection.LeftToRight)
				this.OnLayoutFlowDirectionLeftToRight(levent);
			else if (this.FlowDirection == System.Windows.Forms.FlowDirection.RightToLeft)
				this.OnLayoutFlowDirectionRightToLeft(levent);

			this.ResumeLayout();
			base.OnLayout(levent);
		}

		protected virtual void OnLayoutFlowDirectionTopDown(LayoutEventArgs levent)
		{
			int y = 0;
			foreach (Control c in this.sortedControls)
			{
				c.Top = y + c.Margin.Top;
				c.Left = (this.Width - c.Width) / 2;
				y = c.Bottom;
			}
		}

		protected virtual void OnLayoutFlowDirectionBottomUp(LayoutEventArgs levent)
		{
			int y = this.ClientSize.Height;
			foreach (Control c in this.sortedControls)
			{
				c.Top = y - c.Margin.Bottom - c.Height;
				c.Left = (this.Width - c.Width) / 2;
				y = c.Top;
			}
		}

		protected virtual void OnLayoutFlowDirectionLeftToRight(LayoutEventArgs levent)
		{
			int x = 0;
			foreach (Control c in this.sortedControls)
			{
				c.Left = x + c.Margin.Left;
				c.Top = (this.Height - c.Height) / 2;
				x = c.Right;
			}
		}

		protected virtual void OnLayoutFlowDirectionRightToLeft(LayoutEventArgs levent)
		{
			int x = this.ClientSize.Width;
			foreach (Control c in this.sortedControls)
			{
				c.Left = x - c.Margin.Right - c.Width;
				c.Top = (this.Height - c.Height) / 2;
				x = c.Left;
			}
		}

		protected virtual Control[] GetSortedControls()
		{
			if (this.Controls.Count < 1)
				return new Control[] { };

			List<Control> controls = new List<Control>();
			controls.Add(this.Controls[0]);
			for (int i = 1; i < this.Controls.Count; i++)
			{
				Control c = this.Controls[i];
				if (c.TabIndex >= controls[controls.Count - 1].TabIndex)
				{
					controls.Add(c);
					continue;
				}
				for (int j = 0; j < controls.Count; j++)
				{
					if (c.TabIndex < controls[j].TabIndex)
					{
						controls.Insert(j, c);
						break;
					}
				}
			}
			return controls.ToArray();
		}

		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			e.Control.TabIndexChanged += this.Control_TabIndexChanged;
			this.sortedControls = this.GetSortedControls();
		}

		void Control_TabIndexChanged(object sender, EventArgs e)
		{
			this.sortedControls = this.GetSortedControls();
			this.PerformLayout();
		}

		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			e.Control.TabIndexChanged -= this.Control_TabIndexChanged;
			this.sortedControls = this.GetSortedControls();
		}

		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			if (this.AutoSize == true)
			{
				if (this.sortedControls.Length < 1)
					return;
				if (this.Dock == DockStyle.Fill)
					return;

				Control c = this.sortedControls[this.sortedControls.Length-1];

				Size clientSize = this.ClientSize;

				if (this.FlowDirection == System.Windows.Forms.FlowDirection.TopDown || this.flowDirection== System.Windows.Forms.FlowDirection.BottomUp)
				{
					int width = 0;
					foreach (Control ctrl in this.Controls)
					{
						width = Math.Max(width, ctrl.Width + ctrl.Margin.Horizontal);
					}

					int height = 0;
					if (this.FlowDirection == System.Windows.Forms.FlowDirection.TopDown)
					{
						height = c.Bottom+c.Margin.Bottom;
					}
					else if (this.FlowDirection == System.Windows.Forms.FlowDirection.BottomUp)
					{
						height = c.Top+c.Margin.Top;
					}
					clientSize = new System.Drawing.Size(width, height);
				}
				else if (this.FlowDirection == System.Windows.Forms.FlowDirection.LeftToRight || this.flowDirection == System.Windows.Forms.FlowDirection.RightToLeft)
				{
					int height = 0;
					foreach (Control ctrl in this.Controls)
					{
						height = Math.Max(height, ctrl.Height + ctrl.Margin.Vertical);
					}

					int width = 0;
					if (this.FlowDirection == System.Windows.Forms.FlowDirection.LeftToRight)
					{
						height = c.Right + c.Margin.Right;
					}
					else if (this.FlowDirection == System.Windows.Forms.FlowDirection.RightToLeft)
					{
						height = c.Left + c.Margin.Left;
					}
					clientSize = new System.Drawing.Size(width, height);
				}

				if (this.Dock == DockStyle.Top || this.Dock == DockStyle.Bottom)
					clientSize.Width = this.ClientSize.Width;
				else if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					clientSize.Height = this.ClientSize.Height;
				this.ClientSize = clientSize;
			}
		}
	}
}
