//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /Users/azuri_hirohinata/Desktop/ANTLR/StudyANTLR2/StructuredText/StructuredText.g4 by ANTLR 4.7.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="StructuredTextParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.2")]
[System.CLSCompliant(false)]
public interface IStructuredTextVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.input"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInput([NotNull] StructuredTextParser.InputContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.stmtList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmtList([NotNull] StructuredTextParser.StmtListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.stmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStmt([NotNull] StructuredTextParser.StmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.assignStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssignStmt([NotNull] StructuredTextParser.AssignStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.subprogCtrlStmt"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubprogCtrlStmt([NotNull] StructuredTextParser.SubprogCtrlStmtContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpression([NotNull] StructuredTextParser.ExpressionContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.xorExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitXorExpr([NotNull] StructuredTextParser.XorExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.andExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAndExpr([NotNull] StructuredTextParser.AndExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.compareExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompareExpr([NotNull] StructuredTextParser.CompareExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.equExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEquExpr([NotNull] StructuredTextParser.EquExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.addExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAddExpr([NotNull] StructuredTextParser.AddExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.term"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerm([NotNull] StructuredTextParser.TermContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.powerExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPowerExpr([NotNull] StructuredTextParser.PowerExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.unaryExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryExpr([NotNull] StructuredTextParser.UnaryExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.primaryExpr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrimaryExpr([NotNull] StructuredTextParser.PrimaryExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] StructuredTextParser.ConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.numericLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumericLiteral([NotNull] StructuredTextParser.NumericLiteralContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.variableAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableAccess([NotNull] StructuredTextParser.VariableAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] StructuredTextParser.VariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.variableName"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableName([NotNull] StructuredTextParser.VariableNameContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.funcCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncCall([NotNull] StructuredTextParser.FuncCallContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="StructuredTextParser.funcAccess"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFuncAccess([NotNull] StructuredTextParser.FuncAccessContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>namelessParamAssign</c>
	/// labeled alternative in <see cref="StructuredTextParser.paramAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNamelessParamAssign([NotNull] StructuredTextParser.NamelessParamAssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>inParamAssign</c>
	/// labeled alternative in <see cref="StructuredTextParser.paramAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInParamAssign([NotNull] StructuredTextParser.InParamAssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>outParamAssign</c>
	/// labeled alternative in <see cref="StructuredTextParser.paramAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOutParamAssign([NotNull] StructuredTextParser.OutParamAssignContext context);
}
