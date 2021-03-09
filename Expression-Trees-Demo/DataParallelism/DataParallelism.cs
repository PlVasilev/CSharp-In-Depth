using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace DataParallelism
{
    class DataParallelism // always do to operations that do some work - overhead it too big otherwise
    {
        static ConcurrentDictionary<int, List<int>> dict = new ConcurrentDictionary<int, List<int>>();

        static ConcurrentDictionary<int, List<string>> partDict = new ConcurrentDictionary<int, List<string>>();

        static void Main(string[] args)
        {
            var list = Enumerable.Range(0, 1000000).ToList();
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var item in list)
            {
                for (int i = 0; i < 100; i++)
                {
                    var num = 5;
                }
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);

             sw.Reset();

             sw.Start();
             Parallel.ForEach(list, (el) => {
                 for (int i = 0; i < 100; i++)
                 {
                     var num = 5;
                 }
             });
             sw.Stop();
             Console.WriteLine(sw.ElapsedMilliseconds);

            sw.Reset();

             sw.Start();
             Parallel.For(0, list.Count, (el) => {

                 for (int i = 0; i < 100; i++)
                 {
                     var num = 5;
                 }
             });
             sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);


            //Concurrent Dict
            var nums = Enumerable.Range(0, 100).ToList();
            Parallel.ForEach(nums, (el) =>
            {
                var id = Thread.CurrentThread.ManagedThreadId;
                if (!dict.ContainsKey(id))
                {
                    dict.TryAdd(id, new List<int>());
                }
                dict[id].Add(el);
            });

            foreach (var item in dict)
            {
                item.Value.Sort();
                Console.WriteLine($"Thread {item.Key}: {string.Join(", ", item.Value)}");
            }

            // Partitioner gets the collection and slice it to equal parts
            Console.WriteLine("Partitioner");
            var numsPart = Enumerable.Range(0, 100000).ToList();
            var partitioner = Partitioner.Create(0, numsPart.Count, 1000);

            Parallel.ForEach(partitioner, (startEnd) => // slices work to work parallel
            {
                for (int i = startEnd.Item1; i < startEnd.Item2; i++)
                {
                    var x = i;
                }


                var id = Thread.CurrentThread.ManagedThreadId;
                if (!partDict.ContainsKey(id))
                {
                    partDict.TryAdd(id, new List<string>());
                }
                partDict[id].Add(startEnd.Item1 + " : " + startEnd.Item2);
            });

            foreach (var item in partDict)
            {
                item.Value.Sort();
                Console.WriteLine($"Thread {item.Key}: {string.Join(", ", item.Value)}");
            }

            //Parallel LINQ

            int[] numsLinq = new int[] { 1, 2, 3, 4, 5 }; var evenNumsParallel = from num in numsLinq.AsParallel()
                where num % 2 == 0
                select num;
            var res = numsLinq.ToList();
            
            Console.WriteLine(string.Join(", ", res));

        }

        class Part : Partitioner<int> // our implemantaion
        {
            public override IList<IEnumerator<int>> GetPartitions(int partitionCount)
            {
                throw new NotImplementedException();
            }
        }
    }
}
