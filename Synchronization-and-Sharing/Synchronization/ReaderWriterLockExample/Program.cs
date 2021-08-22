using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLockExample
{
    public class BankAccount
    {
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            Balance += amount;
        }

        public void Withdraw(int amount)
        {
            Balance -= amount;
        }
    }

    class Program
    {
        // Setting LockRecursionPolicy.SupportsRecursion not recommended as it is harder to debug / find out what went wrong
        static ReaderWriterLockSlim padlock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        static Random randomNumber = new Random();

        static void Main(string[] args)
        {
            int x = 0;

            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    //padlock.EnterReadLock();

                    padlock.EnterUpgradeableReadLock();

                    if (i % 2 == 0)
                    {
                        padlock.EnterWriteLock();
                        x = 123;
                        padlock.ExitWriteLock();
                    }
                    Console.WriteLine($"Entered read lock, x = {x}");
                    Thread.Sleep(5000);
                    padlock.ExitUpgradeableReadLock();
                    //padlock.ExitReadLock();
                    Console.WriteLine($"Exited read lock, x = {x}");
                }));
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException ae)
            {

                ae.Handle(e =>
                {
                    Console.WriteLine(e);
                    return true;
                });
            }

            while (true)
            {
                Console.ReadKey();
                padlock.EnterWriteLock();
                Console.WriteLine($"Write lock aquired");
                int newValue = randomNumber.Next(10);
                x = newValue;
                Console.WriteLine($"Set x = {x}");
                padlock.ExitWriteLock();
                Console.WriteLine($"Write lock released");
            }
        }
    }
}
