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
 * $Id: StreamParser.cs 1 2004-08-13 18:29:52Z Angelo $
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
	public class StreamParser
	{

		int max;

		[SetUp]
		public void init()
		{
			Console.WriteLine(Framework.Detect());
			max = 15;
		}

		[Test]
		public void TestRegisterNewParser()
		{
			CompactFormatter CFormatter = new CompactFormatter();
			Interfaces.IStreamParser s = new JunkStreamParser();
			Assert.AreEqual(0,CFormatter.RegisteredParsers.Length);
			CFormatter.RegisterStreamParser(s);
			Assert.AreEqual(1,CFormatter.RegisteredParsers.Length);
		}

		[Test]
		public void TestDeregisterNewParser()
		{
			CompactFormatter CFormatter = new CompactFormatter();
			Interfaces.IStreamParser s = new JunkStreamParser();
			Assert.AreEqual(0,CFormatter.RegisteredParsers.Length);
			CFormatter.RegisterStreamParser(s);
			Assert.AreEqual(1,CFormatter.RegisteredParsers.Length);
			CFormatter.DeregisterStreamParser(s);
			Assert.AreEqual(0,CFormatter.RegisteredParsers.Length);
		}

		[Test]
		public void TestBoolean()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			Interfaces.IStreamParser str = new JunkStreamParser();
			CFormatter.RegisterStreamParser(str);

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
			str = new JunkStreamParser();
			CFormatter2.RegisterStreamParser(str);

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



	}
}
