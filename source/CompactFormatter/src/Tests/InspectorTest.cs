using System;
using System.Reflection;
using NUnit.Framework;

namespace CompactFormatter.Tests
{
	/// <summary>
	/// Summary description for InspectorTest.
	/// </summary>
	[TestFixture]
	public class InspectorTest
	{

		[Test]
		public void MonoInspectType()
		{
			if (Framework.Detect() != FrameworkVersion.MONO) return;
			FieldInfo[] f = ClassInspector.InspectClass(
				typeof(System.Collections.Hashtable));
			
			int counter = 0;
			foreach(FieldInfo field in f)
			{
				if (field.Name.Equals("inUse"))
					counter++;
				else if (field.Name.Equals("modificationCount"))
					counter++;
				else if (field.Name.Equals("loadFactor"))
					counter++;
				else if (field.Name.Equals("table"))
					counter++;
				else if (field.Name.Equals("threshold"))
					counter++;
				else if (field.Name.Equals("hashKeys"))
					counter++;
				else if (field.Name.Equals("hashValues"))
					counter++;
				else if (field.Name.Equals("hcpRef"))
					counter++;
				else if (field.Name.Equals("comparerRef"))
					counter++;
				else Assert.Fail("Field is not an expected one: "+field.Name);
			}
			Assert.AreEqual(9,counter,"Number of fields is different from expected");		
		}
		
		[Test]
		public void NETInspectType()
		{
			if (Framework.Detect() != FrameworkVersion.NET11 
				|| Framework.Detect() != FrameworkVersion.NET10) return;
			FieldInfo[] f = ClassInspector.InspectClass(
				typeof(System.Collections.Hashtable));
			
			int counter = 0;
			foreach(FieldInfo field in f)
			{
				if (field.Name.Equals("buckets"))
					counter++;
				else if (field.Name.Equals("count"))
					counter++;
				else if (field.Name.Equals("occupancy"))
					counter++;
				else if (field.Name.Equals("loadsize"))
					counter++;
				else if (field.Name.Equals("loadFactor"))
					counter++;
				else if (field.Name.Equals("version"))
					counter++;
				else if (field.Name.Equals("keys"))
					counter++;
				else if (field.Name.Equals("values"))
					counter++;
				else if (field.Name.Equals("_hcp"))
					counter++;
				else if (field.Name.Equals("_comparer"))
					counter++;
				else if (field.Name.Equals("m_siInfo"))
					counter++;
				else Assert.Fail("Field is not an expected one: "+field.Name);
			}
			Assert.AreEqual(11,counter,"Number of fields is different from expected");
				

		}
	}
}
