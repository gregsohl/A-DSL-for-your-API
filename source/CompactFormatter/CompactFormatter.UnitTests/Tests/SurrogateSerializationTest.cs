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
 * $Id: SurrogateSerializationTest.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.IO;
using NUnit.Framework;
using System.Reflection;

namespace CompactFormatter.Tests
{

	public class SimpleObjectSurrogate
	{
		int number;
		string text;
		double real;

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SimpleObjectSurrogate) obj);
		}

		protected bool Equals(SimpleObjectSurrogate other)
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

		public SimpleObjectSurrogate()
		{
			number = 0;
			text = "NONE";
			real = 0.0;
		}

		public SimpleObjectSurrogate(int number, string text, double real)
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
	/// A Test to check Surrogate feature.
	/// </summary>
	[TestFixture]
	public class SurrogateSerializationTest
	{
		[Test]
		[ExpectedException(typeof(Exception.SerializationException))]
		public void SerializationWithoutSurrogate()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			Console.WriteLine(CFormatter.Mode);
			SimpleObjectSurrogate obj = new SimpleObjectSurrogate(42,"BELLA RAGA",3.1415);

			try
			{
				CFormatter.Serialize(stream, obj);
			}
			catch(System.Exception err)
			{
				stream.Close();
				throw err;
			}

			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();

			SimpleObjectSurrogate obj2;
			obj2 =(SimpleObjectSurrogate)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Real);

			stream.Close();

			Assert.AreEqual(obj, obj2);

		}

		[Ignore("Needs to create a surrogate, can't use default")]
		public void SerializationWithSurrogate()
		{
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);
			CompactFormatter CFormatter = new CompactFormatter();
			CFormatter.AddSurrogate(typeof(Surrogate.DefaultSurrogates));

			SimpleObjectSurrogate obj = new SimpleObjectSurrogate(42,"BELLA RAGA",3.1415);

			CFormatter.Serialize(stream, obj);
			stream.Flush();
			stream.Close();

			stream = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CompactFormatter CFormatter2 = new CompactFormatter();
			CFormatter2.AddSurrogate(typeof(Surrogate.DefaultSurrogates));
			
			SimpleObjectSurrogate obj2;
			obj2 =(SimpleObjectSurrogate)CFormatter2.Deserialize(stream);
			Console.WriteLine(obj.Real);

			stream.Close();

			Assert.AreEqual(obj, obj2);
		}
	}
}
