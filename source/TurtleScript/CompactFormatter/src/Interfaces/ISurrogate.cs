using System;
using System.IO;

namespace CompactFormatter.Interfaces
{
	/// <summary>
	/// Summary description for ISurrogate.
	/// </summary>
	public interface ISurrogate
	{

		/// <summary>
		/// This method simply returns a "uninitialized" (it's status doesn't 
		/// interests since it will be overwritten by reflection) instance of
		/// type t passed as parameter.
		/// </summary>
		/// <param name="t">The type</param>
		/// <returns>An instance of type t</returns>
		Object CreateObject(Type t);

		/// <summary>
		/// Not totally sure...
		/// </summary>
		/// <param name="Wire"></param>
		void Serialize(Stream Wire);
		
		/// <summary>
		/// Not totally sure...
		/// </summary>
		/// <param name="Wire"></param>
		void Deserialize(Stream Wire);

	}
}
