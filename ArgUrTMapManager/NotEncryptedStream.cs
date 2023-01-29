using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager
{
	class NotEncryptedStream : EncryptedStream
	{
		public NotEncryptedStream()
			: base()
		{
		}

		public NotEncryptedStream(Stream stream)
			: base(stream)
		{
		}

		public override void SetUnderlyingStream(Stream stream)
		{
			base.SetUnderlyingStream(stream);
		}

		protected override byte EncryptByte(byte b)
		{
			throw new NotImplementedException();
		}

		protected override byte DecryptByte(byte b)
		{
			throw new NotImplementedException();
		}

		public override int ReadByte()
		{
			return this.UnderlyingStream.ReadByte();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.UnderlyingStream.Read(buffer, offset, count);
		}

		public override void WriteByte(byte value)
		{
			this.UnderlyingStream.WriteByte(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			this.UnderlyingStream.Write(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.UnderlyingStream.Seek(offset, origin);
		}
	}
}
