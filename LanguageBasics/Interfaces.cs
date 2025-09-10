namespace Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Fib
{
    public static void TestYieldReturn() //iterator block
    {
        foreach (int fib in Fibs(5))
            Console.WriteLine("foreach: " + fib);
        foreach (var el in Foo(true))
            Console.WriteLine(el);
        foreach (var el in Foo(false))
            Console.WriteLine(el);
    }
    private static IEnumerable<int> Fibs(int fibCount)
    {
        for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
        {
            yield return prevFib;
            Console.WriteLine("Fibs: " + prevFib);
            int newFib = prevFib + curFib;
            prevFib = curFib;
            curFib = newFib;
        }
    }

    private static IEnumerable<string> Foo(bool breakEarly)
    {
        //Console.WriteLine("foo");
        yield return "One";
        //Console.WriteLine("foo");
        yield return "Two";
        //Console.WriteLine("foo");
        if (breakEarly) yield break;
        //Console.WriteLine("foo");
        yield return "Three";
        //Console.WriteLine("foo");
    }
}
internal class MyArrInt : IEnumerable
{
    private int[] _arr;
    public MyArrInt(params int[] arr)
    {
        _arr = arr;
    }
    public IEnumerator GetEnumerator()
    {
        return new Enumerator(_arr);
    }
    public class Enumerator : IEnumerator
    {
        private int[] _arr;
        private int _index;
        public Enumerator(params int[] arr)
        {
            _arr = arr;
        }

        public object Current
        {
            get 
            {
                if (_index < 0 || _index >= _arr.Length)
                    throw new InvalidOperationException();
                return _arr[_index];
            }
        }
        public bool MoveNext()
        {
            _index++;
            return _index < _arr.Length;
        }
        public void Reset()
        {
            _index = -1;
        }
    }
}

internal class MyArr<T>: IEnumerable<T>
{
    private T[] _arr;
    public MyArr(params T[] arr)
    {
        _arr = arr;
    }
    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(_arr);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); // вызываем дженерик-версию
    }

    public class Enumerator: IEnumerator<T>
    {
        private T[] _arr;
        private int _index;
        public Enumerator(T[] arr)
        {
            _arr = arr;
        }

        public T Current
        {
            get
            {
                if (_index < 0 || _index >= _arr.Length)
                    throw new InvalidOperationException();
                return _arr[_index];
            }
        }

        object IEnumerator.Current => Current!;

        public bool MoveNext()
        {
            _index++;
            return _index < _arr.Length;
        }
        public void Reset()
        {
            _index = -1;
        }
        public void Dispose()
        {

        }
    }
}

