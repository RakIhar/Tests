namespace Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
internal class Generics
{
    public static void TestGenerics()
    {
        BaseConstraint<BaseClass> base1 = new();
        BaseConstraint<DerivedClass> base2 = new();
        //BaseConstraint<string> base3 = new(); //нельзя
        InterfaceConstraint<IInterface> interf = new();
        InterfaceConstraint<InterfaceRealization> interfReal = new();
        //NewConstraint<BaseClass> newConstraint1 = new(); //нельзя - нет конструктора без параметров
        NewConstraint<DerivedClass> newConstraint2 = new();
        Converter<BaseClass, DerivedClass> conv1 = new();
        //Converter<DerivedClass, BaseClass> conv2 = new();
        //class
        //class?
        //struct
        //unmanaged
        //notnull

        //IEquatable<string> Ai = new Eq<string>();
        //IEquatable<object> Oi = Ai;
    }
    private class Converter<T, U> where U : T { } //тип U должен быть тем же, что и U, или его наследником/реализацией
    private class BaseClass
    {
        public BaseClass(int x) { }
    }
    private class DerivedClass : BaseClass
    {
        public DerivedClass() : base(10) { }
    }

    private class BaseConstraint<T> where T : BaseClass { }

    private class InterfaceConstraint<T> where T : IInterface { }

    private class InterfaceRealization : IInterface
    {
        public void DoSmth() { }
    }
    private interface IInterface
    {
        void DoSmth();
    }

    private class NewConstraint<T> where T : new() { }

    private class A<T> { }
    private class B : A<int> { }
    private class C<T> : A<T> { }
    private class D<T, Y> : A<T> { }

    //непонятная хуйня:
    //interface IOut<out T> 
    //{
        //T Out();
    //}
    public interface IEquatable<T> { bool Equals(T obj); }
    //public class Eq<T>: IOut<T> where T : new()
    //{
    //    public T Out() => new();
    //}
    public class Balloon : IEquatable<Balloon>, IEquatable<int> //при закрытии типа указан Balloon
    {
        public bool Equals(Balloon b) => true;
        public bool Equals(int b) => true;
    }

    class Foo<T> where T : IComparable<T> { }
    class Bar<T> where T : Bar<T> { } //самая странная херня

}