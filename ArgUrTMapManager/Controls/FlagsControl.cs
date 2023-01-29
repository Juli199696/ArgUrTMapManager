using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace ArgUrTMapManager.Controls
{
	class FlagsControl : CheckedListBox
	{
		Type enumType;
		Dictionary<string, ulong> dict;

		public FlagsControl()
			: base()
		{
			base.ForeColor = Color.White;
			base.BackColor = Color.Black;
			this.CheckOnClick = true;
			this.IntegralHeight = false;
			//this.SelectionMode = System.Windows.Forms.SelectionMode.None;
		}

		public event EventHandler ValueChanged;

		#region Keep Designer from changing colors
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set { }
		}

		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set { }
		}
		#endregion

		private ulong value;
		public ulong Value
		{
			get{return this.value;}
			private set
			{
				this.value = value;
				this.OnValueChanged(this, EventArgs.Empty);
			}
		}

		private void OnValueChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
				this.ValueChanged(sender, e);
		}

		public bool SetFlagType(Type FlagType)
		{
			if (FlagType.IsEnum == false)
				return false;
			this.enumType = FlagType;
			string[] names = Enum.GetNames(FlagType);
			this.Items.Clear();
			this.Items.AddRange(names);
			this.dict = new Dictionary<string, ulong>();
			Array values = Enum.GetValues(FlagType);
			for (int i = 0; i < names.Length; i++)
			{
				ulong val = (ulong)Convert.ToUInt64(values.GetValue(i));
				this.dict.Add(names[i], val);
			}
			this.Value = 0;
			return true;
		}

		protected override void OnItemCheck(ItemCheckEventArgs ice)
		{
			base.OnItemCheck(ice);

			if (this.enumType == null)
				return;

			ulong flag = this.dict[(string)this.Items[ice.Index]];
			if (ice.NewValue == CheckState.Checked)
			{
				this.Value |= flag;
			}
			else if (ice.NewValue == CheckState.Unchecked)
			{
				this.Value = this.Value & ~flag;
			}
		}

		public ulong GetValue()
		{
			if (this.enumType == null)
				throw new Exception("FlagType must be set first.");

			ulong value = 0;
			foreach (string item in this.CheckedItems)
			{
				ulong flag = this.dict[item];
				value |= flag;
			}
			return value;
		}

		public void SetValue(ulong value)
		{
			if (this.enumType == null)
				throw new Exception("FlagType must be set first.");

			for (int i = 0; i < this.Items.Count; i++)
			{
				string item = (string)this.Items[i];
				ulong flag = this.dict[item];

				if ((value & flag) == flag)
					this.SetItemChecked(i, true);
				else
					this.SetItemChecked(i, false);
			}
		}
	}
}
