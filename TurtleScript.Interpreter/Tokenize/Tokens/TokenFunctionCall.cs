#region Namespaces

using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	internal class TokenFunctionCall : TokenBase
	{

		#region Public Constructors

		static TokenFunctionCall()
		{
			m_Default = new TokenFunctionCall(
				string.Empty,
				Array.Empty<TokenBase>());
		}

		public TokenFunctionCall()
		{
		}

		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters)
			: this(
				functionName,
				parameters,
				0,
				0)
		{
			m_FunctionName = functionName;
			m_Parameters = parameters;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.FunctionDecl,
				lineNumber,
				charPositionInLine)
		{
			m_FunctionName = functionName;
			m_Parameters = parameters;
		}

		#endregion Public Constructors


		#region Public Properties

		public new static TokenFunctionCall Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public TokenBase[] Parameters
		{
			get { return m_Parameters; }
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

			m_FunctionName = (string)parent.Deserialize(stream);
			m_Parameters = (TokenBase[])parent.Deserialize(stream);
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

			parent.Serialize(stream, m_FunctionName);
			parent.Serialize(stream, m_Parameters);

		}

		public override string ToTurtleScript()
		{
			StringBuilder result = new StringBuilder(FunctionName + "(");

			for (var index = 0; index < Parameters.Length; index++)
			{
				var parameter = Parameters[index];
				result.Append(parameter.ToTurtleScript());
				if (index < Parameters.Length - 1)
				{
					result.Append(',');
				}
			}

			result.Append(')');

			return result.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue result = TurtleScriptValue.NULL;

			// Find the function, if it exists.
			if (context.TryGetFunction(
					m_FunctionName,
					m_Parameters.Length,
					out TokenFunctionDeclaration function))
			{
				context.PushScope(m_FunctionName);

				for (var parameterIndex = 0; parameterIndex < m_Parameters.Length; parameterIndex++)
				{
					TokenBase parameter = m_Parameters[parameterIndex];
					string parameterName = function.ParameterNames[parameterIndex];
					TurtleScriptValue parameterValue = parameter.Visit(context);
					context.SetVariableValue(parameterName, VariableType.Parameter, null, parameterValue);
				}

				result = function.FunctionBody.Visit(context);

				context.PopScope();
			}

			// Push scope onto the context
			// Register parameter values in the context
			// Call the function body
			//context.G

			return result;
		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		[CompactFormatter.Attributes.NotSerialized]
		private static TokenFunctionCall m_Default;

		private string m_FunctionName;
		private TokenBase[] m_Parameters;

		#endregion Private Fields
	}
}
