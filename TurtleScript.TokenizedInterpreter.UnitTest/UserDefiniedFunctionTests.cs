#region Namespaces

using System;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class UserDefinedFunctionTests : TestBase
	{
		[Test]
		public void FunctionDeclarationAndCallNoParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("  b = 15");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("MyFunc()");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 15;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void FunctionDeclarationAndCallOneParameter()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(myparameter)");
			scriptBuilder.AppendLine("  b = myparameter");
			scriptBuilder.AppendLine("  myparameter = b * 2");
			scriptBuilder.AppendLine("  b = myparameter + 1");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("MyFunc(10)");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 21;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void FunctionDeclarationAndCallThreeParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(value1, value2, value3)");
			scriptBuilder.AppendLine("  b = value1 + value2 - value3");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("MyFunc(100, 200, 50)");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 250;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void FunctionDeclarationAfterCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 10");
			scriptBuilder.AppendLine("MyFunc()");
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("  b = 15");
			scriptBuilder.AppendLine("end");

			StringBuilder expectedScript = new StringBuilder();
			expectedScript.AppendLine("def MyFunc()");
			expectedScript.AppendLine("  b = 15");
			expectedScript.AppendLine("end");
			expectedScript.AppendLine();
			expectedScript.AppendLine("b = 10");
			expectedScript.AppendLine("MyFunc()");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 15;

			RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE,
				expectedScript.ToString());
		}

		[Test]
		public void FunctionDeclarationAndCallComplexParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(value1, value2, value3)");
			scriptBuilder.AppendLine("  b = value1 + value2 - value3");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("MyFunc((1 + 2), (4 + 5) * 2, 20)");

			const string VARIABLE_NAME = "b";
			const double EXPECTED_VALUE = 1;

			RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


	}
}