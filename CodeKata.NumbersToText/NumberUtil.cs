using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeKata.NumbersToText
{
    public static class NumberUtil
    {
        private const string BetweenLastTwoPlaces = "und";

        private static readonly Dictionary<string, string> translationTable = new Dictionary<string, string>
        {
            { "0", "null" },
            { "1", "eins" },
            { "2", "zwei" },
            { "3", "drei" },
            { "4", "vier" },
            { "5", "fünf" },
            { "6", "sechs" },
            { "7", "sieben" },
            { "8", "acht" },
            { "9", "neun" },
            { "10", "zehn" },
            { "20", "zwanzig" },
            { "30", "dreißig" },
            { "60", "sechzig" },
            { "70", "siebzig" }
        };

        private static readonly Dictionary<string, string> regexTable = new Dictionary<string, string>
        {
            { @"\d000$", "~tausend" },
            { @"\d00$", "~hundert" },
            { @"\d0$", "~zig" }

        };

        private static readonly Dictionary<string, string> fixTable = new Dictionary<string, string>
        {
            { "einsundzehn", "elf" },
            { "zweiundzehn", "zwölf" },
            { "dreiundzehn", "dreizehn" },
            { "vierundzehn", "vierzehn" },
            { "fünfundzehn", "fünfzehn" },
            { "sechsundzehn", "sechzehn" },
            { "siebenundzehn", "siebzehn" },
            { "achtundzehn", "achtzehn" },
            { "neunundzehn", "neunzehn" }
        };

        private static readonly Dictionary<string, Func<string, string>> regexFixTable = new Dictionary<string, Func<string, string>>()
        {
            {
                "eins(?!$)",
                s => Regex.Replace(s, "eins(?!$)", "ein")
            }
        };

        public static string ToText(int number)
        {
            if (number < 0 || number > 9999)
                throw new ArgumentOutOfRangeException(nameof(number), number, "min is 0 and max is 9999");

            return InternalToText(number);
        }

        private static string InternalToText(int number)
        {
            var normifiedNumbers = ToNormifiedNumbers(number);
            var asStringList = normifiedNumbers.Select(i => Convert.ToString(i)).ToList();

            if (asStringList.Count > 1)
            {
                var realUnitPlaces = asStringList[asStringList.Count - 2];
                var realTens = asStringList[asStringList.Count - 1];

                if (realTens.Length == 1)
                {
                    asStringList[asStringList.Count - 2] = realTens;
                    asStringList[asStringList.Count - 1] = realUnitPlaces;
                }
            }

            var joinedText = Concat(asStringList.Select(ToStringPart), BetweenLastTwoPlaces);
            return ProcessFixes(joinedText);
        }

        private static string ProcessFixes(string s)
        {
            foreach (var entry in fixTable)
            {
                s = s.Replace(entry.Key, entry.Value);
            }

            foreach (var entry in regexFixTable)
            {
                if (Regex.IsMatch(s, entry.Key))
                    s = entry.Value(s);
            }

            return s;
        }

        private static string ToStringPart(string numberAsString)
        {
            if (translationTable.ContainsKey(numberAsString))
            {
                return translationTable[numberAsString];
            }
            else
            {
                var matchString = GetMatch(numberAsString);

                if (matchString.Contains("~"))
                {
                    string firstNumber = numberAsString.Substring(0, 1);

                    return matchString.Replace("~", ToStringPart(firstNumber));
                }

                return matchString;
            }
        }

        private static IEnumerable<int> ToNormifiedNumbers(int number)
        {
            while (number >= 10)
            {
                var normified = NormifyNumber(number);
                number -= normified;
                yield return normified;
            }

            if (number != 0)
                yield return number;
        }

        private static int NormifyNumber(double number)
        {
            var counter = 0;

            while (number >= 10)
            {
                number /= 10;
                counter++;
            }

            var convertedInt = (int)number;

            while (--counter >= 0)
            {
                convertedInt *= 10;
            }

            return convertedInt;
        }

        private static string GetMatch(string s)
        {
            foreach (var entry in regexTable)
            {
                if (Regex.IsMatch(s, entry.Key))
                    return entry.Value;
            }

            return null;
        }

        private static string Concat(IEnumerable<string> strings, string betweenLastTwo)
        {
            var stringList = strings.ToList();

            if (stringList.Count == 1)
                return stringList[0];

            var stringBuilder = new StringBuilder(stringList.Count + 1);

            for (var i = 0; i < stringList.Count; i++)
            {
                stringBuilder.Append(stringList[i]);

                if (i == stringList.Count - 2)
                    stringBuilder.Append(betweenLastTwo);
            }

            return stringBuilder.ToString();
        }
    }
}
