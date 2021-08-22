using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentDictionaryExample
{
    class Program
    {
        private static ConcurrentDictionary<string, string> capitals = new ConcurrentDictionary<string, string>();

        public static void AddDelhi()
        {
            bool success = capitals.TryAdd("India", "New Delhi");
            var who = Task.CurrentId.HasValue ? ("Task " + Task.CurrentId) : "Main thread";
            Console.WriteLine($"{who} {(success? "added": "did not add")} the element.");
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(AddDelhi).Wait();
            AddDelhi();
        }
    }
}
