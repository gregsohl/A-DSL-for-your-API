grammar TurtleScript;

/*
 * Parser Rules
 */

compileUnit
	:	EOF
	;

/*
 * Lexer Rules
 */

WS
	:	[ \t\r\n]+ -> channel(HIDDEN)
	;
