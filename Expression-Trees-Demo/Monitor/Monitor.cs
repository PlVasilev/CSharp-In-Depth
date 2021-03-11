using System;
using System.Threading;

namespace Monitor
{
    class Monitor
    {
        static object obj1 = new object();
        static object obj2 = new object();
        public static void Main(string[] args)
        {
                Thread t1 = new Thread(Calculation);
                t1.Start();
        }



        static object tLock = new object();
        public static void Calculation()
        {
            System.Threading.Monitor.Enter(tLock);
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    Thread.Sleep(new Random().Next(5));
                    Console.Write(" {0},", i);
                }
            }
            catch { }
            finally
            {
                System.Threading.Monitor.Exit(tLock);
            }
            Console.WriteLine();
        }

    }


}

