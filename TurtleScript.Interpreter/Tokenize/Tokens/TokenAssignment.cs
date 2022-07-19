#region Namespaces

using System.Diagnostics;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenAssignment : TokenBase
	{

		#region Public Constructors

		public TokenAssignment(string variableName)
			: base(TokenType.Assignment)
		{
			m_VariableName = variableName;
		}

		public TokenAssignment(
			string variableName,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.Assignment,
				lineNumber,
				charPositionInLine)
		{
			m_VariableName = variableName;
		}

		#endregion Public Constructors


		#region Public Properties

		public string VariableName
		{
			[DebuggerStepThrough]
			get { return m_VariableName; }
		}

		#endregion Public Properties


		#region Public Methods

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

		#endregion Public Methods


		#region Private Fields

		private readonly string m_VariableName;

		#endregion Private Fields
	}
}