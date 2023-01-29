using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Controls
{
	public class NumericDoubleTextBox : NumericTextBox<double>
	{
		protected override TextBox.CharSet[] GetAllowedChars()
		{
			List<CharSet> sets = new List<CharSet>();
			sets.Add(new CharSet('0', '9'));
			sets.AddRange(CharSet.FromChars('+', '-', '.', 'e', 'E'));
			return sets.ToArray();
		}

		protected override bool ValidateText(string text, out double value)
		{
			return double.TryParse(text, out value);
		}


	}
}
