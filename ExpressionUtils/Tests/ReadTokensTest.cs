using System;
using System.Collections.Generic;
using System.Linq;
using ExpressionUtils.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionUtils.Tests
{
    [TestClass]
    public class ReadTokensTest
    {
        [TestMethod]
        public void ReadTokensTest1()
        {
            var tokens = ExpressionParser.ReadTokens("(x + y) * z");
            AssertTokensAre(new[] { "(", "x", "+", "y", ")", "*", "z" }, tokens);
        }

        [TestMethod]
        public void ReadTokensTest2()
        {
            var tokens = ExpressionParser.ReadTokens("\"Hello, World!\" + 45");
            AssertTokensAre(new[] { "\"Hello, World!\"", "+", "45" }, tokens);
        }

        [TestMethod]
        public void ReadTokensTest3()
        {
            var tokens = ExpressionParser.ReadTokens("CharAt(\"blablabla\", max(1, x))");
            AssertTokensAre(new[] { "CharAt(", "\"blablabla\"", ",", "max(", "1", ",", "x", ")", ")" }, tokens);
        }

        [TestMethod]
        public void ReadTokensTest4()
        {
            var tokens = ExpressionParser.ReadTokens("max(x + y, -10) - z");
            AssertTokensAre(new[] { "max(", "x", "+", "y", ",", "-10", ")", "-", "z" }, tokens);
        }


        private static void AssertTokensAre(string[] expected, List<Token> actual)
        {
            CollectionAssert.AreEqual(new List<String>(expected), (from token in actual select token.DisplayString).ToList());
        }

    }
}
