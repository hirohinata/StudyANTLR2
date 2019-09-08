grammar VisualBasic;

input: stmtList EOF;

stmtList
    : ( stmt? EOL )*
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
    ;

expression
    : lhs=orExpr ( operand=XOR rhs=orExpr )*
    ;
orExpr
    : lhs=andExpr ( operand=OR rhs=andExpr )*
    ;
andExpr
    : lhs=notExpr ( operand=AND rhs=notExpr )*
    ;
notExpr
    : operand=NOT* rhs=compareExpr
    ;
compareExpr
    : lhs=addExpr ( operand=(EQ | NEQ | LT | GT | LE | GE) rhs=addExpr )*
    ;
addExpr
    : lhs=modExpr ( operand=(PLUS | MINUS) rhs=modExpr )*
    ;
modExpr
    : lhs=term ( operand=MOD rhs=term )*
    ;
term
    : lhs=unaryExpr ( operand=(ASTERISK | SLASH) rhs=unaryExpr )*
    ;
unaryExpr
    : operand=( MINUS | PLUS )? rhs=powerExpr
    ;
powerExpr
    : lhs=primaryExpr ( operand=HAT rhs=primaryExpr )*
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
    : DEC_NUMBER
    | HEX_NUMBER
    | REAL_NUMBER
    | DOT_NUMBER
    | STRING
    | NULL
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
    | NOT? variableName ASSIGN variable     #outParamAssign
    ;

PLUS : '+';
MINUS : '-';
ASTERISK : '*';
SLASH : '/';
HAT: '^';
LT: '<';
GT: '>';
LE: '<=';
GE: '>=';
EQ: '=';
NEQ: '<>';
ASSIGN: ':=';
OUTREF: '=>';
DOT: '.';
COMMA: ',';
OPEN_PAREN : '(';
CLOSE_PAREN : ')';
COLON: ':';
OPEN_BRACKET: '[';
CLOSE_BRACKET: ']';

OR: O R;
XOR: X O R;
AND: A N D;
MOD: M O D;
NOT: N O T;

END: E N D;
SELECT: S E L E C T;
CASE: C A S E;
TO: T O;
IS: I S;
DO: D O;
UNTIL: U N T I L;

fragment DIGIT: [0-9];
fragment NONZERODIGIT: [1-9];
fragment HEXDIGIT: [0-9a-fA-F];
fragment SIGN: [+\-];
fragment DEC_PREFIX: '#' | K;
fragment HEX_PREFIX: '$' | H;
fragment REAL_NUMBER_PART: DIGIT+ (DOT DIGIT*)? | DOT DIGIT+;
fragment REAL_EXPONENT_PART: E SIGN? DIGIT+;
DOT_NUMBER: DOT DIGIT+;
DEC_NUMBER: DEC_PREFIX? SIGN? DIGIT+;
HEX_NUMBER: HEX_PREFIX HEXDIGIT+;
REAL_NUMBER: DEC_PREFIX? SIGN? REAL_NUMBER_PART REAL_EXPONENT_PART?;
STRING: '"' (~["\r\n] | '""')* '"';

NULL: '???';

fragment ID_START: [A-Za-z_];
fragment ID_CONTINUE: ID_START | [0-9];
IDENTIFIER: ID_START ID_CONTINUE*;

WS: [ \t]+ -> skip;
EOL: '\r'? '\n' | '\r';
LINE_COMMENT: ('\'' | '’' | R E M) ~[\r\n]*;    //"REM"から開始する改行以外の文字列はすべてコメントになってしまうが、それが従来からの仕様。なので"REMOTE"などの識別子はスクリプト中で使用できない

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