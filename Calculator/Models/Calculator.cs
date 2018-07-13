using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleCalc.Models
{
    public class Calculator : ICalculator
    {
        public static readonly char[] OPERATORS = new char[] { '+', '-', '*', '/' };

        private static MatchCollection ParseExpression(string expression)
        {
            Regex expressionParser = new Regex(@"(?<Operator>[\+\-\/\*\=])(?<Operand>[0-9\.]+)", RegexOptions.Compiled);

            if (expressionParser.IsMatch(expression))
            {
                return expressionParser.Matches(expression);
            }
            return null;
        }

        public double Calculate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            var matchCollection = ParseExpression(expression);

            if (matchCollection == null) return 0;

            var firstOperand = expression.Substring(0, expression.IndexOfAny(OPERATORS));

            double result = string.IsNullOrEmpty(firstOperand) ? 0 : double.Parse(firstOperand);

            foreach (Match match in matchCollection)
            {
                string action = match.Groups["Operator"].Value;
                long nextOperand = long.Parse(match.Groups["Operand"].Value);

                switch (action)
                {
                    case "+":
                        result = result + nextOperand; break;
                    case "-":
                        result = result - nextOperand; break;
                    case "/":
                        result = result / nextOperand; break;
                    case "*":
                        result = result * nextOperand; break;
                }
            }

            return result;
        }
    }
}
