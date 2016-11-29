using System;

namespace ExpressionUtils.Parsing
{
    public class ExpressionParseException: Exception
    {
        public ExpressionParseException(string message) : base(message)
        {
        }
    }
}
