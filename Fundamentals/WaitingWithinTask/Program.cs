using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingWithinTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() => {
                Console.WriteLine("Saying good night before sleep!");
                Thread.Sleep(3000); // Releases the thread whle SpintWait blocks the thread
                Console.WriteLine("Saying Hello after a sleep!");
            });
            t.Start();

            Console.ReadKey();
            cts.Cancel();
            Console.WriteLine("Main Program end");
        }
    }
}
