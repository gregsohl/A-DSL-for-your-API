using System.Text;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenBlock : TokenBase
	{
		public TokenBlock()
			: base(TokenType.Block)
		{
		}

		public TokenBlock(TokenType tokenType, TokenBase block)
			: base(tokenType)
		{
		}



		public override string ToTurtleScript()
		{
			StringBuilder result = new StringBuilder();
			foreach (var child in Children)
			{
				result.AppendLine(child.ToTurtleScript());
			}

			if (result.Length >= 2)
			{
				if ((result[result.Length - 2] == '\r') &&
					(result[result.Length - 1] == '\n'))
				{
					result.Length -= 2;
				}
			}

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

	}
}
