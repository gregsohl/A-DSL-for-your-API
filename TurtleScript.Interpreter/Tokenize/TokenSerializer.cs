using System;
using System.IO;

using CompactFormatter;

namespace TurtleScript.Interpreter.Tokenize
{
	public static class TokenSerializer
	{
		/// <summary>
		/// Deserializes the specified object with the CompactFormatter
		/// </summary>
		/// <param name="serializedArray">The serialized array.</param>
		/// <returns>Deserialized <see cref="Object" />.</returns>
		public static object DeserializeFromArray(byte[] serializedArray)
		{
			CompactFormatter.CompactFormatter compactFormatter = new CompactFormatter.CompactFormatter(CFormatterMode.SURROGATE | CFormatterMode.EXACTASSEMBLY);

			object deserializedObject;
			using (MemoryStream memoryStream = new MemoryStream(serializedArray))
			{
				deserializedObject = compactFormatter.Deserialize(memoryStream);
			}

			return deserializedObject;
		}

		/// <summary>
		/// Serializes the specified object with the CompactFormatter
		/// </summary>
		/// <param name="obj"><see cref="Object"/> - The object to be serialized.</param>
		/// <returns>Object's data serialized with the CompactFormatter and converted to a Base64 <see cref="String"/></returns>
		public static byte[] SerializeToArray(object obj)
		{
			CompactFormatter.CompactFormatter compactFormatter = new CompactFormatter.CompactFormatter(CFormatterMode.SURROGATE | CFormatterMode.EXACTASSEMBLY);

			byte[] serializedArray;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				// Serialize the object to the memory stream
				compactFormatter.Serialize(memoryStream, obj);
				memoryStream.Flush();

				// Convert the memory stream to a byte array
				serializedArray = memoryStream.ToArray();
			}

			return serializedArray;
		}


	}
}
