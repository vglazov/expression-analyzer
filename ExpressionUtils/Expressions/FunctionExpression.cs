using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Expressions
{
    public class FunctionExpression: Expression
    {
        public FunctionExpression(string name, List<Expression> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public String Name { get; }
        public List<Expression> Arguments { get; }

        public override string DisplayString 
        {
            get
            {
                String argumentsStr = String.Join(", ", from arg in Arguments select arg.DisplayString);
                return $"{Name}({argumentsStr})";
            }
        }

        protected override Expression[] Children => Arguments.ToArray();
        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            return new FunctionExpression(Name, (from arg in Arguments select arg.CloneWithVariableSubstitute(substitutions)).ToList());
        }
    }
}
