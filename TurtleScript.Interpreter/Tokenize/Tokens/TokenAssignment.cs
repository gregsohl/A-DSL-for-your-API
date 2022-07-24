#region Namespaces

using System.Diagnostics;
using System.IO;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenAssignment : TokenBase
	{

		#region Public Constructors

		public TokenAssignment()
		{
		}

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

		/// <summary>
		/// This function is invoked by CompactFormatter when deserializing a
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be read</param>
		public override void ReceiveObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.ReceiveObjectData(
				parent,
				stream);
			
			int version = (int)parent.Deserialize(stream);
			m_VariableName = (string)parent.Deserialize(stream);
		}

		/// <summary>
		/// This function is invoked by CompactFormatter when serializing a 
		/// Custom Serializable object.
		/// </summary>
		/// <param name="parent">A reference to the CompactFormatter instance which called this method.</param>
		/// <param name="stream">The Stream where object data must be written</param>
		public override void SendObjectData(
			CompactFormatter.CompactFormatter parent,
			Stream stream)
		{
			base.SendObjectData(
				parent,
				stream);
		
			parent.Serialize(stream, VERSION);
			parent.Serialize(stream, m_VariableName);
		}

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.AppendWithIndent($"{VariableName} = ");
			Children[0].ToTurtleScript(builder);
			builder.AppendLine();
			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			var variableValue = Children[0].Visit(context);

			context.SetVariableValue(VariableName, VariableType.Variable, this, variableValue);

			return TurtleScriptValue.VOID;
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		private string m_VariableName;

		#endregion Private Fields
	}
}