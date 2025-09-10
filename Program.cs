namespace Tests;

using System.Collections;
using System.Threading.Tasks;
using LanguageBasics;
using Tests.AdavancedFeatures;

internal class Program
{
    static void Main(string[] args)
    {
        Tests();
        //Exercism();
    }

    static void Exercism()
    {
        //Console.WriteLine(RomanNumeralExtension.ToRoman(1));
        //foreach (var item in BottleSong.Recite(3, 3))
        //    Console.WriteLine(item);

        //var res = Proverb.Recite(["nail", "shoe", "horse", "rider", "message", "battle", "kingdom"]);
        //foreach (var item in res)
        //    Console.WriteLine(item);
        //Console.WriteLine(TelemetryBuffer.FromBuffer(new byte[] { 0xf8, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x7f }) == Int64.MaxValue);

        //const int robotsCount = 10_000;
        //var robots = Enumerable.Range(0, robotsCount).Select(x => new Robot()).OrderBy(x => x.Name).ToArray();
        //NucleotideCount.Count("GGGGGGG");
        SecretHandshake.Commands(19);

    }
    static void Tests()
    {
        //Tasks tasks = new();
        //tasks.TestVoidTasks();
        //tasks.TestTasksWithResult();
        //tasks.TestContinuationTasks();
        //tasks.TestParallel();
        //tasks.TestParallelFor();
        //tasks.TestParallelForEach();
        //tasks.TestAwaitAsync();
        //tasks.TestWhenAll();
        //AsyncAwait asyncAwait = new();
        //asyncAwait.TestAsyncAwait();
        //Console.WriteLine("Main - finished");

        //TestEquals.TestType();
        //TestEquals.TestEquality();
        //Fib.TestYieldReturn();
        //Other.Message("01234: <MESSAGE>"); //5
        //Other.LogLevel("[ERROR]: Disk full");
        //Console.WriteLine(Other.Reformat("[ERROR]: Disk full"));
        //Console.WriteLine("[ERROR]: Disk full".SubstringAfter(":"));
        //Console.WriteLine("[ERROR]: Disk full".SubstringBetween("[", "]") + ".");
        //Console.WriteLine("FIND >>> SOMETHING <===< HERE".SubstringBetween(">>> ", " <===<"));
        //Console.WriteLine(LogAnalysis.Analyze("212-555-1234"));
        //var str = new string("1234567890".Reverse().ToArray());
        //Console.WriteLine(Math.Log(8, 2));
        //SumOfMultiples.Sum([3], 7);

        {
            //LINQS.Deferred.TestWhere();
            //LINQS.Deferred.TestSelect_SelectMany();
            //LINQS.Deferred.TestTake_Skip();
            //LINQS.Deferred.TestConcat();
            //LINQS.Deferred.TestOrderBy();
            //LINQS.Deferred.TestThenBy();
            //LINQS.Deferred.TestJoin_GroupJoin();
            //LINQS.Deferred.TestGroupBy();
            //LINQS.Deferred.TestDistinct_Union_Except_Intersect();
            //LINQS.Deferred.TestCast_OfType_AsEnumerable();
            //LINQS.Deferred.TestDefaultIfEmpty();
            //LINQS.Deferred.TestRange_Repeat_Empty();
            //LINQS.Deferred.TestZip();
            //LINQS.Deferred.TestChunk();
            //LINQS.NonDeferred.TestToArray_ToList();
            //LINQS.NonDeferred.TestToDictionary();
            //LINQS.NonDeferred.TestToLookup();
            //LINQS.NonDeferred.TestSequenceEqual();
            //LINQS.NonDeferred.TestFirst_Last();
            //LINQS.NonDeferred.TestSingle_ElementAt();
            //LINQS.NonDeferred.TestAny_All_Contains();
            //LINQS.NonDeferred.TestCount_LongCount_Sum();
            //LINQS.NonDeferred.TestAverage_Aggregate();
            //LINQS.Parallel.TestAsParallel_AsOrdered_ForAll();
            //LINQS.Parallel.TestAsSequential_AsEnumerable_WithMergeOptions();
            //LINQS.Parallel.TestCancelationToken();
            //LINQS.Parallel.TestWithMergeOptions();

        }
        //Indices.TestIndices();

        Reflection.Test();
    }
}