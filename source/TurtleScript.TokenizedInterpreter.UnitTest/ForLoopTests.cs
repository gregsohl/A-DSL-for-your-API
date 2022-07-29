#region Namespaces

using System;
using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ForLoopTests : TestBase
	{
		[Test]
		public void Simple()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("for a = 1 to 5 do");
			scriptBuilder.AppendLine("  b = b + a");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 15;

			RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


		[Test]
		public void Reverse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("for a = 5 to 1 do");
			scriptBuilder.AppendLine("  b = b + a");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 15;

			RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}
	}
}