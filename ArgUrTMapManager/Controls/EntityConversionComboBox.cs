using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager.Controls
{
	class EntityConversionComboBox : BlackFilenameComboBox
	{
		public EntityConversionComboBox()
			: base()
		{
			this.FirstItem = "None";
			this.DirectoryPath = FilesystemEntries.EntityConversionFilesDirectory;
			this.FileExtension = "*";
		}

		[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
		[System.ComponentModel.Browsable(false)]
		public string EntityConversionFile
		{
			get
			{
				return this.FullFilename;
			}
		}
	}
}
