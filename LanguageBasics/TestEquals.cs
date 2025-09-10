namespace Tests.LanguageBasics;
using System;
internal class TestEquals
{
    public static void TestType()
    {
        int x = 10;
        object obj1 = x;
        object obj2 = new();
        Console.WriteLine("x: " + x.GetType());
        Console.WriteLine("obj1 = x");
        Console.WriteLine("obj1 GetType: " + obj1.GetType());
        Console.WriteLine("obj2 GetType: " + obj2.GetType());
        Console.WriteLine("object typeof: " + typeof(object));
        Console.WriteLine($"obj1.GetType() == typeof(object): {obj1.GetType() == typeof(object)}");
        Console.WriteLine($"obj2.GetType() == typeof(object): {obj2.GetType() == typeof(object)}");
        Console.WriteLine();

        A a = new();
        B b = new();
        A c = new B();
        Console.WriteLine("a: " + a.GetType());
        Console.WriteLine("b: " + b.GetType());
        Console.WriteLine($"a.GetType() == b.GetType(): {a.GetType() == b.GetType()}");
        Console.WriteLine($"a.GetType() == typeof(B): {a.GetType() == typeof(B)}");
        Console.WriteLine($"b.GetType() == typeof(A): {b.GetType() == typeof(A)}");
        Console.WriteLine();

        Console.WriteLine("A c = new B(): " + c.GetType());
        Console.WriteLine($"c.GetType() == b.GetType(): {c.GetType() == b.GetType()}");
        Console.WriteLine($"c.GetType() == typeof(B): {c.GetType() == typeof(B)}");
        Console.WriteLine($"c.GetType() == typeof(A): {c.GetType() == typeof(A)}");
        Console.WriteLine();

        MyArr<int> arr = new(1, 5, 2, 6, 3);
        Console.WriteLine("arr GetType: " + arr.GetType()); // '1 - один дженерик тип
        Console.WriteLine(arr.GetType() == typeof(MyArr<int>));
        Console.WriteLine();
    }
    private class A { public int X = 10; }
    private class B : A { }

    private class ObjWithOverridedEquals
    {
        public int X = 10;

        public override bool Equals(object? obj)
        {
            return obj is ObjWithOverridedEquals && this.X == (obj as ObjWithOverridedEquals)!.X;
        }
        public override int GetHashCode()
        {
            return X.GetHashCode();
        }
    }
    public static void TestEquality()
    {
        object x = 10;
        object y = 10;
        Console.WriteLine("object x = 10; object y = 10");
        Console.WriteLine($"x == y: {x == y}"); //сравнение ссылок - стандартное поведение
        Console.WriteLine($"x.Equals(y): {x.Equals(y)}"); //сравнение значений - для значимых типов
                                                          //у ссылочных по умолчанию сравнение ссылок
        Console.WriteLine($"Object.Equals(x, y): {Object.Equals(x, y)}");
        Console.WriteLine();


        A obj1 = new();
        A obj2 = new();
        Console.WriteLine("A obj1; B:A obj2");
        Console.WriteLine($"obj1 == obj2: {obj1 == obj2}"); //не перегружен оператор == 
        Console.WriteLine($"obj1.Equals(obj2): {obj1.Equals(obj2)}"); //не переопределен Equals
        Console.WriteLine($"Object.Equals(obj1, obj2): {Object.Equals(obj1, obj2)}");
        Console.WriteLine();

        ObjWithOverridedEquals obj3 = new();
        ObjWithOverridedEquals obj4 = new();
        Console.WriteLine($"ObjWithOverridedEquals obj3, obj4");
        Console.WriteLine($"obj3 == obj4: {obj3 == obj4}"); //не перегружен оператор == 
        Console.WriteLine($"obj3.Equals(obj4): {obj3.Equals(obj4)}"); //переопределен Equals
        //obj3.X = 11;
        //Console.WriteLine(obj3.Equals(obj4));
        Console.WriteLine($"Object.Equals(obj3, obj4): {Object.Equals(obj3, obj4)}"); //сначала ==, потом Equals
        Console.WriteLine();

        string str1 = "Hi";
        string str2 = "Hi";
        Console.WriteLine("str1 = Hi, str2 = Hi");
        Console.WriteLine($"str1 == str2: {str1 == str2}"); //перегружен оператор == 
        Console.WriteLine($"str1.Equals(str2): {str1.Equals(str2)}"); //переопределен Equals
        Console.WriteLine($"Object.Equals(str1, str2): {Object.Equals(str1, str2)}");
        Console.WriteLine();
    }
}