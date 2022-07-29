using System;

namespace TurtleScript.Interpreter.Tokenize.Execute
{
	public class TurtleScriptExecutor
	{
		public void Execute(TokenBase script, TurtleScriptExecutionContext context)
		{
			try
			{
				m_ErrorMessage = String.Empty;

				script.Visit(context);
			}
			catch (TurtleScriptExecutionException executionException)
			{
				m_ErrorMessage = executionException.Message;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return m_ErrorMessage;
			}
		}

		public bool IsError
		{
			get
			{
				return !string.IsNullOrEmpty(m_ErrorMessage);
			}
		}


		private string m_ErrorMessage;

	}
}
