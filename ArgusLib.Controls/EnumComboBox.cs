using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A ComboBox that displays the values of an <see cref="Enum"/>.
	/// Directly derives from <see cref="ComboBox"/>.
	/// To specify which Enum's values should be displayed, use <see cref="EnumComboBox.Initialize(Type,string[])"/>.
	/// </summary>
	public class EnumComboBox : ComboBox
	{
		Type enumType;

		/// <summary>
		/// Creates a new instance of <see cref="EnumComboBox"/>.
		/// </summary>
		public EnumComboBox()
			: base()
		{
			this.enumType = null;
			this.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		/// <summary>
		/// Initializes the <see cref="ComboBox.Items"/> with the values of the <see cref="Enum"/>
		/// specified by <paramref name="enumType"/>.
		/// </summary>
		/// <param name="enumType">
		/// A <see cref="Type"/> which specifies the <see cref="Enum"/> whose values will be displayed.
		/// </param>
		/// <param name="displayNames">
		/// An <see cref="Array"/> of <see cref="string"/> which specifies the names to use for the values
		/// of the Enum. A replacement for the array returnes by <see cref="Enum.GetNames(Type)"/>.
		/// </param>
		public void Initialize(Type enumType, string[] displayNames)
		{
			if (enumType.IsEnum == false)
				throw new ArgumentException("Type.IsEnum must be true.", "enumType");

			this.enumType = enumType;
			Array values = Enum.GetValues(this.enumType);
			if (values.Length != displayNames.Length)
				throw new ArgumentException("Dimension error.", "displayNames");

			object[] items = new object[values.Length];
			for (int i = 0; i < items.Length; i++)
			{
				items[i] = new EnumItem(displayNames[i], Convert.ToUInt64(values.GetValue(i)));
			}
			base.Items.Clear();
			base.Items.AddRange(items);
			this.SelectedIndex = 0;
		}

		/// <summary>
		/// Initializes the <see cref="ComboBox.Items"/> with the values of the <see cref="Enum"/>
		/// specified by <paramref name="enumType"/>.
		/// Calls <see cref="EnumComboBox.Initialize(Type, NameFormatting)"/> with <see cref="NameFormatting.None"/>.
		/// </summary>
		/// <param name="enumType">
		/// Specifies the <see cref="Enum"/> whose values will be displayed.
		/// </param>
		public void Initialize(Type enumType)
		{
			this.Initialize(enumType, NameFormatting.None);
		}

		/// <summary>
		/// Initializes the <see cref="ComboBox.Items"/> with the values of the <see cref="Enum"/>
		/// specified by <paramref name="enumType"/>.
		/// </summary>
		/// <param name="enumType">Specifies the <see cref="Enum"/> whose values will be displayed.</param>
		/// <param name="formatting">Specifies how the value names should be formatted.</param>
		public void Initialize(Type enumType, NameFormatting formatting)
		{
			if (enumType.IsEnum == false)
				throw new ArgumentException("Type.IsEnum must be true.", "enumType");

			string[] names = Enum.GetNames(enumType);
			if (formatting == NameFormatting.InsertSpaceBeforeUpperCase)
			{
				for (int i = 0; i < names.Length; i++)
				{
					List<char> chars = new List<char>(names[i].ToCharArray());
					for (int j = 1; j < chars.Count; j++)
					{
						if (char.IsUpper(chars[j]) == true)
						{
							chars.Insert(j, ' ');
							j++;
						}
					}
					if (chars.Count != names[i].Length)
						names[i] = new string(chars.ToArray());
				}
			}
			this.Initialize(enumType, names);
		}

		/// <summary>
		/// Gets or sets the type of <see cref="Enum"/> whose values are to be displayed.
		/// Calls <see cref="Initialize(Type)"/>.
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type EnumType
		{
			get { return this.enumType; }
			set { this.Initialize(value); }
		}

		/// <summary>
		/// Selects the <see cref="Enum"/>-value which is equal to <paramref name="item"/> and displays its name.
		/// </summary>
		/// <param name="item">A <see cref="UInt64"/> specifying the value which should be selected.</param>
		/// <returns>true", if the value specified by <paramref name="item"/> was found in <see cref="ComboBox.Items"/>,
		/// otherwhise false".</returns>
		public bool Select(UInt64 item)
		{
			for (int i = 0; i < base.Items.Count; i++)
			{
				EnumItem eItem = (EnumItem)base.Items[i];
				if (eItem.Value == item)
				{
					this.SelectedIndex = i;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets or sets the selected <see cref="Enum"/>-value.
		/// The set-accessor calls <see cref="EnumComboBox.Select(UInt64)"/> and
		/// throws an <see cref="Exception"/> if <see cref="EnumComboBox.Select(UInt64)"/> returns false".
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public UInt64 SelectedEnum
		{
			get
			{
				EnumItem item = (EnumItem)this.SelectedItem;
				return item.Value;
			}
			set
			{
				if (this.Select(value) == false)
					throw new Exception("Value not found in Enum.");
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new object[] Items
		{
			get
			{
				object[] items = new object[base.Items.Count];
				base.Items.CopyTo(items, 0);
				return items;
			}
		}

		struct EnumItem
		{
			public string Name { get; set; }
			public UInt64 Value { get; set; }

			public EnumItem(string Name, UInt64 Value)
				:this()
			{
				this.Name = Name;
				this.Value = Value;
			}

			public override string ToString()
			{
				return this.Name;
			}
		}

		/// <summary>
		/// See <see cref="EnumComboBox.Initialize(Type,NameFormatting)"/> for how this Enumeration is used.
		/// </summary>
		public enum NameFormatting : byte
		{
			/// <summary>
			/// The value names are not formatted, the name returned by <see cref="Enum.GetNames"/> are used.
			/// </summary>
			None,
			/// <summary>
			/// The value names returned by <see cref="Enum.GetNames"/> are formatted. Before each upper case
			/// character, except for the first character, a white space is inserted to seperate words.
			/// </summary>
			/// <example>
			/// The Enumeration member <see cref="NameFormatting.InsertSpaceBeforeUpperCase"/> would be displayed
			/// in an <see cref="EnumComboBox"/> as "Insert Space Before Upper Case".
			/// </example>
			InsertSpaceBeforeUpperCase
		}
	}
}
