using System;
using System.Collections.Generic;

namespace System
{
	/// <summary>
	/// Defines Extension-Methods for classes in the <see cref="System"/> namespace.
	/// </summary>
	public static class ArgusLibExtensions
	{
		/// <summary>
		/// Checks whether a flag value is set.
		/// </summary>
		public static bool HasFlag(this Enum value, Enum flag)
		{
			if (value.GetType() != flag.GetType())
				throw new ArgumentException("Both arguments must be of the same type.", "value, flag");

			UInt64 v = (UInt64)Convert.ToUInt64(value);
			UInt64 f = (UInt64)Convert.ToUInt64(flag);
			return (v & f) == f;
		}

		/// <summary>
		/// Converts an <see cref="Enum"/> to an <see cref="UInt64"/>.
		/// </summary>
		public static UInt64 ToUInt64(this Enum e)
		{
			return (UInt64)Convert.ToUInt64(e);
		}

		public static void SwapOrder<T>(this T[] array, int count)
		{
			T[] buffer = new T[array.Length];
			array.CopyTo(buffer, 0);
			for (long i = 0; i < count; i++)
			{
				array[count - (i + 1)] = buffer[i];
			}
		}

		public static void SwapOrder<T>(this T[] array)
		{
			array.SwapOrder(array.Length);
		}

		public static string ToFormatString(this object value, IFormatProvider formatProvider, string format)
		{
			return String.Format(formatProvider, "{0:" + format + "}", value);
		}

		public static string[] Group(this string s, string start, string end)
		{
			List<string> RetVal = new List<string>();
			int a = s.IndexOf(start)+1;
			int b = s.IndexOf(end, a);
			while (a > 0 && b > -1)
			{
				if (b - a < 1)
					continue;

				RetVal.Add(s.Substring(a, b - a));

				a = s.IndexOf(start, b+1)+1;
				b = s.IndexOf(end, a);
			}
			return RetVal.ToArray();
		}

		public static string[] Group(this string s, char start, char end)
		{
			List<string> RetVal = new List<string>();
			int a = s.IndexOf(start) + 1;
			int b = s.IndexOf(end, a);
			while (a > -1 && b > -1)
			{
				if (b - a < 1)
					continue;

				RetVal.Add(s.Substring(a, b - a));

				a = s.IndexOf(start, b + 1) + 1;
				b = s.IndexOf(end, a);
			}
			return RetVal.ToArray();
		}

		public static string[] Group(this string s, char[] start, char[] end)
		{
			List<string> RetVal = new List<string>();
			int a = s.IndexOfAny(start) + 1;
			int b = s.IndexOfAny(end, a);
			while (a > -1 && b > -1)
			{
				if (b - a < 1)
					continue;

				RetVal.Add(s.Substring(a, b - a));

				a = s.IndexOfAny(start, b + 1) + 1;
				b = s.IndexOfAny(end, a);
			}
			return RetVal.ToArray();
		}
	}
}
