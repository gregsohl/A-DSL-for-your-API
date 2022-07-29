#region LGPL License
/* 
 * CompactFormatter: A generic formatter for the .NET Compact Framework
 * Copyright (C) 2004  Angelo Scotto (scotto_a@hotmail.com)
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
 * $Id: IStreamParser.cs 14 2004-08-26 09:08:59Z Angelo $
 * */
#endregion

using System;
using System.IO;

namespace CompactFormatter.Interfaces
{
	/// <summary>
	/// This interface must be implemented if it's necessary to transform the stream
	/// produced by CompactFormatter before sending it through the wire.
	/// For example to compress the stream moving through the wire or to encrypt it.
	/// </summary>
	public abstract class IStreamParser : Stream
	{

		public override bool CanRead
		{
			get
			{
				return str.CanRead;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return str.CanWrite;
			}
		}

		public override void Flush()
		{
			str.Flush();
		}

		public override int Read(byte[] buffer, int offset, int len)
		{
			return ParseInput(ref buffer,offset,len);
		}

		public override void Write(byte[] buffer, int offset, int len)
		{
			ParseOutput(ref buffer,offset,len);
		}

		#region Implementation
		public override bool CanSeek
		{
			get
			{
				return str.CanSeek;
			}
		}

		public override long Length
		{
			get
			{
				return str.Length;
			}
		}

		public override long Position
		{
			get
			{
				return str.Position;
			}
			set
			{
				str.Position = value;
			}
		}

		public override void SetLength(long len)
		{
			str.SetLength(len);
		}

		public override long Seek(long pos, SeekOrigin from)
		{
			return str.Seek(pos,from);
		}
		#endregion


		protected Stream str;

		public Stream InnerStream
		{
			set
			{
					str = value;
			}
		}

		
		/// <summary>
		/// This is invoked by CompactFormatter.Deserialize just before starting
		/// serialization.
		/// </summary>
		/// <param name="buffer">the array from which read data to send.</param>
		/// <param name="offset">the offset at which starting to read data.</param>
		/// <param name="len">the number of bytes to write on the stream.</param>
		protected abstract void ParseOutput(ref byte[] buffer, int offset, int len);

		/// <summary>
		/// This is invoked by CompactFormatter.Serialize just before starting
		/// deserialization.
		/// </summary>
		/// <param name="buffer">the array in which write data read.</param>
		/// <param name="offset">the offset at which starting to write data.</param>
		/// <param name="len">the number of bytes to read from stream.</param>
		/// <returns>the number of bytes read</returns>
		protected abstract int ParseInput(ref byte[] buffer, int offset, int len);
	}
}
