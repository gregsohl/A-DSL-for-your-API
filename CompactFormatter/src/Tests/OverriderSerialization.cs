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
 * $Id: OverriderSerialization.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using CompactFormatter.Interfaces;
using System.IO;
using NUnit.Framework;

namespace CompactFormatter.Tests
{

	[Attributes.Overrider(typeof(ObjectWithOverrider))]
	internal class OverriderClass : IOverrider
	{
		/// <summary>
		/// Serializes an object, or graph of objects with the given root 
		/// to the provided stream.
		/// </summary>
		/// <param name="serializationStream">The stream where the formatter 
		/// puts the serialized data. This stream can reference a variety of 
		/// backing stores (such as files, network, memory, and so on).
		/// </param>
		/// <param name="graph">The object, or root of the object graph, 
		/// to serialize. All child objects of this root object are 
		/// automatically serialized.</param>
		public void Serialize(CompactFormatter parent, Stream serializationStream,object graph)
		{
			BinaryWriter BW = new BinaryWriter(serializationStream);
			BW.Write(((ObjectWithOverrider)graph).A);
			BW.Write(((ObjectWithOverrider)graph).F);
			BW.Write(((ObjectWithOverrider)graph).S);
			BW.Flush();
		}

		/// <summary>
		/// Deserializes the data on the provided stream and reconstitutes the 
		/// graph of objects.
		/// </summary>
		/// <param name="serializationStream">The stream containing the data 
		/// to deserialize.</param>
		/// <returns>The top object of the deserialized graph.</returns>
		public object Deserialize(CompactFormatter parent, Stream serializationStream)
		{
			BinaryReader BR = new BinaryReader(serializationStream);
			return new ObjectWithOverrider(BR.ReadInt32(),BR.ReadSingle(),
				BR.ReadString());
		}
	}

	internal class ObjectWithOverrider
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
		public ObjectWithOverrider()
		{}

		public ObjectWithOverrider(int a, float f, String s)
		{
			this.a = a;
			this.f = f;
			this.s = s;
		}
	}
	/// <summary>
	/// Summary description for OverriderTests.
	/// </summary>
	[TestFixture]
	public class OverriderSerialization
	{
		[Test]
		public void SerializeOneWithOverrider()
		{
		
			CompactFormatter CF = new CompactFormatter();
			CF.AddOverrider(typeof(OverriderClass));
			
			FileStream stream = new FileStream("Prova.bin",System.IO.FileMode.Create);

			ObjectWithOverrider ser = new 
				ObjectWithOverrider(42,3.1415F,"Ciao Mondo!");

			CF.Serialize(stream, ser);

			stream.Flush();
			stream.Close();

			
			CF = new CompactFormatter();
			CF.AddOverrider(typeof(OverriderClass));

			FileStream stream2 = new FileStream("Prova.bin",System.IO.FileMode.Open);
			ObjectWithOverrider ser2 = (ObjectWithOverrider)
				CF.Deserialize(stream2);
			
			Assert.AreEqual(42,ser2.A);
			Assert.AreEqual(3.1415,ser2.F);
			Assert.AreEqual("Ciao Mondo!",ser2.S);

			stream2.Close();

		}
	}
}
