using System.Collections.Generic;
using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBlock : TokenBase
	{
		public TokenBlock()
			: base(TokenType.Block)
		{
			m_FunctionDeclarations = new List<TokenFunctionDeclaration>();
			m_ChildrenAndFunctions = new List<TokenBase>();
		}

		//public TokenBlock(TokenType tokenType, TokenBase block)
		//	: base(tokenType)
		//{
		//}

		public override void AddChild(TokenBase token)
		{
			if (token is TokenFunctionDeclaration functionDeclaration)
			{
				m_FunctionDeclarations.Add(functionDeclaration);
				base.AddChild(token);
			}
			else
			{
				base.AddChild(token);
			}

			m_ChildrenAndFunctions.Add(token);
		}

		public override StringBuilder ToTurtleScript(StringBuilder result, int indentLevel)
		{
			if (Children != null)
			{
				foreach (var child in Children)
				{
					result.AppendLine(Indent(indentLevel) + child.ToTurtleScript());
				}
			}

			if (result.Length >= 2)
			{
				if ((result[result.Length - 2] == '\r') &&
					(result[result.Length - 1] == '\n'))
				{
					result.Length -= 2;
				}
			}

			return result;
		}

		public override string ToTurtleScript()
		{
			return ToTurtleScript(0);
		}

		public override string ToTurtleScript(int indentLevel)
		{
			StringBuilder result = new StringBuilder();

			ToTurtleScript(result, indentLevel);

			return result.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			foreach (var token in Children)
			{
				token.Visit(context);
			}

			return TurtleScriptValue.VOID;
		}

		private readonly List<TokenFunctionDeclaration> m_FunctionDeclarations;
		private readonly List<TokenBase> m_ChildrenAndFunctions;
	}
}
