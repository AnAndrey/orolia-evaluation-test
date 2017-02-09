using System.Linq;

namespace Orolia.DataParcer
{

    internal static class StringExtension
    {
        private static char s_headerMark = '#';
        private static byte s_index = 0;
        public static bool IsHeader(this string value)
        {
            return value.ElementAt(s_index) == s_headerMark;
        }
    }
}