using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenParameterDeclaration : TokenBase
	{
		private readonly string m_ParameterName;

		public TokenParameterDeclaration(string parameterName)
			:base(TokenType.Parameter)
		{
			m_ParameterName = parameterName;
		}

		public string ParameterName
		{
			get { return m_ParameterName; }
		}

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
		private readonly TokenParameterDeclaration[] m_Parameters;

		public TokenParameterDeclarationList(TokenParameterDeclaration[] parameters)
			: base(TokenType.ParameterList)
		{
			m_Parameters = parameters;
		}

		public TokenParameterDeclaration[] Parameters
		{
			get { return m_Parameters; }
		}

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
