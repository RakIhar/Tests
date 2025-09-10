namespace Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Indices
{
    public static void TestIndices()
    {
        string str = "0123456789";
        Console.WriteLine("str: " + str);
        Console.WriteLine("str[0]: " + str[0]);
        try
        {
            Console.WriteLine("str[^0]: " + str[^0]);
        }
        catch (IndexOutOfRangeException e)
        {
            Console.WriteLine("str[^0]: " + e.Message);
        }
        Console.WriteLine("str[^1]: " + str[^1]);
        Console.WriteLine("str[0..0]: " + str[0..0]);
        Console.WriteLine("str[0..1]: " + str[0..1]);
        try
        {
            Console.WriteLine("str[1..0]: " + str[1..0]);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("str[1..0]: " + e.Message);
        }
        Console.WriteLine("str[0..2]: " + str[0..2]);
        Console.WriteLine("str[1..1]: " + str[1..1]);
        Console.WriteLine("str[1..2]: " + str[1..2]);
        Console.WriteLine("str[1..3]: " + str[1..3]); //[)
        Console.WriteLine("str[1..^0]: " + str[1..^0]);
        //length = end - start 
    }
}
internal class AnonTypes
{
    public static void Anon()
    {
        var dude = new { Name = "Bob", Age = 20 };
        var dude2 = new { Name = "Susan", Age = 19 };
        dude = dude2;
        int Age = 25;
        var dude3 = new { Name = "Jack", Age };
        dude3 = dude;
        //если дать другие имена внутренним типам,
        //то и сами анонимные типы будут разными
        Console.WriteLine(dude3.Name);
        Console.WriteLine(dude3.Age);
        //иммутабельные
        //dude3.Age = 20; //нельзя
    }
    public static void TuplesBasics()
    {
        var dude = ("Bob", 20);
        var dude2 = ("Susan", 19);
        dude = dude2;
        int Age = 25;
        var dude3 = ("Jack", Age);
        dude3 = dude;

        (String, int) dude4;
        dude4 = dude;
        //можно явно задать тип + неважно какие имена внутренних типов
        Console.WriteLine(dude4.Item1);
        Console.WriteLine(dude4.Item2);
        //мутабельные 
        dude4.Item2 = 20;


    }
    public static void Tuples()
    {
        //именование
        //деконструкция

        var dude = (Name: "Bob", Age: 20);
        Console.WriteLine(dude.Name);
        Console.WriteLine(dude.Age);

        (string Name, int Age) dude2 = ("Susan", 19);
        dude = dude2;
        int Age = 25;
        (string, int) dude3 = ("Jack", Age);
        dude3 = dude; //в отличие от анонимных типов так можно

        (string dudeName, int dudeAge) = dude; //деконструкция
        Console.WriteLine(dudeName);
        Console.WriteLine(dudeAge);
    }
}