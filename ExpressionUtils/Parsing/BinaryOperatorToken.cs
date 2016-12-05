using ExpressionUtils.Expressions;

namespace ExpressionUtils.Parsing
{
    class BinaryOperatorToken: OperatorToken
    {
        public BinaryOperatorToken(ArithmeticOperator @operator)
        {
            Operator = @operator;
        }

        public ArithmeticOperator Operator { get; }

        public override string DisplayString => Operator.DisplayString;
    }
}
