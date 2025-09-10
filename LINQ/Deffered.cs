namespace Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
public static partial class LINQS
{
    //expression trees
    public static class Deferred
    {
        //Delayed opeartions
        //Where

        //Select
        //SelectMany

        //Take
        //TakeWhile
        //TakeLast
        //Skip
        //SkipWhile

        //Concat

        //Order
        //OrderBy
        //OrderByDescending
        //ThenBy

        //Join
        //GroupJoin

        //GroupBy

        //Distinct
        //Union
        //Except
        //Intersect

        //Cast
        //OfType
        //AsEnumerable

        //DefaultIfEmpty
        //Range
        //Repeat
        //Empty

        //Zip
        //Chunk


        public static void TestDelayedOps_Exceptions()
        {
            string[] strings = { "one", "two", null, "three" };

            Console.WriteLine("Before Where() is called.");
            IEnumerable<string> ieStrings = strings.Where(s => s.Length == 3); //отложенное выполнение, следовательно, ошибка будет потом
            Console.WriteLine("After Where() is called.");

            foreach (string s in ieStrings)
            {
                Console.WriteLine("Processing " + s);
            }
        }
        public static void TestDelayedOperations()
        {
            string[] strings = { "one", "two", "three" };

            IEnumerable<string> ieStrings = strings.Where(s => s.Length == 3);

            foreach (string s in ieStrings)
            {
                Console.WriteLine("Processing " + s);
            }

            strings[0] = "four";
            strings[2] = "six"; //отложенная инициализация - новый запрос каждый раз при вызове

            foreach (string s in ieStrings)
            {
                Console.WriteLine("Processing " + s);
            }
        }
        public static void TestWhere()
        {
            var filteredNames = _names.Where(n => n.StartsWith('C'));
            foreach (var name in filteredNames)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            filteredNames = _names.Where((n, i) => i % 3 == 0);
            foreach (var name in filteredNames)
            {
                Console.WriteLine(name);
            }
        }
        public static void TestSelect_SelectMany()
        {
            var hashes = _names.Select(x => x.GetHashCode());
            foreach (var hash in hashes)
            {
                Console.WriteLine(hash);
            }
            Console.WriteLine();
            var hashes2 = _names.Select((x, i) => (x, i, x.GetHashCode()));
            foreach (var hash in hashes2)
            {
                Console.WriteLine(hash);
            }
            Console.WriteLine();
            var symbolsOfCars = _cars.SelectMany(x => x.ToArray());
            foreach (var symbol in symbolsOfCars)
            {
                Console.Write(symbol + " ");
            }
        }
        public static void TestTake_Skip()
        {
            var names1 = _names.Take(10);
            foreach (var name in names1)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            var names2 = _names.Take(2..5);
            foreach (var name in names2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            var names3 = _names.TakeWhile(x => x.Length > 3);
            foreach (var name in names3)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            var names4 = _names.TakeLast(3);
            foreach (var name in names4)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            var names5 = _names.Skip(_names.Length - 3);
            foreach (var name in names5)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            var names6 = _names.SkipWhile(x => x.Length < 10);
            foreach (var name in names6)
            {
                Console.WriteLine(name);
            }
        }
        public static void TestConcat()
        {
            var names = _names.Take(5);
            var cars = _cars.TakeLast(5);
            var concated1 = Enumerable.Concat(names, cars);
            var concated2 = names.Concat(cars);

            foreach (var item in concated1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            foreach (var item in concated2)
            {
                Console.WriteLine(item);
            }
        }
        public static void TestOrderBy()
        {
            //неустойчивая сортировка
            var nums1 = _numbers.Order();
            foreach (var item in nums1)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var nums2 = _numbers.OrderDescending();
            foreach (var item in nums2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var nums3 = _numbers.OrderBy(x => x % 5);
            foreach (var item in nums3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var nums4 = _numbers.OrderByDescending(x => x);
            foreach (var item in nums4)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            //ещё есть перегрузка для компараторов ключей
        }
        public static void TestThenBy()
        {
            //устойчивая сортировка
            var people = new[]
            {
                new { Name = "Bob", Age = 30 },
                new { Name = "Ann", Age = 20 },
                new { Name = "Carl", Age = 20 },
                new { Name = "Alex", Age = 30 }
            };

            var sorted = people
                .OrderBy(p => p.Age)        // 1. Сортировка по возрасту
                .ThenBy(p => p.Name);       // 2. Сортировка по имени среди тех, у кого возраст одинаковый

            foreach (var p in sorted)
                Console.WriteLine($"{p.Name} ({p.Age})");
            /*
            OrderBy:
                Сканирует коллекцию.
                Для каждого элемента получает ключ Age.
                Выполняет устойчивую сортировку: одинаковые Age идут в том же порядке, что и в исходной коллекции.
                Возвращает объект IOrderedEnumerable<T> — это не просто IEnumerable, а специальная коллекция, которая хранит информацию о ключах и компараторах.

            ThenBy:
                Получает этот же объект IOrderedEnumerable<T> с уже упорядоченными элементами.
                Добавляет второй уровень сортировки в цепочку сравнений.
                При сравнении двух элементов:
                Сначала сравнивает по ключу первого уровня (Age).
                Если ключи равны, применяет второй компаратор (Name
                Порядок элементов с разным Age не трогается — он уже зафиксирован OrderBy.
             */
        }
        public static void TestJoin_GroupJoin()
        {
            Pseudoname[] pseudonames = Pseudoname.GetPseudonamesArray();
            Transaction[] transactions = Transaction.GetTransactions();

            // Join → плоский результат: каждая пара (employee, option)
            var transactionsOfPseudoname = pseudonames
                .Join(
                    transactions,
                    e => e.id,
                    o => o.passportId,
                    (e, o) => new
                    {
                        id = e.id,
                        name = $"{e.firstName} {e.lastName}",
                        transaction = o.transaction,
                        dateAwarded = o.dateAwarded
                    }
                );
            foreach (var item in transactionsOfPseudoname)
                Console.WriteLine($"Id: {item.id}, Name: {item.name}, Transaction: {item.transaction}, Date: {item.dateAwarded:yyyy-MM-dd}");

            Console.WriteLine();
 
            // GroupJoin → сгруппированный результат: псевдоним и множество его транзакций
            var transactionsOfPseudonameGrouped = pseudonames
                .GroupJoin(
                    transactions,
                    pseudoname => pseudoname.id,
                    transaction => transaction.passportId,
                    (pseudoname, transactionsGroup) => new
                    {
                        id = pseudoname.id,
                        name = $"{pseudoname.firstName} {pseudoname.lastName}",
                        transactions = transactionsGroup // коллекция всех транзакций для этого псевдонима
                    }
                );

            foreach (var item in transactionsOfPseudonameGrouped)
            {
                Console.WriteLine($"Id: {item.id}, Name: {item.name}, Transactions count: {item.transactions.Count()}");
                foreach (var opt in item.transactions)
                {
                    Console.WriteLine($"\tTransaction: {opt.transaction}, DateAwarded: {opt.dateAwarded:yyyy-MM-dd}");
                }
            }
        }

        public static void TestGroupBy()
        {
            Transaction[] transactions = Transaction.GetTransactions();
            var transactionsById = transactions.GroupBy(x => x.passportId);
            foreach (var item in transactionsById)
            {
                Console.WriteLine($"ID: {item.Key}:");
                foreach (var element in item)
                {
                    Console.WriteLine($"\tTransaction: {element.transaction}, DateAwarded: {element.dateAwarded:yyyy-MM-dd}");
                }
            }

            Console.WriteLine("-----------------------------");
            var transactionsById2 = transactions.GroupBy(x => x.passportId, y => y.transaction); //буквально то же, что и Select
            foreach (var item in transactionsById2)
            {
                Console.WriteLine($"ID: {item.Key}:");
                foreach (var element in item)
                {
                    Console.WriteLine($"\tTransaction: {element}");
                }
            }
        }
        public static void TestDistinct_Union_Except_Intersect()
        {
            //Distinct - оставляет только уникальные элементы
            int[] arr = [1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9, 2]; //13
            var first = arr.Take(10);
            var second = arr.TakeLast(10);
            var distinct = first.Distinct();
            Console.WriteLine("First:");
            foreach (int i in first)
                Console.Write(i + " ");
            Console.WriteLine();
            Console.WriteLine("First.Distinct():");
            foreach (int i in distinct)
                Console.Write(i + " ");

            Console.WriteLine();
            Console.WriteLine("Second:");
            foreach (int i in second)
                Console.Write(i + " ");

            var concat = first.Concat(second);
            Console.WriteLine();
            Console.WriteLine("Concat:");
            foreach (int i in concat)
                Console.Write(i + " ");

            //Union - Concat + Distinct
            var union = first.Union(second);
            Console.WriteLine();
            Console.WriteLine("Union:");
            foreach (int i in union)
                Console.Write(i + " ");

            //Intersect - пересечение + Distinct
            var intersect = first.Intersect(second);
            Console.WriteLine();
            Console.WriteLine("Intersect:");
            foreach (int i in intersect)
                Console.Write(i + " ");

            //Except - логическое вычитание + Distinct
            var except = first.Except(second);
            Console.WriteLine();
            Console.WriteLine("Except:");
            foreach (int i in except)
                Console.Write(i + " ");
        }

        /// <remarks>
        /// AsEnumerable не реализован
        /// </remarks>
        public static void TestCast_OfType_AsEnumerable()
        {
            ArrayList objInt = [1, 0, 10, 5, 8, 5, 1, 0, 12, 5, 9, 9, 2];
            ArrayList objNotAllInt = [0, true, 10, 5, "dsf", 8, 1, 1.4, 0, 5, false, 9, 2];
            var castedInt = objInt.Cast<int>();
            Console.WriteLine("Cast:");
            foreach (int i in castedInt)
                Console.Write(i + " ");

            var ofTypedInt = objNotAllInt.OfType<int>(); //не бросит исключение - если тип не int
            Console.WriteLine();
            Console.WriteLine("OfType:");
            foreach (int i in ofTypedInt)
                Console.Write(i + " ");
        }
        public static void TestDefaultIfEmpty()
        {
            string[] cars = { "Alfa Romeo", "Aston Martin", "Audi", "Nissan", "Chevrolet",  "Chrysler", "Dodge", "BMW",
                            "Ferrari",  "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"};
            try
            {
                string nissan = cars.Where(n => n.Equals("Porshe")).First();

                if (nissan != null)
                    Console.WriteLine("Автомобиль Porshe найден");
                else
                    Console.WriteLine("Автомобиль Porshe не найден");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string porshe = cars
                    .Where(n => n.Equals("Porshe"))
                    .DefaultIfEmpty("Не найдено :(") //в этой перегрузке вручную устанавливается значение по умолчанию
                                                     //.DefaultIfEmpty()
                    .First();

            Console.WriteLine(porshe);

        }
        public static void TestRange_Repeat_Empty()
        {
            IEnumerable<int> nums = Enumerable.Range(1, 100); //цикл можно сказать
            foreach (int i in nums)
                Console.Write(i + "  ");
            Console.WriteLine();

            nums = Enumerable.Repeat(20, 5); //цикл можно сказать
            foreach (int i in nums)
                Console.Write(i + "  ");
            Console.WriteLine();

            IEnumerable<string> str = Enumerable.Empty<string>();
            foreach (string s in str)
                Console.Write(s);
            Console.WriteLine(str.Count());
        }
        public static void TestZip()
        {
            string[] subjects = ["nail", "shoe", "horse", "rider", "message", "battle", "kingdom"];
            var res = subjects.Zip(subjects.Skip(1)).Select(pair => $"{pair.First} + {pair.Second}");
            foreach (var item in res)
                Console.WriteLine(item);
            res = subjects.Zip(subjects.Skip(1), subjects.Skip(2)).Select(triplet => $"{triplet.First} + {triplet.Second} + {triplet.Third}");
            foreach (var item in res)
                Console.WriteLine(item);
        }
        public static void TestChunk()
        {
            string example = "0123456789";
            var res = example.Chunk(3);
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}

