using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StateMachine
{
    class StateMachine
    {
        static void Main(string[] args)
        {
            // Tasks use thread pool ever work is handled by task scheduler
            // Async and await Generates a lot of code in IL cuz of state machine
            // the idea is it takes the state from thread and when the work is done gives it to some free thread
            // Every local variable is becoming a field in state machine 
            // the code generated is equal to for with 40 iterations
            // state machine is Class stays on heep cuz its been called when needed with ref StateMachine;
            // when it was Struct stayed on stack but then everything needed to to be sync
            // everything is synchronous until first return of state machine

            //Task problem is GC.Collect - 

            //long work

            for (int i = 0; i < 100; i++)
            {
                Task.Factory.StartNew(LongWork, TaskCreationOptions.LongRunning); // always use when task is doing some big job
                //Task.Run(LongWork);
            }

            Console.ReadLine();

            // Synchronization context
            // returns await in the same context - if we are in main thread it will return us in main thread 
            //      if returned from thread pool there is no sync context 
            Console.WriteLine("Sync : " + SynchronizationContext.Current);

            Console.ReadLine();

            var t = GetAsync();
            var awaiter = t.GetAwaiter();
            awaiter.UnsafeOnCompleted((() =>
            {
                Console.WriteLine($"After {t.Result}");
            }));
            Console.ReadLine();

        }

        static async Task Hello()
        {
            // returns await in the same context - if we are in main thread it will return us in main thread 
            //      if returned from thread pool there is no sync context 
            await DoLongWork().ConfigureAwait(true);
        }

        static async Task DoLongWork()
        {
            Console.WriteLine($"Work starting on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(20000);
        }

        static void LongWork()
        {
            Console.WriteLine($"Work starting on thread {Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(20000);
        }

        static async Task<string> GetAsync()
        {
            //HttpClient wc = new HttpClient();
            //Console.WriteLine("Before first await");
            //await wc.GetAsync("test");
            //Console.WriteLine("After first await");
            //
            //Console.WriteLine("Before second await");
            //await wc.GetAsync("test");
            //Console.WriteLine("After second await");

            await Task.Delay(1);
            Console.WriteLine("After Hello");
            return "all done";
        }
    }
}
