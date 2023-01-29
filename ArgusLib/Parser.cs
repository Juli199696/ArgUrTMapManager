using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace ArgusLib
{
	/// <summary>
	/// A static class providing several parsing methods.
	/// </summary>
	public static class Parser
	{
		const string PrefixBinary = "0b";
		const string PrefixHex = "0x";

		public delegate bool TryParseHandler(string text, out object value);
		public delegate bool TryParseHandler<T>(string text, out T value);

		/// <summary>
		/// Tries to parse a text representation of a unsigned integer. A text starting with
		/// "0b" will be assumed to be a binary number, a text starting with "0x" will be
		/// assumed to be a hexadecimal number. Any other text will be parsed using <see cref="ulong.TryParse"/>.
		/// </summary>
		/// <param name="text">The text to be parsed.</param>
		/// <param name="value">The output value.</param>
		/// <returns>true, if successfully parsed, otherwise false.</returns>
		public static bool TryParse(string text, out ulong value)
		{
			text = text.ToLowerInvariant();
			if (text.StartsWith(Parser.PrefixBinary) == true)
			{
				return Parser.TryParseBinary(text.Substring(Parser.PrefixBinary.Length), out value);
			}
			else if (text.StartsWith(Parser.PrefixHex) == true)
			{
				return ulong.TryParse(text.Substring(Parser.PrefixHex.Length), System.Globalization.NumberStyles.HexNumber, null, out value);
			}
			else
			{
				return ulong.TryParse(text, out value);
			}
		}

		private static bool TryParseBinary(string text, out ulong value)
		{
			value = 0;
			if (text.Length < 1)
				return false;
			ulong one = 1;
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] == '0')
				{
					continue;
				}
				else if (text[i] == '1')
				{
					value += one << (text.Length - i - 1);
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		public static TryParseHandler<T> GetTryParseHandler<T>()
		{
			Type type = typeof(T);
			MethodInfo mi = type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static);
			if (mi == null)
				return null;
			return (TryParseHandler<T>)Delegate.CreateDelegate(typeof(TryParseHandler<T>), mi);
		}

		public static TryParseHandler GetTryParseHandler(Type type)
		{
			MethodInfo mi = type.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static);
			if (mi == null)
				return null;
			return (TryParseHandler)Delegate.CreateDelegate(typeof(TryParseHandler), mi);
		}

		/// <summary>
		/// Tries to invoke the TryParse-Method on T using <see cref="System.Reflection"/>.
		/// Throws an exception if T does not declare T.TryParse.
		/// </summary>
		public static bool TryParse<T>(string text, out T value)
		{
			TryParseHandler<T> handler = Parser.GetTryParseHandler<T>();
			if (handler == null)
			{
				throw new Exception("Type " + typeof(T).Name + " does not declare a method named \"TryParse\".");
			}
			return handler(text, out value);
		}
	}
}
