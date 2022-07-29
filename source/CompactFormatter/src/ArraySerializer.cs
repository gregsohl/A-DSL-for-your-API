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
 * $Id: ArraySerializer.cs 7 2004-08-21 10:47:16Z Angelo $
 * */
#endregion

using System;
using System.IO;

namespace CompactFormatter
{
	/// <summary>
	/// A set of static methods used to serialize array classes.
	/// </summary>
	public class ArraySerializer
	{
		/// <summary>
		/// Method used to serialize an array of bytes on a stream.
		/// </summary>
		/// <param name="serializationStream">The stream where serialize array</param>
		/// <param name="array">The array</param>
		internal static void SerializeArrayBytes( 
			byte[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFBYTE);
			// Writing array length as Integer
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(array.Length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of bytes
			serializationStream.Write(array,0,array.Length);
		}

		internal static void SerializeArrayBoolean(
			bool[] array, Stream serializationStream)
		{
			/// TODO: Check if it quicker with bytes instead of Int32
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFBOOLEAN);
			int length = array.Length;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of booleans			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static void SerializeArrayChar(
			char[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFCHAR);
			int length = array.Length*2;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static char[] DeserializeArrayChar(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Char[] answer = new Char[length/2];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static bool[] DeserializeArrayBoolean(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Boolean[] answer = new Boolean[length];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static byte[] DeserializeArrayByte(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			byte[] answer = new byte[length];
			serializationStream.Read(answer,0,length);
			return answer;
		}

		internal static void SerializeArrayDecimal(
			decimal[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFDECIMAL);
			int length = array.Length*16;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of decimals		
			byte[] ans = new byte[length];
			for(int i = 0; i<array.Length; i++)
			{
				int[] answer = Decimal.GetBits(array[i]);
				Buffer.BlockCopy(answer,0,ans,i*16,array.Length);
			}
			serializationStream.Write(ans,0,length);
		}

		internal static decimal[] DeserializeArrayDecimal(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			decimal[] answer = new decimal[length/16];
			byte[] ans = new byte[length];
			serializationStream.Read(ans,0,length);
			for(int i = 0; i<length/16; i++)
			{
				int[] array = new int[4];
				Buffer.BlockCopy(ans,i*16,array,0,16);
				answer[i] = new Decimal(array);
			
			}
			return answer;
		}

		internal static void SerializeArraySingle(
			Single[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFSINGLE);
			int length = array.Length*4;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static Single[] DeserializeArraySingle(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Single[] answer = new Single[length/4];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayDouble(
			Double[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFDOUBLE);
			int length = array.Length*8;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static Double[] DeserializeArrayDouble(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Double[] answer = new Double[length/8];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayShort(
			Int16[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFINT16);
			int length = array.Length*2;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static Int16[] DeserializeArrayShort(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Int16[] answer = new Int16[length/2];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayInteger(
			Int32[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFINT32);
			int length = array.Length*4;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static Int32[] DeserializeArrayInteger(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Int32[] answer = new Int32[length/4];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayLong(
			Int64[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFINT64);
			int length = array.Length*8;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static Int64[] DeserializeArrayLong(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Int64[] answer = new Int64[length/8];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}
		internal static void SerializeArraySByte(
			SByte[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFSBYTE);
			int length = array.Length;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static SByte[] DeserializeArraySByte(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			sbyte[] answer = new sbyte[length];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayUInt16(
			UInt16[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFUINT16);
			int length = array.Length*2;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static UInt16[] DeserializeArrayUInt16(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			UInt16[] answer = new UInt16[length/2];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayUInt32(
			UInt32[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFUINT32);
			int length = array.Length*4;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static UInt32[] DeserializeArrayUInt32(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			UInt32[] answer = new UInt32[length/4];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayUInt64(
			UInt64[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFUINT64);
			int length = array.Length*8;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars			
			byte[] temp = new byte[length];
			Buffer.BlockCopy(array,0,temp,0,length);
			serializationStream.Write(temp,0,length);
		}

		internal static UInt64[] DeserializeArrayUInt64(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			UInt64[] answer = new UInt64[length/8];
			byte[] temp = new byte[length];
			serializationStream.Read(temp,0,length);
			Buffer.BlockCopy(temp, 0, answer, 0, length);
			return answer;
		}

		internal static void SerializeArrayString(
			String[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFSTRING);
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(array.Length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			for(int i = 0; i<array.Length; i++)
			{
				String temp = array[i];
				// Use a convention to encode null references
				if (temp==null)
					temp = "!";
				else if (temp.StartsWith("!"))
					temp = "!" + temp;

				byte[] buf = new byte[temp.Length*2 + 4];

				Buffer.BlockCopy(BitConverter.GetBytes(temp.Length*2),0,buf,0,4);
				Buffer.BlockCopy(System.Text.Encoding.Unicode.GetBytes(temp),
					0,buf,4,temp.Length*2);
				serializationStream.Write(buf,0,buf.Length);
			}
		}

		internal static String[] DeserializeArrayString(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in items.
			String[] answer = new String[length];
			for(int i = 0; i<length; i++)
			{
				byte[] integer = new byte[4];
				serializationStream.Read(integer, 0, 4);
				int len = BitConverter.ToInt32(integer,0);

				byte[] str = new byte[len];
				serializationStream.Read(str,0,len);
				answer[i] = System.Text.Encoding.Unicode.GetString(str,0,len);
				// Use a convention to decode null references
				if (answer[i].StartsWith("!")) 
				{
					if (answer[i].Length==1) answer[i] = null;
					else answer[i] = answer[i].Substring(1);
				}

			}	
			return answer;
		}
		internal static void SerializeArrayDateTime(
			DateTime[] array, Stream serializationStream)
		{
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFDATETIME);
			int length = array.Length*8;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars
			byte[] ans = new byte[length];
			for(int i = 0; i<array.Length; i++)
			{
				Buffer.BlockCopy(BitConverter.GetBytes(array[i].Ticks),0,ans,
					i*8,8);
			}
			serializationStream.Write(ans,0,length);
		}

		internal static DateTime[] DeserializeArrayDateTime(
			Stream serializationStream)
		{
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			DateTime[] answer = new DateTime[length/8];
			for(int i=0; i<length/8;i++)
			{
				byte[] buf = new byte[8];
				serializationStream.Read(buf, 0, 8);
				answer[i] = new DateTime(BitConverter.ToInt64(buf,0));
			}
			return answer;
		}
	}
}
