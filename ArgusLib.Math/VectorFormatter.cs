using System;
using System.Collections.Generic;
using System.Text;
using ArgusLib;

namespace ArgusLib.Math
{
	public class VectorFormatter : IFormatProvider, ICustomFormatter
	{
		public IFormatProvider ScalarFormatProvider { get; private set; }
		public string ScalarFormat { get; private set; }

		public VectorFormatter(IFormatProvider scalarFormatProvider, string scalarFormat)
		{
			this.ScalarFormatProvider = scalarFormatProvider;
			this.ScalarFormat = scalarFormat;
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
				return "( \0, \0 )";
			}
			else if (format == "a")
			{
				return "[ \0, \0 ]";
			}
			else if (format == "b")
			{
				return "{ \0, \0 }";
			}
			return formatSpecifier;
		}

		/// <summary>
		/// Formats a <see cref="Vector{T}"/> into a text representation. Only for the standard formats
		/// it is guaranteed that the resulting text can be parsed back, provided the scalars can be parsed back.
		/// <list type="table">
		///		<listheader>
		///			<term>Format</term>
		///			<description>Description</description>
		///		</listheader>
		///		<item>
		///			<term>Left\0Between\0Right</term>
		///			<description>
		///			Specify the string which is put before the first element ("Left"),
		///			the string which is put between each element ("Between") and the string
		///			which is put after the last element ("Right").
		///			Example: "[ \0, \0 )" -> "[ x1, x2, ..., xn )"
		///			</description>
		///		</item>
		///		<item>
		///			<term>g, G</term>
		///			<description>g, G is equal to "( \0, \0 )" and results in "( x1, x2, ..., xn )"</description>
		///		</item>
		///		<item>
		///			<term>a, A</term>
		///			<description>a, A is equal to "[ \0, \0 ]" and results in "[ x1, x2, ..., xn ]"</description>
		///		</item>
		///		<item>
		///			<term>b, B</term>
		///			<description>b, B is equal to "{ \0, \0 }" and results in "{ x1, x2, ..., xn }"</description>
		///		</item>
		/// </list>
		/// </summary>
		/// <example>
		/// <include file='XmlDocumentation.xml' path='Documentation/Examples[@name="MatrixFormatAndParse"]/*' />
		/// </example>
		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			IVector vector;
			try
			{
				vector = (IVector)arg;
			}
			catch (Exception exception)
			{
				throw new ArgumentException("The argument cannot be converted to IVector", "arg", exception);
			}

			format = VectorFormatter.GetFormat(format);

			string[] parts = format.Split('\0');
			if (parts.Length != 3)
			{
				throw new ArgumentException("Invalid format", "format");
			}

			string[] RetVal = new string[vector.Dimension+1];
			RetVal[0] = parts[0];
			for (int i = 0; i < vector.Dimension-1; i++)
			{
				RetVal[i+1] = vector[i].ToString(this.ScalarFormatProvider, this.ScalarFormat) + parts[1];
			}
			RetVal[vector.Dimension] = vector[vector.Dimension - 1].ToString(this.ScalarFormatProvider, this.ScalarFormat) + parts[2];

			return String.Concat(RetVal);
		}
	}
}
