using System;
using System.Collections.Generic;

namespace ExpressionUtils.Expressions
{
    public class ArithmeticOperator
    {
        private static readonly Dictionary<Char, ArithmeticOperator> Operators = new Dictionary<Char, ArithmeticOperator>();

        public static readonly ArithmeticOperator Plus = new ArithmeticOperator('+');
        public static readonly ArithmeticOperator Minus = new ArithmeticOperator('-');
        public static readonly ArithmeticOperator Divide = new ArithmeticOperator('/');
        public static readonly ArithmeticOperator Multiply = new ArithmeticOperator('*');

        private ArithmeticOperator(Char c)
        {
            DisplayString = c.ToString();
            Operators.Add(c, this);
        }

        public String DisplayString { get; }

        public static bool IsOperator(Char c)
        {
            return Operators.ContainsKey(c);
        }

        public static ArithmeticOperator FromChar(Char c)
        {
            return Operators[c];
        }

        public override string ToString() => DisplayString;
    }
}
