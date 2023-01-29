using System;
using System.Collections.Generic;
using System.Text;

namespace ArgusLib
{
	public class IntegerFormatter : IFormatProvider, ICustomFormatter
	{
		public object GetFormat(Type type)
		{
			if (type == typeof(ICustomFormatter))
				return this;

			return null;
			//return this;
		}

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			if (format == "b" || format == "B")
			{
				ulong value;
				try
				{
					value = (ulong)Convert.ToUInt64(arg);
				}
				catch (Exception exception)
				{
					throw new ArgumentException("The argument cannot be converted to an unsigned Integer value.", "arg", exception);
				}
				return "0b" + IntegerFormatter.IntToBin(value);
			}
			else if (format == "x" || format == "X")
			{
				ulong value;
				try
				{
					value = (ulong)Convert.ToUInt64(arg);
				}
				catch (Exception exception)
				{
					throw new ArgumentException("The argument cannot be converted to an unsigned Integer value.", "arg", exception);
				}
				return "0x" + value.ToString(format);
			}
			else
			{
				long value;
				try
				{
					value = (long)Convert.ToInt64(arg);
				}
				catch (Exception exception)
				{
					throw new ArgumentException("The argument cannot be converted to an Integer value.", "arg", exception);
				}
				return value.ToString(format);
			}
		}

		private static string IntToBin(ulong value)
		{
			int length = 64;
			char[] chars = new char[length];
			ulong one = 1;
			for (int i = 0; i < length; i++)
			{
				ulong mask = one << i;
				if ((value & mask) == mask)
				{
					chars[length-i-1] = '1';
				}
				else
				{
					chars[length-i-1] = '0';
				}
			}
			string RetVal = new string(chars);
			return RetVal.TrimStart('0');
		}
	}
}
