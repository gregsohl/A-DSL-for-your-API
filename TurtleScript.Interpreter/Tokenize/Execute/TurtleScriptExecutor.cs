namespace TurtleScript.Interpreter.Tokenize.Execute
{
	public class TurtleScriptExecutor
	{
		public void Execute(TokenBase script, TurtleScriptExecutionContext context)
		{
			script.Visit(context);

		}
	}
}
