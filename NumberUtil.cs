using System;

namespace CodeKata.NumbersToText
{
    public static class NumberUtil
    {

        public static string ToText(int number)
        {
            if (number < 0 || number > 9999)
                throw new ArgumentOutOfRangeException(nameof(number), number, "min is 0 and max is 9999");

            return InternalToText(number);
        }

        private static string InternalToText(int number)
        {

        }
    }
}
