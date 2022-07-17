#region Namespaces

using System;
using System.Diagnostics;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenFunctionDeclaration : TokenBase
	{
		#region Public Constructors

		static TokenFunctionDeclaration()
		{
			m_Default = new TokenFunctionDeclaration(
				"",
				Array.Empty<string>(),
				null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionDeclaration(string functionName, string[] parameterNames, TokenBlock functionBody = null)
			: base(TokenType.FunctionDecl)
		{
			m_FunctionName = functionName;
			m_ParameterNames = parameterNames;
			m_FunctionBody = functionBody;
		}

		#endregion Public Constructors


		#region Public Properties

		public new static TokenFunctionDeclaration Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public TokenBlock FunctionBody
		{
			get { return m_FunctionBody; }
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public int ParameterCount
		{
			get { return ParameterNames.Length; }
		}
		public string[] ParameterNames
		{
			get { return m_ParameterNames; }
		}

		#endregion Public Properties


		#region Public Methods

		public override string ToTurtleScript()
		{
			string parameters = String.Join(", ", ParameterNames);

			return $"{FunctionName}({parameters})\r\n{FunctionBody.ToTurtleScript(1)}\r\nend";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			context.DeclareFunction(this);
			return TurtleScriptValue.VOID;
		}

		#endregion Public Methods


		#region Internal Methods

		internal void SetFunctionBody(
			TokenBlock functionBody)
		{
			m_FunctionBody = functionBody;
		}

		#endregion Internal Methods


		#region Private Fields

		private static TokenFunctionDeclaration m_Default;
		private readonly string m_FunctionName;
		private readonly string[] m_ParameterNames;
		private TokenBlock m_FunctionBody;

		#endregion Private Fields
	}
}
