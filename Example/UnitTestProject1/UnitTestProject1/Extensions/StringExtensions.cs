using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.Extensions
{
    public static class StringExtensions
    {
        public static void ParseString(this string st)
        {
            if (st.EndsWith(@"\"))
            {
                st.Remove(st.Length - 1);
            }

            if (st.StartsWith(@"\"))
            {
                st.Remove(0);
            }
        }

        public static int FirstUnmatchedIndex(this string st, string compare)
        {
            int counter;
            var size = (st.Length > compare.Length) ? compare.Length : st.Length;
            for (counter = 0; counter < size; counter++)
            {
                if (!st[counter].Equals(compare[counter]))
                {
                    return counter;
                }
            }
            if (st.Length != compare.Length)
            {
                return counter;
            }
            return -1;
        }
    }
}
