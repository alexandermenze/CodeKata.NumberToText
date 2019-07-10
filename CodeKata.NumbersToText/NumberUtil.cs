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

        private static readonly Dictionary<string, Func<string, string>> regexFixTable = new Dictionary<string, Func<string, string>>()
        {
            { "^einsundzehn$", s => Regex.Replace(s, "^einsundzehn$", "elf") },
            { "^zweiundzehn$", s => Regex.Replace(s, "^zweiundzehn$", "zwölf") },
            { "^dreiundzehn$", s => Regex.Replace(s, "^dreiundzehn$", "dreizehn") },
            { "^vierundzehn$", s => Regex.Replace(s, "^vierundzehn$", "vierzehn") },
            { "^fünfundzehn$", s => Regex.Replace(s, "^fünfundzehn$", "fünfzehn") },
            { "^sechsundzehn$", s => Regex.Replace(s, "^sechsundzehn$", "sechzehn") },
            { "^siebenundzehn$", s => Regex.Replace(s, "^siebenundzehn$", "siebzehn") },
            { "^achtundzehn$", s => Regex.Replace(s, "^achtundzehn$", "achtzehn") },
            { "^neunundzehn$", s => Regex.Replace(s, "^neunundzehn$", "neunzehn") },
            { "einsundzehn$", s => Regex.Replace(s, "einsundzehn$", "undelf") },
            { "zweiundzehn$", s => Regex.Replace(s, "zweiundzehn$", "undzwölf") },
            { "dreiundzehn$", s => Regex.Replace(s, "dreiundzehn$", "unddreizehn") },
            { "vierundzehn$", s => Regex.Replace(s, "vierundzehn$", "undvierzehn") },
            { "fünfundzehn$", s => Regex.Replace(s, "fünfundzehn$", "undfünfzehn") },
            { "sechsundzehn$", s => Regex.Replace(s, "sechsundzehn$", "undsechzehn") },
            { "siebenundzehn$", s => Regex.Replace(s, "siebenundzehn$", "undsiebzehn") },
            { "achtundzehn$", s => Regex.Replace(s, "achtundzehn$", "undachtzehn") },
            { "neunundzehn$", s => Regex.Replace(s, "neunundzehn$", "undneunzehn") },
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

                if (realTens.Length == 1 && realUnitPlaces.Length == 2)
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
            var fixTableAsList = regexFixTable.ToList();

            for (var i = 0; i < fixTableAsList.Count; i++)
            {
                var entry = fixTableAsList[i];

                if (Regex.IsMatch(s, entry.Key))
                {
                    s = entry.Value(s);
                    i = 0;
                }
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
            var returnZero = true;

            while (number >= 10)
            {
                returnZero = false;
                var normified = NormifyNumber(number);
                number -= normified;
                yield return normified;
            }

            if (number != 0 || returnZero)
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
