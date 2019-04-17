using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace IntegersSorting
{
    internal static class Program
    {
        private static void Main()
        {
            var initialArray = GenerateUniqueNumbersArray(1, 1000);

            Console.Write(string.Join(Environment.NewLine, initialArray));

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            var newArray1 = BubbleSortIntegers(initialArray.ToArray());
            stopWatch.Stop();
            var result1 = stopWatch.ElapsedMilliseconds;

            stopWatch.Reset();
            stopWatch.Start();
            var newArray2 = MergeSortIntegers(initialArray.ToArray(), 0, initialArray.Length - 1);
            stopWatch.Stop();
            var result2 = stopWatch.ElapsedMilliseconds;

            stopWatch.Reset();
            stopWatch.Start();
            var newArray3 = CustomSortIntegers(initialArray.ToArray());
            stopWatch.Stop();
            var result3 = stopWatch.ElapsedMilliseconds;

            stopWatch.Reset();
            stopWatch.Start();
            var newArray4 = initialArray.ToArray().OrderBy(x => x);
            stopWatch.Stop();
            var result4 = stopWatch.ElapsedMilliseconds;
            

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray1));

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray2));

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray3));

            Console.WriteLine(Environment.NewLine);
            Console.Write(string.Join(Environment.NewLine, newArray4));

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Bubble sort result, ms: {result1}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Merge sort result, ms: {result2}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Custom sort result, ms: {result3}");
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"Linq OrderBy sort result, ms: {result4}");
            Console.ReadKey();
        }

        /// <summary>
        /// Generating an unsorted array
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static int[] GenerateUniqueNumbersArray(int start, int end)
        {
            var random = new Random();
            var array = Enumerable.Range(start, end).ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var randomIndex = random.Next(i + 1);
                var n1 = array[randomIndex];
                var n2 = array[i];
                array[randomIndex] = n2;
                array[i] = n1;
            }
            return array;
        }

        /// <summary>
        /// Bubble sorting algorithm.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static IEnumerable<int> BubbleSortIntegers(IList<int> array)
        {
            for (var i = 0; i < array.Count; i++)
            {
                for (var x = 0; x < array.Count - i - 1; x++)
                {
                    if (array[x] <= array[x + 1]) continue;
                    var tempValue = array[x];
                    array[x] = array[x + 1];
                    array[x + 1] = tempValue;
                }
            }

            return array;
        }

        /// <summary>
        /// Merge sort algorithm seems to be the fastest on large numbers.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static IEnumerable<int> MergeSortIntegers(IList<int> array, int start, int end)
        {
            if (start >= end) return array;
            var middleIndex = (start + end) / 2;
            MergeSortIntegers(array, start, middleIndex);
            MergeSortIntegers(array, middleIndex + 1, end);
            Merge(array, start, middleIndex, end);

            return array;
        }

        private static void Merge(IList<int> array, int i, int mid, int j)
        {
            var tempValue = new int[array.Count];
            var l = i;
            var r = j;
            var m = mid + 1;
            var k = l;

            while(l <= mid && m <= r)
            {
              if(array[l] <= array[m])
              {
                tempValue[k++] = array[l++];
              }
              else
              {
                tempValue[k++] = array[m++];
              }
            }

            while(l <= mid)
              tempValue[k++] = array[l++];

            while(m <= r)
            {
              tempValue[k++] = array[m++];
            }

            for(var i1 = i; i1 <= j; i1++)
            {
              array[i1] = tempValue[i1];
            }
        }

        /// <summary>
        /// This custom sorting is fast enough on short numbers and yet seems to be faster than the Bubble sorting algorithm.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private static IEnumerable<int> CustomSortIntegers(IEnumerable<int> array)
        {
            var list = new List<int>();
            foreach (var t in array)
            {
                if (list.Count == 0 || list[list.Count - 1] < t)
                {
                    list.Add(t);
                }
                else if (list[0] > t)
                {
                    list.Insert(0, t);
                }
                else
                {
                    // Insert in the middle.
                    if (t - list[0] < list[list.Count - 1] - t)
                    {
                        //if the number is closer to the beginning.
                        for (var i = 0; i < list.Count; i++)
                        {
                            if (t >= list[i]) continue;
                            list.Insert(i, t);
                            break;
                        }
                    }
                    else
                    {
                        //if the number is closer to the end.
                        for (var i = list.Count - 2; i > -1; i--)
                        {
                            if (t <= list[i]) continue;
                            list.Insert(i + 1, t);
                            break;
                        }
                    }
                }
            }

            return list;
        }


    }
}
