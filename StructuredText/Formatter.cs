using Antlr4.Runtime.Tree;

namespace StructuredText
{
    public class Formatter : StructuredTextBaseVisitor<string>
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
