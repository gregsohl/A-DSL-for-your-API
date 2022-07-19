using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// Summary description for HashtableTest.
	/// </summary>
	[TestFixture]
	public class GenericListTest
	{
		private int m_Max;

		public void Setup()
		{
			m_Max = 15;
			//OperatingSystem os = Environment.OSVersion;
			Console.WriteLine(Framework.Detect());
		}

		/// <summary>
		/// Test Serializing a generic List
		/// </summary>
		/// <remarks>
		/// GMS 10/1/11 - Generic types are not currently supported by the CompactFormatter
		/// Keep this here for later use
		/// </remarks>
		[Test]
		[Ignore]
		public void SerializeList()
		{
			FileStream stream;
			long start;
			List<string> s = new List<string>();
			using (stream = new FileStream("Prova.bin", FileMode.Create))
			{
				CompactFormatter CFormatter = new CompactFormatter();
				CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

				start = DateTime.Now.Ticks;

				s.Add("aaa");
				s.Add("bbb");
				s.Add("ccc");

				Console.WriteLine(
					"Serializing and Deserializing {0} instances of type {1}",
					m_Max,
					s.GetType()
					);

				CFormatter.Serialize(stream, s);
			}

			List<string> temp;
			using (stream = new FileStream("Prova.bin", FileMode.Open))
			{
				CompactFormatter CFormatter2 = new CompactFormatter();
				CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

				temp = (List<string>) CFormatter2.Deserialize(stream);
			}

			long stop = DateTime.Now.Ticks;
			long ts = stop - start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}", ts, start, stop);

			for (int i = 0; i < temp.Count; i++)
			{
				Assert.AreEqual(temp[i], s[i]);
			}
		}
	}
}
