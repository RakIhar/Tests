namespace Tests;
using System;
using System.Threading.Tasks;
internal static class TestFunctions
{
    public static string MonthName(int num) => num switch
    {
        1 => "January",
        2 => "February",
        3 => "March",
        4 => "April",
        5 => "May",
        6 => "June",
        7 => "July",
        8 => "August",
        9 => "September",
        10 => "October",
        11 => "November",
        12 => "December",
        _ => ""
    };
    public static void WriteSomeText(int count, string threadName, int sleepTime)
    {
        Thread thr = Thread.CurrentThread;
        thr.Name = threadName;
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Thread:" + thr.Name + "; " + i);
            Thread.Sleep(sleepTime);
        }
        Console.WriteLine("Thread:" + thr.Name + " - finished; " + Task.CurrentId);
    }

    ///<summary> считает арифметический ряд по модулю </summary>
    ///<param name ="count"> Кол-во членов суммы </param>
    ///<param name="initial"> Начальное число </param>
    ///<param name="module"> Модуль </param>
    ///<returns> Возвращает то, что должно возвращаться </returns>
    ///<remarks> Код работает заебись </remarks>
    public static int Sum(int count, int initial, int module)
    {
        int j = initial;
        int res = 0;
        for (int i = 0; i < count; i++)
        {
            res += j++;
            res %= module;
        }
        return res;
    }
    public static int ComplexCalculation()
    {
        double x = 2;
        for (int i = 1; i < 100000000; i++)
            x += Math.Sqrt(x) / i;
#warning плохое приведение
        return (int)x;
//#error пиздец
    }
}

