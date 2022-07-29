namespace CompactFormatter.PerformanceTest
{
	class Program
	{
		static void Main(string[] args)
		{
			CustomClassSerialization test = new CustomClassSerialization();
			test.DeserializeCustomClass(1000000);
		}
	}
}
