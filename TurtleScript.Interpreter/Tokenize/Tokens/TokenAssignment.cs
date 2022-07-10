using System.Diagnostics;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenAssignment : TokenBase
	{
		private readonly string m_VariableName;

		public TokenAssignment(string variableName)
			: base(TokenType.Assignment)
		{
			m_VariableName = variableName;
		}

		public string VariableName
		{
			[DebuggerStepThrough]
			get { return m_VariableName; }
		}

		public override string ToTurtleScript()
		{
			return $"{VariableName} = {Children[0].ToTurtleScript()}";
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = Children[0].Visit(context);

			context.SetVariableValue(VariableName, VariableType.Variable, this, variableValue);

			return TurtleScriptValue.VOID;
		}
	}
}