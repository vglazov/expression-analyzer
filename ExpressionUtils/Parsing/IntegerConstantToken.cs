using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Parsing
{
    public class IntegerConstantToken: Token
    {
        public IntegerConstantToken(int i)
        {
            Int = i;
        }

        public int Int { get; }


        public override string DisplayString => Int.ToString();
    }
}
