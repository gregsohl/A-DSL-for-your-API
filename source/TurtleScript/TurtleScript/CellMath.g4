grammar CellMath;

expr 
	:  '-'expr					#UnaryNegation	// unary minus
	|   expr op=('*'|'/') expr	#MultiplicativeOp	// MultiplicativeOperation
	|   expr op=('+'|'-') expr	#AdditiveOp			// AdditiveOperation
	|	FLOAT					#Float			// Floating Point Number
	|   INT						#Integer		// Integer Number
	|   '(' expr ')'			#ParenExpr
	;

MUL :   '*' ;
DIV :   '/' ;
ADD :   '+' ;
SUB :   '-' ;

FLOAT
	:   DIGIT+ '.' DIGIT*
	|   '.' DIGIT+ 
	;

// FLOAT :  ([0\.]|[1-9]+)'.'DIGIT* ;

INT :    DIGIT+ ;

fragment
DIGIT : [0-9] ; // match single digit

WS :     [ \t\r\n]+ -> skip ;

