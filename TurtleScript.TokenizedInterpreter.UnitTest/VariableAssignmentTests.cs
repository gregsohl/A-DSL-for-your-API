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
	public class VariableAssignmentTests
	{
		[Test]
		public void SimpleAssignmentTest()
		{
			// Arrange
			var script = "a = 1";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1;
			const double EXPECTED_VARIABLE_COUNT = 1;

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(script);

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, context.GlobalVariableCount);

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME);

			Assert.AreEqual(EXPECTED_VALUE, variableValue.NumericValue);
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

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, context.GlobalVariableCount);

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			variableValue = context.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(EXPECTED_VALUE2, variableValue.NumericValue);
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

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			TurtleScriptExecutor executor = new TurtleScriptExecutor();
			executor.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, context.GlobalVariableCount);

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			variableValue = context.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(EXPECTED_VALUE2, variableValue.NumericValue);
		}

	}
}