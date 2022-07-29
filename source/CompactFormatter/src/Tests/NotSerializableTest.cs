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
			if (!obj.GetType().Equals(typeof(SimpleObject))) return false;
			else
			{
				SimpleObject answer = (SimpleObject)obj;
				return (answer.number == number && answer.real == real 
					&& answer.text == text);
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
