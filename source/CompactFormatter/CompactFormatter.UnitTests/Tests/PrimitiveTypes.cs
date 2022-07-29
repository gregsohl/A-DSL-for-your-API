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
 * $Id: PrimitiveTypes.cs 7 2004-08-21 10:47:16Z Angelo $
 * */
#endregion


using System;
using System.IO;
using NUnit.Framework;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// A set of NUnit tests used to check correct primitive types serialization.
	/// 
	/// </summary>
	[TestFixture]
	public class PrimitiveTypes
	{

		int max;

		[SetUp]
		public void init()
		{
			max = 15;
			
			//OperatingSystem os = Environment.OSVersion;
			Console.WriteLine(Framework.Detect());
		}

		#region Primitive Types for .NET & CompactFormatter
		[Test]
		public void TestBoolean()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Boolean s = true;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s = !s;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			Boolean[] temp = new Boolean[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Boolean)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = true;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s = !s;
			}		
		}

		[Test]
		public void TestByte()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Byte s = 0;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s = ((byte)((s+1)%240));
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Byte[] temp = new Byte[max];
			for(int i = 0; i<max; i++)
			{
				temp[i] = (Byte)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s = ((byte)((s+1)%240));
			}		

		}

		[Test]
		public void TestChar()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Char s = 'a';

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s = ((char)((s+1)%(32768)));
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Char[] temp = new Char[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Char)CFormatter2.Deserialize(stream);;
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 'a';
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s = ((Char)((s+1)%32768));
			}		
			
		}

		[Test]
		public void TestDouble()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Double s = 0;
			
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Double[] temp = new Double[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Double)CFormatter2.Deserialize(stream);;
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}		

		}

		[Test]
		public void TestInt16()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int16 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s=(short)((s+1)%32000);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int16[] temp = new Int16[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Int16)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s=(short)((s+1)%32000);
			}
		}

		[Test]
		public void TestInt32()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int32 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int32[] temp = new Int32[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Int32)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}		
		}

		[Test]
		public void TestInt64()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int64 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int64[] temp = new Int64[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Int64)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}		
		}

		[Test]
		public void TestUInt16()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			UInt16 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s=(ushort)((s+1)%32000);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt16[] temp = new UInt16[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (UInt16)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s=(ushort)((s+1)%32000);
			}		

		}
		[Test]
		public void TestUInt32()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;
			
			UInt32 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt32[] temp = new UInt32[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (UInt32)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}		

		}

		[Test]
		public void TestUInt64()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			UInt64 s = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt64[] temp = new UInt64[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (UInt64)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}				
		}

		[Test]
		public void TestSByte()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			SByte s = 0;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);
			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s=(SByte)((s+1)%250);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			SByte[] temp = new SByte[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (SByte)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s=(SByte)((s+1)%250);
			}		
		
		}

		[Test]
		public void TestSingle()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			int max = 15;
			Single s = 12.7F;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);
			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Single[] temp = new Single[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Single)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 12.7F;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}
		}

		#endregion
		
		#region Primitive Types for CompactFormatter
		[Test]
		public void TestDecimal()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Decimal s = 0;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s++;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Decimal[] temp = new Decimal[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (Decimal)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s++;
			}		
	
		}
		
		[Test]
		public void TestDateTime()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			DateTime s = DateTime.Now;
			// Considering that i can't recreate correct time without saving it
			// i use here an array to store serialized data.
			DateTime[] temp = new DateTime[max];

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				temp[i] = s;
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s = DateTime.Now;
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			DateTime[] temp2 = new DateTime[max];

			for(int i = 0; i<max; i++)
			{
				temp2[i] = (DateTime)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp2[i]);
				Assert.AreEqual(temp[i], temp2[i] );
			}		
		
		}
		[Test]
		public void TestString()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			int s = 0;
			String s1 = s.ToString();
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s1.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s1);
				//Console.WriteLine("Serialized {0}",s);
				s++;
				s1 = s.ToString();
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			String[] temp = new String[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (String)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			s1 = s.ToString();
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s1 );
				s++;
				s1 = s.ToString();
			}		
		}
		#endregion

		#region Enum Serialization

		public enum DummyEnum {FIRST, SECOND, THIRD};
		public void TestEnumSerialization()
		{
			max = 1;
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			DummyEnum s = DummyEnum.SECOND;
			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s);
				//Console.WriteLine("Serialized {0}",s);
				s = (DummyEnum)((int)(s+1)%3);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			DummyEnum[] temp = new DummyEnum[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (DummyEnum)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = DummyEnum.SECOND;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s );
				s = (DummyEnum)((int)(s+1)%3);
			}		
		}
		#endregion
	}
}
