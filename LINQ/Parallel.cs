namespace Tests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

public static partial class LINQS
{
    public static class Parallel
    {
        //Parallel:

        //AsParallel
        //AsOrdered
        //ForAll
        //AggregateException
        //TestAsSequential
        //AsEnumerable
        //WithMergeOptions
        //CancelationToken + OperationCanceledException
        public static void TestAsParallel_AsOrdered_ForAll()
        {
            //LINQ автоматически переключается на PLINQ, когда используется ParallelQuery<T>
            //AsParallel: IEnumerable<T> -> ParallelQuery<T>

            IEnumerable<(string, int)> autos = _cars
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат последовательного запроса: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId)); //Where перегружено для параллельной версии
            Console.WriteLine("Результат параллельного запроса: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .AsOrdered()
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат параллельного упорядоченного запроса: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .AsOrdered()
                .Where(p => p.Contains("a"))
                .AsUnordered() //снимает прошлый порядок
                .Where(x => x.Length > 6)
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат параллельного неупорядоченного запроса: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .WithDegreeOfParallelism(2)
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат параллельного запроса cо степенью параллелизма 2: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.ForceParallelism) //LINQ не может выбрать последовательное выполнение
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат параллельного запроса c ForceParallelism: ");
            foreach (var s in autos)
                Console.WriteLine(s);

            autos = _cars
                .AsParallel()
                .WithExecutionMode(ParallelExecutionMode.Default) //LINQ может выбрать последовательное выполнение
                .Where(p => p.Contains("a"))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId));
            Console.WriteLine("Результат параллельного запроса c Default + поток в цикле foreach: ");
            foreach (var s in autos)
                Console.WriteLine(s + ", Поток: " + Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Результат ForAll + Console.WriteLine в качестве Action: ");
            _cars.AsParallel()
                .Where(p => p.Contains("a"))
                .ForAll(p => Console.WriteLine($"Название: {p}, Поток: {Thread.CurrentThread.ManagedThreadId}")); //ForAll только для ParallelQuery<T>
        }
        public static void TestExceptions()
        {
            // Запрос Parallel LINQ
            IEnumerable<string> auto = _cars
                .AsParallel()
                .Select(p =>
                {
                    if (p == "Aston Martin" || p == "Ford")
                        throw new Exception("Проблемы с машиной " + p);
                    return p;
                });

            try
            {
                foreach (string s in auto)
                    Console.WriteLine("Результат: " + s + "\n");
            }
            catch (AggregateException agex)
            {
                agex.Handle(ex =>
                {
                    Console.WriteLine(ex.Message);
                    return true;
                });
            }
        }
        public static void TestAsSequential_AsEnumerable()
        {
            IEnumerable<(string, int, int)> auto = LINQS._cars
                .AsParallel()
                .AsOrdered()
                .Where(p => p.Contains('a'))
                .Select(x => (x, Thread.CurrentThread.ManagedThreadId))
                .Take(5)
                .AsSequential()
                .Where(p => p.Item1.Contains('o'))
                .Select(p => (p.Item1, p.Item2, Thread.CurrentThread.ManagedThreadId));

            foreach (var s in auto)
                Console.WriteLine("Совпадение: " + s);
        }

        public static void TestWithMergeOptions()
        {
            IEnumerable<(int, int)> results = ParallelEnumerable.Range(0, 25)
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered) //управляет буфером
                .Select(i =>
                {
                    System.Threading.Thread.Sleep(1000);
                    return (i, Thread.CurrentThread.ManagedThreadId);
                });

            Stopwatch sw = Stopwatch.StartNew();

            foreach ((int, int) i in results)
            {
                Console.WriteLine($"Значение: {i}, Время: {sw.ElapsedMilliseconds}");
            }
        }

        public static void TestCancelationToken()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                Thread.Sleep(500);
                cts.Cancel();
            });
            var sw = Stopwatch.StartNew();
            try
            {
                ParallelEnumerable
                .Range(0, 2000)
                .WithCancellation(cts.Token)
                .ForAll
                (x =>
                {
                    //cts.Token.ThrowIfCancellationRequested(); //без этой фигни долго проверяется
                    Console.WriteLine($"{x}, {Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(50);
                }
                );
                Console.WriteLine("Закончено без отмены");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Отменено");
            }
            sw.Stop();
            Console.WriteLine($"Прошло {sw.ElapsedMilliseconds} мс");
        }

    }
}

