using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public class Matrix33<T> : Matrix<T>
		where T:IScalar
	{
		public Matrix33(T value)
			: base(3, 3, value) { }

		public Matrix33()
			: base(3, 3) { }

		public Matrix33(Matrix<T> matrix)
			: base(matrix)
		{
			if (this.Dimension.RowCount != 3 || this.Dimension.ColumnCount != 3)
				throw new DimensionException();
		}
	}
}
