using System;
using System.Threading.Tasks;

namespace TaskProblems
{
    class TaskProblems
    {
        static void Main(string[] args)
        {
            var delta = GC.GetTotalMemory(false);
            // GC.Collect is needed to be called cuz ref set in statemachine
            // Use tasks better code les problems
            for (int i = 0; i < 10000000; i++)
            {
                GetNikiAsync();
            }

            delta =  GC.GetTotalMemory(false) - delta;

            Console.WriteLine(delta);
        }

        static async Task GetNikiAsync()
        {

        }

        static void GetNiki()
        {

        }
    }
}
