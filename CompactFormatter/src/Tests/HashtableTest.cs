using System;
using System.IO;
using NUnit.Framework;
using System.Collections;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// Summary description for HashtableTest.
	/// </summary>
	[TestFixture]
	public class HashtableTest
	{
		int max;

		public void Setup()
		{
			max = 15;
			//OperatingSystem os = Environment.OSVersion;
			Console.WriteLine(Framework.Detect());
		}


		[Test]
		public void SerializeHash()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

			long start = DateTime.Now.Ticks;

			Hashtable s = new Hashtable();
			s[1]=DateTime.Now;
			s[2] = "Ciao Mondo";
			s[3] = 3.1415;

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
			Hashtable temp = new Hashtable();

			temp = (Hashtable)CFormatter2.Deserialize(stream);
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<temp.Count; i++)
			{
				Assert.AreEqual(temp[i], s[i] );
			}		

		}

		public void SerializePortableHash()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter(CFormatterMode.SAFE);
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

			long start = DateTime.Now.Ticks;

			Hashtable s = new Hashtable();
			s[1]=DateTime.Now;
			s[2] = "Ciao Mondo";
			s[3] = 3.1415;

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);
			
			CFormatter.Serialize(stream, s);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter(CFormatterMode.SAFE);
			CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			Hashtable temp = new Hashtable();

			temp = (Hashtable)CFormatter2.Deserialize(stream);
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<temp.Count; i++)
			{
				Assert.AreEqual(temp[i], s[i] );
			}		

		}
}
}
