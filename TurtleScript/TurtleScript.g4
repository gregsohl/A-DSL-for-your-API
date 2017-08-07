grammar TurtleScript;

/*
	Simple scripting language for basic math and function calls
*/

/*
 * Parser Rules
 */

script
	: block EOF
	;

block
 : (statement | functionDecl)* (Return expression)?
 ;

statement
 : assignment (Semicolon)?
 | functionCall (Semicolon)?
 | ifStatement (Semicolon)?
 | forStatement (Semicolon)?
 ;

assignment
 : Identifier Assign expression
 ;

functionCall
 : (Identifier|QualifiedIdentifier) OpenParen expressionList? CloseParen
 ;

ifStatement
 : ifStat elseIfStat* elseStat? End
 ;

ifStat
 : If expression Do block
 ;

elseIfStat
 : Else If expression Do block
 ;

elseStat
 : Else Do block
 ;

functionDecl
 : Def Identifier OpenParen formalParameters? CloseParen block End
 ;

formalParameters
	:   formalParameter (',' formalParameter)*
	;

formalParameter
	:   Identifier
	;

expressionList 
	: expression (',' expression)* ;   // arg list

forStatement
 : For Identifier '=' expression To expression Do block End
 ;

expression 
	:  functionCall						                 #functionCallExpression			// Call to a built-in or user-defined function
	|	'-'expression					                 #unaryNegationExpression	        // unary negation
	|	'!'expression					                 #unaryNotExpression	            // unary not
	|	expression op=(MUL|DIV|MOD) expression	         #multiplicativeOpExpression	    // MultiplicativeOperation
	|	expression op=(ADD|SUB) expression	             #additiveExpression			    // AdditiveOperation
	|	expression op=(EQ|NE|GT|GE|LT|LE) expression     #compareExpression		            // Comparison Operations
	|	expression '&&' expression                       #andExpression			            // Logical AND
	|	expression '||' expression                       #orExpression	                    // Logical OR
	|   Pi                                               #piExpression						// PI constant
	|	Identifier	                                     #variableReferenceExpression       // Variable Reference
	|	Float										     #floatExpression                   // Floating Point Number
	|	Int										         #intExpression                     // Integer Number
	|	'(' expression ')'				                 #parenExpression				    // Parenthesized Expression
	;


/*
 * Lexer Rules
 */

If      : 'if';
Else    : 'else';
Return  : 'return';
For     : 'for';
To      : 'to';
Do      : 'do';
End     : 'end';
Def		: 'def';


MUL :   '*' ;
DIV :   '/' ;
MOD :   '%' ;
ADD :   '+' ;
SUB :   '-' ;
EQ  :   '==';
NE  :   '!=';
GT  :   '>';
LT  :   '<';
GE  :   '>=';
LE  :   '<=';
AND :   '&&';
OR  :   '||';

Assign : '=';

OpenParen  : '(';
CloseParen : ')';

Semicolon : ';';

Float
	:   Digit+ '.' Digit*
	|   '.' Digit+ 
	;

Int :   [0-9]+ ;

Pi	:	'pi';

//STRING
// : '"' (~["\r\n] | '""')* '"'
// ;

Identifier  :   Letter (Letter | [0-9])* ;

QualifiedIdentifier  :   Identifier '.' Identifier ;

fragment
Letter : [a-zA-Z] ;

fragment
Digit : [0-9] ; // match single Digit


WS
	:	[ \t\r\n]+ -> channel(HIDDEN)
	;
