using System;
using Antlr4.Runtime;

namespace TurtleScript.Interpreter
{
	public class TurtleScriptInterpreter
		: TurtleScriptBaseVisitor<TurtleScriptValue>
	{
		#region Constructor

		public TurtleScriptInterpreter(string script)
		{
			m_Script = script;
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
		}

		#endregion Constructor

		#region Public Properties

		public string ErrorMessage
		{
			get { return m_TurtleScriptErrorListener.Message; }
		}

		public bool IsError
		{
			get { return !string.IsNullOrEmpty(m_TurtleScriptErrorListener.Message); }
		}
		#endregion Public Properties

		#region Public Properties

		public void Execute()
		{
			AntlrInputStream input = new AntlrInputStream(m_Script);

			TurtleScriptLexer lexer = new TurtleScriptLexer(input);

			CommonTokenStream tokenStream = new CommonTokenStream(lexer);

			TurtleScriptParser parser = new TurtleScriptParser(tokenStream);

			parser.RemoveErrorListeners();
			m_TurtleScriptErrorListener = new TurtleScriptErrorListener();
			parser.AddErrorListener(m_TurtleScriptErrorListener);

			parser.BuildParseTree = true;

			Visit(parser.script());
		}
		#region Private Fields

		private readonly string m_Script;
		private TurtleScriptErrorListener m_TurtleScriptErrorListener;

		#endregion Private Fields
	}
}