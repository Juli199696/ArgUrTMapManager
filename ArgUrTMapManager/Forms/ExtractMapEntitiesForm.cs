using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ArgUrTMapManager.Forms
{
	public partial class ExtractMapEntitiesForm : ModalToolForm
	{
		public ExtractMapEntitiesForm()
		{
			InitializeComponent();
		}

		private void bOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		public string EntityConversionFile { get { return this.cbEntityConversionFile.EntityConversionFile; } }
	}
}
