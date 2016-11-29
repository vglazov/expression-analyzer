using ExpressionUtils.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionUtils.Tests
{
    [TestClass]
    public class ParseTest
    {
        [TestMethod]
        public void ParseTest1()
        {
            var expression = ExpressionParser.Parse("(x + y) * z");
            Assert.AreEqual("(x + y) * z", expression.DisplayString);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpressionParseException))]
        public void ParseTest2()
        {
            ExpressionParser.Parse("(x + y * z");           
        }

        [TestMethod]
        [ExpectedException(typeof(ExpressionParseException))]
        public void ParseTest3()
        {
            ExpressionParser.Parse("x y +");            
        }

        [TestMethod]
        public void ParseTest4()
        {
            var expression = ExpressionParser.Parse("max(x + y, 10)");
            Assert.AreEqual("max(x + y, 10)", expression.DisplayString);
        }

        [TestMethod]
        [ExpectedException(typeof(ExpressionParseException))]
        public void ParseTest5()
        {
            ExpressionParser.Parse("max(,)");
        }

        [TestMethod]
        [ExpectedException(typeof(ExpressionParseException))]
        public void ParseTest6()
        {
            ExpressionParser.Parse("max(1,)");
        }

    }
}
