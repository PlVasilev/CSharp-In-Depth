using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks101
{
    class Tasks101
    {
        static void Main(string[] args)
        {

            new Thread(Hello).Start();
            Task.Run(Hello); // Use that than - Task task = new Task(() => { Console.WriteLine(""); });
            Task.Factory.StartNew(() => Hello(), TaskCreationOptions.LongRunning); // Used rearly

            Task<long> task = Task<long>.Run(() =>
            {
                Thread.Sleep(2000);
                long sum = 0;
                for (int i = 0; i < 10000; i++) sum += i;
                return sum;
            });

            Console.WriteLine(task.IsCompleted);
            task.Wait(); // Blocking operation stays on the Thread use async-await
            Console.WriteLine(task.IsCompleted);
            Console.WriteLine(task.Result); 
            Console.WriteLine("After result");

        }

        // old way
        public delegate void MethodCompletedEventHandler(object sender, MethodCompletedEventArgs e);


        static void Hello()
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Zdraveite");
        }
    }
    public class MethodCompletedEventArgs : AsyncCompletedEventArgs
    {
        public MethodCompletedEventArgs(Exception ex, bool canceled, object userState)
            : base(ex, canceled, userState)
        {
        }

        public int Result { get; }
    }

}
