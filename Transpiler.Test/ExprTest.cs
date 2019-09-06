using System;
using NUnit.Framework;

namespace Transpiler.Test
{
    [TestFixture]
    public class ExprTest
    {
        [TestCase("A := B;", "A := B;")]
        public void Test(string input, string expected)
        {
            Assert.AreEqual(expected, Parser.StructuredTextToStructuredText(input));
        }
    }
}
