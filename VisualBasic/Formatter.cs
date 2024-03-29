﻿using Antlr4.Runtime.Tree;

namespace VisualBasic
{
    public class Formatter : VisualBasicBaseVisitor<string>
    {
        protected override string AggregateResult(string aggregate, string nextResult)
        {
            return aggregate + nextResult;
        }

        public override string VisitTerminal(ITerminalNode node)
        {
            if (node.Symbol.Type < 0)
                return string.Empty;
            return node.GetText();
        }
    }
}
