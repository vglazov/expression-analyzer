using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Expressions
{
    public class DecimalConstantExpression: Expression
    {
        public DecimalConstantExpression(decimal d)
        {
            Decimal = d;
        }

        public decimal Decimal { get; }

        public override string DisplayString => Decimal.ToString();

        protected override Expression[] Children => new Expression[] { };
        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return this;
        }
    }
}
