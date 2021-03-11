using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace MultiThreadingP1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> numbers = new List<long>();
            Thread t = new Thread(() =>
                SumOddNumbers(numbers, 10, 100000000L));
            t.Start();
            
            Console.WriteLine("What should I do?");
            while (true)
            {
                string command = Console.ReadLine();
                
                if (command == "exit") break;
                Console.WriteLine();
            }
            t.Join();
        }

        static void SumOddNumbers(List<long> numbers, int a, long b)
        {
            for (int i = a; i < b; i++)
            {
                if (i % 2 != 0)
                {
                    numbers.Add(i);
                }
            }
        }
    }
}
