using System;
using System.Collections.Generic;
using System.IO;

namespace ArgUrTMapManager
{
	abstract class EncryptedStream : Stream
	{
		public Stream UnderlyingStream { get; private set; }

		/// <summary>
		/// If this constructor is used, <see cref="SetUnderlyingStream"/> has to be called
		/// before any other method.
		/// </summary>
		public EncryptedStream()
		{
		}

		public virtual void SetUnderlyingStream(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");
			this.UnderlyingStream = stream;
		}

		public EncryptedStream(Stream stream)
		{
			this.SetUnderlyingStream(stream);
		}

		protected abstract byte EncryptByte(byte b);
		protected abstract byte DecryptByte(byte b);

		public override int ReadByte()
		{
			int error = this.UnderlyingStream.ReadByte();
			if (error < 0)
				return error;
			return this.DecryptByte((byte)error);
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			int error = this.UnderlyingStream.Read(buffer, offset, count);
			for (int i = 0; i < offset + count; i++)
			{
				buffer[i] = this.DecryptByte(buffer[i]);
			}
			return error;
		}

		public override void WriteByte(byte value)
		{
			this.UnderlyingStream.WriteByte(this.EncryptByte(value));
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < offset + count; i++)
			{
				buffer[i] = this.EncryptByte(buffer[i]);
			}
			this.UnderlyingStream.Write(buffer, offset, count);
		}

		#region OverrideRedirect
		public override bool CanRead
		{
			get { return this.UnderlyingStream.CanRead; }
		}

		public override bool CanSeek
		{
			get { return this.UnderlyingStream.CanSeek; }
		}

		public override bool CanTimeout
		{
			get { return this.UnderlyingStream.CanTimeout; }
		}

		public override bool CanWrite
		{
			get { return this.UnderlyingStream.CanWrite; }
		}

		public override void Close()
		{
			base.Close();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		public override void Flush()
		{
			this.UnderlyingStream.Flush();
		}

		public override long Length
		{
			get { return this.UnderlyingStream.Length; }
		}

		public override long Position
		{
			get { return this.UnderlyingStream.Position; }
			set { this.UnderlyingStream.Position = value; }
		}

		public override int ReadTimeout
		{
			get { return this.UnderlyingStream.ReadTimeout; }
			set { this.UnderlyingStream.ReadTimeout = value; }
		}

		public override int WriteTimeout
		{
			get { return this.UnderlyingStream.WriteTimeout; }
			set { this.UnderlyingStream.WriteTimeout = value; }
		}
		#endregion

		#region OverrideToNotImplemented
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			throw new NotImplementedException();
		}

		public override int EndRead(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		public override void EndWrite(IAsyncResult asyncResult)
		{
			throw new NotImplementedException();
		}

		//public override long Seek(long offset, SeekOrigin origin)
		//{
		//	throw new NotImplementedException();
		//}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
