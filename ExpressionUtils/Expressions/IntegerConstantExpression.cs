using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Expressions
{
    public class IntegerConstantExpression : Expression
    {
        public IntegerConstantExpression(int i)
        {
            Int = i;
        }

        public int Int { get; }

        public override string DisplayString => Int.ToString();

        protected override Expression[] Children => new Expression[] { };
        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new IntegerConstantExpression(Int);
        }
    }
}
