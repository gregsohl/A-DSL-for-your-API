// #define OBJECT_TABLE
#define COMPACT

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
 * $Id: CompactFormatter.cs 14 2004-08-26 09:08:59Z Angelo $
 * */
#endregion

#region Changes
/*
 * Changes by Greg Sohl, StoneRiver, Inc.
 *
 * 07/16/06 - Modified to use ClassInspector class for more cached attribute lookups
 * 07/16/06 - Added #if OBJECT_TABLE in all the places the object table collection is utilized
 * 07/16/06 - Modified innerSerialize method for better performance
 * 07/16/06 - Modified several methods to only call GetType once, remember and reuse the result
 * 10/03/11 - Modified to allow an Overrider to always take precedence over custom serialization.
 * 10/04/11 - Replaced GetConstructor/Invoke and Activator.CreateInstance with Dynamic Methods
 *
*/
#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;

using CompactFormatter.Interfaces;
using CompactFormatter.Exception;

namespace CompactFormatter
{
	[Flags]
	public enum CFormatterMode {
		/*
		 * This mode is used to instruct CompactFormatter in comparing not just Assembly names
		 * when doing deserialization but comparing the fully qualified name.
		 * */
		EXACTASSEMBLY = 4,
		/*
		 * Safe mode is used by CompactFormatter when trying to avoid possible 
		 * errors:
		 * It consists in enabling both PORTABLE and SURROGATES.
		 * */
		SAFE = 3, 
		/*
		 * In Portable mode, CompactFormatter will serialize also field labels.
		 * This increase size of data stream but guarantees also that no fields 
		 * are uncorrectly assigned during deserialization phase even when 
		 * serializing/deserializing between different frameworks.
		 * */
		PORTABLE = 2, 
		/*
		 * CompactFormatter will always try to serialize automatically primitive 
		 * types and user-defined types marked with a Serializable attribute.
		 * Surrogate Mode drives CompactFormatter behaviour when serializing runtime
		 * types: In Surrogate Mode CompactFormatter won't serialize any runtime 
		 * type which doesn't have an explicit surrogate or overrider, otherwise, it will try
		 * to serialize it anyway.
		 * */
		SURROGATE = 1,
		/*
		 * This mode disable both SURROGATE and PORTABLE, it's the faster method
		 * and request less memory space (you don't have to explicitly load 
		 * surrogates if automatic serialization routines are enough) but it's also
		 * extremely unstable.
		 * If you want to serialize a particular set of types in your application
		 * and you've already tested that those types are correctly serialized by 
		 * NONE Mode you can use it, but doesn't use it if you don't know exactly 
		 * what kinds of objects you are going to serialize.
		 * */
		NONE = 0
	}

	public enum PayloadType {
		// Null reference
		NULL,
		// Primitive Types
		BOOLEAN,BYTE,CHAR,DECIMAL,SINGLE,DOUBLE,INT16,INT32,
		INT64,SBYTE,UINT16,UINT32,UINT64,DATETIME,STRING,
		// Arrays of Primitive values
		ARRAYOFBOOLEAN,ARRAYOFBYTE,ARRAYOFCHAR,ARRAYOFDECIMAL,ARRAYOFSINGLE,
		ARRAYOFDOUBLE,ARRAYOFINT16,ARRAYOFINT32,ARRAYOFINT64,ARRAYOFSBYTE,
		ARRAYOFUINT16,ARRAYOFUINT32,ARRAYOFUINT64,ARRAYOFDATETIME,ARRAYOFSTRING,
		ARRAYOFOBJECTS,
		// Objects (runtime or not)
		OBJECT, 
		// Object which requires custom serialization (are self-serializing)
		CUSTOM,
		// Objects which requested explicit OverrideSerialization.
		OVERRIDESERIALIZATION,
		// Objects which requested explicit SurrogateSerialization.
		SURROGATESERIALIZATION,
		// Assembly Metadata.
		ASSEMBLY,
		// Type Metadata.
		TYPE,
		// Object which was already sent.
		ALREADYSENT,
		// Enum type.
		ENUM,
		// Additional Primative Types
		GUID
	};

	/// <summary>
	/// CompactFormatter.
	/// </summary>
	public class CompactFormatter : ICFormatter
	{

		/// <summary>
		/// The mode in which CompactFormatter is running.
		/// It is set during object construction (if object is used to serialize)
		/// or when receiving from a stream (if object is used to deserialize).
		/// This is because who serialize decides which mode will be used during
		/// the transfer.
		/// </summary>
		private CFormatterMode mode;

		private CFormatterMode remoteMode;
		/// <summary>
		/// The Framework version on the other side of the serialization stream;
		/// It should be ignored when this object is serializing, in fact who 
		/// serialize data doesn't know who will receive it (consider, for example
		/// storing data in a filesystem)
		/// </summary>
		private FrameworkVersion remoteVersion;

		/// <summary>
		/// The Framework on which CompactFormatter is running.
		/// It's the first thing sent over the wire when starting serialization
		/// of an object.
		/// </summary>
		private FrameworkVersion localVersion;

		#region Type Hashcodes

		private static int System_Int32_Hash =		typeof(System.Int32).GetHashCode();
		private static int System_String_Hash =		typeof(System.String).GetHashCode();
		private static int System_Boolean_Hash =	typeof(System.Boolean).GetHashCode();
		private static int System_SByte_Hash =		typeof(System.SByte).GetHashCode();
		private static int System_Byte_Hash =		typeof(System.Byte).GetHashCode();
		private static int System_Char_Hash =		typeof(System.Char).GetHashCode();
		private static int System_Int16_Hash =		typeof(System.Int16).GetHashCode();
		private static int System_UInt16_Hash =		typeof(System.UInt16).GetHashCode();
		private static int System_UInt32_Hash =		typeof(System.UInt32).GetHashCode();
		private static int System_Int64_Hash =		typeof(System.Int64).GetHashCode();
		private static int System_UInt64_Hash =		typeof(System.UInt64).GetHashCode();
		private static int System_Single_Hash =		typeof(System.Single).GetHashCode();
		private static int System_Double_Hash =		typeof(System.Double).GetHashCode();
		private static int System_Decimal_Hash =	typeof(System.Decimal).GetHashCode();
		private static int System_DateTime_Hash =	typeof(System.DateTime).GetHashCode();
		private static int System_Guid_Hash =		typeof(System.Guid).GetHashCode();
 
		#endregion Type Hashcodes


		/// <summary>
		/// The mode in which CompactFormatter is running.
		/// It is set during object construction (if object is used to serialize)
		/// or when receiving from a stream (if object is used to deserialize).
		/// This is because who serialize decides which mode will be used during
		/// the transfer.
		/// </summary>
		public CFormatterMode Mode
		{
			get
			{
				return mode;
			}
		}

		private IStreamParser[] registeredParsers;

		public IStreamParser[] RegisteredParsers
		{
			get
			{
				return registeredParsers;
			}
		}

		/// <summary>
		/// RegisterStreamParser is used to register a new StreamParser object
		/// to CompactFormatter.
		/// These objects are used to transform data stream before sending it on
		/// the wire or after receiving it from the wire,
		/// The list of StreamParsers used by CompactFormatter are stored in a simple
		/// array, this means that, when we add a new StreamParser we must create
		/// a bigger array and copy the content of old one to the newly create.
		/// This means that registering a new StreamParser is slow but, occupy less
		/// memory space and is more efficient to read (since it's a simple array
		/// and not a Collection object).
		/// The overhead in registering is not a problem since usually only one
		/// StreamParser should be registered and this is done once, before starting
		/// serialization.
		/// </summary>
		/// <param name="parser">The IStreamParser object to register.</param>
		public void RegisterStreamParser(Interfaces.IStreamParser parser)
		{
			IStreamParser[] temp = new IStreamParser[registeredParsers.Length+1];
			Array.Copy(registeredParsers,0,temp,0,registeredParsers.Length);
			temp[registeredParsers.Length] = parser;
			registeredParsers = temp;
		}

		/// <summary>
		/// DeregisterStreamParser is used to remove a StreamParser object from
		/// CompactFormatter.
		/// As its twin RegisterStreamParser it uses simple arrays and so it's 
		/// unefficient, but, as its twin, this function is rarely used and repaid
		/// by the gain in efficiency at runtime.
		/// </summary>
		/// <param name="parser">The IStreamParser object to deregister.</param>
		public void DeregisterStreamParser(Interfaces.IStreamParser parser)
		{
			int index = -1;
			for(int i = 0; i < registeredParsers.Length; i++)
			{
				if (registeredParsers[i].Equals(parser))
				{
					index = i;
					break;
				}
			}
			if (index != -1)
			{
				IStreamParser[] temp = new IStreamParser[registeredParsers.Length-1];
				Array.Copy(registeredParsers,0,temp,0,index);
				Array.Copy(registeredParsers,index+1,temp,index,
					registeredParsers.Length-index-1);
				registeredParsers = temp;			
		}
		}

		/// <summary>
		/// Used to flush away tables.
		/// This is useful to make room when tables are grown too much and
		/// when several streams are used from the same serializer.
		/// </summary>
		internal void Reset()
		{
			AssemblyList.Clear();
			OverriderTable.Clear();
			SurrogateTable.Clear();
			SerializedTypesList.Clear();
			
			#if OBJECT_TABLE
			SerializedItemsList.Clear();
			#endif
			
			ClassInspector.Clear();
		}

		public CompactFormatter() : this(CFormatterMode.SURROGATE)
		{
		}

		public CompactFormatter(CFormatterMode mode)
		{
			this.mode = mode;
			localVersion = Framework.Detect();
			
			// Usually two StreamParsers are already too much...
			registeredParsers = new Interfaces.IStreamParser[0];
			
			// Define Lists
			AssemblyList = new ArrayList();
			SurrogateTable = new Hashtable();
			OverriderTable = new Hashtable();
			
			SerializedTypesList = new ArrayList(10);

			#if OBJECT_TABLE
			SerializedItemsList = new ObjectTable(10);
			#endif
			
			// For sure i need to add Assembly mscorlib
			AssemblyList.Add(Assembly.Load("mscorlib"));
		}

		private void innerSerialize(System.IO.Stream serializationStream, object graph)
		{
			#if DEBUG
			if (graph!=null)
				Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,"Serializing "+graph.GetType()+"...");
			else 
				Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,"Serializing NULL instance...");
			#endif

			// If object is null serialize it
			if (graph == null) 
			{
				serializationStream.WriteByte((byte)PayloadType.NULL);
				return;
			}

			Type t = graph.GetType();

			// If object is of primitive type simply serialize it.
			int typeHash = t.GetHashCode();

			if (t.IsPrimitive || 
				(typeHash == System_String_Hash) ||
				(typeHash == System_DateTime_Hash) ||
				(typeHash == System_Decimal_Hash) ||
				(typeHash == System_Guid_Hash))
			{
				#region Serialization of Primitive Types
				if (typeHash == System_Int32_Hash)
				// case "System.Int32":
					{
						PrimitiveSerializer.Serialize(
							(int)graph,serializationStream);
						return;
					}
				else if (typeHash == System_String_Hash)
				// case "System.String":
					{
						PrimitiveSerializer.Serialize(
							(string)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Boolean_Hash)
				// case "System.Boolean":
					{
						PrimitiveSerializer.Serialize(
							(bool)graph,serializationStream);
						return;
					}
				else if (typeHash == System_SByte_Hash)
				// case "System.SByte":
					{
						PrimitiveSerializer.Serialize(
							(sbyte)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Byte_Hash)
				// case "System.Byte":
					{
						PrimitiveSerializer.Serialize(
							(byte)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Char_Hash)
				// case "System.Char":
					{
						PrimitiveSerializer.Serialize(
							(char)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Int16_Hash)
				// case "System.Int16":
					{
						PrimitiveSerializer.Serialize(
							(short)graph,serializationStream);
						return;
					}
				else if (typeHash == System_UInt16_Hash)
				// case "System.UInt16":
					{
						PrimitiveSerializer.Serialize(
							(ushort)graph,serializationStream);
						return;
					}
				else if (typeHash == System_UInt32_Hash)
				// case "System.UInt32":
					{
						PrimitiveSerializer.Serialize(
							(uint)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Int64_Hash)
				// case "System.Int64":
					{
						PrimitiveSerializer.Serialize(
							(long)graph,serializationStream);
						return;
					}
				else if (typeHash == System_UInt64_Hash)
				// case "System.UInt64":
					{
						PrimitiveSerializer.Serialize(
							(ulong)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Single_Hash)
				// case "System.Single":
					{
						PrimitiveSerializer.Serialize(
							(float)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Double_Hash)
				// case "System.Double":
					{
						PrimitiveSerializer.Serialize(
							(double)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Decimal_Hash)
				// case "System.Decimal": 
					{
						PrimitiveSerializer.Serialize(
							(decimal)graph,serializationStream);
						return;
					}
				else if (typeHash == System_DateTime_Hash)
				// case "System.DateTime":
					{
						PrimitiveSerializer.Serialize(
							(DateTime)graph,serializationStream);
						return;
					}
				else if (typeHash == System_Guid_Hash)
				// case "System.Guid":
					{
						PrimitiveSerializer.Serialize(
							(Guid)graph, serializationStream);
						return;
					}

				#endregion
			}

			#region Check inside the ObjectTable
			#if OBJECT_TABLE
			if (SerializedItemsList.Contains(graph))
			{
				serializationStream.WriteByte((byte)PayloadType.ALREADYSENT);
				innerSerialize(serializationStream,
					SerializedItemsList.IndexOf(graph));
				return;
			}
			#endif
			#endregion

			// If it's an array of objects
			if (t.IsArray)
			{
				#region Serialization of Arrays of primitive types
				switch(t.GetElementType().ToString())
				{
					case "System.Byte":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a byte array of "+((byte[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayBytes((byte[])graph,
							serializationStream);
						return;
					}
					case "System.Boolean":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a bool array of "+((bool[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayBoolean((bool[])graph,
							serializationStream);
						return;
					}
					case "System.Char":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a char array of "+((char[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayChar((char[])graph,
							serializationStream);

						return;
					}
					case "System.Decimal":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a decimal array of "+((decimal[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayDecimal((decimal[])graph,
							serializationStream);
						return;
					}
					case "System.Single":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a single array of "+((Single[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArraySingle((Single[])graph,
							serializationStream);
						return;
					}
					case "System.Double":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a double array of "+((Double[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayDouble((Double[])graph,
							serializationStream);
						return;
					}
					case "System.Int16":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a short array of "+((Int16[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayShort((Int16[])graph,
							serializationStream);
						return;
					}
					case "System.Int32":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a short array of "+((Int32[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayInteger((Int32[])graph,
							serializationStream);
						return;
					}
					case "System.Int64":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a short array of "+((Int64[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayLong((Int64[])graph,
							serializationStream);
						return;
					}
					case "System.SByte":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a sbyte array of "+((SByte[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArraySByte((SByte[])graph,
							serializationStream);
						return;
					}
					case "System.UInt16":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a UInt array of "+((UInt16[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayUInt16((UInt16[])graph,
							serializationStream);
						return;
					}
					case "System.UInt32":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a UInt array of "+((UInt32[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayUInt32((UInt32[])graph,
							serializationStream);
						return;
					}
					case "System.UInt64":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a UInt array of "+((UInt64[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayUInt64((UInt64[])graph,
							serializationStream);
						return;
					}
					case "System.String":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a String array of "+((String[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayString((String[])graph,
							serializationStream);
						return;
					}
					case "System.DateTime":
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing a DateTime array of "+((DateTime[])graph).Length+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						ArraySerializer.SerializeArrayDateTime((DateTime[])graph,
							serializationStream);
						return;
					}
					default:
					{
						#if DEBUG
						Util.About.debug.WriteDebug(Util.DebugLevel.VERBOSE,
							"Serializing an Object array of "+((Array)graph).Length+
							//"Serializing an Object array of "+((System.Array)graph).ToString()+
							" elements");						
						#endif
						#if OBJECT_TABLE
						SerializedItemsList.Add(graph);
						#endif
						this.SerializeArrayObjects((Array)graph,
							serializationStream);
						return;
					}
				}
				#endregion
			}

			if (t.IsEnum)
			{
				SerializeEnum(graph,serializationStream);
				return;
			}

			// The object is not marked with Serializable attribute,
			// we must check if we've an overrider or a surrogate.

			if (SerializeWithOverrider(serializationStream, graph, t))
			{
				return;
			}

			if (ClassInspector.IsSerializable(t))
			{
				if (ClassInspector.IsCustomSerializable(t))
				{
					#if OBJECT_TABLE
					SerializedItemsList.Add(graph);
					#endif

					#region Serialization of Custom Serializable Types
					// It could raise a ClassCastException if object doesn't
					// implement ICSerializable, but since it is marked with
					// Custom Serializable attribute this is ok.
					// TODO: Raise a more meaningful exception.
					this.SerializeCustom((ICSerializable)graph,serializationStream);
					return;
					#endregion
				}

				#if OBJECT_TABLE
				SerializedItemsList.Add(graph);
				#endif
				
				SerializeObject(serializationStream,graph);
				return;
			}

			// If not let's try with a surrogate
			MethodInfo surrogate = (MethodInfo)SurrogateTable[t];
			if(surrogate != null)
			{
				// We've a surrogate registered for it!
				// Check if Type was already sent
				//int index = SerializedTypesList.IndexOf(graph.GetType());

				//if (index == -1)
				//{
				// I need to serialize the type!
				//	index = WriteTypeMetadata(serializationStream,
				//		graph.GetType());
				//index = AssemblyList.Add(payload.GetType().Assembly);
				//}

				// At this point Assembly and the type are already sent, 
				// i just have to send the index.
				//serializationStream.WriteByte((byte)PayloadType.OBJECT);
				//serializationStream.WriteByte((byte)index);

				#if OBJECT_TABLE
				SerializedItemsList.Add(graph);
				#endif

				SerializeObject(serializationStream,graph);
				return;
			}
				
			// Class is not marked with Serializable attribute
			// nor we have a surrogate or an overrider registered
			// for the class so we have to look are mode:
			// If it's SURROGATE i've to raise an exception, otherwise
			// i go ahead and try serializing it anyway.
			if ((mode & CFormatterMode.SURROGATE) != 0 && t.IsPublic)
				// TODO: Add a meaningful message/parameter
				// TODO: If t is not public currently CompactFormatter tries to serialize it anyway, this is probably the best thing to do but actually it's useful
				throw new Exception.SerializationException("Unable to serialize "+t+" type, it's not marked with Serializable attribute and no surrogate or overriders are registered for it");
			
			// Try to serialize it anyway
			#if OBJECT_TABLE
			SerializedItemsList.Add(graph);
			#endif

			SerializeObject(serializationStream,graph);
			return;
		}

		private bool SerializeWithOverrider(Stream serializationStream, object graph, Type t)
		{
			// First let's check if we've a Overrider who can handle it.
			IOverrider overrider = (IOverrider) OverriderTable[t];
			if (overrider != null)
			{
				// We've an overrider registered for it!
				// Check if Type was already sent
				int index = SerializedTypesList.IndexOf(t);

				if (index == -1)
				{
					// I need to serialize the type!
					index = WriteTypeMetadata(serializationStream,
					                          t);
					//index = AssemblyList.Add(payload.GetType().Assembly);
				}

				#if DEBUG
				Util.About.debug.WriteDebug(Util.DebugLevel.INFO,
				                            "Serializing a not serializable object using overrider " +
				                            overrider.GetType().Name);
				#endif

				// At this point Assembly and the type are already sent, 
				// i just have to send the index.
				serializationStream.WriteByte((byte) PayloadType.OBJECT);
				serializationStream.WriteByte((byte) index);

				#if OBJECT_TABLE
				SerializedItemsList.Add(graph);
				#endif

				overrider.Serialize(this, serializationStream, graph);
				return true;
			}

			return false;
		}

		private object innerDeserialize(System.IO.Stream serializationStream)
		{
			PayloadType objType = (PayloadType)serializationStream.ReadByte();

			#if DEBUG
			Util.About.debug.WriteDebug(Util.DebugLevel.INFO,
				"Object to deserialize is "+objType);
			#endif

			switch(objType)
			{
				#region Deserialization of null reference
				case(PayloadType.NULL):
					return null;
				#endregion

				#region Deserialization of AlreadySent objects
				
				#if OBJECT_TABLE
				case(PayloadType.ALREADYSENT):
				{
					int i = (int)innerDeserialize(serializationStream);
					return SerializedItemsList[i];
				}
				#endif

				#endregion

				#region Deserialization of Primitive Types
				case (PayloadType.BOOLEAN):
					return PrimitiveSerializer.
						DeserializeBoolean(serializationStream);
				case (PayloadType.BYTE):
					return PrimitiveSerializer.
						DeserializeByte(serializationStream);
				case (PayloadType.CHAR):
					return PrimitiveSerializer.
						DeserializeChar(serializationStream);
				case (PayloadType.DATETIME):
					return PrimitiveSerializer.
						DeserializeDateTime(serializationStream);
				case (PayloadType.DECIMAL):
					return PrimitiveSerializer.
						DeserializeDecimal(serializationStream);
				case (PayloadType.DOUBLE):
					return PrimitiveSerializer.
						DeserializeDouble(serializationStream);
				case (PayloadType.INT16):
					return PrimitiveSerializer.
						DeserializeInt16(serializationStream);
				case (PayloadType.INT32):
					return PrimitiveSerializer.
						DeserializeInt32(serializationStream);
				case (PayloadType.INT64):
					return PrimitiveSerializer.
						DeserializeInt64(serializationStream);
				case (PayloadType.SBYTE):
					return PrimitiveSerializer.
						DeserializeSByte(serializationStream);
				case (PayloadType.SINGLE):
					return PrimitiveSerializer.
						DeserializeSingle(serializationStream);
				case (PayloadType.STRING):
					return PrimitiveSerializer.
						DeserializeString(serializationStream);
				case (PayloadType.UINT16):
					return PrimitiveSerializer.
						DeserializeUInt16(serializationStream);
				case (PayloadType.UINT32):
					return PrimitiveSerializer.
						DeserializeUInt32(serializationStream);
				case (PayloadType.UINT64):
					return PrimitiveSerializer.
						DeserializeUInt64(serializationStream);
				case (PayloadType.GUID):
					return PrimitiveSerializer.
						DeserializeGuid(serializationStream);
				#endregion
				#region Deserialization of CustomSerializable Types
				case (PayloadType.CUSTOM):
				{
					object answer = DeserializeCustom(serializationStream);
					return answer;
				}
				#endregion
				#region Deserialization of Assembly
				case (PayloadType.ASSEMBLY):
				{
					//This is tricky: I can't return an Assembly object because
					//this info simply means that this assembly should be stored
					//in assembly list table, so, after reading this one, let's call
					//again innerDeserialize and return it's answer.
					ReadAssemblyMetadata(serializationStream);
					return innerDeserialize(serializationStream);
				}
				#endregion
				#region Deserialization of Type
				case(PayloadType.TYPE):
				{
					ReadTypeMetadata(serializationStream);
					return innerDeserialize(serializationStream);
				}
				#endregion

				#region Deserialization of Object
				case(PayloadType.OBJECT):
				{
					object answer = DeserializeObject(serializationStream);	
					return answer;
				}
				#endregion
				#region Deserialization of Arrays
				case(PayloadType.ARRAYOFBYTE):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayByte(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case(PayloadType.ARRAYOFBOOLEAN):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayBoolean(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case(PayloadType.ARRAYOFCHAR):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayChar(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case(PayloadType.ARRAYOFDECIMAL):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayDecimal(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFSINGLE):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArraySingle(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFDOUBLE):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayDouble(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFINT16):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayShort(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFINT32):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayInteger(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFINT64):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayLong(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFSBYTE):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArraySByte(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFUINT16):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayUInt16(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFUINT32):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayUInt32(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFUINT64):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayUInt64(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFSTRING):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayString(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFDATETIME):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = ArraySerializer.DeserializeArrayDateTime(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				case (PayloadType.ARRAYOFOBJECTS):
				{
					#if OBJECT_TABLE
					int ph = SerializedItemsList.AddPlaceholder();
					#endif
					object answer = this.DeserializeArrayObject(serializationStream);
					#if OBJECT_TABLE
					SerializedItemsList[ph] = answer;
					#endif
					return answer;
				}
				#endregion
				#region Deserialization of Enums
				case (PayloadType.ENUM):
				{
					object answer = DeserializeEnum(serializationStream);	
					return answer;
				}

				#endregion
			}
			return null;
		}


		#region ICFormatter Members

		/// <summary>
		/// This method is called by external users to serialize an object on 
		/// the stream.
		/// First of all it serialize the header and then call innerSerialize 
		/// methods.
		/// Before returning from this function, the serializer must empty all 
		/// tables.
		/// </summary>
		/// <param name="serializationStream"></param>
		/// <param name="graph"></param>
		public void Serialize(System.IO.Stream serializationStream, object graph)
		{
			serializationStream.WriteByte((byte)localVersion);
			serializationStream.WriteByte((byte)mode);

			System.IO.Stream stream = serializationStream;
			for(int i = 0; i < registeredParsers.Length; i++)
			{
				registeredParsers[i].InnerStream = stream;
				stream = registeredParsers[i];
			}

			innerSerialize(stream,graph);

			// stream.Flush();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="serializationStream"></param>
		/// <returns></returns>
		public object Deserialize(System.IO.Stream serializationStream)
		{
			// TODO: Add a check for compatibility in mode (they need to have the same mode!)
			remoteVersion = (FrameworkVersion)serializationStream.ReadByte();
			remoteMode = (CFormatterMode)serializationStream.ReadByte();
			
			System.IO.Stream stream = serializationStream;
			for(int i = registeredParsers.Length-1; i >= 0; i--)
			{
				registeredParsers[i].InnerStream = stream;
				stream = registeredParsers[i];
			}
			return innerDeserialize(stream);
		}

		#endregion

		#region Serialization Tables
		/// <summary>
		/// An ArrayList containing all previously serialized assemblies.
		/// NOTICE: We don't serialize assemblies, just their metadata!
		/// </summary>
		ArrayList AssemblyList;

		/// <summary>
		/// An hashtable containing all currently registered surrogate.
		/// </summary>
		Hashtable SurrogateTable;

		/// <summary>
		/// An hashtable containing all currently registered overrider.
		/// </summary>
		Hashtable OverriderTable;

		/// <summary>
		/// An arraylist containing all previously serialized types.
		/// TODO: All is a bit heavy... Maybe it's better to use a LRU policy
		/// </summary>
		ArrayList SerializedTypesList;

		#if OBJECT_TABLE
		/// <summary>
		/// A Queue containing all previously serialized variables. Used to avoid recursion through an object graph.
		/// </summary>
		ObjectTable SerializedItemsList;
		#endif

		#endregion

		#region Table of Overriders

		public void AddOverrider(Type overrider)
		{
			// First of all check if it's an overrider (if it's marked with 
			// OverriderAttribute)
			Attributes.OverriderAttribute attribute = null;
			if (overrider.GetCustomAttributes(typeof(Attributes.OverriderAttribute
				),false).Length!=0)
			{
				// If it is, i need to register it.
				attribute = (Attributes.OverriderAttribute)overrider.
					GetCustomAttributes(typeof(Attributes.OverriderAttribute),
					false)[0];
				// Let's add not the type, but an instance of it
				// INFO: This requires Overrider to have parameterless constructor
				// INFO: obviously
				Object formatter = Activator.CreateInstance(overrider);
				OverriderTable.Add(attribute.CustomSerializer,formatter);
			}
			else throw new RegisterOverriderException(overrider);
		}

		#endregion

		#region Table of Surrogates

		public void AddSurrogate(Type surrogate)
		{
			foreach(MethodInfo m in surrogate.GetMethods())
			{
				// First of all check if it's a surrogate (if it's marked with 
				// SurrogateAttribute)
				Attributes.SurrogateAttribute attribute = null;			
				if (m.GetCustomAttributes(typeof(Attributes.SurrogateAttribute
					),false).Length!=0)
				{
					foreach(Attribute att in m.GetCustomAttributes(typeof(Attributes.SurrogateAttribute
						),false))
					{
						// If it is, i need to register it.
						attribute = (Attributes.SurrogateAttribute)att;
						SurrogateTable.Add(attribute.SurrogateOf,m);
					}
				}
				//else throw new RegisterSurrogateException(m);
			}
		}

		#endregion

		#region Type Serialization

		internal int WriteTypeMetadata(System.IO.Stream stream, Type type)
		{
			// Check if assembly was already sent
			int index = AssemblyList.IndexOf(type.Assembly);

			if (index == -1)
			{
				// I need to serialize the assembly!
				index = WriteAssemblyMetadata(stream,type.Assembly);
			}

			// Now the assembly has been sent.
			stream.WriteByte((byte)PayloadType.TYPE);
			stream.WriteByte((byte)index);

			//The index has been sent, now i need to send class FullName
			String classname = type.FullName;
			byte[] array = new byte[classname.Length*2 + 4];

			Buffer.BlockCopy(BitConverter.GetBytes(classname.Length*2),0,array,0,4);
			Buffer.BlockCopy(System.Text.Encoding.Unicode.GetBytes(classname),
				0,array,4,classname.Length*2);
			
			stream.Write(array,0,array.Length);

			// Add Type to type list
			return SerializedTypesList.Add(type);

		}

		public void ReadTypeMetadata(System.IO.Stream serializationStream)
		{
			int assembly = serializationStream.ReadByte();

			byte[] integer = new byte[4];
			serializationStream.Read(integer, 0, 4);
			int len = BitConverter.ToInt32(integer,0);

			byte[] array = new byte[len];
			serializationStream.Read(array,0,len);
			String typename = System.Text.Encoding.Unicode.GetString(
				array,0,array.Length);
#if DEBUG
			Util.About.debug.WriteDebug(Util.DebugLevel.INFO,
				"Requested to load a type:"+typename);
#endif
			
			try
			{
				Type type = ((Assembly)AssemblyList[assembly]).GetType(typename);
				SerializedTypesList.Add(type);
			}
			catch(System.Exception)
			{
				// TODO:Probably it's better to catch all exceptions here
				throw new TypeSerializationException(
					"Unable to load type "+typename);
			}			
		}

		#endregion

		#region Assembly Serialization

		public void ReadAssemblyMetadata(System.IO.Stream serializationStream)
		{
			byte[] integer = new byte[4];
			serializationStream.Read(integer, 0, 4);
			int len = BitConverter.ToInt32(integer,0);

			byte[] array = new byte[len];
			serializationStream.Read(array,0,len);
			String assembly = System.Text.Encoding.Unicode.GetString(
				array,0,array.Length);
#if DEBUG
			Util.About.debug.WriteDebug(Util.DebugLevel.INFO,
				"Requested to load an assembly:"+assembly);
#endif
			try
			{
				if((Mode & CFormatterMode.EXACTASSEMBLY) != 0)
					AssemblyList.Add(Assembly.Load(assembly));
				//else AssemblyList.Add(Assembly.LoadWithPartialName(assembly));
				//HACK: This is not good but there's no LoadWithPartialName in CF
				else AssemblyList.Add(Assembly.Load(assembly));
			}
			catch(System.IO.FileNotFoundException err)
			{
				Console.WriteLine(err);
				// TODO:Probably it's better to catch all exceptions here
				throw new AssemblySerializationException(
					"Unable to load assembly "+assembly+" file not found!");
			}
		}

		public int WriteAssemblyMetadata(System.IO.Stream stream, 
			Assembly assembly)
		{
#if DEBUG
			if (AssemblyList.Contains(assembly))
				throw new AssertionException(
				"Assembly already contained in AssemblyList, item was already sent!");
			Util.About.debug.WriteDebug(Util.DebugLevel.INFO,
				"Writing assembly metadata for assembly "+assembly.FullName);			
#endif
			String name;
			if((assembly.GetName().Name.StartsWith("System")) ||
				(this.Mode & CFormatterMode.EXACTASSEMBLY) != 0)
			{
				name = assembly.FullName;
			}
			else
			{
				name = assembly.GetName().Name;
			}
			
			int position = AssemblyList.Add(assembly);
			stream.WriteByte((byte)PayloadType.ASSEMBLY);
			byte[] array = new byte[name.Length*2 + 4];

			Buffer.BlockCopy(BitConverter.GetBytes(name.Length*2),0,array,0,4);
			Buffer.BlockCopy(System.Text.Encoding.Unicode.GetBytes(name),
				0,array,4,name.Length*2);
			
			stream.Write(array,0,array.Length);
			return position;
		}
		#endregion

		#region Custom Serialization

		internal void SerializeCustom(ICSerializable payload, System.IO.Stream stream)
		{
			// Check if Type was already sent
			int index = SerializedTypesList.IndexOf(payload.GetType());

			if (index == -1)
			{
				// I need to serialize the type!
				index = WriteTypeMetadata(stream,payload.GetType());
			}

			// At this point Assembly and the type are already sent, 
			// i just have to send the index.
			stream.WriteByte((byte)PayloadType.CUSTOM);
			stream.WriteByte((byte)index);

			// Now i must pass the control to Custom Serialization mechanism
			payload.SendObjectData(this, stream);
		}


		internal object DeserializeCustom(System.IO.Stream stream)
		{
			// First of all i need to read the byte representing the Type
			Type type = (Type)SerializedTypesList[stream.ReadByte()];

			// Attempt to deserialize from an Overrider. 
			object deserializeObject;
			if (DeserializeFromOverrider(stream, type, out deserializeObject))
			{
				return deserializeObject;
			}

		    ICSerializable obj;
            if (type.IsValueType)
            {
                obj = (ICSerializable)Activator.CreateInstance(type);
            }
            else
            {
				// Now i've to instantiate the object. Get the ConstructorInfo
				// to give a useful error message.
				//ConstructorInfo constructorInfo = type.GetConstructor(new Type[] { });
				
				//if (constructorInfo == null)
				//{
				//    throw new MissingMemberException(
				//        "The following type does not have an parameterless constructor: " + type);
				//}
				//// TODO: Check if this approach is not too limited, in fact
				//// it request public access to the constructor.
				//obj = (ICSerializable)constructorInfo.Invoke(new object[] { });

            	InstantiateHandler dynamicConstructor = ClassInspector.GetDynamicConstructor(type);
				if (dynamicConstructor == null)
				{
					throw new MissingMemberException(
						"The following type does not have an parameterless constructor: " + type);
				}
				obj = (ICSerializable)dynamicConstructor();
            }

			#if OBJECT_TABLE
			SerializedItemsList.Add(obj);
			#endif

			//Now the object has been instantiated, it's time to invoke custom
			//method
			obj.ReceiveObjectData(this, stream);
			return obj;
		}

		#endregion

		#region Object Serialization
		internal Object DeserializeObject(System.IO.Stream stream)
		{
			// First of all i need to read the byte representing the Type
			Type t = (Type)SerializedTypesList[stream.ReadByte()];

			// Attempt to deserialize from an Overrider. 
			object deserializeObject;
			if (DeserializeFromOverrider(stream, t, out deserializeObject))
			{
				return deserializeObject;
			}

			// The first thing to check is if the type is marked as serializable:
			if (ClassInspector.IsSerializable(t))
			{
				// It is marked, obviously it can't request Overriders or
				// Surrogates because otherwise the PayloadType couldn't be OBJECT

				// Deserialize it automatically
				// Object obj = Activator.CreateInstance(t);
				InstantiateHandler dynamicConstructor = ClassInspector.GetDynamicConstructor(t);
				object obj = dynamicConstructor();

#if OBJECT_TABLE
				SerializedItemsList.Add(obj);
#endif

				return populateObject(stream, obj);
			}
	
			// It's not marked with Serializable attribute!

			// If not let's try with a surrogate
			MethodInfo surrogate = (MethodInfo)SurrogateTable[t];
			if (surrogate != null)
			{
				// We've a surrogate registered for it!
				// INFO: Surrogates MUST BE static methods!
				Object[] param = { t };
				Object answer = surrogate.Invoke(null, param);
#if OBJECT_TABLE
				SerializedItemsList.Add(answer);
#endif
				return populateObject(stream, answer);
			}
			
			
			// There is no surrogate, it is not marked with Serializable
			// and it has no overrider.
			// If it's in unsafe mode i've to try to deserialize it anyway
			if ((mode & CFormatterMode.SURROGATE) == 0 || !t.IsPublic)
			{
				// We're not in SAFE mode, i have to deserialize it anyway.
				// Deserialize it automatically
				Object obj = Activator.CreateInstance(t);
				return populateObject(stream, obj);

			}
			
			throw new Exception.SerializationException("Unable to deserialize " + t.Name + " instances: it lacks Serializable attribute, overrider or surrogate. Try running CFormatter in UNSAFE mode");
		}

		private bool DeserializeFromOverrider(Stream stream, Type t, out object deserializeObject)
		{
			// First of all check if we've a Overrider who can handle it.
			IOverrider overrider = (IOverrider) OverriderTable[t];
			if (overrider != null)
			{
				// We've an overrider registered for it!

				// Invoke overrider services
				#if OBJECT_TABLE
				int ph = SerializedItemsList.AddPlaceholder();
				#endif

				Object obj = overrider.Deserialize(this, stream);

				#if OBJECT_TABLE
				SerializedItemsList[ph] = obj;
				#endif

				deserializeObject = obj;
				return true;
			}

			deserializeObject = null;
			return false;
		}

		/// <summary>
		/// This method is called wherever an object, marked with serializable
		/// attribute, is serialized.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="obj"></param>
		internal void SerializeObject(System.IO.Stream stream, Object obj)
		{
			Type t = obj.GetType();

			// Check if Type was already sent
			int index = SerializedTypesList.IndexOf(t);

			if (index == -1)
			{
				// I need to serialize the type!
				index = WriteTypeMetadata(stream, t);
				//index = AssemblyList.Add(payload.GetType().Assembly);
			}

			// At this point Assembly and the type are already sent, 
			// i just have to send the index.
			stream.WriteByte((byte)PayloadType.OBJECT);
			stream.WriteByte((byte)index);

			if ((mode & CFormatterMode.PORTABLE) != 0)
			{
				// CFormatter is in portable mode, i need to declare number of
				// fields and name for each field (Is GetHashCode on name enough?).
				FieldInfo[] list = ClassInspector.InspectClass(t);
				innerSerialize(stream,list.Length);
				for(int i = 0; i < list.Length; i++)
				{
					// Here the field value is set.
					innerSerialize(stream, list[i].Name);
					innerSerialize(stream, list[i].GetValue(obj));
				}
			}
			else
			{
				// CFormatter is not in portable mode, i need just to send fields
				// in order.
				FieldInfo[] list = ClassInspector.InspectClass(t);
				for(int i = 0; i < list.Length; i++)
				{
					// Here the field value is set.
					object fieldValue = list[i].GetValue(obj);
					innerSerialize(stream, fieldValue);
				}

			}

		}

		#endregion

		/// <summary>
		/// This inner method is used during deserialization phase to populate
		/// a previously instantiated object (through Activator or a surrogate)
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="graph">The object instantiated but still uninitialized</param>
		/// <returns>
		/// graph object with all fields correctly set
		/// </returns>
		private Object populateObject(System.IO.Stream stream, Object graph)
		{
			if ((mode & CFormatterMode.PORTABLE) != 0)
			{
				// CFormatter is in portable mode, i need to declare number of
				// fields and name for each field (Is GetHashCode on name enough?).
				FieldInfo[] list = ClassInspector.InspectClass(graph.GetType());
				ArrayList a = new ArrayList(list);
				int length = (int)innerDeserialize(stream);
				for(int i = 0; i < length; i++)
				{
					// Here the field value is set.
					String name = (String)innerDeserialize(stream);
					for(int j = 0; j < a.Count; j++)
					{
						if (((FieldInfo)a[j]).Name.Equals(name))
						{
							list[i].SetValue(graph,innerDeserialize(stream));
							a.RemoveAt(j);
							break;
						}
					}					
				}
				return graph;
			}
			else
			{
				// CFormatter is not in portable mode, i need just to receive fields
				// in order.
				FieldInfo[] list = ClassInspector.InspectClass(graph.GetType());
				for(int i = 0; i < list.Length; i++)
				{
					// Here the field value is set.
					object fieldValue = innerDeserialize(stream);
					list[i].SetValue(graph,fieldValue);
				}
				return graph;
			}
		}

		private Object DeserializeEnum(System.IO.Stream stream)
		{
			// First of all i need to read the byte representing the Type
			Type t = (Type)SerializedTypesList[stream.ReadByte()];
			byte[] buffer = new byte[4];
			stream.Read(buffer, 0, 4);
			long l = (long) BitConverter.ToInt32(buffer,0);			
			return Enum.ToObject(t,l);

		}

		private void SerializeEnum(Object value, System.IO.Stream stream)
		{
			// Check if Type was already sent
			int index = SerializedTypesList.IndexOf(value.GetType());

			if (index == -1)
			{
				// I need to serialize the type!
				index = WriteTypeMetadata(stream,value.GetType());
				//index = AssemblyList.Add(payload.GetType().Assembly);
			}

			// At this point Assembly and the type are already sent, 
			// i just have to send the index.
			stream.WriteByte((byte)PayloadType.ENUM);
			stream.WriteByte((byte)index);
			
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes((int)value),0,buffer,0,4);
			stream.Write(buffer,0,4);
		}


		private void SerializeArrayObjects(
			Array array, System.IO.Stream serializationStream)
		{
			// TODO: i need to serialize the type declared for the array
			// Check if Type was already sent
			int index = SerializedTypesList.IndexOf(array.GetType().GetElementType());

			if (index == -1)
			{
				// I need to serialize the type!
				index = WriteTypeMetadata(serializationStream,array.GetType().GetElementType());
				//index = AssemblyList.Add(payload.GetType().Assembly);
			}

			// At this point Assembly and the type are already sent, 
			// i just have to send the index.			
			serializationStream.WriteByte((byte)PayloadType.ARRAYOFOBJECTS);
			serializationStream.WriteByte((byte)index);

			int length = array.Length;
			// Writing array length as Integer (in bytes)
			byte[] buffer = new byte[4];
			Buffer.BlockCopy(BitConverter.GetBytes(length),0,buffer,0,4);
			serializationStream.Write(buffer,0,4);
			//Writing sequence of chars
			for(int i = 0; i<array.Length; i++)
			{
				this.innerSerialize(serializationStream,array.GetValue(i));
			}
		}

		private Array DeserializeArrayObject(
			System.IO.Stream serializationStream)
		{
			// TODO: i need to read the type declared for the array
			Type t = (Type)SerializedTypesList[serializationStream.ReadByte()];			
			// First of all let's read the size of the array
			byte[] buffer = new byte[4];
			serializationStream.Read(buffer, 0, 4);
			int length = BitConverter.ToInt32(buffer,0);
			// Now we've the size in bytes.
			Array answer = Array.CreateInstance(t,length);
			for(int i=0; i<length;i++)
			{
				answer.SetValue(innerDeserialize(serializationStream),i);
			}
			return answer;
		}

	}
}
