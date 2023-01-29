using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib.Math
{
	public interface IMatrix
	{
		MatrixDimension Dimension { get; }
		IScalar this[int row, int column] { get; }
		IEnumerable<IVector> RowVectors { get; }
		IEnumerable<IVector> ColumnVectors { get; }
	}

	public class Matrix<T> : IMatrix
		where T:IScalar
	{
		public MatrixDimension Dimension { get; private set; }

		Vector<T>[] vectors;

		public Matrix(int rowCount, int columnCount)
			: this(new MatrixDimension(rowCount, columnCount)) { }

		public Matrix(MatrixDimension dimension)
			: this(dimension, default(T)) { }

		public Matrix(int rowCount, int columnCount, T value)
			: this(new MatrixDimension(rowCount, columnCount), value) { }

		public Matrix(MatrixDimension dimension, T value)
		{
			if (dimension.RowCount < 0 || dimension.ColumnCount < 0)
				throw new DimensionException();

			this.Dimension = dimension;

			int vecCount = this.Dimension.RowCount;
			int elementCount = this.Dimension.ColumnCount;
			if (this.containsRowVectors == false)
			{
				vecCount = this.Dimension.ColumnCount;
				elementCount = this.Dimension.RowCount;
			}
			this.vectors = new Vector<T>[vecCount];

			Vector<T> v = new Vector<T>(elementCount);
			for (int i = 0; i < v.Dimension; i++)
			{
				v[i] = value;
			}

			for (int i = 0; i < vecCount; i++)
			{
				this.vectors[i] = new Vector<T>(v);
			}

			this.rowVectors = new RowVectorCollection(this);
			this.columnVectors = new ColumnVectorCollection(this);
		}

		public Matrix(Matrix<T> matrix)
			: this(matrix.Dimension)
		{
			for (int i = 0; i < this.vectors.Length; i++)
			{
				this.vectors[i] = new Vector<T>(matrix.vectors[i]);
			}
		}

		public Matrix(T[,] matrix)
			: this(matrix.GetLength(0), matrix.GetLength(1))
		{
			for (int row = 0; row < this.Dimension.RowCount; row++)
			{
				for (int col = 0; col < this.Dimension.ColumnCount; col++)
				{
					this[row, col] = matrix[row, col];
				}
			}
		}

		public static Matrix<T> FromRowVectors(params Vector<T>[] rowVectors)
		{
			Matrix<T> matrix = new Matrix<T>(rowVectors.Length, rowVectors[0].Dimension);
			for (int i = 0; i < rowVectors.Length; i++)
				matrix.RowVectors[i] = rowVectors[i];
			return matrix;
		}

		public static Matrix<T> FromColumnVectors(params Vector<T>[] columnVectors)
		{
			Matrix<T> matrix = new Matrix<T>(columnVectors[0].Dimension, columnVectors.Length);
			for (int i = 0; i < columnVectors.Length; i++)
				matrix.ColumnVectors[i] = columnVectors[i];
			return matrix;
		}

		private bool containsRowVectors { get { return this.Dimension.RowCount < this.Dimension.ColumnCount; } }

		public T this[int row, int column]
		{
			get
			{
				if (this.containsRowVectors == true)
					return this.vectors[row][column];
				return this.vectors[column][row];
			}
			set
			{
				if (this.containsRowVectors == true)
					this.vectors[row][column] = value;
				else
					this.vectors[column][row] = value;
			}
		}

		IScalar IMatrix.this[int row, int column]
		{
			get { return this[row, column]; }
		}

		private RowVectorCollection rowVectors;

		/// <summary>
		/// Should only be used to get/set whole Vectors. To get/set elements use <see cref="this[int,int]"/>.
		/// </summary>
		public RowVectorCollection RowVectors { get { return this.rowVectors; } }

		IEnumerable<IVector> IMatrix.RowVectors { get { return (IEnumerable<IVector>)this.rowVectors; } }

		private ColumnVectorCollection columnVectors;

		/// <summary>
		/// Should only be used to get/set whole Vectors. To get/set elements use <see cref="this[int,int]"/>.
		/// </summary>
		public ColumnVectorCollection ColumnVectors { get { return this.columnVectors; } }

		IEnumerable<IVector> IMatrix.ColumnVectors { get { return (IEnumerable<IVector>)this.columnVectors; } }

		public override string ToString()
		{
			string s = this.RowVectors[0].ToString();
			for (int i = 1; i < this.Dimension.RowCount; i++)
				s += Environment.NewLine + this.RowVectors[i].ToString();
			return s;
		}

		public T[,] ToArray()
		{
			T[,] RetVal = new T[this.Dimension.RowCount, this.Dimension.ColumnCount];
			for (int row = 0; row < this.Dimension.RowCount; row++)
			{
				for (int col = 0; col < this.Dimension.ColumnCount; col++)
				{
					RetVal[row, col] = this[row, col];
				}
			}
			return RetVal;
		}

		public static Matrix<T> Transpose(Matrix<T> matrix)
		{
			if (matrix.Dimension.RowCount != matrix.Dimension.ColumnCount)
			{
				Matrix<T> m = new Matrix<T>(matrix);
				m.Dimension = new MatrixDimension(matrix.Dimension.ColumnCount, matrix.Dimension.RowCount);
				return m;
			}
			else
			{
				Matrix<T> m = new Matrix<T>(matrix.Dimension);
				for (int row = 0; row < matrix.Dimension.RowCount; row++)
				{
					for (int col = 0; col < matrix.Dimension.ColumnCount; col++)
					{
						m[row, col] = matrix[col, row];
					}
				}
				return m;
			}
		}

		public static Matrix<T> Add(Matrix<T> a, Matrix<T> b)
		{
			if (a.Dimension != b.Dimension)
				throw new DimensionException();

			Matrix<T> RetVal = new Matrix<T>(a.Dimension);
			for (int i = 0; i < RetVal.vectors.Length; i++)
			{
				RetVal.vectors[i] = a.vectors[i] + b.vectors[i];
			}
			return RetVal;
		}

		public static Matrix<T> operator +(Matrix<T> a, Matrix<T> b)
		{
			return Matrix<T>.Add(a, b);
		}

		public static Matrix<T> Subtract(Matrix<T> a, Matrix<T> b)
		{
			if (a.Dimension != b.Dimension)
				throw new DimensionException();

			Matrix<T> RetVal = new Matrix<T>(a.Dimension);
			for (int i = 0; i < RetVal.vectors.Length; i++)
			{
				RetVal.vectors[i] = a.vectors[i] - b.vectors[i];
			}
			return RetVal;
		}

		public static Matrix<T> operator -(Matrix<T> a, Matrix<T> b)
		{
			return Matrix<T>.Subtract(a, b);
		}

		public static Matrix<T> Multiply(T a, Matrix<T> b)
		{
			Matrix<T> RetVal = new Matrix<T>(b.Dimension);
			for (int i = 0; i < RetVal.vectors.Length; i++)
			{
				RetVal.vectors[i] = b.vectors[i] * a;
			}
			return RetVal;
		}

		public static Matrix<T> operator *(T a, Matrix<T> b)
		{
			return Matrix<T>.Multiply(a, b);
		}

		public static Matrix<T> operator *(Matrix<T> a, T b)
		{
			return Matrix<T>.Multiply(b, a);
		}

		public static Matrix<T> Multiply(Matrix<T> a, Matrix<T> b)
		{
			if (a.Dimension.ColumnCount != b.Dimension.RowCount)
				throw new DimensionException();

			Matrix<T> RetVal = new Matrix<T>(a.Dimension.RowCount, b.Dimension.ColumnCount);
			for (int row = 0; row < RetVal.Dimension.RowCount; row++)
			{
				for (int col = 0; col < RetVal.Dimension.ColumnCount; col++)
				{
					RetVal[row, col] = Vector<T>.ScalarProduct(a.RowVectors[row], b.ColumnVectors[col]);
				}
			}
			return RetVal;
		}

		public static Matrix<T> operator *(Matrix<T> a, Matrix<T> b)
		{
			return Matrix<T>.Multiply(a, b);
		}

		public static Vector<T> Multiply(Vector<T> v, Matrix<T> m)
		{
			if (v.Dimension != m.Dimension.RowCount)
				throw new DimensionException();

			Vector<T> RetVal = new Vector<T>(m.Dimension.ColumnCount);
			for (int col = 0; col < RetVal.Dimension; col++)
			{
				RetVal[col] = Vector<T>.ScalarProduct(v, m.ColumnVectors[col]);
			}
			return RetVal;
		}

		public static Vector<T> Multiply(Matrix<T> m, Vector<T> v)
		{
			if (v.Dimension != m.Dimension.ColumnCount)
				throw new DimensionException();

			Vector<T> RetVal = new Vector<T>(m.Dimension.RowCount);
			for (int row = 0; row < RetVal.Dimension; row++)
			{
				RetVal[row] = Vector<T>.ScalarProduct(v, m.RowVectors[row]);
			}
			return RetVal;
		}

		public static Vector<T> operator *(Vector<T> v, Matrix<T> m)
		{
			return Matrix<T>.Multiply(v, m);
		}

		public static Vector<T> operator *(Matrix<T> m, Vector<T> v)
		{
			return Matrix<T>.Multiply(m, v);
		}

		/// <example>
		/// <include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public static bool TryParse(string text, out Matrix<T> value, IFormatProvider formatProvider, string format)
		{
			MatrixFormatter mFormatter = formatProvider as MatrixFormatter;
			string vectorStart = null;
			string vectorEnd = null;
			string vectorFormat = null;
			if (mFormatter != null)
			{
				if (string.IsNullOrEmpty(mFormatter.VectorFormat) == false)
				{
					vectorFormat = VectorFormatter.GetFormat(mFormatter.VectorFormat);
					string[] temp = vectorFormat.Split('\0');
					if (temp.Length != 3)
					{
						throw new ArgumentException("Invalid vector format", "formatProvider");
					}
					vectorStart = temp[0].Trim();
					vectorEnd = temp[2].Trim();
				}
			}
			else if (formatProvider != null)
			{
				throw new ArgumentException("Unexpected IFormatProvider, MatrixFormatter expected.", "formatProvider");
			}
			else
			{
				mFormatter = new MatrixFormatter(null, null);
			}

			format = MatrixFormatter.GetFormat(format);
			string[] parts = format.Split('\0');
			if (parts.Length != 4)
			{
				throw new ArgumentException("Invalid format", "format");
			}

			for (int i = 1; i < parts.Length; i++)
			{
				string temp = parts[i].Trim();
				if (temp.Length > 0)
					parts[i] = temp;
			}

			if (text.StartsWith(parts[1]) == true)
				text = text.Substring(parts[1].Length);
			if (text.EndsWith(parts[3]) == true)
				text = text.Substring(0, text.Length - parts[3].Length);

			string[] groups;
			if (string.IsNullOrEmpty(vectorStart) == false && string.IsNullOrEmpty(vectorEnd) == false)
			{
				groups = text.Group(vectorStart, vectorEnd);
			}
			else
			{
				groups = text.Group(new char[] { '(', '[', '{' }, new char[] { ')', ']', '}' });
			}

			value = new Matrix<T>(0, 0);
			if (groups.Length < 1)
			{
				groups = text.Split(new string[] { parts[2] }, StringSplitOptions.None);
				if (groups.Length < 1)
				{
					return false;
				}
			}

			Vector<T>[] vectors = new Vector<T>[groups.Length];
			if (string.IsNullOrEmpty(vectorFormat) == false)
			{
				for (int i = 0; i < vectors.Length; i++)
				{
					Vector<T> vec;
					if (Vector<T>.TryParse(groups[i], out vec, mFormatter.VectorFormatProvider, vectorFormat) == false)
						return false;
					vectors[i] = vec;
				}
			}
			else
			{
				for (int i = 0; i < vectors.Length; i++)
				{
					Vector<T> vec;
					if (Vector<T>.TryParse(groups[i], out vec) == false)
						return false;
					vectors[i] = vec;
				}
			}

			if (parts[0].ToLowerInvariant() == "row")
			{
				value = Matrix<T>.FromRowVectors(vectors);
			}
			else if (parts[1].ToLowerInvariant() == "column")
			{
				value = Matrix<T>.FromColumnVectors(vectors);
			}
			else
			{
				throw new ArgumentException("Invalid format", "format");
			}
			return true;
		}

		/// <example>
		/// <include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public static bool TryParse(string text, out Matrix<T> value)
		{
			if (Matrix<T>.TryParse(text, out value, null, "g") == true)
				return true;
			if (Matrix<T>.TryParse(text, out value, null, "a") == true)
				return true;
			if (Matrix<T>.TryParse(text, out value, null, "b") == true)
				return true;
			return false;
		}

		#region Subtypes
		public abstract class VectorCollection : IEnumerable<Vector<T>>, IEnumerable<IVector>
		{
			protected Matrix<T> parent;
			protected abstract bool parentContainsVectors { get; }

			public VectorCollection(Matrix<T> parent)
			{
				this.parent = parent;
			}

			public Vector<T> this[int index]
			{
				get
				{
					if (this.parentContainsVectors == true)
						return this.parent.vectors[index];

					Vector<T> RetVal = new Vector<T>(this.parent.vectors.Length);
					for (int i = 0; i < this.parent.vectors.Length; i++)
					{
						RetVal[i] = this.parent.vectors[i][index];
					}
					return RetVal;
				}
				set
				{
					if (this.parentContainsVectors == true)
					{
						if (this.parent.vectors[index].Dimension != value.Dimension)
							throw new DimensionException();
						this.parent.vectors[index] = value;
						return;
					}

					if (this.parent.vectors.Length != value.Dimension)
						throw new DimensionException();

					for (int i = 0; i < this.parent.vectors.Length; i++)
					{
						this.parent.vectors[i][index] = value[i];
					}
				}
			}

			public abstract int Count { get; }

			public Enumerator GetEnumerator()
			{
				return new Enumerator(this);
			}

			IEnumerator<Vector<T>> IEnumerable<Vector<T>>.GetEnumerator()
			{
				return new Enumerator(this);
			}

			IEnumerator<IVector> IEnumerable<IVector>.GetEnumerator()
			{
				return new Enumerator(this);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Enumerator(this);
			}

			public class Enumerator : IEnumerator<Vector<T>>, IEnumerator<IVector>
			{
				int index;
				VectorCollection vCollection;

				public Vector<T> Current { get { return vCollection[this.index]; } }
				object IEnumerator.Current { get { return vCollection[this.index]; } }
				IVector IEnumerator<IVector>.Current { get { return vCollection[this.index]; } }

				public Enumerator(VectorCollection vCollection)
				{
					this.index = -1;
					this.vCollection = vCollection;
				}

				public bool MoveNext()
				{
					this.index++;
					return this.index < this.vCollection.Count;
				}

				public void Reset()
				{
					this.index = -1;
				}

				public void Dispose()
				{
					this.vCollection = null;
				}
			}
		}

		public class RowVectorCollection : VectorCollection
		{
			public RowVectorCollection(Matrix<T> parent)
				: base(parent) { }

			protected override bool parentContainsVectors
			{
				get { return this.parent.containsRowVectors; }
			}

			public override int Count
			{
				get { return this.parent.Dimension.RowCount; }
			}
		}

		public class ColumnVectorCollection : VectorCollection
		{
			public ColumnVectorCollection(Matrix<T> parent)
				: base(parent) { }

			protected override bool parentContainsVectors
			{
				get { return this.parent.containsRowVectors == false; }
			}

			public override int Count
			{
				get { return this.parent.Dimension.ColumnCount; }
			}
		}
		#endregion
	}

	public struct MatrixDimension
	{
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }

		public MatrixDimension(int rowCount, int columnCount)
			:this()
		{
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(MatrixDimension))
				return false;
			MatrixDimension dim = (MatrixDimension)obj;
			return this == dim;
		}

		public static bool operator ==(MatrixDimension a, MatrixDimension b)
		{
			return a.RowCount == b.RowCount && a.ColumnCount == b.ColumnCount;
		}

		public static bool operator !=(MatrixDimension a, MatrixDimension b)
		{
			return a.RowCount != b.RowCount || a.ColumnCount != b.ColumnCount;
		}
	}
}
