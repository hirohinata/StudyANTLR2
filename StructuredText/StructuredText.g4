grammar StructuredText;

input: stmtList EOF;

stmtList
    : ( stmt? SEMI_COLON )*
    ;
stmt
    : assignStmt
    | subprogCtrlStmt
//    | selectionStmt
//    | iterationStmt
    ;
assignStmt
    : variable ASSIGN expression
//    | refAssign
//    | assignmentAttempt
    ;
subprogCtrlStmt
    : funcCall
//    | invocation
    | SUPER OPEN_PAREN CLOSE_PAREN
    | RETURN
    ;

expression
    : lhs=xorExpr ( operand=OR rhs=xorExpr )*
    ;
xorExpr
    : lhs=andExpr ( operand=XOR rhs=andExpr )*
    ;
andExpr
    : lhs=compareExpr ( operand=(AND | AMPERSAND) rhs=compareExpr )*
    ;
compareExpr
    : lhs=equExpr ( operand=(EQ | NEQ) rhs=equExpr )*
    ;
equExpr
    : lhs=addExpr ( operand=(LT | GT | LE | GE) rhs=addExpr )*
    ;
addExpr
    : lhs=term ( operand=(PLUS | MINUS) rhs=term )*
    ;
term
    : lhs=powerExpr ( operand=(ASTERISK | SLASH | MOD) rhs=powerExpr )*
    ;
powerExpr
    : lhs=unaryExpr ( operand=POW rhs=unaryExpr )*
    ;
unaryExpr
    : operand=( MINUS | PLUS | NOT )? rhs=primaryExpr
    ;
primaryExpr
    : constant
//    | enumValue
    | variableAccess
    | funcCall
//    | refValue
    | OPEN_PAREN expression CLOSE_PAREN
    ;

constant
    : numericLiteral
    | CHAR_LITERAL
//    | timeLiteral
    | BIT_STR_LITERAL
    | BOOL_LITERAL
    ;
numericLiteral
    : INT_LITERAL
    | REAL_LITERAL
    ;

variableAccess
    : variable      //TODO:配列・メンバ・ワード中ビット等の対応
    ;
variable
    : variableName  //TODO:直接表現や名前空間等の対応
    ;
variableName
    : IDENTIFIER
    ;

funcCall
    : funcAccess OPEN_PAREN ( args=paramAssign ( COMMA args=paramAssign )* )? CLOSE_PAREN
    ;
funcAccess
    : IDENTIFIER    //TODO:名前空間等の対応
    ;
paramAssign
    : expression                            #namelessParamAssign
//    | refAssign                             #namelessParamAssign
    | variableName ASSIGN expression        #inParamAssign
    | NOT? variableName OUTREF variable     #outParamAssign
    ;

PLUS : '+';
MINUS : '-';
ASTERISK : '*';
SLASH : '/';
POW: '**';
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '=';
NEQ: '<>';
ASSIGN: ':=';
OUTREF: '=>';
RANGE: '..';
COMMA: ',';
OPEN_PAREN : '(';
CLOSE_PAREN : ')';
SEMI_COLON: ';';
AMPERSAND: '&';

OR: O R;
XOR: X O R;
AND: A N D;
MOD: M O D;
NOT: N O T;

SUPER: S U P E R;
RETURN: R E T U R N;

CASE: C A S E;
OF: O F;
END_CASE: E N D '_' C A S E;
REPEAT: R E P E A T;
UNTIL: U N T I L;
END_REPEAT: E N D '_' R E P E A T;

fragment DIGIT: [0-9];
fragment UNSIGNED_INT: DIGIT ( '_'? DIGIT )*;
fragment SIGNED_INT: ( PLUS | MINUS )? UNSIGNED_INT;
fragment BINARY_INT: '2#' ( '_'? [0-1] )+;
fragment OCTAL_INT: '8#' ( '_'? [0-7] )+;
fragment HEX_INT: '16#' ( '_'? [0-9A-Fa-f] )+;
INT_LITERAL: ( INT_TYPE_NAME '#' )? ( SIGNED_INT | BINARY_INT | OCTAL_INT | HEX_INT );
REAL_LITERAL: ( REAL_TYPE_NAME '#' )? SIGNED_INT '.' UNSIGNED_INT ( 'E' SIGNED_INT )?;
BIT_STR_LITERAL: ( MULTIBITS_TYPE_NAME '#' )? ( UNSIGNED_INT | BINARY_INT | OCTAL_INT | HEX_INT );
BOOL_LITERAL: ( BOOL '#' )? ( '0' | '1' | 'FALSE' |'TRUE' );
CHAR_LITERAL: ( STRING '#' )? ( '\'' ~['\r\n]* '\'' | '"' ~["\r\n]* '"' );

fragment ID_START: [A-Za-z_];
fragment ID_CONTINUE: ID_START | [0-9];
IDENTIFIER: ID_START ID_CONTINUE*;

WS: [ \t]+ -> skip;
EOL: ('\r'? '\n' | '\r') -> skip;
SINGLE_LINE_COMMENT: '//' ~[\r\n]*;

fragment BOOL: B O O L;
fragment INT: I N T;
fragment SINT: S INT;
fragment DINT: D INT;
fragment LINT: L INT;
fragment USINT: U SINT;
fragment UINT: U INT;
fragment UDINT: U DINT;
fragment ULINT: U LINT;
fragment REAL: R E A L;
fragment LREAL: L REAL;
fragment STRING: S T R I N G;
fragment WSTRING: W STRING;
fragment BYTE: B Y T E;
fragment WORD: W O R D;
fragment DWORD: D W O R D;
fragment LWORD: L W O R D;

fragment SIGN_INT_TYPE_NAME: SINT | INT | DINT | LINT;
fragment UNSIGN_INT_TYPE_NAME: USINT | UINT | UDINT | ULINT;
fragment INT_TYPE_NAME: SIGN_INT_TYPE_NAME | UNSIGN_INT_TYPE_NAME;
fragment REAL_TYPE_NAME: REAL | LREAL;
fragment MULTIBITS_TYPE_NAME: BYTE | WORD | DWORD | LWORD;

fragment A: [Aa];
fragment B: [Bb];
fragment C: [Cc];
fragment D: [Dd];
fragment E: [Ee];
fragment F: [Ff];
fragment G: [Gg];
fragment H: [Hh];
fragment I: [Ii];
fragment J: [Jj];
fragment K: [Kk];
fragment L: [Ll];
fragment M: [Mm];
fragment N: [Nn];
fragment O: [Oo];
fragment P: [Pp];
fragment Q: [Qq];
fragment R: [Rr];
fragment S: [Ss];
fragment T: [Tt];
fragment U: [Uu];
fragment V: [Vv];
fragment W: [Ww];
fragment X: [Xx];
fragment Y: [Yy];
fragment Z: [Zz];