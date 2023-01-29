using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class Vector3Int32 : Vector3<Int32Wrapper>
	{
		public Vector3Int32()
			: base() { }

		public Vector3Int32(int x, int y, int z)
			: base(new Int32Wrapper(x), new Int32Wrapper(y), new Int32Wrapper(z)) { }
	}
}
