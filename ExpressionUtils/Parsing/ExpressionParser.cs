using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ExpressionUtils.Expressions;

namespace ExpressionUtils.Parsing
{
    public class ExpressionParser
    {

        private static readonly Regex VariableRegex = new Regex("[a-zA-Z_][\\w_]*");
        private static readonly Regex FunctionNameRegex = new Regex("[a-zA-Z][\\w_]*");
        private static readonly Regex IntegerRegex = new Regex("-?\\d+");
        private static readonly Regex DecimalRegex = new Regex("-?\\d+\\.\\d+");

        public static Expression Parse(String str)
        {
            var tokens = ReadTokens(str);
            return DoParse(tokens);
        }

        private static String PrepareString(String str)
        {
            return str.Trim().Replace('–', '-') + '\n';
        }

        private static Expression DoParse(List<Token> tokens)
        {
            var expressionStack = new Stack<Expression>();
            var operatorsStack = new Stack<Token>();
            bool expressionOnTheLeft = false;

            foreach (var token in tokens)
            {
                if (token is OpeningParenthesisToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    operatorsStack.Push(token);
                }
                else if (token is ClosingParenthesisToken)
                {
                    if (!expressionOnTheLeft)
                    {
                        if (operatorsStack.Count > 0 && operatorsStack.Peek() is FunctionToken &&
                            ((FunctionToken) operatorsStack.Peek()).ArgumentCount == 0) // this is 0 argument function
                        {
                            ProcessFunction(expressionStack, operatorsStack.Pop() as FunctionToken);
                        }
                        else
                        {
                            throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            if (operatorsStack.Count == 0)
                            {
                                throw new ExpressionParseException("Parentheses mismatch");
                            }
                            var oper = operatorsStack.Pop();
                            if (oper is OpeningParenthesisToken)
                            {
                                expressionStack.Push(ExpressionBuilder.Parentheses(expressionStack.Pop()));
                                break;
                            }
                            else if (oper is ArithmeticOperatorToken)
                            {
                                ProcessArithmeticOperator(expressionStack, oper as ArithmeticOperatorToken);
                            }
                            else if (oper is FunctionToken)
                            {
                                (oper as FunctionToken).IncrementArgumentCounter();
                                ProcessFunction(expressionStack, oper as FunctionToken);
                                break;
                            }
                            else
                            {
                                throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                            }
                        }
                    }
                }
                else if (token is ArgumentSeparatorToken)
                {
                    if (!expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    while (true)
                    {
                        if (operatorsStack.Count == 0)
                        {
                            throw new ExpressionParseException("Argument separator misplaced");
                        }
                        var oper = operatorsStack.Peek();
                        if (oper is ArithmeticOperatorToken)
                        {
                            ProcessArithmeticOperator(expressionStack, operatorsStack.Pop() as ArithmeticOperatorToken);
                        }
                        else if (oper is FunctionToken)
                        {
                            (oper as FunctionToken).IncrementArgumentCounter();
                            break;
                        }
                        else
                        {
                            throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                        }
                    }
                    expressionOnTheLeft = false;
                }
                else if (token is ArithmeticOperatorToken)
                {
                    if (!expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    while (operatorsStack.Count > 0 && operatorsStack.Peek() is ArithmeticOperatorToken)
                        // TODO: add operator precedence condition
                    {
                        ProcessArithmeticOperator(expressionStack, operatorsStack.Pop() as ArithmeticOperatorToken);
                    }
                    operatorsStack.Push(token);
                    expressionOnTheLeft = false;
                }
                else if (token is VariableToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    expressionStack.Push(ExpressionBuilder.Variable((token as VariableToken).Name));
                    expressionOnTheLeft = true;
                }
                else if (token is StringConstantToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    expressionStack.Push(ExpressionBuilder.String((token as StringConstantToken).String));
                    expressionOnTheLeft = true;
                }
                else if (token is IntegerConstantToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    expressionStack.Push(ExpressionBuilder.Integer((token as IntegerConstantToken).Int));
                    expressionOnTheLeft = true;
                }
                else if (token is DecimalConstantToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    expressionStack.Push(ExpressionBuilder.Decimal((token as DecimalConstantToken).Decimal));
                    expressionOnTheLeft = true;
                }
                else if (token is FunctionToken)
                {
                    if (expressionOnTheLeft)
                        throw new ExpressionParseException($"Unexpected token: {token.DisplayString}");
                    operatorsStack.Push(token);
                }

            }

            while (operatorsStack.Count > 0)
            {
                var oper = operatorsStack.Pop();
                if (oper is ArithmeticOperatorToken)
                {
                    ProcessArithmeticOperator(expressionStack, oper as ArithmeticOperatorToken);
                }
                else
                {
                    // this could be either opening parenthesis or funcion (with opening parenthesis)
                    throw new ExpressionParseException("Parentheses mismatch");
                }
            }

            if (expressionStack.Count == 1)
            {
                return expressionStack.Peek();
            }
            else
            {
                throw new ExpressionParseException("");
            }
        }

        private static void ProcessArithmeticOperator(Stack<Expression> expressionStack, ArithmeticOperatorToken oper)
        {
            if (expressionStack.Count < 2)
                throw new ExpressionParseException($"Missing argument(s) for {oper.DisplayString} operator");
            var rightExp = expressionStack.Pop();
            var leftExp = expressionStack.Pop();
            expressionStack.Push(ExpressionBuilder.Arithmetic(leftExp, oper.Operator, rightExp));
        }

        private static void ProcessFunction(Stack<Expression> expressionStack, FunctionToken func)
        {
            if (expressionStack.Count < func.ArgumentCount)
                throw new ExpressionParseException($"Missing argument(s) for {func.Name} function");
            var args = new LinkedList<Expression>();
            for (var i = 0; i < func.ArgumentCount; i++)
            {
                args.AddFirst(expressionStack.Pop());
            }
            expressionStack.Push(ExpressionBuilder.Function(func.Name, args.ToArray()));
        }

        public static List<Token> ReadTokens(String str)
        {
            var inWord = false;
            var inStringConstant = false;
            var tokens = new List<Token>();
            var chars = PrepareString(str).ToCharArray();
            StringBuilder buffer = null;

            for (var i = 0; i < chars.Length; i++)
            {
                var nextChar = chars[i];
                if (inWord)
                {
                    if (Char.IsWhiteSpace(nextChar) || nextChar == '\n' || nextChar == ')' || nextChar == ',' ||
                        ArithmeticOperator.IsOperator(nextChar))
                    {
                        var identifier = buffer.ToString();
                        if (VariableRegex.IsMatch(identifier))
                        {
                            tokens.Add(new VariableToken(identifier));
                        }
                        else if (IntegerRegex.IsMatch(identifier))
                        {
                            try
                            {
                                tokens.Add(new IntegerConstantToken(long.Parse(identifier)));
                            }
                            catch (OverflowException)
                            {
                                throw new ExpressionParseException($"Too big integer: {identifier}");
                            }
                        }
                        else if (DecimalRegex.IsMatch(identifier))
                        {
                            try
                            {
                                tokens.Add(new DecimalConstantToken(decimal.Parse(identifier)));
                            }
                            catch (Exception)
                            {
                                throw new ExpressionParseException($"Too big decimal: {identifier}");
                            }
                        }
                        else
                        {
                            throw new ExpressionParseException($"Illegal identifier: {identifier}");
                        }
                        inWord = false;
                        buffer = null;
                    }
                    else if(Char.IsLetterOrDigit(nextChar) || nextChar == '_')
                    {
                        buffer.Append(nextChar);
                    }
                    else if(nextChar == '(')
                    {
                        var identifier = buffer.ToString();
                        if (FunctionNameRegex.IsMatch(identifier))
                        {
                            tokens.Add(new FunctionToken(identifier));
                        }
                        else
                        {
                            throw new ExpressionParseException($"Illegal function name: {identifier}");
                        }
                        inWord = false;
                        buffer = null;
                        continue;
                    }
                    else
                    {
                        throw new ExpressionParseException($"Unexpected symbol at {i}: {nextChar}");
                    }
                }
                else if (inStringConstant)
                {
                    if (nextChar == '"')
                    {
                        tokens.Add(new StringConstantToken(buffer.ToString()));
                        buffer = null;
                        inStringConstant = false;
                    }
                    else if(nextChar == '\n')
                    {
                        throw new ExpressionParseException("Missing closing quote");
                    }
                    else
                    {
                        buffer.Append(nextChar);
                    }
                    continue;
                }
                
                if (!inWord)
                {
                    if (Char.IsWhiteSpace(nextChar) || nextChar == '\n')
                    {
                        // just skip
                    }
                    else if (Char.IsLetterOrDigit(nextChar) || nextChar == '_' || (nextChar == '-' && Char.IsDigit(chars[i + 1])))
                    {
                        inWord = true;
                        buffer = new StringBuilder().Append(nextChar);
                    }
                    else if (nextChar == '(')
                    {
                        tokens.Add(new OpeningParenthesisToken());
                    }
                    else if (nextChar == ')')
                    {
                        tokens.Add(new ClosingParenthesisToken());
                    }
                    else if (nextChar == ',')
                    {
                        tokens.Add(new ArgumentSeparatorToken());
                    }
                    else if (ArithmeticOperator.IsOperator(nextChar))
                    {
                        tokens.Add(new ArithmeticOperatorToken(ArithmeticOperator.FromChar(nextChar)));
                    }
                    else if(nextChar == '"')
                    {
                        inStringConstant = true;
                        buffer = new StringBuilder();
                    }
                    else
                    {
                        throw new ExpressionParseException($"Unexpected symbol at {i}: {nextChar}");
                    }
                }
            }

            return tokens;
        }
    }
}
