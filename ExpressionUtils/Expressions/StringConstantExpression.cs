using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Expressions
{
    public class StringConstantExpression : Expression
    {
        public StringConstantExpression(string s)
        {
            String = s;
        }

        public String String { get; }

        public override string DisplayString => $"\"{String}\"";

        protected override Expression[] Children => new Expression[] {};
        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new StringConstantExpression(String);
        }
    }
}
