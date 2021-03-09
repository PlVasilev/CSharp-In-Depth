using System;
using System.Threading;

namespace StackTrace
{
    class StackTrace
    {
        static void Main(string[] args)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            Console.WriteLine(st);
            new Thread(Hey).Start();
            new Thread(Hey).Start();
            new Thread(Hey).Start();
            Console.WriteLine("END");
            Console.ReadLine();
        }

        static void Hey()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}" + st);
            Thread.Sleep(15000);
        }
    }
}
