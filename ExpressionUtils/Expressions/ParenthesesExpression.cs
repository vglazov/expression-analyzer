using System.Collections.Generic;

namespace ExpressionUtils.Expressions
{
    public class ParenthesesExpression: Expression
    {
        public ParenthesesExpression(Expression innerExpression)
        {
            this.InnerExpression = innerExpression;
        }

        public Expression InnerExpression { get; }

        public override string DisplayString => "(" + InnerExpression.DisplayString + ")";

        protected override Expression[] Children => new []{InnerExpression};

        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new ParenthesesExpression(InnerExpression.CloneWithVariableSubstitute(substitutions));
        }
    }
}
