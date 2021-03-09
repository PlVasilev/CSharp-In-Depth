using System;

namespace GCollection
{
    class Program
    {
        private static int x; // gen 0 not at the same place as the others gen 0


        static void Main(string[] args)
        {
            
            var pgmArr = new byte[10];
            Console.WriteLine(GC.GetGeneration(pgmArr));

            var pgmArrNotLarge = new byte[84975];
            Console.WriteLine(GC.GetGeneration(pgmArrNotLarge)); // Red line small - large object

            var pgmArrLarge = new byte[84977];
            Console.WriteLine(GC.GetGeneration(pgmArrLarge)); // Large object 4th generation\\ not in dependency graph

            Console.WriteLine(GC.GetGeneration(x));

            var program = new Program();
            Console.WriteLine(GC.GetGeneration(program));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(program));
            GC.Collect();
            Console.WriteLine(GC.GetGeneration(program));
        }
    }
}
