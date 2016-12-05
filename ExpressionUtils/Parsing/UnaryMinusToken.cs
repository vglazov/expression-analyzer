using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Parsing
{
    public class UnaryMinusToken: OperatorToken
    {
        public override string DisplayString => "-";
    }
}
