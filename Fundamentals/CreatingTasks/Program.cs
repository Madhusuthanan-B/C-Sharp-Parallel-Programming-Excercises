using System;
using System.Threading.Tasks;

namespace CreatingTasks
{
    class Program
    {
        public static void Write(char c)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        static void Main(string[] args)
        {
            Task.Factory.StartNew(() => Write('.'));

            var t1 = new Task(() => Write('?'));
            t1.Start();

            var t2 = new Task(() => Write("foo"));
            t2.Start();

            Task t3 = new Task(Write, "moo");
            t3.Start();

            Write('-');

            Console.ReadKey();
        }
    }
}
