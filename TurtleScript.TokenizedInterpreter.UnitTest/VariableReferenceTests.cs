﻿#region Namespaces

using System.Linq;
using System.Text;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class VariableReferenceTests
	{
		[Test]
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

			TurtleScriptTokenizer interpreter = new TurtleScriptTokenizer(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Parse(out TokenBase rootToken);
			TurtleScriptExecutionContext context = new TurtleScriptExecutionContext();
			interpreter.Execute(rootToken, context);

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			Assert.AreEqual(EXPECTED_VARIABLE_COUNT, context.Variables.Count);

			TurtleScriptValue variableValue = context.GetVariableValue(VARIABLE_NAME1);
			Assert.AreEqual(EXPECTED_VALUE1, variableValue.NumericValue);

			variableValue = context.GetVariableValue(VARIABLE_NAME2);
			Assert.AreEqual(EXPECTED_VALUE2, variableValue.NumericValue);
		}

		[Test]
		public void AssignmentAndSelfReassignment()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 12345");
			scriptBuilder.AppendLine("a = a");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["a"];
			Assert.AreEqual(12345, variableValue.NumericValue);
		}

		[Test]
		public void AssignmentAndReferenceInExpression()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("a = 1");
			scriptBuilder.AppendLine("b = a + 500");

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(scriptBuilder.ToString());

			// Act
			bool success = interpreter.Execute();

			// Assert
			Assert.IsTrue(success, interpreter.ErrorMessage);

			TurtleScriptValue variableValue = interpreter.Variables["b"];
			Assert.AreEqual(501, variableValue.NumericValue);
		}
	}
}