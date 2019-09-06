using Antlr4.Runtime;

namespace Transpiler
{
    public class Parser
    {
        public static string StructuredTextToStructuredText(string input)
        {
            var stream = new AntlrInputStream(input);
            var lexer = new StructuredTextLexer(stream);
            var parser = new StructuredTextParser(new CommonTokenStream(lexer));
            var visitor = new StructuredText.Formatter();
            return visitor.Visit(parser.input());
        }
    }
}
