using System;
using System.IO;
using NUnit.Framework;

namespace CompactFormatter.Tests
{

	[Attributes.Serializable()]
	public class SelfReferencingObject
	{
		int inner;
		SelfReferencingObject obj;

		public SelfReferencingObject(int i)
		{
			inner = 42;
			obj = this;
		}

		public SelfReferencingObject() : this(42)
		{
		}

		public int Number
		{
			get
			{
				return inner;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj.GetType() != typeof(SelfReferencingObject)) return false;
			SelfReferencingObject o = (SelfReferencingObject)obj;

			return (this.inner == o.Number);
		}
		
		}

	/// <summary>
	/// Summary description for SelfReferencingTest.
	/// </summary>
	[TestFixture]
	public class SelfReferencingTest
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
		public void ObjectTest()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();


			SelfReferencingObject[] s = new SelfReferencingObject[max];
			for(int i = 0; i<max; i++)
			{
				s[i] = new SelfReferencingObject(i);
			}

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				max,s.GetType().ToString()
				);

			long start = DateTime.Now.Ticks;

			for(int i = 0; i<max; i++)
			{
				CFormatter.Serialize(stream, s[i]);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			
			SelfReferencingObject[] temp = new SelfReferencingObject[max];

			for(int i = 0; i<max; i++)
			{
				temp[i] = (SelfReferencingObject)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i<max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s[i] );
			}		
		}
	}
}
