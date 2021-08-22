using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MutexExample
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
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var bankAccount = new BankAccount();
            var mutex = new Mutex();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            bankAccount.Deposit(100);
                        }
                        finally
                        {

                           if(haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                        
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bool haveLock = mutex.WaitOne();
                        try
                        {
                            bankAccount.Withdraw(100);
                        }
                        finally
                        {

                            if (haveLock)
                            {
                                mutex.ReleaseMutex();
                            }
                        }
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Balance in acount {bankAccount.Balance}");
            Console.ReadKey();
        }
    }
}
