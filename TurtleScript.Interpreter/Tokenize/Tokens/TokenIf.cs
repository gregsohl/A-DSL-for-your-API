﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TurtleScript.Interpreter.Tokenize.Execute;

namespace TurtleScript.Interpreter.Tokenize
{
	public class TokenIf : TokenBase
	{
		private readonly TokenBlock m_Block;
		private readonly TokenBase m_ConditionalExpression;
		private readonly List<Tuple<TokenBase, TokenBase>> m_ElseIf;
		private readonly TokenBase m_ElseStatement;

		public TokenIf(
			TokenBlock block,
			TokenBase conditionalExpression,
			List<Tuple<TokenBase, TokenBase>> elseIf,
			TokenBase elseStatement,
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

		public override string ToTurtleScript()
		{
			string conditionalExpressionTurtleScript = ConditionalExpression.ToTurtleScript();
			string block = Block.ToTurtleScript();
			int indent = 0;

			StringBuilder turtleScript = new StringBuilder($"if ({conditionalExpressionTurtleScript}) Do\r\n");
			Block.ToTurtleScript(turtleScript, indent);
			// AppendBlockToStatementScript(block, turtleScript, indent);

			foreach (Tuple<TokenBase, TokenBase> elseIf in ElseIf)
			{
				indent++;
				turtleScript.AppendLine($"{new string('\t', indent)}elseif ({conditionalExpressionTurtleScript}) Do");
				//elseIf.Item2.ToTurtleScript(turtleScript, indent);
				//AppendBlockToStatementScript(elseIf.Item2, turtleScript, indent);
			}

			if (ElseStatement != null)
			{
				turtleScript.AppendLine($"{new string('\t', indent)}else");
				//AppendBlockToStatementScript(ElseStatement.ToTurtleScript(), turtleScript, indent);
			}

			turtleScript.AppendLine("end");

			return turtleScript.ToString();
		}

		//private static void AppendBlockToStatementScript(string block, StringBuilder turtleScript, int indent)
		//{
		//	var blockLines = Regex.Split(block, "\r\n|\r|\n");

		//	foreach (string blockLine in blockLines)
		//	{
		//		turtleScript.AppendLine(new string('\t', indent + 1) + blockLine);
		//	}
		//}

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
	}
}