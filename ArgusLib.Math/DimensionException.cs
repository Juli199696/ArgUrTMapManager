using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class DimensionException : Exception
	{
		public DimensionException(string message)
			: base(message) { }

		public DimensionException()
			: base() { }
	}
}
