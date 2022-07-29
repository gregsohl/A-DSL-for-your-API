#region Namespaces

using System;
using NUnit.Framework;

using TurtleScript.Interpreter.Tokenize;
using TurtleScript.Interpreter.Tokenize.Execute;
using TurtleScript.Interpreter.Tokenize.Parse;

#endregion Namespaces

namespace TurtleScript.Interpreter.UnitTest
{
	public class ArithmeticTests : TestBase
	{
		[Test]
		[Category("Success")]
		public void SimpleAddition()
		{
			// Arrange
			const string SCRIPT = "a = 1 + 1";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 2;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void ComplexAddition()
		{
			// Arrange
			const string SCRIPT = "a = 1 + 1 + 2 + 4";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 8;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}


		[Test]
		[Category("Success")]
		public void SimpleSubtraction()
		{
			// Arrange
			const string SCRIPT = "a = 10 - 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 8;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void ComplexSubtraction()
		{
			// Arrange
			const string SCRIPT = "a = 10 - 2 - 4";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 4;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void SimpleMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = 5 * 6";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 30;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void ComplexMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = 5 * 6 * 2 * 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 600;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void SimpleDivision()
		{
			// Arrange
			const string SCRIPT = "a = 10 / 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 5;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void DivideByZero()
		{
			// Arrange
			const string SCRIPT = "a = 5 / 0";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 0;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void ComplexDivision()
		{
			// Arrange
			const string SCRIPT = "a = 100 / 2 / 5";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 10;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void AdditionFirstThenMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = 10 + 10 * 100";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1010;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void MultiplicationFirstThenAddition()
		{
			// Arrange
			const string SCRIPT = "a = 10 * 100 + 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1010;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void MultiplicationFirstThenDivision()
		{
			// Arrange
			const string SCRIPT = "a = 10 * 100 / 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 500;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void DivisionFirstThenMultiplication()
		{
			// Arrange
			const string SCRIPT = "a = 100 / 2 * 10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 500;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void SimpleModulus()
		{
			// Arrange
			const string SCRIPT = "a = 11 % 2";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void ComplexModulus()
		{
			// Arrange
			const string SCRIPT = "a = 98 % 9 % 3";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 2;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void AdditionFirstThenModulus()
		{
			// Arrange
			const string SCRIPT = "a = 10 + 10 % 3";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 11;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void Negation()
		{
			// Arrange
			const string SCRIPT = "a = -10";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = -10;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void NegationParenthesis()
		{
			// Arrange
			const string SCRIPT = "a = -(5 + 5)";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = -10;

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE);
		}

		[Test]
		[Category("Success")]
		public void Complex_Arithmetic_OddWhiteSpace()
		{
			// Arrange
			const string SCRIPT = "a=((1+55)*         10/2    *(3+1)       )";
			const string VARIABLE_NAME = "a";
			const double EXPECTED_VALUE = 1120;
			const string EXPECTED_REGENERATED_SCRIPT = "a = ((1 + 55) * 10 / 2 * (3 + 1))";

			RunTest(SCRIPT,
				VARIABLE_NAME,
				EXPECTED_VALUE,
				EXPECTED_REGENERATED_SCRIPT);
		}


		/// <summary>
		/// Mismatched data types for addition operator discovered at execution time
		/// </summary>
		[Test]
		[Category("Error")]
		public void SimpleAdditionMismatchedOperandTypes()
		{
			// Arrange
			const string SCRIPT = "a = 1 + (1 > 2)";

			RunTestWithExecutionError(SCRIPT);
		}
	}
}