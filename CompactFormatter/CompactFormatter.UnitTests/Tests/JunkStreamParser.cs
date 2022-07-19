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
 * $Id: JunkStreamParser.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// An useless stream parser used for testing purpose.
	/// It simply adds string "Ciao!" to each read call.
	/// </summary>
	public class JunkStreamParser : Interfaces.IStreamParser
	{

		/// <summary>
		/// This is invoked by CompactFormatter.Deserialize just before starting
		/// serialization.
		/// </summary>
		/// <param name="buffer">the array from which read data to send.</param>
		/// <param name="offset">the offset at which starting to read data.</param>
		/// <param name="len">the number of bytes to write on the stream.</param>
		protected override void ParseOutput(ref byte[] buffer, int offset, int len)
		{
			// First of all write correct data 
			str.Write(buffer,offset,len);
			// Now i've to add offending line
			str.Write(System.Text.Encoding.Unicode.GetBytes("Ciao!"),0,10);
		}

		/// <summary>
		/// This is invoked by CompactFormatter.Serialize just before starting
		/// deserialization.
		/// </summary>
		/// <param name="buffer">the array in which write data read.</param>
		/// <param name="offset">the offset at which starting to write data.</param>
		/// <param name="len">the number of bytes to read from stream.</param>
		/// <returns>the number of bytes read</returns>
		protected override int ParseInput(ref byte[] buffer, int offset, int len)
		{
			byte[] temp = new byte[10];
			// First of all read correct data in buffer
			int l = str.Read(buffer,offset,len);
			// Now i've to discard offending line
			str.Read(temp,0,10);
			return l;
		}

	}
}
