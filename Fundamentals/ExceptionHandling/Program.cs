using System;
using System.Threading.Tasks;

namespace ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() => {
                throw new InvalidOperationException("Cant perform this") { Source = "t1"};
            });

            var t2 = Task.Factory.StartNew(() => {
                throw new AccessViolationException("Cant do this") { Source = "t2" };
            });


            try
            {
                Task.WaitAll(t1, t2);
            }
            // Aggregate exception is typically designed for TPL
            catch (AggregateException ae)
            {

                foreach (var exception in ae.InnerExceptions)
                {
                    Console.WriteLine($"Exception {exception.GetType()} from {exception.Source}");
                }
            }
            
            Console.ReadKey();
            Console.WriteLine("Main end");
        }
    }
}
