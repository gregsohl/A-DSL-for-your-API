using System;

namespace CompactFormatter.Exception
{
	/// <summary>
	/// This Exception is raised during deserialization phase, if CompactFormatter
	/// notice that Serialization used a particular StreamParser currently not
	/// available.
	/// 
	/// TODO: List of parsers must be explored backwards if more than one Serializer
	/// is available, a check must be made to ensure that exactly all Parser are
	/// registered correctly before starting deserialization.
	/// </summary>
	public class StreamParserNotAvailableException : System.Exception
	{
		public StreamParserNotAvailableException(String parser) : 
			base(parser+" class is currently unregistered")
		{
			
		}
	}
}
