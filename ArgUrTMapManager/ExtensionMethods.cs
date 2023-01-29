using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Ionic.Zip;

namespace ArgUrTMapManager
{
	static class ExtensionMethods
	{
		public static double GetArea(this Rectangle rect)
		{
			return rect.Width * rect.Height;
		}

		public static byte[] GetBytes(this Stream stream)
		{
			byte[] data = new byte[stream.Length];
			long pos = stream.Position;
			stream.Seek(0, SeekOrigin.Begin);
			stream.Read(data, 0, data.Length);
			stream.Position = pos;
			return data;
		}

		public static void WriteToStream(this Stream source, Stream target, int bufferSize)
		{
			byte[] data = new byte[bufferSize];
			int count = 1;
			while (count > 0)
			{
				count = source.Read(data, 0, bufferSize);
				target.Write(data, 0, count);
			}
		}

		public static void ReadFromStream(this Stream target, Stream source, int bufferSize)
		{
			ExtensionMethods.WriteToStream(source, target, bufferSize);
		}

		/// <summary>
		/// Reads <paramref name="length"/> bytes from the stream, converts them to <see cref="char"/>
		/// and returns a <see cref="string"/>.
		/// If the string is null terminated, the returned string can be shorter than <paramref name="length"/>.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string ReadString(this BinaryReader stream, int length)
		{
			byte[] data = new byte[length];
			length = stream.Read(data, 0, length);
			char[] str = new char[length];
			int i;
			for (i = 0; i < length; i++)
			{
				char c = (char)data[i];
				if (c == '\0')
					break;
				str[i] = c;
			}

			return new string(str, 0, i);
		}

		public static void WriteString(this BinaryWriter stream, string value, int length)
		{
			byte[] data = new byte[length];
			for (int i = 0; i < value.Length; i++)
			{
				data[i] = (byte)value[i];
			}
			stream.Write(data, 0, data.Length);
		}

		public static void WriteString(this BinaryWriter stream, string value)
		{
			stream.WriteString(value, value.Length);
		}

		//public static void CopyTo(this ZipEntry zipEntry, ZipFile target)
		//{
		//	using (MemoryStream ms = new MemoryStream((int)zipEntry.UncompressedSize))
		//	{
		//		zipEntry.Extract(ms);
		//		ms.Seek(0, SeekOrigin.Begin);
		//		target.AddEntry(zipEntry.FileName, ms);
		//	}
		//}
	}
}
