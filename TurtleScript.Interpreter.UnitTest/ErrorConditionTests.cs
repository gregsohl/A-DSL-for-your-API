#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ErrorConditionTests
	{
		[Test]
		public void MissingTrailingParenthesis()
		{
			// Arrange
			var script = "a = ( 1 + 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("missing ')' at '<EOF>'. Line 1, Col 11", interpreter.ErrorMessage);
		}

		[Test]
		public void MultipleMissingTrailingParenthesis()
		{
			// Arrange
			var script = "a = ( 5 * ( 1 + 1";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("missing ')' at '<EOF>'. Line 1, Col 17", interpreter.ErrorMessage);
		}

		[Test]
		public void MissingTrailingParenthesis2()
		{
			// Arrange
			var script = "a = ( 5 * ( 1 + 1 )";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("missing ')' at '<EOF>'. Line 1, Col 19", interpreter.ErrorMessage);
		}

		[Test]
		public void MissingTrailingParenthesis3()
		{
			// Arrange
			var script = "a = (( 5 * ( 1 + 1 ))";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("missing ')' at '<EOF>'. Line 1, Col 21", interpreter.ErrorMessage);
		}

		[Test]
		public void ExtraParenthesis1()
		{
			// Arrange
			var script = "a = 1 + 1 )";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("extraneous input ')' expecting <EOF>. Line 1, Col 10", interpreter.ErrorMessage);
		}

		[Test]
		public void ExtraParenthesis2()
		{
			// Arrange
			var script = "a = ( 1 + 1 ) )";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("extraneous input ')' expecting <EOF>. Line 1, Col 14", interpreter.ErrorMessage);
		}

		[Test]
		public void Garbage1()
		{
			// Arrange
			var script = "abcd";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("no viable alternative at input 'abcd'. Line 1, Col 4", interpreter.ErrorMessage);
		}

		[Test]
		public void Garbage2()
		{
			// Arrange
			var script = "1234";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("extraneous input '1234' expecting <EOF>. Line 1, Col 0", interpreter.ErrorMessage);
		}

		[Test]
		public void Garbage3()
		{
			// Arrange
			var script = "++++";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("mismatched input '+' expecting <EOF>. Line 1, Col 0", interpreter.ErrorMessage);
		}

		[Test]
		public void Garbage4()
		{
			// Arrange
			var script = "55 +";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("mismatched input '55' expecting <EOF>. Line 1, Col 0", interpreter.ErrorMessage);
		}

		[Test]
		public void Garbage5()
		{
			// Arrange
			var script = "55 + ABC";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("mismatched input '55' expecting <EOF>. Line 1, Col 0", interpreter.ErrorMessage);
		}

		[Test]
		public void UnknownVariableReference()
		{
			// Arrange
			var script = "a = 55 + ABC";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual("Reference to an unknown variable, 'ABC'. Line 1, Col 9", interpreter.ErrorMessage);
		}

		[Test]
		public void UnknownFunctionReference()
		{
			// Arrange
			var script = "a = 55 + ABC()";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual(
				"Invalid identifier. Function name not previously defined. Line 1, Column 9",
				interpreter.ErrorMessage);
		}

		[Test]
		public void UnknownRuntimeFunctionReference()
		{
			// Arrange
			var script = "a = 55 + ur.ABC()";

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
;			Assert.AreEqual(
				"Invalid runtime library name 'ur' specified on function call. Line 1, Column 9",
				interpreter.ErrorMessage);
		}

		[Test]
		public void ForLoopStartParameterMustBeNumeric()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("b = 0");
			scriptBuilder.AppendLine("for a = (2>1) to 5 do");
			scriptBuilder.AppendLine("b = b + a");
			scriptBuilder.AppendLine("end");
			var script = scriptBuilder.ToString();

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual(
				$"For loop starting value must be numeric. Line 2, Column 0",
				interpreter.ErrorMessage);
		}

		[Test]
		public void FunctionCallWrongNumberOfParameters()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(a)");
			scriptBuilder.AppendLine("	b = 15 * a");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(1,2,3)");
			var script = scriptBuilder.ToString();

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual(
				"Invalid identifier. Function name not previously defined. Line 4, Column 0",
				interpreter.ErrorMessage);
		}

		[Test]
		public void DuplicateFunctionDefinition()
		{
			// Arrange
			StringBuilder scriptBuilder = new StringBuilder();
			scriptBuilder.AppendLine("def MyFunc(a)");
			scriptBuilder.AppendLine("	b = 15 * a");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("def MyFunc(a)");
			scriptBuilder.AppendLine("	b = 15 * a");
			scriptBuilder.AppendLine("end");
			scriptBuilder.AppendLine("MyFunc(1,2,3)");
			var script = scriptBuilder.ToString();

			TurtleScriptInterpreter interpreter = new TurtleScriptInterpreter(script);

			// Act
			bool success = interpreter.Execute();

			// Assert
			OutputResults(script, interpreter);
			Assert.IsFalse(success);
			Assert.AreEqual(
				"A function with the name 'MyFunc' already exists. Line 4, Column 0",
				interpreter.ErrorMessage);
		}

		#region Private Methods

		private static void OutputResults(string script, TurtleScriptInterpreter interpreter)
		{
			Console.Write(
				$"Script:{Environment.NewLine}{script}{Environment.NewLine}{Environment.NewLine}Error Message:{Environment.NewLine}{interpreter.ErrorMessage}");
		}

		#endregion Private Methods

	}
}