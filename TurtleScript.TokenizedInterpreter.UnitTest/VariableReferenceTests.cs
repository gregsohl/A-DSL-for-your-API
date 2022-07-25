#region Namespaces

using System.Text;

using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class VariableReferenceTests : TestBase
	{
		[Test]
		[Category("Success")]
		public void AssignmentAndReference()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("b = a");
			scriptBuilder.AppendLine("a = 3");

			const string VARIABLE_NAME1 = "a";
			const string VARIABLE_NAME2 = "b";
			const double EXPECTED_VALUE1 = 3;
			const double EXPECTED_VALUE2 = 1;
			const double EXPECTED_VARIABLE_COUNT = 2;

			TestContext testContext = RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME1,
				EXPECTED_VALUE1);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, testContext.ExecutionContext.GlobalVariableCount);

			TurtleScriptValue variableValue = testContext.ExecutionContext.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(
				EXPECTED_VALUE2,
				variableValue.NumericValue);
		}

		[Test]
		[Category("Success")]
		public void AssignmentAndSelfReassignment()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 12345");
			scriptBuilder.AppendLine("a = a");

			const string VARIABLE_NAME1 = "a";
			const double EXPECTED_VALUE1 = 12345;
			const double EXPECTED_VARIABLE_COUNT = 1;

			TestContext testContext = RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME1,
				EXPECTED_VALUE1);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, testContext.ExecutionContext.GlobalVariableCount);
		}

		[Test]
		[Category("Success")]
		public void AssignmentAndReferenceInExpression()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("b = a + 500");

			const string VARIABLE_NAME1 = "a";
			const string VARIABLE_NAME2 = "b";
			const double EXPECTED_VALUE1 = 1;
			const double EXPECTED_VALUE2 = 501;
			const double EXPECTED_VARIABLE_COUNT = 2;

			TestContext testContext = RunTest(
				scriptBuilder.ToString(),
				VARIABLE_NAME1,
				EXPECTED_VALUE1);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, testContext.ExecutionContext.GlobalVariableCount);

			TurtleScriptValue variableValue = testContext.ExecutionContext.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(
				EXPECTED_VALUE2,
				variableValue.NumericValue);
		}

		[Test]
		[Category("Error")]
		public void BadReference()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = b");

			RunTestWithParserError(scriptBuilder.ToString());
		}

		[Test]
		[Category("Error")]
		public void BadReferenceLocalVariableNotAvailableInGlobalScope()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	myLocal = 15");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc()");
			scriptBuilder.AppendLine("b = myLocal");

			RunTestWithParserError(scriptBuilder.ToString());
		}

		[Test]
		[Category("Error")]
		public void BadReferenceGlobalVariableNotAvailableForFunctionCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(myParameter)");
			scriptBuilder.AppendLine("	myLocal = 15");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(badVariable)");

			RunTestWithParserError(scriptBuilder.ToString());
		}

		[Test]
		[Category("Error")]
		public void BadReferenceInsideFunctionCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	myLocal = badVariable");
			scriptBuilder.AppendLine("end");

			RunTestWithParserError(scriptBuilder.ToString());
		}

		[Test]
		[Category("Error")]
		public void BadReferencesInsideFunctionCall()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc()");
			scriptBuilder.AppendLine("	myLocal = badVariable1");
			scriptBuilder.AppendLine("	myLocal = badVariable2");
			scriptBuilder.AppendLine("end");

			RunTestWithParserError(scriptBuilder.ToString());
		}

	}
}