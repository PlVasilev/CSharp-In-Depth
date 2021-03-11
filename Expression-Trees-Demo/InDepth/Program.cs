using System;
using System.Runtime.InteropServices;

namespace InDepth
{

     class Program
    {
        static unsafe void Main(string[] args)
        {
            string a = "x";
            string b = "x";
            Console.WriteLine(GetAddress(a));
            Console.WriteLine(GetAddress(b));

        }

        public static unsafe long GetAddress(object o)
        {
            TypedReference tr = __makeref(o);
            IntPtr ptr = **(IntPtr**)(&tr);
            return ptr.ToInt64();
        }

        // public struct MyStruct
        // {
        //     double a, b, c, d, e, f, g, h, i, j, k, l, m;
        // }
        //
        // static long topOfStack;
        // static long stackSize = 1024 * 1024;
        //
        // static unsafe void Main(string[] args)
        // {
        //     int x = 5;
        //     int y = 5;
        //     var pointer = &x;
        //     topOfStack = (long) &x; 
        //     Console.WriteLine(*pointer); // value of pointer
        //     MyStruct s = new MyStruct();
        //     Console.WriteLine((long)&x); // place in memory
        //     Console.WriteLine((long)&y);
        //     Recurce(s, 0);
        //     RefRecurce(ref s, 0);
        // }
        //
        // unsafe static void Recurce(MyStruct s, int times)
        // {
        //     long remaining;
        //     remaining = topOfStack  - ((long) &remaining);
        //     if (stackSize - remaining < 0)
        //     {
        //         Console.WriteLine(times);
        //         return;
        //     }
        //     Recurce(s, ++times);
        // }
        //
        // unsafe static void RefRecurce(ref MyStruct s, int times)
        // {
        //     long remaining;
        //     remaining = topOfStack - ((long)&remaining);
        //     if (stackSize - remaining < 0)
        //     {
        //         Console.WriteLine(times);
        //         return;
        //     }
        //     RefRecurce(ref s, ++times);
        // }
        //
    }


}
