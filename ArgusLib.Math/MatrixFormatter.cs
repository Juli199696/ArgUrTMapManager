using System;
using System.Collections.Generic;
using System.Text;
using ArgusLib;

namespace ArgusLib.Math
{
	public class MatrixFormatter : IFormatProvider, ICustomFormatter
	{
		public IFormatProvider VectorFormatProvider { get; private set; }
		public string VectorFormat { get; private set; }

		public MatrixFormatter(IFormatProvider vectorFormatProvider, string vectorFormat)
		{
			this.VectorFormatProvider = vectorFormatProvider;
			this.VectorFormat = vectorFormat;
		}

		public object GetFormat(Type type)
		{
			if (type == typeof(ICustomFormatter))
				return this;
			else
				return null;
		}

		public static string GetFormat(string formatSpecifier)
		{
			string format = formatSpecifier.ToLowerInvariant();
			if (format == "g")
			{
				return "row\0[ \0, \0 ]";
			}
			else if (format == "a")
			{
				return "row\0( \0, \0 )";
			}
			else if (format == "b")
			{
				return "row\0{ \0, \0 }";
			}
			else if (format == "n")
			{
				return "row\0| \0 |\n| \0 |";
			}
			return formatSpecifier;
		}

		/// <summary>
		/// Formats a <see cref="Matrix{T}"/> into a text representation. Only for the standard formats
		/// it is guaranteed that the resulting text can be parsed back, provided the vectors can be parsed back.
		/// <list type="table">
		///		<listheader>
		///			<term>Format</term>
		///			<description>Description</description>
		///		</listheader>
		///		<item>
		///			<term>Row/Column\0Left\0Between\0Right</term>
		///			<description>
		///			Specify whether the row or the column vectors should be formatted ("row" or "column").
		///			Specify the string which is put before the first vector ("Left"),
		///			the string which is put between each vector ("Between") and the string
		///			which is put after the last vector ("Right").
		///			Example: "column\0[ \0, \0 )" -> "[ columnVector1, columnVector2, ..., columnVectorN )"
		///			</description>
		///		</item>
		///		<item>
		///			<term>g, G</term>
		///			<description>g, G is equal to "row\0[ \0, \0 ]" and results in "[ rowVector1, rowVector2, ..., rowVectorN ]"</description>
		///		</item>
		///		<item>
		///			<term>a, A</term>
		///			<description>a, A is equal to "row\0( \0, \0 )" and results in "( rowVector1, rowVector2, ..., rowVectorN )"</description>
		///		</item>
		///		<item>
		///			<term>b, B</term>
		///			<description>b, B is equal to "row\0{ \0, \0 }" and results in "{ rowVector1, rowVector2, ..., rowVectorN }"</description>
		///		</item>
		///		<item>
		///			<term>n, N</term>
		///			<description>n, N is equal to "row\0| \0 |\n| \0 |" and results in "| rowVector1 |\n| rowVector2 |\n| ... |\n| rowVectorN |"</description>
		///		</item>
		/// </list>
		/// </summary>
		/// <example>
		///		<include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			IMatrix matrix;
			try
			{
				matrix = (IMatrix)arg;
			}
			catch (Exception exception)
			{
				throw new ArgumentException("The argument cannot be converted to IMatrix", "arg", exception);
			}

			format = MatrixFormatter.GetFormat(format);

			string[] parts = format.Split('\0');
			if (parts.Length != 4)
			{
				throw new ArgumentException("Invalid format", "format");
			}

			int count;
			IEnumerable<IVector> vectors;
			if (parts[0].ToLowerInvariant() == "row")
			{
				count = matrix.Dimension.RowCount;
				vectors = matrix.RowVectors;
			}
			else if (parts[0].ToLowerInvariant() == "column")
			{
				count = matrix.Dimension.ColumnCount;
				vectors = matrix.ColumnVectors;
			}
			else
			{
				throw new ArgumentException("Invalid format", "format");
			}

			string[] RetVal = new string[count+1];
			RetVal[0] = parts[1];
			int i = 0;
			foreach (IVector vector in vectors)
			{
				RetVal[i + 1] = vector.ToFormatString(this.VectorFormatProvider, this.VectorFormat) + parts[2];
				i++;
			}
			RetVal[count] = RetVal[count].Remove(RetVal[count].Length - parts[2].Length) + parts[3];

			return String.Concat(RetVal);
		}
	}
}
