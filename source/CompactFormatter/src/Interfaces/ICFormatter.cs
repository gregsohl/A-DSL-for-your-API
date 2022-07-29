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
 * $Id: ICFormatter.cs 1 2004-08-13 18:29:52Z Angelo $
 * */
#endregion

using System;
using System.IO;

namespace CompactFormatter.Interfaces
{
	/// <summary>
	/// Considering that System.Runtime.Formatters.IFormatter interface is not
	/// present in .NET Compact Framework, but we still wants to be "compatible"
	/// with BinaryFormatter behaviour (so people already familiar with Binary or
	/// SOAPFormatter can move to CompactFormatter with a small effort) we need
	/// to "clone" IFormatter interface, and this is ICFormatter.
	/// <remarks>Obviously we've left out Binder, Context and SurrogateSelector
	/// since they're useless in CF algorithm.</remarks>
	/// 
	/// </summary>
	public interface ICFormatter
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
		void Serialize(
			Stream serializationStream,
			object graph
			);

		/// <summary>
		/// Deserializes the data on the provided stream and reconstitutes the 
		/// graph of objects.
		/// </summary>
		/// <param name="serializationStream">The stream containing the data 
		/// to deserialize.</param>
		/// <returns>The top object of the deserialized graph.</returns>
		object Deserialize(
			Stream serializationStream
			);
	}
}
