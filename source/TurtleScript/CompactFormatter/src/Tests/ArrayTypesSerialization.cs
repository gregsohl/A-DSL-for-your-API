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
 * $Id: PrimitiveTypes.cs 58 2004-07-08 10:25:24Z RemoteDesktop $
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
	public class ArrayTypes
	{

		int max;

		[SetUp]
		public void init()
		{
			max = 15;

			//OperatingSystem os = Environment.OSVersion;
			Console.WriteLine(Framework.Detect());
		}
		[Test]
		public void TestBoolean()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Boolean[] s = new Boolean[max];

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			s[0] = true;

			for(int i = 1; i<max; i++)
			{
				s[i]=!s[i-1];
			}

			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			Boolean[] temp = new Boolean[max];

			temp = (Boolean[])CFormatter2.Deserialize(stream);
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Boolean s2 = true;
			for(int i = 0; i<max; i++)
			{
				Assert.AreEqual(temp[i], s2 );
				s2 = !s2;
			}
		}

		[Test]
		public void TestByte()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Byte[] s = new Byte[max];
			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			s[0] = 0;
			for(int i = 1; i<max; i++)
			{
				s[i] = ((byte)((s[i-1]+1)%240));
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Byte[] temp = new Byte[max];

			temp = (Byte[])CFormatter2.Deserialize(stream);
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			byte s2 = 0;
			for(int i = 0; i<max; i++)
			{
				Assert.AreEqual(temp[i], s2 );
				s2 = ((byte)((s2+1)%240));
			}		
		}

		[Test]
		public void TestChar()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Char[] s = new Char[max];
			s[0] = 'a';

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s[i-1]);
				s[i] = ((char)((s[i-1]+1)%(32768)));
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Char[] temp = new Char[max];

			temp = (Char[])CFormatter2.Deserialize(stream);

			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Char s2 = 'a';
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("{0}={1}",temp.Length,s2);
				Assert.AreEqual( s2,temp[i] );
				s2 = ((Char)((s2+1)%32768));
			}		
			
		}

		[Test]
		public void TestDecimal()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Decimal[] s = new Decimal[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=s[i-1]+1;
			}
			CFormatter.Serialize(stream, s);

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Decimal[] temp = new Decimal[max];

			//for(int i = 0; i<max; i++)
			//{
			temp = (Decimal[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Decimal s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0},{1}",temp[i],temp.Length);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}		
	
		}


		[Test]
		public void TestSingle()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Single[] s = new Single[max];
			s[0] = 12.7F;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=s[i-1]+1;
			}
			CFormatter.Serialize(stream, s);

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Single[] temp = new Single[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (Single[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Single s2 = 12.7F;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0},{1}",temp[i],temp.Length);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}
		}

		[Test]
		public void TestDouble()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Double[] s = new Double[max];
			s[0] = 0;
			
			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i] = s[i-1]+1;
			}
			CFormatter.Serialize(stream, s);

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Double[] temp = new Double[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (Double[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Double s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0},{1}",temp[i],temp.Length);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}		
		}

		[Test]
		public void TestInt16()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int16[] s = new Int16[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=(short)((s[i-1]+1)%32000);
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int16[] temp = new Int16[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (Int16[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Int16 s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2 = (short)((s2+1)%32000);
			}
		}


		[Test]
		public void TestInt32()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int32[] s = new Int32[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=s[i-1]+1;
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int32[] temp = new Int32[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (Int32[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			int s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}		
		}

		[Test]
		public void TestInt64()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			Int64[] s = new Int64[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i] = s[i-1] + 1;
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			Int64[] temp = new Int64[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (Int64[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Int64 s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}		
		}
		[Test]
		public void TestSByte()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			SByte[] s = new SByte[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);
			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=(SByte)((s[i-1]+1)%250);
			}
			CFormatter.Serialize(stream, s);

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			SByte[] temp = new SByte[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (SByte[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			SByte s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2=(SByte)((s2+1)%250);
			}				
		}
		[Test]
		public void TestUInt16()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			UInt16[] s = new UInt16[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i]=(ushort)((s[i-1]+1)%32000);
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt16[] temp = new UInt16[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (UInt16[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			UInt16 s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2=(ushort)((s2+1)%32000);
			}		
		}

		[Test]
		public void TestUInt32()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;
			
			UInt32[] s = new UInt32[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i] = s[i-1] + 1;
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt32[] temp = new UInt32[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (UInt32[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			UInt32 s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}
		}

		[Test]
		public void TestUInt64()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			UInt64[] s = new UInt64[max];
			s[0] = 0;

			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s[i] = s[i-1] + 1;
			}
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			UInt64[] temp = new UInt64[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (UInt64[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			UInt64 s2 = 0;
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s2 );
				s2++;
			}				
		}
		[Test]
		public void TestString()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			long start = DateTime.Now.Ticks;

			int s = 0;
			String[] s1 = new String[max];
			s1[0] = s.ToString();
			
			Console.WriteLine(
				"Serializing and Deserializing an array of type {1} composed by {0} elements",
				max,s1.GetType().ToString()
				);

			for(int i = 1; i<max; i++)
			{
				//Console.WriteLine("Serialized {0}",s);
				s++;
				s1[i] = s.ToString();
			}
			CFormatter.Serialize(stream, s1);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			String[] temp = new String[max];

			//for(int i = 0; i<max; i++)
			//{
				temp = (String[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			s = 0;
			String s12 = s.ToString();
			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}:{1}",temp[i],temp.Length);
				Assert.AreEqual(temp[i], s12 );
				s++;
				s12 = s.ToString();
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
				s = DateTime.Now;
			}
			CFormatter.Serialize(stream, temp);

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			DateTime[] temp2 = new DateTime[max];

			//for(int i = 0; i<max; i++)
			//{
				temp2 = (DateTime[])CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}:{1}",temp2[i],temp2.Length);
				Assert.AreEqual(temp[i], temp2[i] );
			}		
		
		}


		[Attributes.Serializable()]
		public class SimpleObject
		{
			int number;
			string text;
			double real;

			public override bool Equals(object obj)
			{
				if (!obj.GetType().Equals(typeof(SimpleObject))) return false;
				else
				{
					SimpleObject answer = (SimpleObject)obj;
					return (answer.number == number && answer.real == real 
						&& answer.text == text);
				}
			}

			public SimpleObject()
			{
				number = 0;
				text = "NONE";
				real = 0.0;
			}

			public SimpleObject(int number, string text, double real)
			{
				this.number = number;
				this.text = text;
				this.real = real;
			}

			public int Number
			{
				get
				{
					return number;
				}
			}
			
			public string Text
			{
				get
				{
					return text;
				}
			}

			public double Real
			{
				get
				{
					return real;
				}
			}
		}

		[Test]
		public void TestObject()
		{
			int max = 42;
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			SimpleObject[] obj = new SimpleObject[max];
			for(int i = 0; i<max; i++)
				obj[i] = new SimpleObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			SimpleObject[] obj2 =(SimpleObject[])CFormatter2.Deserialize(stream);

			stream.Close();

			Assert.AreEqual(obj, obj2);
		}
	}
}
