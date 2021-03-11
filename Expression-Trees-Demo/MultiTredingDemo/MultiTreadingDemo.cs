using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace MultiTreadingDemo
{
    class MultiTreadingDemo
    {
        static void Main(string[] args)
        {
            var primes = new Primes();
            int times = 8;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < times; i++)
            {
                primes.CalculatePrimes(new Tuple<int, int>(i * 10000, (i * 10000) + 10000));
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(primes.primes.Count);
            Console.WriteLine();
            sw.Reset();
            sw.Start();
            var primes2 = new Primes();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < times; i++)
            {
                // new Thread(Primes.CalculatePrimes).Start();
                //CLOSURE int j = i;

                var tuple = new Tuple<int, int>(i * 10000, (i * 10000) + 10000);
                var t = new Thread((() => primes2.CalculatePrimes(tuple)));
                t.Start();
                threads.Add(t);
            }

            // w8 until thread is finished work
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(primes2.primes.Count);
        }
    }

    public class Primes
    {
        public List<int> primes = new List<int>();

        public void CalculatePrimes(object data)
        {
            int start, end;
            var input = (Tuple<int, int>) data;
            start = input.Item1;
            end = input.Item2;
            try
            {
                for (int i = start; i < end; i++)
                {
                    bool isPrime = true;
                    for (int j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }

                    if (isPrime)
                    {
                        primes.Add(i);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}
