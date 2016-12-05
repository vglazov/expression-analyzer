using ExpressionUtils.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ExpressionUtils.Expressions.ArithmeticOperator;
using static ExpressionUtils.Expressions.ExpressionBuilder;

namespace ExpressionUtils.Tests
{
    [TestClass]
    public class ExpressionsTest
    {
        [TestMethod]
        public void ExpressionTest1()
        {
            Expression xPlusY = Arithmetic(Variable("x"), Plus , Variable("y"));
            Assert.AreEqual("x + y", xPlusY.DisplayString);
        }

        [TestMethod]
        public void ExpressionTest2()
        {
            Expression expr = Arithmetic(Arithmetic(Parentheses(Arithmetic(Variable("x"), Plus, Integer(15))), Multiply, String("abc")), Minus, Decimal(0.19m));
            Assert.AreEqual("(x + 15) * \"abc\" - 0.19", expr.DisplayString);
        }

        [TestMethod]
        public void ExpressionTest3()
        {
            Expression expr = Function("max", Arithmetic(Variable("x"), Plus, Integer(15)), Function("f"));
            Assert.AreEqual("max(x + 15, f())", expr.DisplayString);
        }

        [TestMethod]
        public void ExpressionTest4()
        {
            Expression expr = Arithmetic(UnaryMinus(Variable("a")), Plus, Function("f", Variable("x"), UnaryMinus(Parentheses(Arithmetic(Variable("x1"), Plus, Variable("x2"))))));
            Assert.AreEqual("-a + f(x, -(x1 + x2))", expr.DisplayString);
        }

        [TestMethod]
        public void ExpressionTest5()
        {
            Expression expr = Arithmetic(UnaryMinus(Integer(-8)), Plus, Variable("x1"));
            Assert.AreEqual("-(-8) + x1", expr.DisplayString);
        }

        [TestMethod]
        public void ExpressionTest6()
        {
            Expression expr = UnaryMinus(UnaryMinus(Decimal(-10.1432m)));
            Assert.AreEqual("-(-(-10.1432))", expr.DisplayString);
        }


    }
}
