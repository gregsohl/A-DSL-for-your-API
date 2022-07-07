#region Namespaces

using System;
using System.Text;

#endregion Namespaces

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

			StringBuilder result = new StringBuilder();
			result.AppendLine($"{FunctionName}({parameters})");

			result.AppendLine("end");

			return $"{FunctionName}({parameters})\r\n{FunctionBody.ToTurtleScript()}\r\nend";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			return FunctionBody.Visit(context);
		}
	}
}
