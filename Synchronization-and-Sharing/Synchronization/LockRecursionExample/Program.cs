using System;
using System.Threading;

namespace LockRecursionExample
{
    class Program
    {
        // If we specifiy true, then we can catch lock recursion exception
        // Learning from this ==> Spin lock does not support lock recursion

        static SpinLock sl = new SpinLock(true);

        static void LockRecursion(int depth)
        {
            bool lockTaken = false;
            try
            {
                sl.Enter(ref lockTaken);
            }
            catch (LockRecursionException e)
            {

                Console.WriteLine($"Exception: {e}");
            }
            finally
            {
                if (lockTaken)
                {
                    Console.WriteLine($"Took a lock, depth = {depth}");
                    LockRecursion(depth - 1);
                    sl.Exit();
                } else
                {
                    Console.WriteLine($"Failed to take a lock, depth = {depth}");
                }
            }
        }

        static void Main(string[] args)
        {
            LockRecursion(5);
            Console.WriteLine("Hello World!");
        }
    }
}
