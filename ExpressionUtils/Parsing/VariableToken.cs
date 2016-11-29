using System;

namespace ExpressionUtils.Parsing
{
    class VariableToken: Token
    {
        public VariableToken(string name)
        {
            Name = name;
        }

        public String Name { get; }

        public override string DisplayString => Name;
    }
}
