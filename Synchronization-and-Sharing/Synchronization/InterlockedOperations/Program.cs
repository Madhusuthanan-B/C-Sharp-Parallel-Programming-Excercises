using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedOperations
{
    public class BankAccount
    {
        public object padlock = new object();
        private int balance;
        public int Balance
        {
            get { return balance; }
            private set { balance = value; }
        }
        public void Deposit(int amount)
        {

            // Alternate approach to lock
            // Lock free programming
            Interlocked.Add(ref balance, amount);
        }
        public void Withdraw(int amount)
        {
            Interlocked.Add(ref balance, -amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tasks = new List<Task>();
            var bankAccount = new BankAccount();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Deposit(100);
                    }
                }));

                tasks.Add(Task.Factory.StartNew(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        bankAccount.Withdraw(100);
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine($"Balance in acount {bankAccount.Balance}");
            Console.ReadKey();
        }
    }
}
