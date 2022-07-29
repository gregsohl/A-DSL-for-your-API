using System;
using NUnit.Framework;
using System.IO;
using CompactFormatter;

namespace CompactFormatter.Tests
{
	[Attributes.Serializable()]
	public class SimpleObject
	{
		int number;
		string text;
		double real;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SimpleObject) obj);
		}

		protected bool Equals(SimpleObject other)
		{
			return number == other.number && text == other.text && real.Equals(other.real);
		}

		/// <summary>Serves as the default hash function. </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = number;
				hashCode = (hashCode * 397) ^ (text != null ? text.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ real.GetHashCode();
				return hashCode;
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

	/// <summary>
	/// Summary description for SimpleObjectTest.
	/// </summary>
	[TestFixture]
	public class SimpleObjectTest
	{

		[Test]
		public void SerializeSimpleObject()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			SimpleObject obj = new SimpleObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			SimpleObject obj2 = new SimpleObject();
			obj2 =(SimpleObject)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Real);

			stream.Close();

			Assert.AreEqual(obj, obj2);
		}

		[Test]
		public void SerializeWithPortable()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			SimpleObject obj = new SimpleObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			SimpleObject obj2 = new SimpleObject();
			obj2 =(SimpleObject)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Real);

			stream.Close();

			Assert.AreEqual(obj, obj2);		
		}
	}
}
