#region Namespaces

using System;
using System.Diagnostics;
using System.IO;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces

namespace TurtleScript.Interpreter.Tokenize
{
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenFunctionDeclaration : TokenBase
	{
		#region Public Constructors

		static TokenFunctionDeclaration()
		{
			m_Default = new TokenFunctionDeclaration(
				"",
				Array.Empty<string>());
		}

		public TokenFunctionDeclaration()
		{
		}

		public TokenFunctionDeclaration(
			string functionName,
			string[] parameterNames)
			: this(
				functionName,
				parameterNames,
				0,
				0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionDeclaration(
			string functionName,
			string[] parameterNames,
			int lineNumber,
			int charPositionInLine)
			: base(
				TokenType.FunctionDecl,
				lineNumber,
				charPositionInLine)
		{
			m_FunctionName = functionName;
			m_ParameterNames = parameterNames;
		}

		#endregion Public Constructors


		#region Public Properties

		public new static TokenFunctionDeclaration Default
		{
			[DebuggerStepThrough]
			get { return m_Default; }
		}

		public TokenBlock FunctionBody
		{
			get { return m_FunctionBody; }
		}

		public string FunctionName
		{
			get { return m_FunctionName; }
		}

		public int ParameterCount
		{
			get { return ParameterNames.Length; }
		}
		public string[] ParameterNames
		{
			get { return m_ParameterNames; }
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
			m_ParameterNames = (string[])parent.Deserialize(stream);
			m_FunctionBody = (TokenBlock)parent.Deserialize(stream);
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
			parent.Serialize(stream, m_ParameterNames);
			parent.Serialize(stream, m_FunctionBody);
		}

		public override string ToTurtleScript(TurtleScriptBuilder builder)
		{
			string parameters = String.Join(", ", ParameterNames);

			builder.AppendLine($"def {FunctionName}({parameters})");

			builder.IncrementNestingLevel();

			m_FunctionBody.ToTurtleScript(builder);

			builder.DerementNestingLevel();

			builder.AppendLine("end");

			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			context.DeclareFunction(this);
			return TurtleScriptValue.VOID;
		}

		#endregion Public Methods


		#region Internal Methods

		internal void SetFunctionBody(
			TokenBlock functionBody)
		{
			m_FunctionBody = functionBody;
		}

		#endregion Internal Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants


		#region Private Fields

		[CompactFormatter.Attributes.NotSerialized]
		private static TokenFunctionDeclaration m_Default;

		private string m_FunctionName;
		private string[] m_ParameterNames;
		private TokenBlock m_FunctionBody;

		#endregion Private Fields
	}
}
