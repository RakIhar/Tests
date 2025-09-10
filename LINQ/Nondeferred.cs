namespace Tests;
using System;
using System.Collections.Generic;
using System.Linq;
public static partial class LINQS
{
    public static class NonDeferred
    {
        //NonDelayedOperations:

        //ToArray
        //ToList
        //ToDictionary
        //ToLookup

        //SequenceEqual

        //First
        //Last
        //FirstOrDefault

        //Any
        //All
        //Contains

        //TestCount
        //LongCount
        
        //Sum
        //Max
        //Min
        //MaxBy
        //Average

        //Aggregate
        public static void TestNonDelayedOperations()
        {
            string[] strings = { "one", "two", "three" };

            string[] ieStrings = strings.Where(s => s.Length == 3).ToArray();

            foreach (string s in ieStrings)
            {
                Console.WriteLine("Processing " + s);
            }

            strings[0] = "four";
            strings[2] = "six"; //ничего не изменится

            foreach (string s in ieStrings)
            {
                Console.WriteLine("Processing " + s);
            }
        }
        public static void TestToArray_ToList()
        {
            IEnumerable<int> item = Enumerable.Range(1, 20);
            Console.WriteLine("Начальный тип: " + item);

            int[] arr = item.ToArray();
            Console.WriteLine("Используем ToArray: " + arr);

            List<int> list = item.ToList();
            Console.WriteLine("Используем ToList: " + list);
        }
        public static void TestToDictionary()
        {
            List<(int id, string firstName, string lastName)> al = new();
            al.Add((1, "Joe", "Rattz"));
            al.Add((2, "William", "Gates"));
            al.Add((3, "Anders", "Hejlsberg"));
            al.Add((9, "John", "Doe"));
            al.Add((4, "David", "Lightman"));
            al.Add((101, "Kevin", "Flynn"));
            al.Add((11, "Robert", "Smith"));

            Dictionary<int, (int id, string firstName, string lastName)> dictWithId = al.ToDictionary(x => x.id);
            Dictionary<int, (string firstName, string lastName)> dictWithoutId = al.ToDictionary(x => x.id, y => (y.firstName, y.lastName));

            //можно впихнуть компаратор ключей, чтобы, например, ключи были равны по некоторому модулю и т.д.
        }
        public static void TestToLookup()
        {
            var people = new[]
            {
                new { Name = "Alice",  City = "London" },
                new { Name = "Bob",    City = "Paris"  },
                new { Name = "Chris",  City = "London" },
                new { Name = "David",  City = "Paris"  },
                new { Name = "Eva",    City = "Rome"   }
            };

            // Создаём Lookup: ключ — город, значение — список людей
            var lookup = people.ToLookup(p => p.City, p => p.Name);

            // Доступ к значениям по ключу
            foreach (var name in lookup["London"])
            {
                Console.WriteLine(name); // Alice, Chris
            }

            // Проверка наличия ключа
            if (lookup.Contains("Rome"))
            {
                Console.WriteLine("Есть кто-то из Рима");
            }

            foreach (var item in lookup)
            {
                Console.WriteLine(item.Key + ":");
                foreach (var item1 in item)
                {
                    Console.WriteLine("    " + item1);
                }
            }

        }
        public static void TestSequenceEqual()
        {
            string[] strArr1 = { "0012", "130", "0000019", "4" };
            string[] strArr2 = { "12", "0130", "019", "0004" };

            bool eq = strArr1.SequenceEqual(strArr2, new MyStringifiedNumberComparer());
            Console.WriteLine(eq);

        }
        private class MyStringifiedNumberComparer : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return (Int32.Parse(x) == Int32.Parse(y));
            }

            public int GetHashCode(string obj)
            {
                return Int32.Parse(obj).ToString().GetHashCode();
            }
        }
        public static void TestFirst_Last()
        {
            string res = _cars.First();
            Console.WriteLine(res);
            res = _cars.First(x => x.Length < 4);
            Console.WriteLine(res);
            res = _cars.FirstOrDefault(x => x.Length > 20, "не найдено)");
            Console.WriteLine(res);
        }
        public static void TestSingle_ElementAt()
        {
            //последовательность из 1 элемента => 1 элемент
            //string res = _cars.Single(); //исключение, так как больше 1 элемента
            string res = LINQS._cars.Single(x => x.Length == 3); //если бы было несколько машин с такой длинной, то было бы ислючение
            Console.WriteLine(res);
            res = _cars.SingleOrDefault(x => x.Length == 2, "нет машин с такой длинной"); //если несколько соответствует, то снова исключение
            Console.WriteLine(res);
            res = _cars.ElementAt(2);
            Console.WriteLine(res);
            res = _cars.ElementAtOrDefault(100);
            Console.WriteLine(res == null ? "null" : res);

        }
        public static void TestAny_All_Contains()
        {
            Console.WriteLine("Any: " + _cars.Any());
            Console.WriteLine("Any Volvo: " + _cars.Any(x => x == "Volvo"));
            Console.WriteLine("Any Lada: " + _cars.Any(x => x == "Lada"));
            Console.WriteLine("Any Length < 2: " + _cars.Any(x => x.Length < 2));
            Console.WriteLine("All Length > 2: " + _cars.All(x => x.Length > 2));
            Console.WriteLine("All Length > 2: " + _cars.All(x => x.Length > 2));
            Console.WriteLine("Contains Lada: " + _cars.Contains("Lada"));
            int[] arr = [1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9]; //12, 7
            foreach (var item in arr)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\nContains 22: " + arr.Contains(22, new MyNumberComparer()));
            Console.WriteLine("Contains 7: " + arr.Contains(7, new MyNumberComparer()));
        }

        private class MyNumberComparer : IEqualityComparer<int>
        {
            public bool Equals(int x, int y)
            {
                return (x % 10 == y % 10);
            }

            public int GetHashCode(int obj)
            {
                return (obj % 10).GetHashCode();
            }
        }
        public static void TestCount_LongCount_Sum()
        {
            Console.WriteLine("Cars count: " + _cars.Count());
            Console.WriteLine("Cars count with length 4: " + _cars.Count(x => x.Length == 4));
            Console.WriteLine("1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9");
            Console.WriteLine("Sum: " + new int[] { 1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9 }.Sum());
            Console.WriteLine("Sum of % 10: " + new int[] { 1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9 }.Sum(x => x % 10));
        }
        public static void TestMin_Max()
        {
            Console.WriteLine("1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9");
            Console.WriteLine("Min: " + new int[] { 1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9 }.Min()); //+IComparer и селектор
        }

        public static void TestMaxBy()
        {
            var str = new Dictionary<int, string>()
            {
                [1] = "United States of America",
                [55] = "Brazil",
                [91] = "India"
            }.MaxBy(x => x.Value.Length).Value;
            Console.WriteLine(str);
        }
        public static string FindLongestCountryName(Dictionary<int, string> existingDictionary)
            => existingDictionary.MaxBy(x => x.Value.Length).Value;
        public static void TestAverage_Aggregate()
        {
            var arr = new int[] { 1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9 };
            Console.WriteLine("1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9");
            Console.WriteLine("Average: " + arr.Average());
            Console.WriteLine("Average % 2: " + arr.Average(x => x % 2));

            int N = 5;
            int agg = Enumerable
                     .Range(1, N)
                     .Aggregate((accumulator, current) => accumulator * current);
            Console.WriteLine($"{N}! = {agg}");
            int sum = Enumerable
                     .Range(1, N)
                     .Aggregate((accumulator, current) => accumulator + current);
            Console.WriteLine($"Sum from 1 to {N} = {sum}");
            sum = Enumerable
                    .Range(1, N)
                    .Aggregate(3, (accumulator, current) => accumulator + current); //задаём начальное значение аккумулятору, а не начинаем с первого элемента
            Console.WriteLine($"Sum from 1 to {N} with initial 3 = {sum}");



            int[] nums = { 2, 3, 4 };

            int productLength = nums.Aggregate(
                1, //если 0, то 0*2*3*4=0 и "0".Length = 1                       
                (acc, x) => acc * x,
                result => result.ToString().Length // длина числа в строковом виде
            );

            Console.WriteLine(productLength); // 2, потому что 2*3*4 = 24 → "24" → длина 2
        }
    }
}

