using System;
using System.Threading;

namespace SemaphoreDemo
{
    class SemaphoreDemo
    {
        static Semaphore semaphore = new Semaphore(2, 4);


        static void Main(string[] args)
        {
            for (int i = 1; i <= 5; i++)
            {
                new Thread(SemaphoreStart).Start(i);
            }

        }

        public static void SemaphoreStart(object id)
        {
            Console.WriteLine(id + "-->>Wants to Get Enter");
            try
            {
                semaphore.WaitOne();
                Console.WriteLine(" Success: " + id + " is in!");
                Thread.Sleep(2000);
                Console.WriteLine(id + "<<-- is Evacuating");
            }
            finally
            {
                semaphore.Release();
            }
        }

    }
}
