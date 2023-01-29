using System;
using System.Collections.Generic;
using System.IO;

namespace ArgusLib.IO
{
	public class EndianConsideringStream : Stream
	{
		public Stream BaseStream{get;private set;}

		public EndianConsideringStream(Stream stream)
			: base()
		{
			this.BaseStream = stream;
			this.Endianess = Endianness.LittleEndian;
		}

		public Endianness Endianess { get; set; }

		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
			return base.BeginRead(buffer, offset, count, callback, state);
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
			return base.BeginWrite(buffer, offset, count, callback, state);
		}

		public override bool CanRead
		{
			get { return this.BaseStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return this.BaseStream.CanSeek; }
		}

		public override bool CanTimeout
		{
			get { return this.BaseStream.CanTimeout; }
		}

		public override bool CanWrite
		{
			get { return this.BaseStream.CanWrite; }
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
			return base.EndRead(asyncResult);
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
			base.EndWrite(asyncResult);
		}

		public override void Flush()
		{
			this.BaseStream.Flush();
		}

		public override long Length
		{
			get { return this.BaseStream.Length; }
		}

		public override long Position
		{
			get { return this.BaseStream.Position; }
			set { this.BaseStream.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int i = this.BaseStream.Read(buffer, offset, count);
			if (this.Endianess == Endianness.BigEndian)
			{
				buffer.SwapOrder(count);
			}
			return i;
		}

		public override int ReadByte()
		{
			return this.BaseStream.ReadByte();
		}

		public override int ReadTimeout
		{
			get { return this.BaseStream.ReadTimeout; }
			set { this.BaseStream.ReadTimeout = value; }
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.BaseStream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			this.BaseStream.SetLength(value);
		}

		public override string ToString()
		{
			return this.BaseStream.ToString();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.Endianess == Endianness.BigEndian)
			{
				buffer.SwapOrder(count);
			}
			this.BaseStream.Write(buffer, offset, count);
		}

		public override void WriteByte(byte value)
		{
			this.BaseStream.WriteByte(value);
		}

		public override int WriteTimeout
		{
			get { return this.BaseStream.WriteTimeout; }
			set { this.BaseStream.WriteTimeout = value; }
		}
	}
}
