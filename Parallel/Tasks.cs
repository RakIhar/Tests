namespace Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Tests.TestFunctions;


internal class Tasks
{
    public void TestVoidTasks()
    {
        int count1 = 10;
        string name1 = "thr1";
        int sleep1 = 200;
        Task task1 = new(() => WriteSomeText(count1, name1, sleep1));
        task1.Start();

        int count2 = 15;
        string name2 = "thr2";
        int sleep2 = 150;
        Task task2 = Task.Factory.StartNew(() => WriteSomeText(count2, name2, sleep2));

        int count3 = 20;
        string name3 = "thr3";
        int sleep3 = 100;
        Task task3 = Task.Run(() => WriteSomeText(count3, name3, sleep3));

        task1.Wait();
        Console.WriteLine("Task1 isCompleted:" + task1.IsCompleted);
        Console.WriteLine("Task2 isCompleted:" + task2.IsCompleted);
        Console.WriteLine("Task3 isCompleted:" + task3.IsCompleted);
        task2.Wait();
        task3.Wait();
        Console.WriteLine("TasksMain - finished");
    }

    public void TestTasksWithResult()
    {
        Task<int> task = new(() => Sum(100002, 17, 99));
        //task.RunSynchronously(); //синхронное выполнение
        task.Start();
        Console.WriteLine("Result: " + task.Result); //Result - блокирует поток
                                                     //для блокировки можно ещё и GetAwaiter + OnCompleted
                                                     //или await
    }
    public void TestContinuationTasks()
    {
        Task task1 = new Task(() =>
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
        });

        Task task2 = task1.ContinueWith(PrintTask);

        task1.Start();

        task2.Wait();
        Console.WriteLine("TestContinuationTasks - finished");

        void PrintTask(Task t)
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
            Console.WriteLine($"Id предыдущей задачи: {t.Id}");
            Thread.Sleep(3000);
        }
    }

    public Task<int> ComplexCalculationAsync() => Task.Run(ComplexCalculation);
}



