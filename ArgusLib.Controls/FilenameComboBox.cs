using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace ArgusLib.Controls
{
	/// <summary>
	/// A <see cref="ComboBox"/>, which displays the filenames of the files with a specific
	/// fileextension in a specific directory. Only the directory specified by <see cref="DirectoryPath"/>
	/// is searched without including its subdirectories.
	/// </summary>
	/// <example>
	/// This example shows how to use <see cref="FilenameComboBox"/> to list the filenames of the files with
	/// the extensions ".jpg", ".png" and ".tga" in the directory "C:\Test".
	/// <code>
	/// protected override void OnLoad(EventArgs e)
	///	{
	///		FilenameComboBox cbFiles = new FilenameComboBox();
	///		cbFiles.Location = new Point(10, 10);
	///		cbFiles.Size = new Size(100, 25);
	///		cbFiles.DirectoryPath = @"C:\Test";
	///		cbFiles.FileExtension = ".jpg|.png|.tga";
	///		this.Controls.Add(cbFiles);
	///		base.OnLoad(e);
	///	}
	/// </code>
	/// </example>
	public class FilenameComboBox : ComboBox
	{
		/// <summary>
		/// Gets or sets the text of the first item in <see cref="ComboBox.Items"/>.
		/// The default value is "&lt;Select&gt;".
		/// </summary>
		public string FirstItem
		{
			get { return this._FirstItem; }
			set
			{
				this._FirstItem = value;
				if (base.Items.Count > 0)
					base.Items[0] = this._FirstItem;
			}
		}
		private string _FirstItem = "<Select>";

		/// <summary>
		/// Gets or sets the fileextension used to specify which filenames should be listed.
		/// </summary>
		/// <example>
		/// See <see cref="FilenameComboBox"/> for example.
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string FileExtension
		{
			get { return this._FileExtension; }
			set
			{
				this._FileExtension = value;
				this.Reset();
			}
		}
		private string _FileExtension;

		/// <summary>
		/// Gets or sets the path of the directory which contains the files to be listed.
		/// </summary>
		/// <example>
		/// See <see cref="FilenameComboBox"/> for example.
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DirectoryPath
		{
			get { return this._DirectoryPath; }
			set
			{
				this._DirectoryPath = value;
				this.Reset();
			}
		}
		private string _DirectoryPath;

		/// <summary>
		/// Creates a new instance of <see cref="FilenameComboBox"/>.
		/// </summary>
		public FilenameComboBox()
			: base()
		{
			base.DropDownStyle = ComboBoxStyle.DropDownList;
		}

		private void Reset()
		{
			if (string.IsNullOrEmpty(this.FileExtension) == true)
				return;
			if (string.IsNullOrEmpty(this.DirectoryPath) == true)
				return;
			base.Items.Clear();
			base.Items.Add(this.FirstItem);

			string[] exts = this.FileExtension.Split('|');
			foreach (string ext in exts)
			{
				string[] files = Directory.GetFiles(this.DirectoryPath, "*" + ext);
				foreach (string file in files)
				{
					base.Items.Add(Path.GetFileName(file));
				}
			}
			this.SelectedIndex = 0;
		}

		/// <summary>
		/// Selects and displays a filename.
		/// </summary>
		/// <param name="filename">A <see cref="string"/> specifying the filename to select.</param>
		/// <returns>true, if an item which matches <paramref name="filename"/> was found and selected,
		/// otherwhise false.</returns>
		public bool Select(string filename)
		{
			filename = filename.ToLowerInvariant();
			for (int i = 0; i < base.Items.Count; i++)
			{
				string s = ((string)base.Items[i]).ToLowerInvariant();
				if (filename == s)
				{
					this.SelectedIndex = i;
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Gets the filename of the currently selected file.
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public string Filename
		{
			get
			{
				if (this.SelectedIndex < 1)
					return null;
				return (string)this.SelectedItem;
			}
		}

		/// <summary>
		/// Gets the full path of the currently selected file.
		/// </summary>
		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public string FullFilename
		{
			get
			{
				string filename = this.Filename;
				if (filename == null)
					return null;
				return Path.Combine(this.DirectoryPath, filename);
			}
		}

		#region Keep Designer from adding items

		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public new object[] Items
		{
			get
			{
				object[] items = new object[base.Items.Count];
				base.Items.CopyTo(items, 0);
				return items;
			}
		}

		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public new ComboBoxStyle DropDownStyle
		{
			get { return base.DropDownStyle; }
		}

		#endregion
	}
}
