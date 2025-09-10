namespace Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tests.TestFunctions;



internal class Parallels
{
    public void TestParallel()
    {
        Parallel.Invoke(
            () => WriteSomeText(10, "thr1", 200),
            () => WriteSomeText(13, "thr2", 150),
            () => WriteSomeText(20, "thr3", 100)
        );
    }

    public void TestParallelFor()
    {
        Parallel.For(1, 100, PrintIndex);

        void PrintIndex(int i)
        {
            Console.WriteLine("i = " + i + "\n" + Thread.CurrentThread.ManagedThreadId);
        }
    }

    public void TestParallelForEach()
    {
        List<double> nums = new();

        for (int i = 0; i < 1000; i++)
        {
            nums.Add(i + i / 10.0);
            i++;
        }
        Parallel.ForEach(nums, PrintIndex);

        void PrintIndex(double i)
        {
            Console.WriteLine("i = " + i + "\n" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}

