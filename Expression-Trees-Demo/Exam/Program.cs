using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(List<>);

            var genericType = type.MakeGenericType(new[] {typeof(object)});

            var instance = Activator.CreateInstance(genericType);

            var method = instance.GetType().GetMethod("Add");

            method?.Invoke(instance, new object[] {"magicString"});

            var countOfElements = instance.GetType().GetProperty("Count")?.GetValue(instance);

            Console.WriteLine(countOfElements);

        }

        
    }
}
