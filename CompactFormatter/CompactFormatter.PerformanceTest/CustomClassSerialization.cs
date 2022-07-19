using System.IO;
using CompactFormatter.Attributes;

namespace CompactFormatter.PerformanceTest
{
	public class CustomClassSerialization
	{
		public void SerializeCustomClass(int count)
		{
			CompactFormatter compactFormatter = new CompactFormatter(CFormatterMode.SURROGATE);

			for (int index = 0; index < count; index++)
			{
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
				}
			}
		}

		public void DeserializeCustomClass(int count)
		{
			CompactFormatter compactFormatter = new CompactFormatter(CFormatterMode.SURROGATE);

			byte[] personSerialized;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				Person person = new Person(
					"Fred Johnson",
					"Treasurer",
					"515-555-1212",
					"a@b.com",
					47,
					1000000m
				);

				compactFormatter.Serialize(memoryStream, person);

				personSerialized = memoryStream.ToArray();
			}

			for (int index = 0; index < count; index++)
			{
				using (MemoryStream memoryStream = new MemoryStream(personSerialized))
				{
					Person derserializedPerson = (Person) compactFormatter.Deserialize(memoryStream);
				}
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
