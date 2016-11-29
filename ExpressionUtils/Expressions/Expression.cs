using System;
using System.Collections.Generic;

namespace ExpressionUtils.Expressions
{
    public abstract class Expression
    {
        public abstract String DisplayString { get; }

        protected abstract Expression[] Children { get; }

        public abstract Expression CloneWithVariableSubstitute(Dictionary<String, Expression> substitutions);

        public override string ToString() => DisplayString;        

        public HashSet<VariableExpression> GetAllVariables()
        {
            var vars = new HashSet<VariableExpression>();
            GetAllVarsRecursive(vars, this);
            return vars;
        }

        private static void GetAllVarsRecursive(HashSet<VariableExpression> vars, Expression expression)
        {
            if (expression is VariableExpression)
            {
                vars.Add(expression as VariableExpression);
            }
            foreach (var child in expression.Children)
            {
                GetAllVarsRecursive(vars, child);
            }
        }
    }
}
