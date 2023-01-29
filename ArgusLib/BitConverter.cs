using System;
using System.Collections.Generic;
using System.Text;
using SBitConv = System.BitConverter;

namespace ArgusLib
{
	/// <summary>
	/// Endian-dependent BitConverter.
	/// </summary>
	/// <seealso cref="BitConverter"/>
	public static class BitConverter
	{
		public static byte[] GetBytes(bool value, Endianness endianness)
		{
			return SBitConv.GetBytes(value);
		}

		public static byte[] GetBytes(char value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(double value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(float value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(int value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(long value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(short value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(uint value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(ulong value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		public static byte[] GetBytes(ushort value, Endianness endianness)
		{
			byte[] data = SBitConv.GetBytes(value);
			if (endianness == Endianness.BigEndian)
				data.SwapOrder();
			return data;
		}

		private static byte[] SwapBytes(byte[] data, int index, int count)
		{
			byte[] b = new byte[count];
			for (int i = 0; i < count; i++)
				b[count - i - 1] = data[index + i];
			return b;
		}

		public static bool ToBoolean(byte[] value, int startIndex, Endianness endianness)
		{
			return SBitConv.ToBoolean(value, startIndex);
		}

		public static char ToChar(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToChar(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 2);
			return SBitConv.ToChar(data, 0);
		}

		public static double ToDouble(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToDouble(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 8);
			return SBitConv.ToDouble(data, 0);
		}

		public static Int16 ToInt16(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToInt16(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 2);
			return SBitConv.ToInt16(data, 0);
		}

		public static Int32 ToInt32(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToInt32(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 4);
			return SBitConv.ToInt32(data, 0);
		}

		public static Int64 ToInt64(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToInt64(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 8);
			return SBitConv.ToInt64(data, 0);
		}

		public static Single ToSingle(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToSingle(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 4);
			return SBitConv.ToSingle(data, 0);
		}

		public static UInt16 ToUInt16(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToUInt16(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 2);
			return SBitConv.ToUInt16(data, 0);
		}

		public static UInt32 ToUInt32(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToUInt32(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 4);
			return SBitConv.ToUInt32(data, 0);
		}

		public static UInt64 ToUInt64(byte[] value, int startIndex, Endianness endianness)
		{
			if (endianness == Endianness.LittleEndian)
				return SBitConv.ToUInt64(value, startIndex);
			byte[] data = SwapBytes(value, startIndex, 8);
			return SBitConv.ToUInt64(data, 0);
		}
	}
}
