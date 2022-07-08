#region Namespaces

using System.Collections.Generic;

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

		public List<IParseTree> Parameters
		{
			get { return m_Parameters; }
		}

		public IParseTree Body
		{
			get { return m_Body; }
		}

		private readonly List<IParseTree> m_Parameters;
		private readonly IParseTree m_Body;
	}
}
