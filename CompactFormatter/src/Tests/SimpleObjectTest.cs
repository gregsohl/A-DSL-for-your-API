using System;
using NUnit.Framework;
using System.IO;
using CompactFormatter;

namespace CompactFormatter.Tests
{
	[Attributes.Serializable()]
	public class NotSerializedObject
	{
		[Attributes.NotSerialized()]
		int number;
		string text;
		double real;

		public override bool Equals(object obj)
		{
			if (!obj.GetType().Equals(typeof(NotSerializedObject))) return false;
			else
			{
				NotSerializedObject answer = (NotSerializedObject)obj;
				return (answer.number == number && answer.real == real 
					&& answer.text == text);
			}
		}

		public NotSerializedObject()
		{
			number = 0;
			text = "NONE";
			real = 0.0;
		}

		public NotSerializedObject(int number, string text, double real)
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
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();

			NotSerializedObject obj = new NotSerializedObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			NotSerializedObject obj2 = new NotSerializedObject();
			obj2 =(NotSerializedObject)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Number);

			stream.Close();

			Assert.AreEqual(0,obj2.Number);
			Assert.AreEqual(42,obj.Number);
		}

		[Test]
		public void NotSerializedPortable()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			NotSerializedObject obj = new NotSerializedObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter(CFormatterMode.PORTABLE|CFormatterMode.SAFE);

			NotSerializedObject obj2 = new NotSerializedObject();
			obj2 =(NotSerializedObject)CFormatter2.Deserialize(stream);

			stream.Close();

			Assert.AreEqual(0,obj2.Number);
			Assert.AreEqual(42,obj.Number);
		}
	}
}
