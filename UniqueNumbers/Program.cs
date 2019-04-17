using System;
using System.Linq;

namespace UniqueNumbers
{
    internal static class Program
    {
        private static void Main()
        {
            CreateUniqueNumbersArray(1, 100000);
        }

        private static void CreateUniqueNumbersArray(int start, int end)
        {
            var random = new Random();
            //Creating a sorted array
            var array = Enumerable.Range(start, end).ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                //Obtaining a random index.
                var randomIndex = random.Next(i + 1);
                // Getting the numbers at the current position and the random position.
                var n1 = array[randomIndex];
                var n2 = array[i];
                // Swapping them.
                array[randomIndex] = n2;
                array[i] = n1;
            }
        }

    }
}
