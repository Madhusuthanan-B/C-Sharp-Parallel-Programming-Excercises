using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            token.Register(() => {
                Console.WriteLine("Cancellation Requested");
            });


            var t = new Task(() => {
                int i = 0;
                while(true)
                {
                    // Canonical way recommended by TPL
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.WriteLine("Program end");
        }
    }
}
