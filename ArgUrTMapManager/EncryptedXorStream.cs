using System;
using System.Collections.Generic;
using System.IO;
namespace ArgUrTMapManager
{
	class EncryptedXorStream : EncryptedStream
	{
		byte[] xorKey;
		int xorIndex;

		public EncryptedXorStream(byte[] XorKey)
			:base()
		{
			this.xorKey = XorKey;
		}

		public EncryptedXorStream(Stream stream, byte[] XorKey)
			: base(stream)
		{
			this.xorKey = XorKey;
		}

		public override void SetUnderlyingStream(Stream stream)
		{
			this.xorIndex = 0;
			base.SetUnderlyingStream(stream);
		}

		private byte GetNextKeyByte()
		{
			byte b = this.xorKey[this.xorIndex];
			this.xorIndex++;
			if (this.xorIndex >= this.xorKey.Length)
				this.xorIndex = 0;
			return b;
		}

		protected override byte DecryptByte(byte b)
		{
			return (byte)(b ^ this.GetNextKeyByte());
		}

		protected override byte EncryptByte(byte b)
		{
			return (byte)(b ^ this.GetNextKeyByte());
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long error = this.UnderlyingStream.Seek(offset, origin);
			this.xorIndex = (int)(this.Position % this.xorKey.Length);
			return error;
		}
	}
}
