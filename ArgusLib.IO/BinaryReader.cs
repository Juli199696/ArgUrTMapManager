using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SBinaryReader = System.IO.BinaryReader;

namespace ArgusLib.IO
{
	public class BinaryReader : SBinaryReader
	{
		public BinaryReader(Stream input)
			: base(new EndianConsideringStream(input))
		{
		}

		public BinaryReader(Stream input, Encoding encoding)
			: base(new EndianConsideringStream(input), encoding)
		{
		}

		public Endianness Endianness
		{
			get { return ((EndianConsideringStream)this.BaseStream).Endianess; }
			set { ((EndianConsideringStream)this.BaseStream).Endianess = value; }
		}

		public override int Read(byte[] buffer, int index, int count)
		{
			Endianness end = this.Endianness;
			this.Endianness = ArgusLib.Endianness.LittleEndian;
			int error = base.Read(buffer, index, count);
			this.Endianness = end;
			return error;
		}

		public override byte[] ReadBytes(int count)
		{
			Endianness end = this.Endianness;
			this.Endianness = ArgusLib.Endianness.LittleEndian;
			byte[] RetVal = base.ReadBytes(count);
			this.Endianness = end;
			return RetVal;
		}
	}
}
