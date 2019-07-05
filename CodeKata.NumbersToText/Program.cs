using System;

namespace CodeKata.NumbersToText
{
    internal class Program
    {
        private const string ExitSymbol = "e";

        internal static void Main(string[] args)
        {
            var input = GetInput();

            while (string.Compare(ExitSymbol, input, StringComparison.OrdinalIgnoreCase) != 0)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine(NumberUtil.ToText(int.Parse(input)));
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine();
                input = GetInput();
            }
        }

        private static string GetInput()
        {
            Console.Write("Gib eine Zahl ein: ");
            return Console.ReadLine();
        }
    }
}
