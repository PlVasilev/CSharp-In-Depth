namespace CompareStructs
{
    using System;
    using System.Diagnostics;

    // https://msdn.microsoft.com/en-us/library/2dts52z7.aspx
    // If none of the fields of the current instance and obj are reference types,
    // the Equals method performs a byte-by-byte comparison of the two objects in memory.
    // Otherwise, it uses reflection to compare the corresponding fields of obj and this instance.
    public static class Program
    {
        private const int TimesToCompare = 10000000;

        public static void Main()
        {
            // Point with value members
            var stopwatch = Stopwatch.StartNew();
            var point1 = new Point { X = 1, Y = 2 };
            var point2 = new Point { X = 1, Y = 2 };
            for (var i = 0; i < TimesToCompare; i++)
            {
                point1.Equals(point2);
            }

            Console.WriteLine("Point with value members: {0}", stopwatch.Elapsed);

            // Point with reference member
            stopwatch = Stopwatch.StartNew();
            var pointWithName1 = new PointWithName { X = 1, Y = 2, Name = "Point with name 1" };
            var pointWithName2 = new PointWithName { X = 1, Y = 2, Name = "Point with name 2" };
            for (var i = 0; i < TimesToCompare; i++)
            {
                pointWithName1.Equals(pointWithName2);
            }

            Console.WriteLine("Point with reference member: {0}", stopwatch.Elapsed);

            // Point with reference member and equals
            stopwatch = Stopwatch.StartNew();
            var pointWithNameAndEquals1 = new PointWithNameAndEquals { X = 1, Y = 2, Name = "Point with name and Equals() 1" };
            var pointWithNameAndEquals2 = new PointWithNameAndEquals { X = 1, Y = 2, Name = "Point with name and Equals() 2" };
            for (var i = 0; i < TimesToCompare; i++)
            {
                pointWithNameAndEquals1.Equals(pointWithNameAndEquals2);
            }

            Console.WriteLine("Point with reference member and equals: {0}", stopwatch.Elapsed);
        }
    }
}