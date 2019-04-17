using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FooBar
{
    public static class PrintManager
    {
        private const string Foo = "Foo";
        private const string Bar = "Bar";

        public static void Print()
        {
            var stopWatch = new Stopwatch();

            const int start = 1;
            const int end = 1000;
            
            stopWatch.Start();
            PrintFooBar1(start, end);
            stopWatch.Stop();
            var result1 = stopWatch.ElapsedMilliseconds;

            stopWatch.Reset();
            stopWatch.Start();
            PrintFooBar2(start, end);
            stopWatch.Stop();
            var result2 = stopWatch.ElapsedMilliseconds;
            
            Console.WriteLine($"First method time, ms: {result1}");
            Console.WriteLine($"Second method time, ms: {result2}");
        }

        private static void PrintFooBar1(int start, int end)
        {
            var toPrint = "";
            for (var n = start; n < end + 1; n++)
            {
                toPrint += GetCorrectValue(n) + Environment.NewLine;
            }
            Console.Write(toPrint);
        }

        /// <summary>
        /// This method is faster on big numbers like 10000 instead of 100.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private static void PrintFooBar2(int start, int end)
        {
            var toPrint = "";
            var dictionary = new Dictionary<int, string>();
            for (var n = start; n < end + 1; n++)
            {
                dictionary.Add(n, n.ToString());
            }

            SetFooBar(3, Foo, dictionary, end);
            SetFooBar(5, Bar, dictionary, end);

            foreach (var val in dictionary.Values)
            {
                toPrint += val + Environment.NewLine;
            }
            Console.Write(toPrint);
        }

        private static void SetFooBar(int divisionBy, string newValue, IDictionary<int, string> dictionary, int end)
        {
            for (var i = divisionBy; i <= end; i += divisionBy)
            {
                if (!dictionary.ContainsKey(i)) continue;
                if(!int.TryParse(dictionary[i], out _))
                    dictionary[i] += $" {newValue}";
                else
                    dictionary[i] = newValue;
            }
        }

        private static string GetCorrectValue(int number)
        {
            if (number % 3 == 0 && number % 5 == 0) return $"{Foo} {Bar}";
            if (number % 3 == 0) return Foo;
            return number % 5 == 0 ? Bar : number.ToString();
        }
    }
}
