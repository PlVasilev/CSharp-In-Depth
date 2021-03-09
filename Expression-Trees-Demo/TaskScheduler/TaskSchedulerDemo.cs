using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSchedulerDemo

{
    class TaskSchedulerDemo
    {
        static void Main(string[] args)
        {
            // make all tasks to be on one thread
            var taskFactory = new TaskFactory(new SimpleScheduler());
            for (int i = 0; i < 100; i++)
            {
                taskFactory.StartNew((() => { Console.WriteLine(Thread.CurrentThread.ManagedThreadId); }));
            }

            Console.ReadLine();
        }
    }

    internal class SimpleScheduler : TaskScheduler
    {


        // make all tasks to be on one thread
        BlockingCollection<Task> tasks = new BlockingCollection<Task>();
        private Thread main;

        public SimpleScheduler()
        {
            main = new Thread(Execute);
            main.Start();
        }

        protected override IEnumerable<Task> GetScheduledTasks() // get all tasks
        {
            return tasks.GetConsumingEnumerable();
        }

        private void Execute()
        {
            while (tasks.Count >0)
            {
               var task =  tasks.Take();
               TryExecuteTask(task);
            }
        }

        protected override void QueueTask(Task task) //
        {
            tasks.Add(task);
            if (!main.IsAlive)
            {
                main = new Thread(Execute);
                main.Start();
            }
            
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) // should it be runned on the same thread
        {
           return false;
        }
    }

    //In presenation
    public sealed class SimpleSchedulerPresentation : TaskScheduler, IDisposable
    {
        private readonly BlockingCollection<Task> tasks;
        private readonly Thread mainThread;

        public SimpleSchedulerPresentation()
        {
            tasks = new BlockingCollection<Task>();
            mainThread = new Thread(this.Main);
        }

        private void Main()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Starting Thread " + threadId);
            foreach (var t in tasks.GetConsumingEnumerable())
            {
                this.TryExecuteTask(t);
            }
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return tasks.ToArray();
        }

        protected override void QueueTask(Task task)
        {
            tasks.Add(task);
            if (!mainThread.IsAlive)
            {
                mainThread.Start();
            }
        }

        protected override bool TryExecuteTaskInline(Task task, bool previouslyQueue)
            => false;

        public void Dispose()
        {
            tasks.CompleteAdding();
        }
    }
}
