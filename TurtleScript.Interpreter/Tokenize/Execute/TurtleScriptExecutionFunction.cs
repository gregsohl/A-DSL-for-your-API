﻿#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;

using Antlr4.Runtime.Tree;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize.Execute
{
	internal class TurtleScriptExecutionFunction : TurtleScriptFunctionBase
	{
		public TurtleScriptExecutionFunction(
			string name,
			int parameterCount,
			TokenFunctionDeclaration declaration)
		: base(name, parameterCount)
		{
			m_Declaration = declaration;
		}

		public TokenFunctionDeclaration Declaration
		{
			[DebuggerStepThrough]
			get { return m_Declaration; }
		}

		private readonly TokenFunctionDeclaration m_Declaration;
	}
}
