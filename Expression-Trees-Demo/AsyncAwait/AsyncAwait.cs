using System;
using System.IO;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class AsyncAwait
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Task task = DoWorkAsync();
            task.Wait();
            task.ContinueWith((task1 => { Console.WriteLine("Over"); }))
                .ContinueWith((task1 => { Console.WriteLine("OverAgain"); }));

            Console.ReadLine();
        }

       // public static Task<int> ReadTask(this Stream stream, byte[] buffer, int offset, int count, object state)
       // {
       //     var tcs = new TaskCompletionSource<int>();
       //     stream.BeginRead(buffer, offset, count, ar => {
       //         try
       //         {
       //             tcs.SetResult(stream.EndRead(ar));
       //         }
       //         catch (Exception exc)
       //         {
       //             tcs.SetException(exc);
       //         }
       //     }, state);
       //
       //     return tcs.Task;
       // }


        static async Task DoWorkAsync()
        {
            Console.WriteLine($"Before await {Thread.CurrentThread.ManagedThreadId}" );
            await Task.Run(DoWork);
            Console.WriteLine($"After awat {Thread.CurrentThread.ManagedThreadId}" );
            Console.WriteLine("Hello");
        }

        static void DoWork()
        {
            Console.WriteLine($"In do work {Thread.CurrentThread.ManagedThreadId}" );
            Thread.Sleep(2000);
        }
    }
}
