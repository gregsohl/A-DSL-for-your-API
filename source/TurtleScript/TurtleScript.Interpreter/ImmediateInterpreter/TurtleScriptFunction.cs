#region Namespaces

using System.Collections.Generic;
using System.Diagnostics;

using Antlr4.Runtime.Tree;

#endregion Namespaces

namespace TurtleScript.Interpreter.ImmediateInterpreter
{
	internal class TurtleScriptFunction : TurtleScriptFunctionBase
	{
		public TurtleScriptFunction(
			string name,
			IParseTree[] parameters,
			IParseTree body)
			: base(name, parameters.Length)
		{
			m_Parameters = new List<IParseTree>(parameters);
			m_Body = body;
		}

		public int ParametersCount
		{
			[DebuggerStepThrough]
			get { return m_Parameters.Count; }
		}

		public List<IParseTree> Parameters
		{
			[DebuggerStepThrough]
			get { return m_Parameters; }
		}

		public IParseTree Body
		{
			[DebuggerStepThrough]
			get { return m_Body; }
		}

		public void AddParametersAndBody(
			IParseTree[] parameters,
			IParseTree body)
		{
			if ((m_Parameters == null) &&
				(m_Body == null))
			{
				m_Parameters = new List<IParseTree>(parameters);
				m_Body = body;
			}
		}

		private List<IParseTree> m_Parameters;
		private IParseTree m_Body;
	}
}
