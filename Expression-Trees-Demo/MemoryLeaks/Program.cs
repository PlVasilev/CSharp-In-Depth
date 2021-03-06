using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace MemoryLeaks
{
    class Olympics
    {
        public static List<Runner> TryoutRunners;
    }
    class Runner
    {
        private string _fileName = "text.txt";
        private FileStream _fStream;
        public void GetStats()
        {
            FileInfo fInfo = new FileInfo(_fileName);
            _fStream = fInfo.OpenRead();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Olympics.TryoutRunners = new List<Runner>();

            while (true)
            {
                var runner = new Runner();
                Olympics.TryoutRunners.Add(runner);
                runner.GetStats();
            }
        }
    }
}
