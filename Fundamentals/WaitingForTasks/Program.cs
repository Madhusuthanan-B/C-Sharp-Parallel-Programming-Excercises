using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t1 = new Task(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Task 1 end");
            }, token);

            t1.Start();

            var t2 = new Task(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Task 2 end");
            }, token);

            t2.Start();

            Task.WaitAll(new[] { t1, t2 }, 4000, token);

            Console.WriteLine($"Task t1 {t1.Status}");
            Console.WriteLine($"Task t2 {t2.Status}");

            Console.WriteLine("Main program end.");
            Console.ReadKey();

        }
    }
}
