using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionUtils.Parsing
{
    public class DecimalConstantToken: Token
    {
        public DecimalConstantToken(decimal d)
        {
            Decimal = d;
        }

        public Decimal Decimal { get; }


        public override string DisplayString => Decimal.ToString();

    }
}
