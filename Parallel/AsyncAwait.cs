namespace Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

internal class AsyncAwait
{
    public async Task WriteSomeTextAsync(int count, int sleepTime)
    {
        Console.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " - started; " + Task.CurrentId);
        await Task.Run(print);
        //Запускает print() в отдельном потоке.
        //Пока print выполняется, основной поток(вызвавший WriteSomeTextAsync) может продолжать свою работу,
        //если не блокирует Task.
        //Как только print в отдельном потоке завершится,
        //await продолжит выполнение оставшегося кода в WriteSomeTextAsync.
        Console.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " - finished; " + Task.CurrentId);
        print();

        void print()
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " - Meth; " + i);
                Thread.Sleep(sleepTime);
            }
        }
    }
    public void TestAsyncAwait()
    {
        var task = WriteSomeTextAsync(25, 10);
        print();
        Console.WriteLine(task.IsCompleted);
        Thread.Sleep(2000);
        Console.WriteLine(task.IsCompleted);

        void print()
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " - Main; " + i);
                Thread.Sleep(10);
            }
        }
    }
    //public async Task TestWhenAll()
    //{
    //    var task1 = WriteSomeTextAsync(100, 10);
    //    var task2 = WriteSomeTextAsync(200, 5);
    //    var task3 = WriteSomeTextAsync(50, 20);
    //    await Task.WhenAll(task1, task2, task3);
    //}
}

