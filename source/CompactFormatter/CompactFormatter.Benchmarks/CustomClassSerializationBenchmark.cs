using System.IO;
using BenchmarkDotNet.Attributes;
using CompactFormatter.Attributes;

namespace CompactFormatter.Benchmarks
{
	public class CustomClassSerializationBenchmark
	{
		[Benchmark]
		public void SerializeCustomClass()
		{
			CompactFormatter compactFormatter = new CompactFormatter(CFormatterMode.SURROGATE);

			Person person = new Person(
				"Fred Johnson",
				"Treasurer",
				"515-555-1212",
				"a@b.com",
				47,
				1000000m
				);

			using(MemoryStream memoryStream = new MemoryStream())
			{
				compactFormatter.Serialize(memoryStream, person);
			}
		}

		[Benchmark]
		public void DeserializeCustomClass()
		{
			CompactFormatter compactFormatter = new CompactFormatter(CFormatterMode.SURROGATE);

			Person person = new Person(
				"Fred Johnson",
				"Treasurer",
				"515-555-1212",
				"a@b.com",
				47,
				1000000m
			);

			using (MemoryStream memoryStream = new MemoryStream())
			{
				compactFormatter.Serialize(memoryStream, person);

				compactFormatter.Deserialize(memoryStream);
			}
		}

	}

	[Serializable]
	public class Person
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public Person()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public Person(string name, string title, string phone, string email, int age, decimal netWorth)
		{
			m_Name = name;
			m_Title = title;
			m_Phone = phone;
			m_Email = email;
			m_Age = age;
			m_NetWorth = netWorth;
		}

		private string m_Name;
		private string m_Title;
		private string m_Phone;
		private string m_Email;
		private int m_Age;
		private decimal m_NetWorth;
	}
}
