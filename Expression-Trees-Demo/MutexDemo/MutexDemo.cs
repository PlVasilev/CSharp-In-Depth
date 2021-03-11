using System;
using System.Threading;

namespace MutexDemo
{
    class MutexDemo
    {
        private static Mutex mutex = new Mutex(); 
        private static Mutex mutexPesho = new Mutex(false,"Pesho");

        static void Main(string[] args)
        {
            for (int i = 0; i < 4; i++)
            {
                Thread t = new Thread(MutexDemoMethod)
                {
                    Name = $"Thread {i + 1} :"
                };
                t.Start();
            }

            Mutex mutex;
            Mutex.TryOpenExisting("Pesho", out mutexPesho);
            if (mutexPesho == null)
            {
                mutexPesho = new Mutex(false, "Pesho");
            }
            else
            {
                mutexPesho.WaitOne();
            }

            mutexPesho.WaitOne();
            Console.WriteLine("Inside of mutex");
            Console.ReadLine();
            mutexPesho.ReleaseMutex();


        }

        public static void MutexDemoMethod()
        {
            try
            {
                mutex.WaitOne();   // Wait until it is safe to enter.  
                Console.WriteLine("{0} has entered in the Domain",
                    Thread.CurrentThread.Name);
                Thread.Sleep(1000);    // Wait until it is safe to enter.  
                Console.WriteLine("{0} is leaving the Domain\r\n",
                    Thread.CurrentThread.Name);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }



}
