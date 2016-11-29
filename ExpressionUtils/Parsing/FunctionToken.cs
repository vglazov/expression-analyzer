using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Parsing
{
    public class FunctionToken: Token
    {
        public FunctionToken(string name)
        {
            Name = name;
            ArgumentCount = 0;
        }

        public String Name { get; }

        public int ArgumentCount { get; private set; }        

        public void IncrementArgumentCounter()
        {
            ArgumentCount++;
        }

        public override string DisplayString => Name + "(";

    }
}
