using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpManaged
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(subtract(5,6));
            Console.WriteLine(add(5,6));
        }

        [DllImport("Cplusplusproject.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int subtract(int a, int b);

        [DllImport("Cplusplusproject.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int add(int a, int b);
    }
}
