using System;
using NUnit.Framework;

namespace Transpiler.Test
{
    [TestFixture]
    public class ExprTest
    {
        [TestCase("A := B;", "A=B\n")]
        public void Test(string input, string expected)
        {
            Assert.AreEqual(expected, StructuredTextToVisualBasic.Parse(input));
        }
    }
}
