using System;
using NUnit.Framework;
using System.IO;
using CompactFormatter;

namespace CompactFormatter.Tests
{
	[Attributes.Serializable()]
	public class ObjectWithNotSerializedMember
	{
		[Attributes.NotSerialized()]
		int number;
		string text;
		double real;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ObjectWithNotSerializedMember) obj);
		}

		protected bool Equals(ObjectWithNotSerializedMember other)
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

		public ObjectWithNotSerializedMember()
		{
			number = 0;
			text = "NONE";
			real = 0.0;
		}

		public ObjectWithNotSerializedMember(int number, string text, double real)
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
	public class NotSerializedTest
	{

		[Test]
		public void NotSerializedObject()
		{
			FileStream stream;
			ObjectWithNotSerializedMember obj;
			using(stream = new FileStream("Prova.bin",FileMode.Create))
			{
				CompactFormatter CFormatter = new CompactFormatter();

				obj = new ObjectWithNotSerializedMember(42, "BELLA RAGA", 3.1415);

				CFormatter.Serialize(stream, obj);
			}

			ObjectWithNotSerializedMember obj2;
			using(stream = new FileStream("Prova.bin",System.IO.FileMode.Open))
			{
				CompactFormatter CFormatter2 = new CompactFormatter();

				obj2 = (ObjectWithNotSerializedMember) CFormatter2.Deserialize(stream);
				Console.WriteLine(obj.Number);

			}

			Assert.AreEqual(0,obj2.Number);
			Assert.AreEqual(42,obj.Number);
		}

		[Test]
		public void NotSerializedPortable()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			ObjectWithNotSerializedMember obj = new ObjectWithNotSerializedMember(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			ObjectWithNotSerializedMember obj2 = new ObjectWithNotSerializedMember();
			obj2 =(ObjectWithNotSerializedMember)CFormatter2.Deserialize(stream);

			stream.Close();

			Assert.AreEqual(0,obj2.Number);
			Assert.AreEqual(42,obj.Number);
		}
	}
}
