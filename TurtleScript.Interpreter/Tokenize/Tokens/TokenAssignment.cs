using System.Diagnostics;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenAssignment : TokenBase
	{
		public TokenAssignment(string variableName)
			: base(TokenType.Assignment)
		{
			VariableName = variableName;
		}

		public string VariableName
		{
			[DebuggerStepThrough]
			get;
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