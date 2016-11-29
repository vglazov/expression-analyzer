using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Parsing
{
    public class StringConstantToken: Token
    {
        public StringConstantToken(string s)
        {
            String = s;
        }

        public String String { get; }

        public override string DisplayString => $"\"{String}\"";

    }
}
