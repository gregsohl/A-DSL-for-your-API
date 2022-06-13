﻿namespace TurtleScript.Interpreter.Tokenize
{
	public enum TokenType
	{
		Script,
		Block,
		Boolean,
		Numeric,
		NullValue,
		Assignment,
		OpAdd,
		OpSubtract,
		Parenthesized,
		OpMultiply,
		OpDivide,
		OpModulus,
		VariableReference,
		If,
		OpEqual,
		OpNotEqual,
		OpGreaterThan,
		OpLessThan,
		OpGreaterThanOrEqual,
		OpLessThanOrEqual,
		OpConditionalAnd,
		OpConditionalOr,
		Pi,
		ForStatement,
		OpUnaryNot,
		OpUnaryNegation,
	}
}