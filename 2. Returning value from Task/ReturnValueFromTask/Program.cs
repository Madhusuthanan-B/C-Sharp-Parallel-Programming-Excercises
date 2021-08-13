using System;
using System.Threading.Tasks;

namespace ReturnValueFromTask
{
    class Program
    {
        public static int TextLength(object o)
        {
            Console.WriteLine($"\n Task with id {Task.CurrentId} processing object {o}...");
            return o.ToString().Length;
        }

        static void Main(string[] args)
        {
            string text1 = "Hello world!", text2 = "Hello some other world!";

            var t1 = new Task<int>(TextLength, text1);
            t1.Start();

            Console.WriteLine($"Length of {text1} is {t1.Result}");

            var t2 = new Task<int>(TextLength, text2);
            t2.Start();

            Console.WriteLine($"Length of {text2} is {t2.Result}");

            Console.WriteLine("Main thread.");
            Console.ReadKey();
        }
    }
}
