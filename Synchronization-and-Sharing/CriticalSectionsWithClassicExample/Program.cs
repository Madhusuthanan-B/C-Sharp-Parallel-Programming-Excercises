using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CriticalSectionsWithClassicExample
{

    public class BankAccount
    {
        public object padlock = new object();
        public int Balance { get; private set; }
        public void Deposit(int amount)
        {
            // += is Non atomic operation
            // op1: temp  = getBalance() + amount
            // op2: updateBalance(temp)
            // Between op1 and op2, something can happen that corrupts the result
            // Hence we need to setup a critical section

            // This means that, only one thread can work on this at a time.
            // Another thread can use this variable when it is released
            lock (padlock)
            {
                Balance += amount;
            }
        }

        public void Withdraw(int amount)
        {
            lock (padlock)
            {
                Balance -= amount;
            }
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
