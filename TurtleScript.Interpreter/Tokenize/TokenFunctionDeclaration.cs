using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenFunctionDeclaration : TokenBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionDeclaration(string functionName, string[] parameterNames, TokenBlock functionBody)
			: base(TokenType.FunctionDecl)
		{
			FunctionName = functionName;
			ParameterNames = parameterNames;
			FunctionBody = functionBody;
		}

		public string FunctionName { get; }

		public string[] ParameterNames { get; }

		public TokenBlock FunctionBody { get; }

		public override string ToTurtleScript()
		{
			string parameters = String.Join(", ", ParameterNames);

			return $"{FunctionName}({parameters})\r\n{FunctionBody.ToTurtleScript()}";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return TurtleScriptValue.VOID;
		}
	}
}
