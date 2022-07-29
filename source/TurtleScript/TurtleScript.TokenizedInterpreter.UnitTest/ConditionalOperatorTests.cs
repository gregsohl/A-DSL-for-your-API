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
	public class ConditionalOperatorTests : TestBase
	{
		[Test]
		public void AndConditionTrue()
		{
			// Arrange
			const string SCRIPT = "a = 1 < 2 && 3 > 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void AndConditionFalse()
		{
			// Arrange
			const string SCRIPT = "a = 1 > 2 && 3 > 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void OrConditionBothTrue()
		{
			// Arrange
			const string SCRIPT = "a = 1 < 2 || 3 > 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void OrConditionOneTrue()
		{
			// Arrange
			const string SCRIPT = "a = 1 < 2 || 3 < 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void OrConditionFalse()
		{
			// Arrange
			const string SCRIPT = "a = 1 > 2 || 3 < 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void AndOrConditionTrue()
		{
			// Arrange
			const string SCRIPT = "a = 1 < 2 || 3 < 2 && 1 == 1";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void AndOrConditionFalse()
		{
			// Arrange
			const string SCRIPT = "a = 1 > 2 || 3 < 2 && 1 == 1";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void Not_Simple_True()
		{
			// Arrange
			const string SCRIPT = "a = !(1 > 2)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}


		[Test]
		public void NotOrConditionTrue()
		{
			// Arrange
			const string SCRIPT = "a = !(1 < 2) || (3 > 2)";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void NotAndConditionTrue()
		{
			// Arrange
			const string SCRIPT = "a = !(!(1 < 2) && (3 > 2))";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void GreaterThan_True()
		{
			// Arrange
			const string SCRIPT = "a = 3 > 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void GreaterThanOrEqual_Greater_True()
		{
			// Arrange
			const string SCRIPT = "a = 3 >= 2";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void GreaterThanOrEqual_Equal_True()
		{
			// Arrange
			const string SCRIPT = "a = 3 >= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void GreaterThan_False()
		{
			// Arrange
			const string SCRIPT = "a = 3 > 4";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void GreaterThanOrEqual_Greater_False()
		{
			// Arrange
			const string SCRIPT = "a = 3 >= 4";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}


		[Test]
		public void LessThan_True()
		{
			// Arrange
			const string SCRIPT = "a = 2 < 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void LessThanOrEqual_Greater_True()
		{
			// Arrange
			const string SCRIPT = "a = 2 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void LessThanOrEqual_Equal_True()
		{
			// Arrange
			const string SCRIPT = "a = 3 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = true;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void LessThan_False()
		{
			// Arrange
			const string SCRIPT = "a = 4 < 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

		[Test]
		public void LessThanOrEqual_Greater_False()
		{
			// Arrange
			const string SCRIPT = "a = 4 <= 3";
			const string VARIABLE_NAME = "a";
			const bool EXPECTED_VALUE = false;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				TurtleScriptValueType.Boolean);
		}

	}
}