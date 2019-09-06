using System;
using System.Linq;
using Antlr4.Runtime.Misc;

namespace StructuredText
{
    public class Formatter : StructuredTextBaseVisitor<string>
    {
        public override string VisitInput([NotNull] StructuredTextParser.InputContext context)
        {
            return Visit(context.stmtList());
        }

        public override string VisitStmtList([NotNull] StructuredTextParser.StmtListContext context)
        {
            return context.stmt()
                    .Select(stmt => Visit(stmt) + ";")
                    .Aggregate((a, b) => a + Environment.NewLine);
        }

        public override string VisitStmt([NotNull] StructuredTextParser.StmtContext context)
        {
            return base.VisitStmt(context);
        }

        public override string VisitAssignStmt([NotNull] StructuredTextParser.AssignStmtContext context)
        {
            return context.variable().GetText() + " := " + context.expression().GetText();
        }
    }
}
