#region Namespaces

using System;
using System.IO;
using CompactFormatter.Interfaces;
using NUnit.Framework;

#endregion Namespaces

namespace CompactFormatter.Tests
{
	public class CustomSerializationWithOverriderTest
	{
		#region Public Methods

		#region Test Methods

		/// <summary>
		/// Simply serialize and deserialize using an overrider on both
		/// </summary>
		[Test]
		public void SerializeDeserializeObject()
		{
			CompactFormatter cf = new CompactFormatter();
			cf.AddOverrider(typeof(OverriderForCustomSerializableObject));

			FileStream stream = new FileStream("Prova.bin", FileMode.Create);

			CustomSerializableObject ser = new
				CustomSerializableObject(42, 3.1415M, "Ciao Mondo!");

			cf.Serialize(stream, ser);

			stream.Flush();
			stream.Close();


			cf = new CompactFormatter();
			cf.AddOverrider(typeof(OverriderForCustomSerializableObject));

			FileStream stream2 = new FileStream("Prova.bin", FileMode.Open);
			CustomSerializableObject ser2 = (CustomSerializableObject)
				cf.Deserialize(stream2);
			Assert.AreEqual(42, ser2.A);
			Assert.AreEqual(3.1415M, ser2.F);
			Assert.AreEqual("Ciao Mondo!", ser2.S);

			stream2.Close();
		}

		#endregion

		[Test]
		public void SerializeCustomDeserializeOverrider()
		{
			CompactFormatter cf = new CompactFormatter();

			FileStream stream = new FileStream("Prova.bin", FileMode.Create);

			CustomSerializableObject ser = new
				CustomSerializableObject(42, 3.1415M, "Ciao Mondo!");

			cf.Serialize(stream, ser);

			stream.Flush();
			stream.Close();


			cf = new CompactFormatter();
			cf.AddOverrider(typeof(OverriderForCustomSerializableObject2));

			FileStream stream2 = new FileStream("Prova.bin", FileMode.Open);
			CustomSerializableObject ser2 = (CustomSerializableObject)
				cf.Deserialize(stream2);
			Assert.AreEqual(42, ser2.A);
			Assert.AreEqual(3.1415M, ser2.F);
			Assert.AreEqual("Ciao Mondo!", ser2.S);

			stream2.Close();
		}

		#endregion Public Methods
	}

	[Attributes.Overrider(typeof(CustomSerializableObject))]
	internal class OverriderForCustomSerializableObject : IOverrider
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
		public void Serialize(CompactFormatter parent, Stream serializationStream, object graph)
		{
			BinaryWriter bw = new BinaryWriter(serializationStream);
			bw.Write(((CustomSerializableObject)graph).F);
			bw.Write(((CustomSerializableObject)graph).S);
			bw.Write(((CustomSerializableObject)graph).A);
			bw.Flush();
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
			BinaryReader br = new BinaryReader(serializationStream);
			Decimal f = br.ReadDecimal();
			string s = br.ReadString();
			int a = br.ReadInt32();
			return new CustomSerializableObject(a, f, s);
		}
	}

	[Attributes.Overrider(typeof(CustomSerializableObject))]
	internal class OverriderForCustomSerializableObject2 : IOverrider
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
		public void Serialize(CompactFormatter parent, Stream serializationStream, object graph)
		{
			throw new NotSupportedException("No overrider support for serialization here.");
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
			BinaryReader br = new BinaryReader(serializationStream);
			string s = br.ReadString();
			int a = br.ReadInt32();
			Decimal f = br.ReadDecimal();
			return new CustomSerializableObject(a, f, s);
		}
	}

}