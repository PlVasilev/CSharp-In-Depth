using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WaitAll
{
    class WaitAll
    {
        static void Main(string[] args)
        {
            List<Task<Thread>> tasks = new List<Task<Thread>>();
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(Task.Run(Zdr));
                tasks.Add(Zdr()); // faster
            }
            
            int index = Task.WaitAny(tasks.ToArray()); // first one to finish work
            Console.WriteLine(tasks[index].Result.ManagedThreadId);
            Task.WaitAll(tasks.ToArray()); // wait all to finish work
            Console.WriteLine("Done");
        }

        static async Task<Thread> Zdr()
        {
           // Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            var rand = new Random();
            await Task.Delay(rand.Next(1000, 3000));
            return Thread.CurrentThread;
        }
    }
}
