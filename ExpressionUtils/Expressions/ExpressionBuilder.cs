using System;
using System.Linq;

namespace ExpressionUtils.Expressions
{
    public class ExpressionBuilder
    {
        public static ParenthesesExpression Parentheses(Expression innerExpression)
        {
            return new ParenthesesExpression(innerExpression);
        }

        public static VariableExpression Variable(String name)
        {
            return new VariableExpression(name);
        }

        public static ArithmeticExpression Arithmetic(Expression firstOperand, ArithmeticOperator operatorr, Expression secondOperand)
        {
            return new ArithmeticExpression(operatorr, firstOperand, secondOperand);
        }

        public static StringConstantExpression String(String str)
        {
            return new StringConstantExpression(str);
        }

        public static IntegerConstantExpression Integer(long i)
        {
            return new IntegerConstantExpression(i);
        }

        public static DecimalConstantExpression Decimal(decimal d)
        {
            return new DecimalConstantExpression(d);
        }

        public static FunctionExpression Function(String name, params Expression[] expressions)
        {
            return new FunctionExpression(name, expressions.ToList());
        }

        public static UnaryMinusExpression UnaryMinus(Expression expression)
        {
            return new UnaryMinusExpression(expression);
        }
    }
}
