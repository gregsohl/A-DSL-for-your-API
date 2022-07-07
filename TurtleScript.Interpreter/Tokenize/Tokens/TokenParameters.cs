using System.Text;

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenParameterDeclaration : TokenBase
	{
		public TokenParameterDeclaration(string parameterName)
			:base(TokenType.Parameter)
		{
			ParameterName = parameterName;
		}

		public string ParameterName { get; }

		public override string ToTurtleScript()
		{
			return ParameterName;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}
	}

	internal class TokenParameterDeclarationList : TokenBase
	{
		public TokenParameterDeclarationList(TokenParameterDeclaration[] parameters)
			: base(TokenType.ParameterList)
		{
			Parameters = parameters;
		}

		public TokenParameterDeclaration[] Parameters { get; }

		public override string ToTurtleScript()
		{
			StringBuilder parameterList = new StringBuilder();

			for (var index = 0; index < Parameters.Length; index++)
			{
				TokenParameterDeclaration parameterDeclaration = Parameters[index];
				parameterList.Append(parameterDeclaration.ToTurtleScript());
				if (index < Parameters.Length - 1)
				{
					parameterList.Append(", ");
				}
			}

			return parameterList.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.NULL;
		}

	}
}
