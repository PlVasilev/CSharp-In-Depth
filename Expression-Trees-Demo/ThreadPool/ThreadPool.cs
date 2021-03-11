using System;
using System.Threading;

namespace ThreadPool1
{
    class ThreadPool1
    {
        static void Main(string[] args)
        {
            int max, num;
            ThreadPool.GetAvailableThreads(out max, out num);
            Console.WriteLine($"Available threads {max}");
            ThreadPool.GetMinThreads(out max, out num);
            Console.WriteLine($"Minimum threads {max}");
            ThreadPool.GetMaxThreads(out max, out num);
            Console.WriteLine($"Minimum threads {max}");


            //ALL THREADS ARE BACKGROUND BY DEFAULT
            var t = new Thread(Print);
            t.IsBackground = true;
            t.Start();

            ThreadPool.QueueUserWorkItem((object obj) => Print());

        }

        public static void Print()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
