using System;
using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace Transpiler
{
    public class StructuredTextToVisualBasic
    {
        public static string Parse(string input)
        {
            var stream = new AntlrInputStream(input);
            var lexer = new StructuredTextLexer(stream);
            var parser = new StructuredTextParser(new CommonTokenStream(lexer));
            var transpiler = new TranspileVisitor();
            var ast = transpiler.Visit(parser.input());

            var formatter = new VisualBasic.Formatter();
            return formatter.Visit(ast);
        }

        private class TranspileVisitor : StructuredTextBaseVisitor<ParserRuleContext>
        {
            private readonly Stack<ParserRuleContext> _contextStack = new Stack<ParserRuleContext>();

            private ParserRuleContext ParseCommon(ParserRuleContext tree, ParserRuleContext context)
            {
                _contextStack.Push(tree);
                foreach (var child in context.children)
                {
                    tree.AddChild(Visit(child));
                }
                return _contextStack.Pop();
            }

            private void AddChild<TParent, TChild>(TParent parentContext, Func<ParserRuleContext, TChild> createChild)
                where TParent : ParserRuleContext
                where TChild : ParserRuleContext
            {
                ParserRuleContext AggregateFunc(ParserRuleContext parent, Type type)
                {
                    var child = (ParserRuleContext)Activator.CreateInstance(type, new object[] { parent, 0 });
                    parent.AddChild(child);
                    return child;
                }

                var contextTypes = new Type[]
                {
                    typeof(VisualBasicParser.OrExprContext),
                    typeof(VisualBasicParser.AndExprContext),
                    typeof(VisualBasicParser.NotExprContext),
                    typeof(VisualBasicParser.CompareExprContext),
                    typeof(VisualBasicParser.AddExprContext),
                    typeof(VisualBasicParser.ModExprContext),
                    typeof(VisualBasicParser.TermContext),
                    typeof(VisualBasicParser.UnaryExprContext),
                    typeof(VisualBasicParser.PowerExprContext),
                    typeof(VisualBasicParser.PrimaryExprContext),
                };

                var contextTypesWithoutParentType = contextTypes
                    .SkipWhile(type => type != typeof(TParent))
                    .Skip(1)
                    .ToList();
                if (contextTypesWithoutParentType.Exists(type => type == typeof(TChild)))
                {
                    contextTypesWithoutParentType
                        .TakeWhile(type => type != typeof(TChild))
                        .Aggregate((ParserRuleContext)parentContext, AggregateFunc);
                    return;
                }

                var primaryExprContext = contextTypesWithoutParentType
                    .Aggregate((ParserRuleContext)parentContext, AggregateFunc);

                var exprContext = new VisualBasicParser.ExpressionContext(primaryExprContext, 0);
                primaryExprContext.AddChild(new CommonToken(VisualBasicParser.OPEN_PAREN, "("));
                primaryExprContext.AddChild(exprContext);
                primaryExprContext.AddChild(new CommonToken(VisualBasicParser.CLOSE_PAREN, ")"));

                var fromParentContext = contextTypes
                    .TakeWhile(type => type != typeof(TChild))
                    .Aggregate((ParserRuleContext)exprContext, AggregateFunc);

                var fromContext = createChild(fromParentContext);
                fromParentContext.AddChild(fromContext);
            }

            public override ParserRuleContext VisitInput([NotNull] StructuredTextParser.InputContext context)
            {
                var result = new VisualBasicParser.InputContext(null, 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.stmtList()));
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitStmtList([NotNull] StructuredTextParser.StmtListContext context)
            {
                var result = new VisualBasicParser.StmtListContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                foreach (var child in context.children)
                {
                    if (child is ITerminalNode)
                    {
                        result.AddChild(new CommonToken(VisualBasicParser.EOL, Environment.NewLine));
                    }
                    else
                    {
                        result.AddChild(Visit(child));
                    }
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitStmt([NotNull] StructuredTextParser.StmtContext context)
            {
                var result = new VisualBasicParser.StmtContext(_contextStack.Peek(), 0);
                return ParseCommon(result, context);
            }

            public override ParserRuleContext VisitAssignStmt([NotNull] StructuredTextParser.AssignStmtContext context)
            {
                var result = new VisualBasicParser.AssignStmtContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.variable()));
                result.AddChild(new CommonToken(VisualBasicParser.ASSIGN, "="));
                result.AddChild(Visit(context.expression()));
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitSubprogCtrlStmt([NotNull] StructuredTextParser.SubprogCtrlStmtContext context)
            {
                return base.VisitSubprogCtrlStmt(context);
            }

            public override ParserRuleContext VisitExpression([NotNull] StructuredTextParser.ExpressionContext context)
            {
                var result = Visit(context.lhs);
                _contextStack.Push(result);
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    //TODO: 実装
                    throw new NotImplementedException();
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitXorExpr([NotNull] StructuredTextParser.XorExprContext context)
            {
                var result = new VisualBasicParser.ExpressionContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.lhs));
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    result.AddChild(new CommonToken(VisualBasicParser.XOR, "XOR"));
                    result.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitAndExpr([NotNull] StructuredTextParser.AndExprContext context)
            {
                var result = new VisualBasicParser.AndExprContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.lhs));
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    result.AddChild(new CommonToken(VisualBasicParser.AND, "AND"));
                    result.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitCompareExpr([NotNull] StructuredTextParser.CompareExprContext context)
            {
                //TODO: 演算の優先度調節
                var result = Visit(context.lhs);
                _contextStack.Push(result);
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    var operand = context.GetChild<ITerminalNode>(i).Symbol;
                    int type;
                    switch (operand.Type)
                    {
                        case StructuredTextParser.EQ: type = VisualBasicParser.EQ; break;
                        case StructuredTextParser.NEQ: type = VisualBasicParser.NEQ; break;
                        default: throw new InvalidOperationException();
                    }
                    result.AddChild(new CommonToken(type, operand.Text));

                    result.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitEquExpr([NotNull] StructuredTextParser.EquExprContext context)
            {
                //TODO: 演算の優先度調節
                var result = new VisualBasicParser.CompareExprContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.lhs));
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    var operand = context.GetChild<ITerminalNode>(i).Symbol;
                    int type;
                    switch (operand.Type)
                    {
                        case StructuredTextParser.LT: type = VisualBasicParser.LT; break;
                        case StructuredTextParser.GT: type = VisualBasicParser.GT; break;
                        case StructuredTextParser.LE: type = VisualBasicParser.LE; break;
                        case StructuredTextParser.GE: type = VisualBasicParser.GE; break;
                        default: throw new InvalidOperationException();
                    }
                    result.AddChild(new CommonToken(type, operand.Text));

                    result.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitAddExpr([NotNull] StructuredTextParser.AddExprContext context)
            {
                var result = new VisualBasicParser.AddExprContext(_contextStack.Peek(), 0);
                _contextStack.Push(result);
                result.AddChild(Visit(context.lhs));
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    var operand = context.GetChild<ITerminalNode>(i).Symbol;
                    int type;
                    switch (operand.Type)
                    {
                        case StructuredTextParser.PLUS: type = VisualBasicParser.PLUS; break;
                        case StructuredTextParser.MINUS: type = VisualBasicParser.MINUS; break;
                        default: throw new InvalidOperationException();
                    }
                    result.AddChild(new CommonToken(type, operand.Text));

                    result.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitTerm([NotNull] StructuredTextParser.TermContext context)
            {
                //TODO: MOD対応
                var result = new VisualBasicParser.ModExprContext(_contextStack.Peek(), 0);
                var term = new VisualBasicParser.TermContext(result, 0);
                result.AddChild(term);
                _contextStack.Push(term);
                term.AddChild(Visit(context.lhs));
                for (int i = 1; i < context.ChildCount; i += 2)
                {
                    var operand = context.GetChild<ITerminalNode>(i).Symbol;
                    int type;
                    switch (operand.Type)
                    {
                        case StructuredTextParser.ASTERISK: type = VisualBasicParser.ASTERISK; break;
                        case StructuredTextParser.SLASH: type = VisualBasicParser.SLASH; break;
                        case StructuredTextParser.MOD: throw new NotImplementedException();
                        default: throw new InvalidOperationException();
                    }
                    term.AddChild(new CommonToken(type, operand.Text));

                    term.AddChild(Visit(context.GetChild(i + 1)));
                }
                _contextStack.Pop();
                return result;
            }

            public override ParserRuleContext VisitPowerExpr([NotNull] StructuredTextParser.PowerExprContext context)
            {
                var unaryExpr = (VisualBasicParser.UnaryExprContext)Visit(context.lhs);
                if (context.rhs == null) return unaryExpr;

                VisualBasicParser.PowerExprContext powerExpr;
                if (unaryExpr.ChildCount == 1)
                {
                    powerExpr = unaryExpr.rhs;
                }
                else
                {
                    powerExpr = new VisualBasicParser.PowerExprContext(null, 0);
                    AddChild(powerExpr, parent =>
                    {
                        unaryExpr.Parent = parent;
                        return unaryExpr;
                    });
                }

                foreach (var rhs in context.unaryExpr().Skip(1))
                {
                    powerExpr.AddChild(new CommonToken(VisualBasicParser.HAT, "^"));
                    AddChild(powerExpr, parent =>
                    {
                        _contextStack.Push(parent);
                        var unary = (VisualBasicParser.UnaryExprContext)Visit(rhs);
                        _contextStack.Pop();
                        return unary;
                    });
                }

                var result = new VisualBasicParser.UnaryExprContext((ParserRuleContext)unaryExpr.Parent, 0);
                result.AddChild(powerExpr);
                powerExpr.Parent = result;
                return result;
            }

            public override ParserRuleContext VisitUnaryExpr([NotNull] StructuredTextParser.UnaryExprContext context)
            {
                var result = new VisualBasicParser.UnaryExprContext(_contextStack.Peek(), 0);
                if (context.operand != null)
                {
                    int type;
                    switch (context.operand.Type)
                    {
                        case StructuredTextParser.PLUS: type = VisualBasicParser.PLUS; break;
                        case StructuredTextParser.MINUS: type = VisualBasicParser.MINUS; break;
                        case StructuredTextParser.NOT: return ParseNotExpr(context);
                        default: throw new InvalidOperationException();
                    }
                    result.AddChild(new CommonToken(type, context.operand.Text));
                }

                var dummyChild = new VisualBasicParser.PowerExprContext(result, 0);
                result.AddChild(dummyChild);
                _contextStack.Push(dummyChild);
                dummyChild.AddChild(Visit(context.rhs));
                _contextStack.Pop();
                return result;
            }

            private ParserRuleContext ParseNotExpr([NotNull] StructuredTextParser.UnaryExprContext context)
            {
                var result = new VisualBasicParser.UnaryExprContext(_contextStack.Peek(), 0);
                AddChild(result, parent =>
                {
                    var notExpr = new VisualBasicParser.NotExprContext(parent, 0);
                    notExpr.AddChild(new CommonToken(VisualBasicParser.NOT, "NOT"));
                    AddChild(notExpr, parent2 =>
                    {
                        _contextStack.Push(parent2);
                        var primaryExpr = (VisualBasicParser.PrimaryExprContext)Visit(context.rhs);
                        _contextStack.Pop();
                        return primaryExpr;
                    });
                    return notExpr;
                });
                return result;
            }

            public override ParserRuleContext VisitPrimaryExpr([NotNull] StructuredTextParser.PrimaryExprContext context)
            {
                var result = new VisualBasicParser.PrimaryExprContext(_contextStack.Peek(), 0);
                return ParseCommon(result, context);
            }

            public override ParserRuleContext VisitConstant([NotNull] StructuredTextParser.ConstantContext context)
            {
                return base.VisitConstant(context);
            }

            public override ParserRuleContext VisitNumericLiteral([NotNull] StructuredTextParser.NumericLiteralContext context)
            {
                return base.VisitNumericLiteral(context);
            }

            public override ParserRuleContext VisitVariableAccess([NotNull] StructuredTextParser.VariableAccessContext context)
            {
                var result = new VisualBasicParser.VariableAccessContext(_contextStack.Peek(), 0);
                return ParseCommon(result, context);
            }

            public override ParserRuleContext VisitVariable([NotNull] StructuredTextParser.VariableContext context)
            {
                var result = new VisualBasicParser.VariableContext(_contextStack.Peek(), 0);
                return ParseCommon(result, context);
            }

            public override ParserRuleContext VisitVariableName([NotNull] StructuredTextParser.VariableNameContext context)
            {
                var result = new VisualBasicParser.VariableNameContext(_contextStack.Peek(), 0);
                result.AddChild(new CommonToken(VisualBasicParser.IDENTIFIER, context.IDENTIFIER().GetText()));
                return result;
            }

            public override ParserRuleContext VisitFuncCall([NotNull] StructuredTextParser.FuncCallContext context)
            {
                return base.VisitFuncCall(context);
            }

            public override ParserRuleContext VisitFuncAccess([NotNull] StructuredTextParser.FuncAccessContext context)
            {
                return base.VisitFuncAccess(context);
            }

            public override ParserRuleContext VisitNamelessParamAssign([NotNull] StructuredTextParser.NamelessParamAssignContext context)
            {
                return base.VisitNamelessParamAssign(context);
            }

            public override ParserRuleContext VisitInParamAssign([NotNull] StructuredTextParser.InParamAssignContext context)
            {
                return base.VisitInParamAssign(context);
            }

            public override ParserRuleContext VisitOutParamAssign([NotNull] StructuredTextParser.OutParamAssignContext context)
            {
                return base.VisitOutParamAssign(context);
            }
        }
    }
}
