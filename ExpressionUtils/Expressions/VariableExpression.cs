using System;
using System.Collections.Generic;

namespace ExpressionUtils.Expressions
{
    public class VariableExpression: Expression
    {
        public VariableExpression(string name)
        {
            Name = name;
        }

        public String Name { get; }

        public override string DisplayString => Name;

        protected override Expression[] Children => new Expression[] {};

        public override Expression CloneWithVariableSubstitute(Dictionary<string, Expression> substitutions)
        {
            if (substitutions.ContainsKey(Name))
            {
                return substitutions[Name]; // TODO: better to do Clone() of this (but not with substitutions)
            }
            else
            {
                return new VariableExpression(Name);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is VariableExpression)
            {
                return Name.Equals(((VariableExpression) obj).Name);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
