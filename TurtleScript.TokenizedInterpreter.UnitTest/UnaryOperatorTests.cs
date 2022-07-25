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
	public class UnaryOperatorTests : TestBase
	{
		[Test]
		public void UnaryNegation()
		{
			// Arrange
			const string SCRIPT = "a = -(1 + 1)";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = -2;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


		[Test]
		public void UnaryNotTrueToFalse()
		{
			// Arrange
			const string SCRIPT = "a = !(1 == 1)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void UnaryNotFalseToTrue()
		{
			// Arrange
			const string SCRIPT = "a = !(1 != 1)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

	}
}