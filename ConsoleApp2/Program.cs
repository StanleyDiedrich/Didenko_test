using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public static class Server
    {
        static int counter = 0;
        static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        static bool _isRunning = true;



        public static void GetCount()
        {
            while (_isRunning)
            {
                try
                {
                    _lock.EnterReadLock();
                    Console.WriteLine($"R: Thread{Thread.CurrentThread.ManagedThreadId} counter: {counter}");
                }
                finally
                {
                    _lock.ExitReadLock();
                }
                Thread.Sleep(1000);
            }
        }

        public static int AddToValue(int value)
        {
            _lock.EnterWriteLock();

            try
            {

                counter += value;
                return counter;
            }
            finally
            {
                _lock.ExitWriteLock();
            }



        }
        public static bool Stop()
        {
            _isRunning = false;
            return _isRunning;
        }
    }
    internal class Program
    {

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(() => Server.GetCount()).Start();
            }
            for (int j = 0; j < 5; j++)
            {
                Random random = new Random();

                int value = random.Next(0, 1000);
                new Thread(() => Server.AddToValue(value)).Start();
            }

            Console.WriteLine("Press Enter to stop...");
            Console.ReadLine();
            Server.Stop();

            // Дайте время потокам завершиться
            Thread.Sleep(2000);
        }
    }
}
