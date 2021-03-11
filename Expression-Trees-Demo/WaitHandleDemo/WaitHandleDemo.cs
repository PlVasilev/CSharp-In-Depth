using System;
using System.Threading;

namespace WaitHandleDemo
{
    class WaitHandleDemo
    {
        static void Main(string[] args)
        {
            ManualResetEvent[] events = new ManualResetEvent[10];
            for (int i = 0; i < events.Length; i++)
            {
                events[i] = new ManualResetEvent(false);
                Runner runner = new Runner(events[i], i);
                new Thread(runner.Run).Start();
            }
            int index = WaitHandle.WaitAny(events);
            Console.WriteLine("***** The winner is {0} *****", index);
            WaitHandle.WaitAll(events);
            Console.WriteLine("All finished!");
        }
    }

    public class Runner
    {
        static readonly object rngLock = new object();
        static Random random = new Random();
        ManualResetEvent manualResetEvent;
        int id;
        public Runner(ManualResetEvent manualResetEvent, int id)
        {
            this.manualResetEvent = manualResetEvent;
            this.id = id;
        }
        public void Run()
        {
            for (int i = 0; i < 10; i++)
            {
                int sleepTime;
                lock (rngLock)
                {
                    sleepTime = random.Next(2000);
                }
                Thread.Sleep(sleepTime);
                Console.WriteLine($"Runner {id} at stage {i}");
            }
            manualResetEvent.Set();
        }
    }
}
