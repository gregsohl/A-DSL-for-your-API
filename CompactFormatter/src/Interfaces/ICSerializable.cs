using System;

namespace CompactFormatter.Interfaces
{
	/// <summary>
	/// Every class who wants to use CustomSerialization must implement 
	/// this interface, it is a CompactFramework version of .NET ISerializable
	/// interface.
	/// On CompactFramework we don't have ISerializable interface, so 
	/// ICSerializable is declared by CompactFormatter, obviously it's interface
	/// is changed respect to ISerializable because we don't have SerializationInfo
	/// and StreamingContext on the CompactFramework and plus we've a reference to 
	/// CompactFormatter to make, if necessary, use of standard serialization algorithms 
	/// during custom serialization. 
	/// 
	/// </summary>
	public interface ICSerializable
	{

		// Implied a parameterless constructor (needed by CompactFormatter).
		// ISerializable();

		/// <summary>
		/// This function is invoked by CompactFormatter when serializing a 
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be written</param>
		void SendObjectData(CompactFormatter parent, System.IO.Stream stream);

		/// <summary>
		/// This function is invoked by CompactFormatter when deserializing a
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be read</param>
		void ReceiveObjectData(CompactFormatter parent, System.IO.Stream stream);		
	}
}
