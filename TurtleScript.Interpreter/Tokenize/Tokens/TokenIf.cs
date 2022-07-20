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
	[CompactFormatter.Attributes.Serializable(Custom = true)]
	public class TokenIf : TokenBase
	{

		#region Public Constructors

		public TokenIf()
		{
		}

		public TokenIf(
			TokenBlock block,
			TokenBase conditionalExpression,
			List<Tuple<TokenBase, TokenBase>> elseIf,
			TokenBlock elseStatement,
			int lineNumber,
			int charPositionInLine)
			: base(TokenType.If,
				lineNumber,
				charPositionInLine)
		{
			m_Block = block;
			m_ConditionalExpression = conditionalExpression;
			m_ElseIf = elseIf;
			m_ElseStatement = elseStatement;
		}

		#endregion Public Constructors

		#region Public Properties

		public TokenBlock Block
		{
			[DebuggerStepThrough]
			get { return m_Block; }
		}

		public TokenBase ConditionalExpression
		{
			[DebuggerStepThrough]
			get { return m_ConditionalExpression; }
		}

		public List<Tuple<TokenBase, TokenBase>> ElseIf
		{
			[DebuggerStepThrough]
			get { return m_ElseIf; }
		}

		public TokenBase ElseStatement
		{
			[DebuggerStepThrough]
			get { return m_ElseStatement; }
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

			m_Block = (TokenBlock)parent.Deserialize(stream);
			m_ConditionalExpression = (TokenBase)parent.Deserialize(stream);

			int elseIfCount = (int)parent.Deserialize(stream);

			m_ElseIf = new List<Tuple<TokenBase, TokenBase>>();
			for (int index = 0; index < elseIfCount; index++)
			{
				TokenBase item1 = (TokenBase)parent.Deserialize(stream);
				TokenBase item2 = (TokenBase)parent.Deserialize(stream);

				m_ElseIf.Add(new Tuple<TokenBase, TokenBase>(item1, item2));
			}

			m_ElseStatement = (TokenBlock)parent.Deserialize(stream);

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

			parent.Serialize(stream, m_Block);
			parent.Serialize(stream, m_ConditionalExpression);

			parent.Serialize(stream, m_ElseIf.Count);

			foreach (Tuple<TokenBase, TokenBase> statement in m_ElseIf)
			{
				parent.Serialize(stream, statement.Item1);
				parent.Serialize(stream, statement.Item2);
			}

			parent.Serialize(stream, m_ElseStatement);
		}

		public override string ToTurtleScript()
		{
			string conditionalExpressionTurtleScript = ConditionalExpression.ToTurtleScript();
			string block = Block.ToTurtleScript();
			int indent = 0;

			StringBuilder turtleScript = new StringBuilder($"if ({conditionalExpressionTurtleScript}) Do\r\n");
			Block.ToTurtleScript(turtleScript, indent);
			turtleScript.AppendLine();

			foreach (Tuple<TokenBase, TokenBase> elseIf in ElseIf)
			{
				indent++;
				turtleScript.AppendLine($"{new string('\t', indent)}elseif ({elseIf.Item1.ToTurtleScript()}) Do");
				turtleScript.AppendLine(elseIf.Item2.ToTurtleScript());
			}

			if (ElseStatement != null)
			{
				turtleScript.AppendLine($"{new string('\t', indent)}else");
				turtleScript.AppendLine($"{new string('\t', indent)}{ElseStatement.ToTurtleScript()}");
			}

			turtleScript.AppendLine("end");

			return turtleScript.ToString();
		}

		public override TurtleScriptValue Visit(TurtleScriptExecutionContext context)
		{
			TurtleScriptValue ifResult = ConditionalExpression.Visit(context);

			if ((ifResult.IsBoolean) &&
				(ifResult.BooleanValue))
			{
				Block.Visit(context);
				return TurtleScriptValue.VOID;
			}

			foreach (Tuple<TokenBase, TokenBase> elseIf in ElseIf)
			{
				TurtleScriptValue elseIfResult = elseIf.Item1.Visit(context);

				if ((elseIfResult.IsBoolean) &&
					(elseIfResult.BooleanValue))
				{
					elseIf.Item2.Visit(context);
					return TurtleScriptValue.VOID;
				}
			}

			if (ElseStatement != null)
			{
				ElseStatement.Visit(context);
			}

			return TurtleScriptValue.VOID;

		}

		#endregion Public Methods

		#region Private Constants

		private const int VERSION = 1;

		#endregion Private Constants

		#region Private Fields

		private TokenBlock m_Block;
		private TokenBase m_ConditionalExpression;
		private List<Tuple<TokenBase, TokenBase>> m_ElseIf;
		private TokenBlock m_ElseStatement;

		#endregion Private Fields

	}
}