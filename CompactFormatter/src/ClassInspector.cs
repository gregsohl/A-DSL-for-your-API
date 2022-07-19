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
 * $Id: ClassInspector.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

#region Changes
/*
 * Changes by Greg Sohl, Fiserv, Inc.
 *
 * 7/16/06 - Modified InspectClass method to fix caching 
 * 7/16/06 - Added IsSerializable method to encapsulate this functionality and support caching
 * 7/16/06 - Added IsCustomSerializable method to encapsulate this functionality and support caching
 * 10/04/11 - Major rewrite, encapsulating all cached data into a class and adding a dynamic method for default constructor cache
*/
#endregion

#region Namespaces

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;

using CompactFormatter.Attributes;
using SerializableAttribute = CompactFormatter.Attributes.SerializableAttribute;

#endregion Namespaces

namespace CompactFormatter
{
	/// <summary>
	/// Delegate to invoke create methods.
	/// </summary>
	public delegate object InstantiateHandler();

	/// <summary>
	/// Class Inspector is the class responsible to extract fields from 
	/// a given type.
	/// The define symbol CACHE_ENABLED is used to activate field cache table.
	/// For each type serialized a copy of its field list is cached, therefore
	/// eliminating delay caused by reflection.
	/// </summary>
	public static class ClassInspector
	{
		#region Private Fields

		/// Straight Lock(object) locking mechanism chosen as only Write locks
		/// are going to occur. Lock is faster than other mechanisms when used primarily
		/// for this purpose.
		private static readonly object SyncRoot = new object();

		/// Hashtable chosen here for their internal spinlock capability, allowing
		/// us to do read/write simultaneously and it handles it internally.
		/// TypeCacheCollection contains a cache of information about Types that is used
		/// by the CompactFormatter to provide serialization/deserialziation services.
		private static readonly Hashtable TypeCacheCollection = new Hashtable();

		#endregion Private Fields

		#region Public Methods

		/// <summary>
		/// Clears this cache
		/// </summary>
		public static void Clear()
		{
			lock (SyncRoot)
			{
				TypeCacheCollection.Clear();
			}
		}

		/// <summary>
		/// Retrieves the FieldInfo list for the specified type
		/// </summary>
		/// <param name="type">The data type to retrieve the field list for.</param>
		/// <returns>
		///   Array of <see cref="System.Reflection.FieldInfo"/>
		/// </returns>
		public static FieldInfo[] InspectClass(Type type)
		{
			return GetType(type).FieldInfo;
		}

		/// <summary>
		/// Determines whether the specified type is serializable.
		/// </summary>
		/// <param name="type">The type to test for serializability.</param>
		/// <returns>
		///   <c>true</c> if the specified type is serializable; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSerializable(Type type)
		{
			return GetType(type).IsSerializable;
		}

		/// <summary>
		/// Determines whether the specified type is custom serializable.
		/// </summary>
		/// <param name="type">The type to test for custom serializability.</param>
		/// <returns>
		///   <c>true</c> if [the specified type is custom serializable; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsCustomSerializable(Type type)
		{
			return GetType(type).IsCustomSerializable;
		}

		/// <summary>
		/// Gets a dynamic method to the parameterless constructor for the specified <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The data type to get the dynamic constructor for</param>
		/// <returns>
		///   An <see cref="InstantiateHandler"/> type delegate to the parameterless constructor.
		/// </returns>
		public static InstantiateHandler GetDynamicConstructor(Type type)
		{
			return GetType(type).DynamicConstructor;
		}

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Retrieves a TypeCache object for the specified <paramref name="type"/>. Gets it from the 
		/// TypeCacheCollection or creates it and adds it to the collection.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <see cref="TypeCache"/>
		/// </returns>
		private static TypeCache GetType(Type type)
		{
			TypeCache typeCache = (TypeCache) TypeCacheCollection[type];

			if (typeCache == null)
			{
				typeCache = new TypeCache(type);

				lock(SyncRoot)
				{
					TypeCacheCollection[type] = typeCache;
				}
			}

			return typeCache;
		}

		#endregion Private Methods

		#region Private Types

		private class TypeCache
		{
			#region Constructor

			public TypeCache(Type type)
			{
				PopulateFieldInfo(type);
				PopulateSerializable(type);
				PopulateDynamicConstructor(type);
			}

			#endregion Constructor

			#region Public Properties

			/// <summary>
			/// Gets the field info.
			/// </summary>
			public FieldInfo[] FieldInfo
			{
				[DebuggerStepThrough]
				get { return m_FieldInfo; }
			}

			/// <summary>
			/// Gets a value indicating whether this instance is serializable.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this instance is serializable; otherwise, <c>false</c>.
			/// </value>
			public bool IsSerializable
			{
				[DebuggerStepThrough]
				get { return m_IsSerializable; }
			}

			/// <summary>
			/// Gets a value indicating whether this instance is custom serializable.
			/// </summary>
			/// <value>
			/// 	<c>true</c> if this instance is custom serializable; otherwise, <c>false</c>.
			/// </value>
			public bool IsCustomSerializable
			{
				[DebuggerStepThrough]
				get { return m_IsCustomSerializable; }
			}

			/// <summary>
			/// Gets a dynamic method to the parameterless constructor for this type
			/// </summary>
			public InstantiateHandler DynamicConstructor
			{
				[DebuggerStepThrough]
				get { return m_DynamicConstructor; }
			}

			#endregion Public Properties

			#region Private Constants

			private static readonly Type COMPACTFORMATTER_NOT_SERIALIZABLE_ATTRIBUTE_TYPE = typeof (NotSerializedAttribute);
			private static readonly Type COMPACTFORMATTER_SERIALIZABLE_ATTRIBUTE_TYPE = typeof (SerializableAttribute);

			#endregion Private Constants

			#region Private Fields

			private FieldInfo[] m_FieldInfo;
			private bool m_IsSerializable;
			private bool m_IsCustomSerializable;
			private InstantiateHandler m_DynamicConstructor;

			#endregion Private Fields

			#region Private Methods

			private void PopulateFieldInfo(Type type)
			{
				FieldInfo[] fieldInfo =
					type.GetFields(
						BindingFlags.Public | 
						BindingFlags.NonPublic | 
						BindingFlags.Instance | 
						BindingFlags.DeclaredOnly);

				ArrayList list = new ArrayList(fieldInfo);

				for (int i = 0; i < fieldInfo.Length; i++)
				{
					// Remove fields marked with the NotSerialized attribute
					if (fieldInfo[i].GetCustomAttributes(
						COMPACTFORMATTER_NOT_SERIALIZABLE_ATTRIBUTE_TYPE, false).Length != 0)
						list.Remove(fieldInfo[i]);
				}

				m_FieldInfo = (FieldInfo[])list.ToArray(typeof(FieldInfo));
			}

			private void PopulateSerializable(Type type)
			{
				object[] serializableAttribute = 
					type.GetCustomAttributes(COMPACTFORMATTER_SERIALIZABLE_ATTRIBUTE_TYPE, false);

				if (serializableAttribute.Length != 0)
				{
					SerializableAttribute attribute = (SerializableAttribute)serializableAttribute[0];

					m_IsSerializable = true;
					m_IsCustomSerializable = attribute.Custom;
				}
			}

			private void PopulateDynamicConstructor(Type type)
			{
				m_DynamicConstructor = CreateInstantiateHandler(type);
			}

			/// <summary>
			/// Creates a delegate for instantiating an object.
			/// </summary>
			/// <param name="type">The type.</param>
			/// <returns><see cref="InstantiateHandler"/></returns>
			private static InstantiateHandler CreateInstantiateHandler(Type type)
			{
				ConstructorInfo constructorInfo = 
					type.GetConstructor(
						BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
						null, 
						new Type[0], 
						null);

				if (constructorInfo != null)
				{
					DynamicMethod dynamicMethod = new DynamicMethod(
						"InstantiateObject",
						MethodAttributes.Static | MethodAttributes.Public,
						CallingConventions.Standard,
						typeof (object),
						null,
						type,
						true);
					ILGenerator generator = dynamicMethod.GetILGenerator();
					generator.Emit(OpCodes.Newobj, constructorInfo);
					generator.Emit(OpCodes.Ret);
					return (InstantiateHandler) dynamicMethod.CreateDelegate(typeof (InstantiateHandler));
				}

				return null;
			}

			#endregion Private Methods
		}

		#endregion Private Types
	}
}
