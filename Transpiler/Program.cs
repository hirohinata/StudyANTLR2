using System;
using System.Diagnostics;

namespace Transpiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "A := B;";
            var expect = input;
            var actual = StructuredTextToStructuredText(input);
            Debug.Assert(expect == actual);
        }

        static string StructuredTextToStructuredText(string input)
        {
            return input;
        }
    }
}
