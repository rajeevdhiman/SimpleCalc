using System.Linq;

namespace SimpleCalc.Extensions
{
    static class StringExtensions
    {
        public static bool EndWithAny(this string message, char[] chars)
        {
            return chars.Any(ch => message.EndsWith(new string(ch, 1)));
        }
    }
}
