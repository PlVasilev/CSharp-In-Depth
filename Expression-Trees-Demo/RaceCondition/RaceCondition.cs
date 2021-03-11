using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace RaceCondition
{
    class RaceCondition
    {
        private static int nums = 0;
        static void Main(string[] args)
        {
            for (int i = 0; i < 8; i++)
            {
                new Thread(Increment).Start();
            }

            Console.ReadLine();
            Console.WriteLine(nums);

            List<int> numbers = Enumerable.Range(0, 10000).ToList();
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    while (numbers.Count > 0)
                    {
                       // numbers.RemoveAt(numbers.Count - 1);
                        lock (numbers)
                        {
                            if (numbers.Count == 0) break;
                            int lastIndex = numbers.Count - 1;
                            numbers.RemoveAt(lastIndex);
                        }
                    }
                }).Start();
            }

        }

        static object obj = new object();

        public static void Increment()
        {
            for (int i = 0; i < 100000; i++)
            {
               // nums++;
                lock (obj)
                {
                    nums++;
                }
            }
            Console.WriteLine("Finished");
        }
    }
}
