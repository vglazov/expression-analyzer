using ExpressionUtils.Expressions;

namespace ExpressionUtils.Parsing
{
    class ArithmeticOperatorToken: Token
    {
        public ArithmeticOperatorToken(ArithmeticOperator @operator)
        {
            Operator = @operator;
        }

        public ArithmeticOperator Operator { get; }

        public override string DisplayString => Operator.DisplayString;
    }
}
