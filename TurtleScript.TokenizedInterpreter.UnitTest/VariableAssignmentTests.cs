#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class VariableAssignmentTests : TestBase
	{
		[Test]
		public void SimpleAssignmentTest()
		{
			// Arrange
			const string SCRIPT = "a = 1";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		public void MultipleVariableAssignmentTest()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("bcd = 200");

			const string VARIABLE_NAME1 = "a";
			const string VARIABLE_NAME2 = "bcd";
			const double EXPECTED_VALUE1 = 1;
			const double EXPECTED_VALUE2 = 200;
			const double EXPECTED_VARIABLE_COUNT = 2;

			TestContext testContext = RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME1,
				EXPECTED_VALUE1);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, testContext.ExecutionContext.GlobalVariableCount);

			TurtleScriptValue variableValue = testContext.ExecutionContext.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(
				EXPECTED_VALUE2,
				variableValue.NumericValue);
		}

		[Test]
		public void VariableReassignmentTest()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("bcd = 200");
			scriptBuilder.AppendLine("a = 100");

			const string VARIABLE_NAME1 = "a";
			const string VARIABLE_NAME2 = "bcd";
			const double EXPECTED_VALUE1 = 100;
			const double EXPECTED_VALUE2 = 200;
			const double EXPECTED_VARIABLE_COUNT = 2;

			TestContext testContext = RunTest(scriptBuilder.ToString(),
				VARIABLE_NAME1,
				EXPECTED_VALUE1);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, testContext.ExecutionContext.GlobalVariableCount);

			TurtleScriptValue variableValue = testContext.ExecutionContext.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(
				EXPECTED_VALUE2,
				variableValue.NumericValue);
		}

	}
}