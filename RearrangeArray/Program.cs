using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RearrangeArray
{
    internal static class Program
    {
        private static void Main()
        {
            ReArrangeArray();
        }

        private static void ReArrangeArray()
        {
            var array = new [] {"red", "green", "yellow", "blue", "purple"};
            
            var stopWatch = new Stopwatch();
            
            stopWatch.Start();
            var newArray = GetArrayWithDifferentFirstElement1(array.ToArray(), "blue");
            stopWatch.Stop();
            var result1 = stopWatch.ElapsedTicks;

            stopWatch.Reset();
            stopWatch.Start();
            var newArray2 = GetArrayWithDifferentFirstElement2(array.ToArray(), "blue");
            stopWatch.Stop();
            var result2 = stopWatch.ElapsedTicks;

            stopWatch.Reset();
            stopWatch.Start();
            var newArray3 = GetArrayWithDifferentFirstElement3(array.ToArray(), "blue");
            stopWatch.Stop();
            var result3 = stopWatch.ElapsedTicks;

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray));

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray2));

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray3));

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"First result, ticks: {result1}");
            Console.WriteLine($"Second result, ticks: {result2}");
            Console.WriteLine($"Third result, ticks: {result3}");
            Console.ReadKey();
        }

        /// <summary>
        /// Straight forward approach.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private static string[] GetArrayWithDifferentFirstElement1(string[] array, string color)
        {
            var index = Array.IndexOf(array, color);
            if (index <= 0) return array; // Failed to set the new color.
            var list = new List<string>();
            for (var i = index; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
            for (var i = 0; i < index; i++)
            {
                list.Add(array[i]);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Shift by one item approach.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private static string[] GetArrayWithDifferentFirstElement2(string[] array, string color)
        {
            var index = Array.IndexOf(array, color);
            if (index <= 0) return array; // Failed to set the new color.
            for (var i = 0; i < index; i++)
            {
                ShiftArray(array);
            }
            return array;
        }

        /// <summary>
        /// Shifts items by 1.
        /// </summary>
        /// <param name="array"></param>
        private static void ShiftArray(IList<string> array) 
        { 
            var firstItem = array[0];
            for (var i = 0; i < array.Count - 1; i++)
            {
                array[i] = array[i + 1];
            }
            array[array.Count - 1] = firstItem; 
        }

        /// <summary>
        /// Algorithmic approach, theoretically should be faster.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private static string[] GetArrayWithDifferentFirstElement3(string[] array, string color)
        {
            var index = Array.IndexOf(array, color);
            if (index <= 0) return array; // Failed to set the new color.
            ShiftArray2(array,index);
            return array;
        }

        private static void ShiftArray2(IList<string> array, int index)
        {
            var arrayLength = array.Count;
            var gcd = GetGreatestCommonDivisor(index, arrayLength);
            for (var i = 0; i < gcd; i++) 
            { 
                var upperItem = array[i]; 
                var tempIndex = i; 
                while (true) { 
                    var tempIndex2 = tempIndex + index; 
                    if (tempIndex2 >= arrayLength) 
                        tempIndex2 = tempIndex2 - arrayLength; 
                    if (tempIndex2 == i) 
                        break; 
                    array[tempIndex] = array[tempIndex2]; 
                    tempIndex = tempIndex2; 
                } 
                array[tempIndex] = upperItem; 
            } 
        }

        /// <summary>
        /// Gets the greatest common divisor - https://en.wikipedia.org/wiki/Greatest_common_divisor
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        private static int GetGreatestCommonDivisor(int n1, int n2)
        {
            while (true)
            {
                if (n2 == 0) return n1;
                var a = n1;
                n1 = n2;
                n2 = a % n2;
            }
        }
    }
}
