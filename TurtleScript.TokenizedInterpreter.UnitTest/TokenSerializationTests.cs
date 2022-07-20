#region Namespaces

using System;
using System.Collections.Generic;

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

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_BooleanValue()
		{
			// Arrange
			TokenBooleanValue token = new TokenBooleanValue(true, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_Pi()
		{
			// Arrange
			TokenPi token = new TokenPi(1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
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

			// Assert
			VerifySerialization(token,
				serializedData);
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

			// Assert
			VerifySerialization(token,
				serializedData);
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

			// Assert
			VerifySerialization(token,
				serializedData);
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
			assignment.AddChild(new TokenVariableReference("loopvar", 20, 25));
			block.AddChild(assignment);

			TokenForStatement token = new TokenForStatement("loopVar", startValue, endValue, block, 1, 2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_FunctionCall()
		{
			// Arrange
			TokenNumericValue param1 = new TokenNumericValue(5, 100, 200);
			TokenNumericValue param2 = new TokenNumericValue(10, 200, 300);
			TokenFunctionCall token = new TokenFunctionCall(
				"function1",
				new TokenBase[2] { param1, param2 },
				1,
				2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_FunctionDeclaration()
		{
			// Arrange
			TokenFunctionDeclaration token = new TokenFunctionDeclaration(
				"function1",
				new string[2] { "a", "b" },
				1,
				2);
			TokenBlock functionBody = new TokenBlock(2, 4);
			token.SetFunctionBody(functionBody);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_IfStatement()
		{
			// Arrange
			TokenBooleanValue conditionalExpression = new TokenBooleanValue(true, 1, 1);

			// Make a body
			TokenBlock bodyBlock = new TokenBlock(1, 2);
			TokenAssignment assignment = new TokenAssignment("destVar1", 5, 10);
			assignment.AddChild(new TokenVariableReference("sourceVar1", 20, 25));
			bodyBlock.AddChild(assignment);

			// Make an ElseIf statement
			List<Tuple<TokenBase, TokenBase>> elseIfTokens = new List<Tuple<TokenBase, TokenBase>>();
			TokenBase elseIfExpression = new TokenBooleanValue(true, 1, 1);;

			// Make a ElseIf Body
			TokenBlock elseIfBody = new TokenBlock(1, 2);
			assignment = new TokenAssignment("destVar2", 5, 10);
			assignment.AddChild(new TokenVariableReference("sourceVar2", 20, 25));
			elseIfBody.AddChild(assignment);

			elseIfTokens.Add(new Tuple<TokenBase, TokenBase>(elseIfExpression, elseIfBody));

			// Make an Else body
			TokenBlock elseBody = new TokenBlock(1, 2);
			assignment = new TokenAssignment("destVar3", 5, 10);
			assignment.AddChild(new TokenVariableReference("sourceVar3", 20, 25));
			elseBody.AddChild(assignment);

			TokenIf token = new TokenIf(
				elseIfBody,
				conditionalExpression,
				elseIfTokens,
				elseBody,
				1,
				2);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_ParameterDeclaration()
		{
			// Arrange
			TokenParameterDeclaration token = new TokenParameterDeclaration("paramName1", 2, 4);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		[Test]
		[Category("Success")]
		public void SerializeDeserialize_ParameterDeclarationList()
		{
			// Arrange
			TokenParameterDeclaration param1 = new TokenParameterDeclaration("paramName1", 2, 4);
			TokenParameterDeclaration param2 = new TokenParameterDeclaration("paramName2", 2, 8);
			TokenParameterDeclarationList token = new TokenParameterDeclarationList(new []{param1, param2}, 2, 4);

			// Act
			byte[] serializedData = TokenSerializer.SerializeToArray(token);

			// Assert
			VerifySerialization(token,
				serializedData);
		}

		#region Private Methods

		private static void VerifySerialization(
			TokenBase token,
			byte[] serializedData)
		{
			TokenBase reconstitutedToken =
				(TokenBase)TokenSerializer.DeserializeFromArray(serializedData);

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
