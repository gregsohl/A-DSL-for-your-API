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
 * $Id: FrameworkClassesSerialization.cs 12 2004-08-24 16:49:21Z Angelo $
 * */
#endregion


using System;
using System.IO;
using NUnit.Framework;
using System.Collections;
using System.Data;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// A set of NUnit tests used to check correct framework types serialization.
	/// 
	/// </summary>
	[TestFixture]
	public class FrameworkTypes
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
		public void TestArrayList()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

			long start = DateTime.Now.Ticks;

			ArrayList s = new ArrayList();
			s.Add(DateTime.Now);
			s.Add("Ciao Mondo");
			s.Add(3.1415);

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);
			
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			ArrayList temp = new ArrayList();

			temp = (ArrayList)CFormatter2.Deserialize(stream);
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<temp.Count; i++)
			{
				Assert.AreEqual(temp[i], s[i] );
			}		
		}

		[Test]
		public void TestGhostDataTable()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			CFormatter.AddOverrider(typeof(Surrogate.GhostDataTableOverrider));

			long start = DateTime.Now.Ticks;

			DataTable s = new DataTable();
			s.Columns.Add("Col1");
			s.Columns.Add("Col2");
			s.Rows.Add(new Object[] {13,14});

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			//for(int i = 0; i<max; i++)
			//{
			CFormatter.Serialize(stream, s);
			//}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			CFormatter2.AddOverrider(typeof(Surrogate.GhostDataTableOverrider));
			CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

			DataTable temp;
			//for(int i = 0; i<max; i++)
			//{
			temp = (DataTable)CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			Assert.AreEqual(13,Int32.Parse((String)s.Rows[0]["Col1"]));		
			Assert.AreEqual(temp.Rows.Count, s.Rows.Count);

		}

		[Test]
		public void TestDataSet()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			Surrogate.GhostDataTableOverrider over = new Surrogate.GhostDataTableOverrider();
			CFormatter.AddOverrider(typeof(Surrogate.DataSetOverrider));
			CFormatter.AddOverrider(typeof(Surrogate.GhostDataTableOverrider));

			/*long start = DateTime.Now.Ticks;

			DataSet s = new DataSet();
			s.ReadXml("dataset.xml");

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			//for(int i = 0; i<max; i++)
			//{
			CFormatter.Serialize(stream, s);
			//}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			CFormatter2.AddOverrider(typeof(Surrogate.GhostDataTableOverrider));
			CFormatter2.AddOverrider(typeof(Surrogate.DataSetOverrider));
			Console.WriteLine("Deserializing...");

			DataSet temp;
			//for(int i = 0; i<max; i++)
			//{
			temp = (DataSet)CFormatter2.Deserialize(stream);
			//}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			temp.WriteXml("prova.xml");
			Assert.AreEqual(s,temp);*/
		}



	}
}
