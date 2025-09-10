namespace Tests; 
using System;
using System.Collections;


public static partial class LINQS
{
    public static readonly string[] _names =
    {
        "Adams", "Arthur", "Buchanan", "Bush", "Joe", "Carter", "Cleveland",
        "Clinton", "Coolidge", "Eisenhower", "Fillmore", "Ford",  "Garfield",
        "Grant", "Harding", "Harrison", "Hayes", "Hoover", "Jackson",
        "Jefferson", "Johnson", "Kennedy", "Lincoln", "Madison", "McKinley",
        "Monroe", "Nixon", "Obama", "Pierce", "Polk", "Reagan", "Roosevelt",
        "Taft", "Taylor", "Truman", "Tyler", "Van Buren", "Washington", "Wilson", "Adams"
    };

    public static readonly string[] _cars = { "Nissan", "Aston Martin", "Chevrolet", "Alfa Romeo", "Chrysler", "Dodge", "BMW",
        "Ferrari", "Audi", "Bentley", "Ford", "Lexus", "Mercedes", "Toyota", "Volvo", "Subaru", "Жигули :)"};

    private static readonly int[] _numbers =
    {
            42, 7, 93, 18, 56, 11, 74, 3, 65, 29
        };
    private class Pseudoname
    {
        public int id;
        public string firstName;
        public string lastName;

        public static ArrayList GetPseudonamesArrayList()
        {
            ArrayList al = new ArrayList();

            al.Add(new Pseudoname { id = 1, firstName = "Joe", lastName = "Rattz" });
            al.Add(new Pseudoname { id = 2, firstName = "William", lastName = "Gates" });
            al.Add(new Pseudoname { id = 3, firstName = "Anders", lastName = "Hejlsberg" });
            al.Add(new Pseudoname { id = 3, firstName = "John", lastName = "Doe" });
            al.Add(new Pseudoname { id = 4, firstName = "David", lastName = "Lightman" });
            al.Add(new Pseudoname { id = 101, firstName = "Kevin", lastName = "Flynn" });
            al.Add(new Pseudoname { id = 101, firstName = "Robert", lastName = "Smith" });
            return (al);
        }

        public static Pseudoname[] GetPseudonamesArray()
        {
            return ((Pseudoname[])GetPseudonamesArrayList().ToArray(typeof(Pseudoname)));
        }
    }
    public class Transaction
    {
        public int passportId;           // id сотрудника
        public string transaction = "";  // описание транзакции
        public DateTime dateAwarded;     // дата транзакции

        public static Transaction[] GetTransactions()
        {
            return new Transaction[]
            {
                    new Transaction { passportId = 1,   transaction = "Оплата бонуса",          dateAwarded = DateTime.Parse("1999-12-31") },
                    new Transaction { passportId = 2,   transaction = "Выдача опциона",         dateAwarded = DateTime.Parse("1992-06-30") },
                    new Transaction { passportId = 2,   transaction = "Оплата за проект",       dateAwarded = DateTime.Parse("1994-01-01") },
                    new Transaction { passportId = 3,   transaction = "Премия",                 dateAwarded = DateTime.Parse("1997-09-30") },
                    new Transaction { passportId = 2,   transaction = "Оплата бонуса",          dateAwarded = DateTime.Parse("2003-04-01") },
                    new Transaction { passportId = 3,   transaction = "Выдача опциона",         dateAwarded = DateTime.Parse("1998-09-30") },
                    new Transaction { passportId = 3,   transaction = "Оплата сверхурочных",    dateAwarded = DateTime.Parse("1998-09-30") },
                    new Transaction { passportId = 4,   transaction = "Премия",                 dateAwarded = DateTime.Parse("1997-12-31") },
                    new Transaction { passportId = 101, transaction = "Выдача опциона",         dateAwarded = DateTime.Parse("1998-12-31") },
                    new Transaction { passportId = 101, transaction = "Оплата бонуса",          dateAwarded = DateTime.Parse("2000-01-15") },
                    new Transaction { passportId = 3,   transaction = "Оплата проекта",         dateAwarded = DateTime.Parse("2001-07-20") }
            };
        }
    }
}

