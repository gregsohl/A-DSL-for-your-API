#region Namespaces

using System;

using NUnit.Framework;

using KellermanSoftware.CompareNetObjects;

using TurtleScript.Interpreter.Tokenize;

#endregion Namespaces


namespace TurtleScript.TokenizedInterpreter.UnitTest
{
	internal class TokenSerializationTests
	{
		[Test]
		[Category("Success")]
		public void SerializeDeserialize_NumericValue()
		{
			// Arrange
			TokenNumericValue token = new TokenNumericValue(55, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenNumericValue reconstitutedToken =
				(TokenNumericValue)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_BooleanValue()
		{
			// Arrange
			TokenBooleanValue token = new TokenBooleanValue(true, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenBooleanValue reconstitutedToken =
				(TokenBooleanValue)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_Pi()
		{
			// Arrange
			TokenPi token = new TokenPi(1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenPi reconstitutedToken =
				(TokenPi)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_Assignment()
		{
			// Arrange
			TokenAssignment token = new TokenAssignment("varname", 1, 2);
			token.AddChild(new TokenNumericValue(55));

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenAssignment reconstitutedToken =
				(TokenAssignment)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_BinaryOperator()
		{
			// Arrange
			TokenBinaryOperator token = new TokenBinaryOperator(TokenType.OpAdd, 1, 2);
			token.AddChild(new TokenNumericValue(50));
			token.AddChild(new TokenNumericValue(55));

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenBinaryOperator reconstitutedToken =
				(TokenBinaryOperator)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_Block()
		{
			// Arrange
			TokenBlock token = new TokenBlock(1, 2);
			TokenFunctionDeclaration function1 = new TokenFunctionDeclaration(
				"function1",
				new string[2] { "a", "b" },
				1,
				2);
			TokenBlock function1Body = new TokenBlock(2, 4);
			function1.SetFunctionBody(function1Body);
			TokenFunctionDeclaration function2 = new TokenFunctionDeclaration(
				"function2",
				new string[2] { "c", "d" },
				4,
				5);
			TokenBlock function2Body = new TokenBlock(6, 8);
			function2.SetFunctionBody(function2Body);

			token.AddChild(function1);
			token.AddChild(function2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenBlock reconstitutedToken =
				(TokenBlock)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_ForStatement()
		{
			// Arrange
			TokenNumericValue startValue = new TokenNumericValue(5, 100, 200);
			TokenNumericValue endValue = new TokenNumericValue(10, 200, 300);
			TokenBlock block = new TokenBlock(1, 2);
			TokenAssignment assignment = new TokenAssignment("varname", 5, 10);
			assignment.AddChild(new TokenVariableReference("varname", 20, 25));
			block.AddChild(assignment);

			TokenForStatement token = new TokenForStatement("loopVar", startValue, endValue, block, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			TokenForStatement reconstitutedToken =
				(TokenForStatement)TokenSerializer.DeserializeFromArray(serializedData);

			// Assert
			VerifySerialization(token,
				reconstitutedToken);
		}

		#region Private Methods

		private static void VerifySerialization(
			TokenBase token,
			TokenBase reconstitutedToken)
		{
			CompareLogic compareLogic = new CompareLogic();
			ComparisonResult comparisonResult = compareLogic.Compare(
				token,
				reconstitutedToken);

			if (!comparisonResult.AreEqual)
			{
				Console.WriteLine(comparisonResult.DifferencesString);
			}

			Assert.IsTrue(comparisonResult.AreEqual);

			Console.WriteLine("Reconstituted Turtlescript:");
			Console.WriteLine(reconstitutedToken.ToTurtleScript());
		}

		#endregion Private Methods

	}
}
