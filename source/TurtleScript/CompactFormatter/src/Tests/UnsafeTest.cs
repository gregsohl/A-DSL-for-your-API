using System;
using NUnit.Framework;
using System.IO;
using CompactFormatter;

namespace CompactFormatter.Tests
{
	
	public class UnsafeObject
	{

		int number;
		string text;
		double real;

		public override bool Equals(object obj)
		{
			if (!obj.GetType().Equals(typeof(UnsafeObject))) return false;
			else
			{
				UnsafeObject answer = (UnsafeObject)obj;
				return (answer.number == number && answer.real == real 
					&& answer.text == text);
			}
		}

		public UnsafeObject()
		{
			number = 0;
			text = "NONE";
			real = 0.0;
		}

		public UnsafeObject(int number, string text, double real)
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
	public class UnsafeTest
	{

		[Test]
		public void SerializationTest()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter(CFormatterMode.NONE);

			UnsafeObject obj = new UnsafeObject(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter(CFormatterMode.NONE);

			UnsafeObject obj2 = new UnsafeObject();
			obj2 =(UnsafeObject)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Number);

			stream.Close();

			Assert.AreEqual(obj, obj2);
		}

	}
}
