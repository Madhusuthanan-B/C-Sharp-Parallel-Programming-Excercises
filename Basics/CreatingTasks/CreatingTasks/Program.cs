using System;
using System.Threading.Tasks;

namespace CreatingTasks
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while(i --> 0)
            {
                Console.Write(c);
            }
        }
        static void Main(string[] args)
        {
            // Making a task and starting it immediately
            Task.Factory.StartNew(() => Write('.'));

            // Creating a task and starting it later
            var t = new Task(() => Write('?'));
            t.Start();

            Write('-');

            Console.ReadKey();
        }
    }
}
