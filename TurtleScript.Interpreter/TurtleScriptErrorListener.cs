#region Namespaces

using Antlr4.Runtime;

#endregion Namespaces

namespace TurtleScript.Interpreter
{
	public class TurtleScriptErrorListener : IAntlrErrorListener<IToken>
	{
		public string Message { get; private set; }

		public void SyntaxError(
			IRecognizer recognizer,
			IToken offendingSymbol,
			int line,
			int charPositionInLine,
			string msg,
			RecognitionException e)
		{
			Message = string.Format("{0}. Line {1}, Col {2}", msg, line, charPositionInLine);
		}
	}
}