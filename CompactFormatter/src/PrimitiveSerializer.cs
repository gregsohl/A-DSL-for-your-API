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
	public class PrimitiveSerializer
	{

		#region Primitive Types for .NET and CompactFormatter
// Used to test time difference between BinaryWriter/Reader usage and ArrayCopy 
// approach
#if BINARY_WRITER
		internal static void Serialize(Single value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.SINGLE);
			new BinaryWriter(stream).Write(value);
		}

		internal static Single DeserializeSingle(Stream stream)
		{
			return new BinaryReader(stream).ReadSingle();
		}
#else
		internal static void Serialize(Single value, Stream stream)
		{
#if DEBUG
			Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
				"Serializing Single type, value "+value);
#endif
			stream.WriteByte((byte)PayloadType.SINGLE);
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		internal static Single DeserializeSingle(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToSingle(buffer,0);
		}
#endif

		/// <summary>
		/// Static method used to serialize a Boolean to the stream.
		/// <remarks>This custom approach is faster than using BitConverter.
		/// </remarks>
		/// </summary>
		/// <param name="value"></param>
		/// <param name="stream"></param>
		internal static void Serialize(Boolean value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.BOOLEAN);
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
		internal static Boolean DeserializeBoolean(Stream stream)
		{
			if(stream.ReadByte() == 1) return true;
			else return false;
		}

		internal static void Serialize(Byte value, Stream stream)
		{		
			stream.WriteByte((byte)PayloadType.BYTE);
			stream.WriteByte(value);			
		}

		internal static Byte DeserializeByte(Stream stream)
		{
			return (Byte)stream.ReadByte();
		}

		internal static void Serialize(Char value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.CHAR);
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		internal static Char DeserializeChar(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToChar(buffer,0);
		}

		internal static void Serialize(Double value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.DOUBLE);
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		internal static Double DeserializeDouble(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToDouble(buffer,0);
		}

		internal static void Serialize(Int16 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.INT16);
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		internal static Int16 DeserializeInt16(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToInt16(buffer,0);
		}

		internal static void Serialize(Int32 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.INT32);
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		internal static Int32 DeserializeInt32(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToInt32(buffer,0);
		}

		internal static void Serialize(Int64 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.INT64);
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		internal static Int64 DeserializeInt64(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToInt64(buffer,0);
		}

		internal static void Serialize(SByte value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.SBYTE);
			stream.WriteByte((byte)value);			
		}

		internal static SByte DeserializeSByte(Stream stream)
		{
			return (SByte)stream.ReadByte();
		}

		internal static void Serialize(UInt16 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.UINT16);
			byte[] buffer = new byte[2];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,2);
			stream.Write(buffer,0,2);
		}

		internal static UInt16 DeserializeUInt16(Stream stream)
		{
			byte[] buffer = new byte[2];
			stream.Read(buffer, 0, 2);
			return BitConverter.ToUInt16(buffer,0);
		}

		internal static void Serialize(UInt32 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.UINT32);
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}

		internal static UInt32 DeserializeUInt32(Stream stream)
		{
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			return BitConverter.ToUInt32(buffer,0);
		}

		internal static void Serialize(UInt64 value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.UINT64);
			byte[] buffer = new byte[8];
			Buffer.BlockCopy(BitConverter.GetBytes(value),0,buffer,0,8);
			stream.Write(buffer,0,8);
		}

		internal static UInt64 DeserializeUInt64(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return BitConverter.ToUInt64(buffer,0);
		}

		#endregion

		#region Primitive Types for CompactFormatter

		internal static void Serialize(Decimal value, Stream stream)
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
			stream.WriteByte((byte)PayloadType.DECIMAL);
			int[] answer = Decimal.GetBits(value);
			byte[] ans = new byte[16];
			Buffer.BlockCopy(answer,0,ans,0,16);
			stream.Write(ans,0,16);
		}

		internal static Decimal DeserializeDecimal(Stream stream)
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
		#if BINARY_WRITER
		internal static void Serialize(String value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.STRING);
			new BinaryWriter(stream).Write(value);
		}

		internal static String DeserializeString(Stream stream)
		{
			return new BinaryReader(stream).ReadString();
		}
		#else
		internal static void Serialize(String value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.STRING);
			byte[] array = new byte[value.Length*2 + 4];

			Buffer.BlockCopy(BitConverter.GetBytes(value.Length*2),0,array,0,4);
			Buffer.BlockCopy(System.Text.Encoding.Unicode.GetBytes(value),
				0,array,4,value.Length*2);
			
			stream.Write(array,0,array.Length);
		}

		internal static String DeserializeString(Stream stream)
		{
			byte[] integer = new byte[4];
			stream.Read(integer, 0, 4);
			int len = BitConverter.ToInt32(integer,0);

			byte[] array = new byte[len];
			stream.Read(array,0,len);
			return System.Text.Encoding.Unicode.GetString(array,0,array.Length);
		}

		#endif

		internal static void Serialize(DateTime value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.DATETIME);
			byte[] array = new byte[8];

			Buffer.BlockCopy(BitConverter.GetBytes(value.Ticks),0,array,0,8);
			
			stream.Write(array,0,8);
		}

		internal static DateTime DeserializeDateTime(Stream stream)
		{
			byte[] buffer = new byte[8];
			stream.Read(buffer, 0, 8);
			return new DateTime(BitConverter.ToInt64(buffer,0));
		}

		public static void Serialize(Guid value, Stream stream)
		{
			stream.WriteByte((byte)PayloadType.GUID);
			byte[] buffer = value.ToByteArray();
			stream.Write(buffer, 0, 16);
		}

		public static Guid DeserializeGuid(Stream stream)
		{
			byte[] buffer = new byte[16];
			stream.Read(buffer, 0, 16);
			return new Guid(buffer);
		}

		#endregion
	}
}
