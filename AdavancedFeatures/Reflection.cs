namespace Tests.AdavancedFeatures;
using System.Reflection;
interface DoSmth
{
    void DoSmth();
}

[CustomAttribute("test", Value = "Nihil")] //positional and named parameters
internal class ReflectionExampleData : DoSmth
{
    int x = 10;
    double y = 20;
    string str = "Example";
    public void DoSmth() { }
}

internal class CustomAttribute : Attribute
{
    public string Name { get; set; } = "None";
    public string Value { get; set; } = "None";
    public string ID { get; }
    public CustomAttribute(string ConstructorString) => ID = ConstructorString; 
}

public static class Reflection
{
    public static void Test()
    { 
        ReflectionExampleData refl = new ReflectionExampleData();
        Type type = typeof(ReflectionExampleData);
        Assembly assembly = Assembly.GetExecutingAssembly();
        Assembly assembly2 = type.Assembly;
        Console.WriteLine(assembly);
        Console.WriteLine(assembly2);
        Console.WriteLine($"assembly2 == assembly {assembly2 == assembly}");
        Console.WriteLine("type.GetMethods()");
        foreach (var item in type.GetMethods())
        {
            Console.WriteLine(item.Name);
        }
        Console.WriteLine("type.GetInterfaces()");
        foreach (var item in type.GetInterfaces())
        {
            Console.WriteLine(item.Name);
        }
        Console.WriteLine("type.GetCustomAttributes()");
        foreach (var item in type.GetCustomAttributes())
        {
            Console.WriteLine(item.ToString());
            Console.WriteLine(item.IsDefaultAttribute());
            Console.WriteLine(item.GetType());
            Console.WriteLine(item.TypeId);
        }
    }
}
