using System;
using System.Threading;

namespace Monitor2
{
    class Monitor2
    {
        public static object obj = new object();

        static void Main(string[] args)
        {

            new Thread(Boss).Start();
            Thread.Sleep(300);
            new Thread(Worker).Start();
            Console.WriteLine();
        }

        private static void Worker()
        {
            while (true)
            {
                Monitor.Enter(obj);
                Console.WriteLine("I`m inside worker");
                Thread.Sleep(1000);
                Monitor.PulseAll(obj);
                Console.WriteLine("I`m outside worker");
                Monitor.Exit(obj);
            }
        }

        private static void Boss()
        {

            while (true)
            {
                Monitor.Enter(obj);
                Console.WriteLine("I`m inside BOSS");
                Thread.Sleep(1000);
                Console.WriteLine("waiting BOSS");
                Monitor.Wait(obj);
                Console.WriteLine("I`m outside BOSS");
                Monitor.Exit(obj);
            }
        }
    }
}
