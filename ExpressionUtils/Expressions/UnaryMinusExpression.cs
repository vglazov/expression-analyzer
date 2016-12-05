using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Expressions
{
    public class UnaryMinusExpression: Expression
    {
        public UnaryMinusExpression(Expression innerExpression)
        {
            InnerExpression = innerExpression;
        }

        public Expression InnerExpression { get; }

        public override string DisplayString
        {
            get {
                if ((InnerExpression is IntegerConstantExpression &&
                    ((IntegerConstantExpression) InnerExpression).Int < 0) ||
                    (InnerExpression is DecimalConstantExpression &&
                    ((DecimalConstantExpression)InnerExpression).Decimal < 0))
                {
                    return $"-({InnerExpression.DisplayString})"; // e.g. display -(-1) instead of --1
                }
                else
                {
                    return "-" + InnerExpression.DisplayString;
                }
            }
        }

        protected override Expression[] Children => new[] { InnerExpression };

        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new UnaryMinusExpression(InnerExpression.CloneWithVariableSubstitute(substitutions));
        }
    }
}
