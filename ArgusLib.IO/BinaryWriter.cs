using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SBinaryWriter = System.IO.BinaryWriter;

namespace ArgusLib.IO
{
	public class BinaryWriter : SBinaryWriter
	{
		public BinaryWriter(Stream input)
			: base(new EndianConsideringStream(input)) { }

		public BinaryWriter(Stream input, Encoding encoding)
			: base(new EndianConsideringStream(input), encoding) { }
	}
}
