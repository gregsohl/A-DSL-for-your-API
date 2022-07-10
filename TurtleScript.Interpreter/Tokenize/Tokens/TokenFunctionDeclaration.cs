#region Namespaces

using System;
using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	internal class TokenFunctionDeclaration : TokenBase
	{

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionDeclaration(string functionName, string[] parameterNames, TokenBlock functionBody)
			: base(TokenType.FunctionDecl)
		{
			m_FunctionName = functionName;
			m_ParameterNames = parameterNames;
			m_FunctionBody = functionBody;

			m_Key += functionName + "_" + parameterNames.Length;

		}

		public int ParameterCount
		{
			get { return ParameterNames.Length; }
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public string[] ParameterNames
		{
			get { return m_ParameterNames; }
		}

		public TokenBlock FunctionBody
		{
			get { return m_FunctionBody; }
		}

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
			// Register the function with the context

			return FunctionBody.Visit(context);
		}

		private readonly string m_Key;
		private readonly string m_FunctionName;
		private readonly string[] m_ParameterNames;
		private readonly TokenBlock m_FunctionBody;
	}
}
