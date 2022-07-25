#region Namespaces

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using TurtleScript.Interpreter.Tokenize.Execute;

#endregion Namespaces


namespace TurtleScript.Interpreter.Tokenize
{
	public enum FunctionType
	{
		Runtime,
		UserDefined,
	}

	[CompactFormatter.Attributes.Serializable(Custom = true)]
	internal class TokenFunctionCall : TokenBase
	{

		#region Public Constructors

		static TokenFunctionCall()
		{
			m_Default = new TokenFunctionCall(
				string.Empty,
				Array.Empty<TokenBase>(),
				0,
				0);
		}

		public TokenFunctionCall()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters,
			int lineNumber,
			int charPositionInLine)
			: this(
				functionName,
				parameters,
				FunctionType.UserDefined,
				lineNumber,
				charPositionInLine)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
		public TokenFunctionCall(
			string functionName,
			TokenBase[] parameters,
			FunctionType functionType,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.FunctionCall,
				lineNumber,
				charPositionInLine)
		{
			m_FunctionName = functionName;
			m_Parameters = parameters;
			m_FunctionType = functionType;
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

		public override string ToTurtleScript(
			TurtleScriptBuilder builder)
		{
			builder.Append($"{m_FunctionName}(");

			for (var index = 0; index < Parameters.Length; index++)
			{
				var parameter = Parameters[index];
				parameter.ToTurtleScript(builder);
				if (index < Parameters.Length - 1)
				{
					builder.Append(", ");
				}
			}

			builder.AppendLine(")");

			return builder.Text;
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue result = TurtleScriptValue.NULL;

			if (m_FunctionType == FunctionType.UserDefined)
			{
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
			}

			if (m_FunctionType == FunctionType.Runtime)
			{
				string[] identifierParts = m_FunctionName.Split('.');

				string runtimeName = identifierParts[0];
				string functionName = identifierParts[1];


				if (context.TryGetRuntimeLibrary(
						runtimeName,
						out ITurtleScriptRuntime runtime))
				{
					TurtleScriptRuntimeFunction function;
					if (context.TryGetRuntimeFunction(
							runtime,
							functionName,
							m_Parameters.Length,
							out function))
					{

						List<TurtleScriptValue> functionParameters = new List<TurtleScriptValue>();

						for (var parameterIndex = 0; parameterIndex < m_Parameters.Length; parameterIndex++)
						{
							TokenBase parameter = m_Parameters[parameterIndex];
							TurtleScriptValue parameterValue = parameter.Visit(context);
							functionParameters.Add(parameterValue);
						}

						result = function.Function(functionParameters);
					}
					else
					{
						throw new TurtleScriptExecutionException(
							$"Invalid runtime function name '{m_FunctionName}' specified on function call. Line {LineNumber}, Column {CharPositionInLine}");
					}
				}
				else
				{
					throw new TurtleScriptExecutionException(
						$"Invalid runtime library name '{runtimeName}' specified on function call. Line {LineNumber}, Column {CharPositionInLine}");
				}
			}

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
		private readonly FunctionType m_FunctionType;

		#endregion Private Fields
	}
}
