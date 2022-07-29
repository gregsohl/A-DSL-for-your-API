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
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SelfReferencingObject) obj);
		}

		protected bool Equals(SelfReferencingObject other)
		{
			return inner == other.inner;
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			return inner;
		}
	}

	/// <summary>
	/// Summary description for SelfReferencingTest.
	/// </summary>
	/// <remarks>
	/// GMS 10/1/11 - Causes a stack overflow due to circular reference
	/// </remarks>
	[TestFixture]
	[Ignore]
	public class SelfReferencingTest
	{
		int m_Max;

		[SetUp]
		public void init()
		{
			m_Max = 15;

			//OperatingSystem os = Environment.OSVersion;
			Console.WriteLine(Framework.Detect());
		}

		[Test]
		public void ObjectTest()
		{
			FileStream stream = new FileStream("Prova.bin", FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			SelfReferencingObject[] s = new SelfReferencingObject[m_Max];
			for(int i = 0; i<m_Max; i++)
			{
				s[i] = new SelfReferencingObject(i);
			}

			Console.WriteLine(
				"Serializing and Deserializing {0} instances of type {1}",
				m_Max, s.GetType()
				);

			long start = DateTime.Now.Ticks;

			for(int i = 0; i<m_Max; i++)
			{
				CFormatter.Serialize(stream, s[i]);
			}
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin", FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			
			SelfReferencingObject[] temp = new SelfReferencingObject[m_Max];

			for(int i = 0; i<m_Max; i++)
			{
				temp[i] = (SelfReferencingObject)CFormatter2.Deserialize(stream);
			}
			stream.Close();

			long stop = DateTime.Now.Ticks;
			long ts = stop-start;

			Console.WriteLine("Elapsed Time:{0},{1},{2}",ts,start,stop);

			for(int i = 0; i < m_Max; i++)
			{
				//Console.WriteLine("Deserialized {0}",temp[i]);
				Assert.AreEqual(temp[i], s[i] );
			}		
		}
	}
}
