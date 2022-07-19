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
 * $Id: CustomSerialization.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.IO;
using CompactFormatter.Interfaces;
using CompactFormatter;
using NUnit.Framework;

namespace CompactFormatter.Tests
{
	[Attributes.Serializable(Custom = true)]
	public class CustomSerializableObject : ICSerializable
	{
		int a;
		float f;
		String s;

		public int A
		{
			get
			{
				return a;
			}
		}

		public float F
		{
			get
			{
				return f;
			}
		}

		public String S
		{
			get
			{
				return s;
			}
		}

		/// <summary>
		/// Parameterless constructor, requested by the CompactFormatter.
		/// </summary>
		public CustomSerializableObject()
		{}

		public CustomSerializableObject(int a, float f, String s)
		{
			this.a = a;
			this.f = f;
			this.s = s;
		}

		public void SendObjectData(CompactFormatter parent, System.IO.Stream stream)
		{
			BinaryWriter BW = new BinaryWriter(stream);
			BW.Write(a);
			BW.Write(f);
			BW.Write(s);
			BW.Flush();
		}

		public void ReceiveObjectData(CompactFormatter parent, System.IO.Stream stream)
		{
			BinaryReader BR = new BinaryReader(stream);
			a = BR.ReadInt32();
			f = BR.ReadSingle();
			s = BR.ReadString();
		}

	}
	
	/// <summary>
	/// Summary description for CustomSerialization.
	/// </summary>
	[TestFixture]
	public class CustomSerialization
	{
		[Test]
		public void TestCustomSerialization()
		{
			CompactFormatter CF = new CompactFormatter();
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);

			CustomSerializableObject ser = new 
				CustomSerializableObject(42,3.1415F,"Ciao Mondo!");

			CF.Serialize(stream, ser);

			stream.Flush();
			stream.Close();

			
			CF = new CompactFormatter();
			FileStream stream2 = new FileStream("Prova.bin",System.IO.FileMode.Open);
			CustomSerializableObject ser2 = (CustomSerializableObject)
				CF.Deserialize(stream2);
			Assert.AreEqual(42,ser2.A);
			Assert.AreEqual(3.1415,ser2.F);
			Assert.AreEqual("Ciao Mondo!",ser2.S);

			stream2.Close();
		}
	
	}
}
