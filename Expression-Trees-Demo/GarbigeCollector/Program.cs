using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GarbigeCollector
{
    public class User
    {
        public User()
        {
           // Console.WriteLine("An Instance of class created");
        }
        // Destructor is called when GC is removing the class from the heep
        ~User() 
        {
          //  Console.WriteLine("An Instance of class destroyed");
        }

        public class Person
        {
            
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            User user;
            List<User> users = new List<User>();
            //while (true)
            //{
            //    users.Add(new User());
            //}
            for (int i = 0; i < 100000; i++)
            {
               // GC.Collect();
                user = new User();
            }

            Console.WriteLine(sw.ElapsedMilliseconds);

            var weakRef =  Test();
            Console.WriteLine(weakRef.IsAlive);
            GC.Collect();
            Console.WriteLine(weakRef.IsAlive);

        }

        static WeakReference Test() // pointer to heep
        {
            var p = new User();
            return  new WeakReference(p);
        }
    }
}
