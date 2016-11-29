using System.Collections.Generic;

namespace ExpressionUtils.Expressions
{
    public class ArithmeticExpression: Expression
    {
        public ArithmeticExpression(ArithmeticOperator @operator, Expression firstOperand, Expression secondOperand)
        {
            Operator = @operator;
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
        }

        public ArithmeticOperator Operator { get; }

        public Expression FirstOperand { get; }

        public Expression SecondOperand { get; }

        public override string DisplayString => FirstOperand + " " + Operator + " " + SecondOperand;

        protected override Expression[] Children => new[] {FirstOperand, SecondOperand};
        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new ArithmeticExpression(Operator, FirstOperand.CloneWithVariableSubstitute(substitutions), SecondOperand.CloneWithVariableSubstitute(substitutions));
        }
    }
}
