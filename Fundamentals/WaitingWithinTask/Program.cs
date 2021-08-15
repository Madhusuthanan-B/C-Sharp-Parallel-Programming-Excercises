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

            var bombTimer = new Task(() =>
            {
                Console.WriteLine("You have 5 seconds to disarm");
                var cancelled = token.WaitHandle.WaitOne(5000);
                Console.WriteLine(cancelled ? "Disarmed!" : "Boom!");
            }, token);

            var t = new Task(() =>
            {
                Console.WriteLine("Saying good night before sleep!");
                Thread.Sleep(3000); // Releases the thread whle SpintWait blocks the thread
                Console.WriteLine("Saying Hello after a sleep!");
                bombTimer.Start();
                Console.ReadKey();
                cts.Cancel();
            });
            t.Start();

            Console.ReadKey();
            Console.WriteLine("Main Program end");
        }
    }
}
