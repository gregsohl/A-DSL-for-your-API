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
 * $Id$
 * */
#endregion

using System;

namespace CompactFormatter.Attributes
{
	/// <summary>
	/// This attribute is used to override temporary normal serialization behaviour.
	/// Using this attribute on a field of a serializable class, the user is telling
	/// to CompactFormatter to serialize this field using serialization policy 
	/// implemented into another class (the customSerializer passed as parameter).
	/// This attribute can be seen as a way to implement a custom serialization policy
	/// also on those classes which are not under direct control of final user.
	/// NOTICE: Data about component containing custom serialization mechanism must
	/// be serialized on the wire with the field, this means that this attribute
	/// should be used only when it's really necessary: if user implements a class
	/// and wants to give it custom serialization it's better to use Custom
	/// named parameter of Serializable attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class OverriderAttribute : Attribute
	{

		private Type customSerializer;

		public Type CustomSerializer
		{
			get
			{
				return customSerializer;
			}
		}

		public OverriderAttribute(Type customSerializer)
		{
			this.customSerializer = customSerializer;
		}
	}
}
