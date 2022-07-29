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
 * $Id: PrimitiveSerializer.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.IO;

namespace CompactFormatter
{
	/// <summary>
	/// This class contains static methods used to serialize primitive types.
	/// </summary>
	public class CustomSerializationHelper
	{

		#region Primitive Types for .NET and CompactFormatter
// Used to test time difference between BinaryWriter/Reader usage and ArrayCopy 
// approach
		public static void Serialize(Stream stream, float value)
		{
#if DEBUG
			Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
				"Serializing Single type, value "+value);
#endif
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		public static Single DeserializeSingle(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToSingle(buffer,0);
		}

		/// <summary>
		/// Static method used to serialize a Boolean to the stream.
		/// <remarks>This custom approach is faster than using BitConverter.
		/// </remarks>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="stream"></param>
		public static void Serialize(Stream stream, bool value)
		{
			if (value) stream.WriteByte(1);
			else stream.WriteByte(0);
		}

		/// <summary>
		/// Static method used to deserialize a Boolean from the stream.
		/// <remarks>This custom approach is more efficient than using BitConverter.
		/// </remarks>
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Boolean DeserializeBoolean(Stream stream)
		{
			if(stream.ReadByte() == 1) return true;
			else return false;
		}

		public static void Serialize(Stream stream, byte value)
		{		
			stream.WriteByte(value);			
		}

		public static Byte DeserializeByte(Stream stream)
		{
			return (Byte)stream.ReadByte();
		}

		public static void Serialize(Stream stream, char value)
		{
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		public static Char DeserializeChar(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToChar(buffer,0);
		}

		public static void Serialize(Stream stream, double value)
		{
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		public static Double DeserializeDouble(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToDouble(buffer,0);
		}

		public static void Serialize(Stream stream, Guid value)
		{
			byte[] buffer = value.ToByteArray();
			stream.Write(buffer, 0, 16);
		}

		public static Guid DeserializeGuid(Stream stream)
		{
			byte[] buffer = new byte[16];
			stream.Read(buffer, 0, 16);
			return new Guid(buffer);
		}

		public static void Serialize(Stream stream, short value)
		{
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		public static Int16 DeserializeInt16(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToInt16(buffer,0);
		}

		public static void Serialize(Stream stream, int value)
		{
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		public static Int32 DeserializeInt32(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToInt32(buffer,0);
		}

		public static void Serialize(Stream stream, long value)
		{
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		public static Int64 DeserializeInt64(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToInt64(buffer,0);
		}

		public static void Serialize(Stream stream, sbyte value)
		{
			stream.WriteByte((byte)value);			
		}

		public static SByte DeserializeSByte(Stream stream)
		{
			return (SByte)stream.ReadByte();
		}

		public static void Serialize(Stream stream, ushort value)
		{
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		public static UInt16 DeserializeUInt16(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToUInt16(buffer,0);
		}

		public static void Serialize(Stream stream, uint value)
		{
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		public static UInt32 DeserializeUInt32(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToUInt32(buffer,0);
		}

		public static void Serialize(Stream stream, ulong value)
		{
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		public static UInt64 DeserializeUInt64(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToUInt64(buffer,0);
		}
		#endregion

		#region Primitive Types for CompactFormatter

		/// <summary>
		/// Serializes a byte array to the given stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="value">The value.</param>
		public static void Serialize(Stream stream, byte[] value)
		{
			// If we have a null value, then we serialize a -1 for the byte
			// length to indicate that.
			if (value == null)
			{
				Serialize(stream, -1);
				return;
			}

			// Otherwise start by writing out the length of the byte array.
			Serialize(stream, value.Length);

			// Write out the byte array directly.
			stream.Write(value, 0, value.Length);
		}

		/// <summary>
		/// Deserializes a byte array from the CF stream.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <returns></returns>
		public static byte[] DeserializeByteArray(Stream stream)
		{
			// Pull out the length of the array we need to allocate.
			int length = DeserializeInt32(stream);

			if (length < 0)
			{
				// A negative length means we have a null.
				return null;
			}

			// Allocate a space and read it into memory.
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);

			// Return the resulting array.
			return buffer;
		}

		public static void Serialize(Stream stream, decimal value)
		{
			/* The currently used approach is faster than the approach showed
			 * below, tests serializing 1500000 Decimal used 27187500 ticks
			 * against 14687500 obtained using this approach.
			 * 
			 * Serialize(answer[0],stream);
			 * Serialize(answer[1],stream);
			 * Serialize(answer[2],stream);
			 * Serialize(answer[3],stream);
			 * */
			int[] answer = Decimal.GetBits(value);
			byte[] ans = new byte[16];
			Buffer.BlockCopy(answer,0,ans,0,16);
			stream.Write(ans,0,16);
		}

		public static Decimal DeserializeDecimal(Stream stream)
		{
			/* The currently used approach is faster than the approach showed
			 * below, tests serializing 1500000 Decimal used 27187500 ticks
			 * against 14687500 obtained using this approach.
			 * 
			 * array[0] = DeserializeInt32(stream);
			 * array[1] = DeserializeInt32(stream);
			 * array[2] = DeserializeInt32(stream);
			 * array[3] = DeserializeInt32(stream);
			 * return new Decimal(array);
			 * */

			int[] array = new int[4];
			byte[] ans = new byte[16];
			stream.Read(ans,0,16);
			Buffer.BlockCopy(ans,0,array,0,16);
			return new Decimal(array);
		}


		// Used to test time difference between BinaryWriter/Reader usage and ArrayCopy 
		// approach (2x slower with BinaryReader/Writer)
		public static void Serialize(Stream stream, string value)
		{
			// We have slightly different processing if we are a null value.
			if (value == null)
			{
				byte[] byteCountArray = new byte[4];
				Buffer.BlockCopy(BitConverter.GetBytes(-1), 0, byteCountArray, 0, 4);
				stream.Write(byteCountArray, 0, byteCountArray.Length);
				return;
			}

			byte[] array = new byte[value.Length*2 + 4];

			Buffer.BlockCopy(BitConverter.GetBytes(value.Length*2),0,array,0,4);
			Buffer.BlockCopy(System.Text.Encoding.Unicode.GetBytes(value),
				0,array,4,value.Length*2);
			
			stream.Write(array,0,array.Length);
		}

		public static String DeserializeString(Stream stream)
		{
			byte[] integer = new byte[4];
			stream.Read(integer, 0, 4);
			int len = BitConverter.ToInt32(integer,0);

			// If the length is -1, then we have a null value.
			if (len == -1)
			{
				return null;
			}

			byte[] array = new byte[len];
			stream.Read(array,0,len);
			return System.Text.Encoding.Unicode.GetString(array,0,array.Length);
		}


		public static void Serialize(Stream stream, DateTime value)
		{
			byte[] array = new byte[8];

			Buffer.BlockCopy(BitConverter.GetBytes(value.Ticks),0,array,0,8);
			
			stream.Write(array,0,8);
		}

		public static DateTime DeserializeDateTime(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return new DateTime(BitConverter.ToInt64(buffer,0));
		}
		
		#endregion
	}
}
