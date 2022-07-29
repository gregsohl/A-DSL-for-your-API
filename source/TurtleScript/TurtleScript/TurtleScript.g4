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
 : IF expression Do block
 ;

elseIfStat
 : Else IF expression Do block
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
	:  functionCall						                 #functionCallExpression			// Call to a runtime or user-defined function
	|	'-'expression					                 #unaryNegationExpression	        // unary negation
	|	'!'expression					                 #unaryNotExpression	            // unary not
	|	expression op=(Mul|Div|Mod) expression	         #multiplicativeOpExpression	    // MultiplicativeOperation
	|	expression op=(Add|Sub) expression	             #additiveExpression			    // AdditiveOperation
	|	expression op=(EQ|NE|GT|GE|LT|LE) expression     #compareExpression		            // Comparison Operations
	|	expression And expression                        #andExpression			            // Logical AND
	|	expression Or expression                         #orExpression	                    // Logical OR
	|   Pi                                               #piExpression						// PI constant
	|	Identifier	                                     #variableReferenceExpression       // Variable Reference
	|	Float										     #floatExpression                   // Floating Point Number
	|	Int										         #intExpression                     // Integer Number
	|	'(' expression ')'				                 #parenExpression				    // Parenthesized Expression
	;


/*
 * Lexer Rules
 */

IF      : 'if';
Else    : 'else';
Return  : 'return';
For     : 'for';
To      : 'to';
Do      : 'do';
End     : 'end';
Def		: 'def';


Mul :   '*' ;
Div :   '/' ;
Mod :   '%' ;
Add :   '+' ;
Sub :   '-' ;
EQ  :   '==';
NE  :   '!=';
GT  :   '>';
LT  :   '<';
GE  :   '>=';
LE  :   '<=';
And :   '&&';
Or  :   '||';

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
