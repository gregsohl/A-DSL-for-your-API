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
	public class IfStatementTests : TestBase
	{
		[Test]
		public void IfStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 20;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void IfStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 10");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 10;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


		[Test]
		public void IfStatementTrueMultilineBody()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 40;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void IfElseStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 15");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 40;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void IfElseStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 5");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("  b = b * 2");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 10;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void IfElseIfStatementTrue()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 5");
			scriptBuilder.AppendLine("if (b > 10) do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("else if (b >= 5) do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 1");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 10;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void IfElseIfStatementFalse()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 2");
			scriptBuilder.AppendLine("if b > 10 do");
			scriptBuilder.AppendLine("  b = 20");
			scriptBuilder.AppendLine("else if b >= 5 do");
			scriptBuilder.AppendLine("  b = 10");
			scriptBuilder.AppendLine("else do");
			scriptBuilder.AppendLine("  b = 1");
			scriptBuilder.AppendLine("end");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 1;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

	}
}