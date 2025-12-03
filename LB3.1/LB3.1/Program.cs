using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3_Variant6
{
    // ==========================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 1 (ФІРМА)
    // ==========================================
    class Firma
    {
        public string Nazva;
        public DateTime DataZasnuvannya;
        public string Profil; // Маркетинг, ІТ і т.д.
        public string DirectorPIB;
        public int KilkistSivrobitnykiv;
        public string Adresa;

        public Firma(string nazva, DateTime data, string profil, string director, int count, string adresa)
        {
            Nazva = nazva;
            DataZasnuvannya = data;
            Profil = profil;
            DirectorPIB = director;
            KilkistSivrobitnykiv = count;
            Adresa = adresa;
        }

        // Щоб гарно виводилось на екран
        public override string ToString()
        {
            return Nazva + " (" + Profil + ") - Дир: " + DirectorPIB + ", Сотр: " + KilkistSivrobitnykiv;
        }
    }

    // ==========================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 2 (ТЕЛЕФОН)
    // ==========================================
    class Phone
    {
        public string Nazva;
        public string Vyrobnyk;
        public decimal Price;
        public DateTime DataVypusku;

        public Phone(string nazva, string vyrobnyk, decimal price, DateTime data)
        {
            Nazva = nazva;
            Vyrobnyk = vyrobnyk;
            Price = price;
            DataVypusku = data;
        }

        public override string ToString()
        {
            return Nazva + " (" + Vyrobnyk + ") - " + Price + " грн. Дата: " + DataVypusku.ToShortDateString();
        }
    }

    // ==========================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 3 (СПІВРОБІТНИКИ)
    // ==========================================
    // Базовий клас
    class Spivrobitnyk
    {
        public string PIB;
        public decimal Zarplata;
        public int Stazh; // у роках
        public DateTime DataNarodjennya;
        public bool VyshchaOsvita;
        public string Posada; // Щоб знати хто це (Manager, Worker...)

        public Spivrobitnyk(string pib, decimal zp, int stazh, DateTime dn, bool vo, string posada)
        {
            PIB = pib;
            Zarplata = zp;
            Stazh = stazh;
            DataNarodjennya = dn;
            VyshchaOsvita = vo;
            Posada = posada;
        }

        public override string ToString()
        {
            return PIB + " (" + Posada + ") - " + Zarplata + " грн. Стаж: " + Stazh;
        }
    }

    class Worker : Spivrobitnyk
    {
        public Worker(string pib, decimal zp, int stazh, DateTime dn, bool vo)
            : base(pib, zp, stazh, dn, vo, "Worker") { }
    }

    class Manager : Spivrobitnyk
    {
        public Manager(string pib, decimal zp, int stazh, DateTime dn, bool vo)
            : base(pib, zp, stazh, dn, vo, "Manager") { }
    }

    class President : Spivrobitnyk
    {
        public President(string pib, decimal zp, int stazh, DateTime dn, bool vo)
            : base(pib, zp, stazh, dn, vo, "President") { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n=== ЛАБОРАТОРНА РОБОТА №3 (LINQ) ===");
                Console.WriteLine("1. Завдання 1 (Фірми)");
                Console.WriteLine("2. Завдання 2 (Телефони)");
                Console.WriteLine("3. Завдання 3 (Співробітники)");
                Console.WriteLine("0. Вихід");
                Console.Write("Ваш вибір: ");

                string vybor = Console.ReadLine();

                switch (vybor)
                {
                    case "1":
                        Task1();
                        break;
                    case "2":
                        Task2();
                        break;
                    case "3":
                        Task3();
                        break;
                    case "0":
                        return; // Вихід з програми
                    default:
                        Console.WriteLine("Щось не те натиснули.");
                        break;
                }
            }
        }

        static void Task1()
        {
            Console.WriteLine("\n--- ЗАВДАННЯ 1: ФІРМИ ---");

            // Створюємо список фірм
            List<Firma> firmList = new List<Firma>()
            {
                new Firma("Food", new DateTime(2020, 1, 1), "Marketing", "White", 120, "London"),
                new Firma("IT-Solutions", new DateTime(2018, 5, 20), "IT", "Smith", 50, "New York"),
                new Firma("Best Marketing", new DateTime(2022, 10, 10), "Marketing", "Black", 250, "London"),
                new Firma("Super Soft", new DateTime(2010, 3, 15), "IT", "Doe", 350, "Kyiv"),
                new Firma("White Bakery", new DateTime(2023, 1, 1), "Food", "Johnson", 10, "Paris"),
                new Firma("Tech Giant", new DateTime(2000, 1, 1), "IT", "White", 500, "London")
            };

            Console.WriteLine("1. Всі фірми:");
            foreach (var f in firmList) Console.WriteLine(f);

            Console.WriteLine("\n2. Фірми з назвою 'Food':");
            var q2 = firmList.Where(f => f.Nazva.Contains("Food"));
            foreach (var f in q2) Console.WriteLine(f);

            Console.WriteLine("\n3. Фірми маркетингу:");
            var q3 = firmList.Where(f => f.Profil == "Marketing");
            foreach (var f in q3) Console.WriteLine(f);

            Console.WriteLine("\n4. Маркетинг або IT:");
            var q4 = firmList.Where(f => f.Profil == "Marketing" || f.Profil == "IT");
            foreach (var f in q4) Console.WriteLine(f);

            Console.WriteLine("\n5. Більше 100 співробітників:");
            var q5 = firmList.Where(f => f.KilkistSivrobitnykiv > 100);
            foreach (var f in q5) Console.WriteLine(f);

            Console.WriteLine("\n6. Співробітників від 100 до 300:");
            var q6 = firmList.Where(f => f.KilkistSivrobitnykiv >= 100 && f.KilkistSivrobitnykiv <= 300);
            foreach (var f in q6) Console.WriteLine(f);

            Console.WriteLine("\n7. Знаходяться в Лондоні:");
            var q7 = firmList.Where(f => f.Adresa == "London");
            foreach (var f in q7) Console.WriteLine(f);

            Console.WriteLine("\n8. Прізвище директора White:");
            var q8 = firmList.Where(f => f.DirectorPIB == "White");
            foreach (var f in q8) Console.WriteLine(f);

            Console.WriteLine("\n9. Засновані більше 2 років тому:");
            var q9 = firmList.Where(f => f.DataZasnuvannya < DateTime.Now.AddYears(-2));
            foreach (var f in q9) Console.WriteLine(f);

            Console.WriteLine("\n10. Директор Black і назва містить White:");
            var q10 = firmList.Where(f => f.DirectorPIB == "Black" && f.Nazva.Contains("White"));
            foreach (var f in q10) Console.WriteLine(f);

            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }

        static void Task2()
        {
            Console.WriteLine("\n--- ЗАВДАННЯ 2: ТЕЛЕФОНИ ---");

            List<Phone> phones = new List<Phone>()
            {
                new Phone("iPhone 13", "Apple", 900, new DateTime(2021, 9, 14)),
                new Phone("iPhone 10", "Apple", 400, new DateTime(2017, 11, 3)),
                new Phone("Galaxy S22", "Samsung", 850, new DateTime(2022, 2, 25)),
                new Phone("Galaxy A52", "Samsung", 350, new DateTime(2021, 3, 17)),
                new Phone("Redmi Note 10", "Xiaomi", 200, new DateTime(2021, 3, 4)),
                new Phone("Xperia 1", "Sony", 950, new DateTime(2019, 6, 1)),
                new Phone("Xperia 5", "Sony", 800, new DateTime(2020, 10, 1)),
                new Phone("Nokia 3310", "Nokia", 50, new DateTime(2000, 9, 1)) //легендарний телефон:)
            };

            Console.WriteLine("Кількість телефонів: " + phones.Count());
            Console.WriteLine("Ціна > 100: " + phones.Count(p => p.Price > 100));
            Console.WriteLine("Ціна 400-700: " + phones.Count(p => p.Price >= 400 && p.Price <= 700));
            Console.WriteLine("Телефонів Samsung: " + phones.Count(p => p.Vyrobnyk == "Samsung"));

            Console.WriteLine("Мінімальна ціна: " + phones.Min(p => p.Price));
            Console.WriteLine("Максимальна ціна: " + phones.Max(p => p.Price));

            var oldest = phones.OrderBy(p => p.DataVypusku).First();
            Console.WriteLine("Найстаріший: " + oldest);

            var newest = phones.OrderByDescending(p => p.DataVypusku).First();
            Console.WriteLine("Найсвіжіший: " + newest);

            Console.WriteLine("Середня ціна: " + phones.Average(p => p.Price));

            Console.WriteLine("\n5 найдорожчих:");
            foreach (var p in phones.OrderByDescending(x => x.Price).Take(5))
                Console.WriteLine(p);

            Console.WriteLine("\n5 найдешевших:");
            foreach (var p in phones.OrderBy(x => x.Price).Take(5))
                Console.WriteLine(p);

            Console.WriteLine("\nСтатистика по виробниках:");
            var groups = phones.GroupBy(p => p.Vyrobnyk);
            foreach (var g in groups)
            {
                Console.WriteLine(g.Key + " - " + g.Count() + " шт.");
            }

            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }

        static void Task3()
        {
            Console.WriteLine("\n--- ЗАВДАННЯ 3: СПІВРОБІТНИКИ ---");

            List<Spivrobitnyk> stuff = new List<Spivrobitnyk>()
            {
                new Worker("Іванов І.І.", 15000, 5, new DateTime(1990, 5, 20), false),
                new Worker("Петров П.П.", 18000, 10, new DateTime(1985, 3, 10), true),
                new Worker("Сидоров С.С.", 14000, 2, new DateTime(2000, 10, 5), false),
                new Manager("Володимир В.В.", 30000, 15, new DateTime(1980, 1, 1), true),
                new Manager("Олексієнко О.О.", 28000, 8, new DateTime(1992, 7, 7), true),
                new President("Головний Г.Г.", 100000, 20, new DateTime(1975, 5, 5), true),
                new Worker("Володимир К.К.", 16000, 3, new DateTime(1999, 12, 12), true),
                new Worker("Новачок Н.Н.", 10000, 1, new DateTime(2003, 10, 20), false),
                // Додам ще когось для масовості
                new Worker("Старий С.С.", 20000, 25, new DateTime(1965, 2, 2), false),
                new Manager("Молодий М.М.", 25000, 2, new DateTime(2001, 11, 11), true)
            };

            Console.WriteLine("Кількість робітників: " + stuff.Count);

            decimal totalSalary = stuff.Sum(s => s.Zarplata);
            Console.WriteLine("Загальна зарплата: " + totalSalary);

            Console.WriteLine("\nЗапит: 10 найдосвідченіших, серед них наймолодший з вищою освітою:");
            // Спочатку беремо топ 10 по стажу
            var topExp = stuff.OrderByDescending(s => s.Stazh).Take(10).ToList();
            // Серед них шукаємо з вищою освітою і сортуємо по віку (Дата народження спадає = молодший)
            var target = topExp.Where(s => s.VyshchaOsvita)
                               .OrderByDescending(s => s.DataNarodjennya) // найпізніша дата = наймолодший
                               .FirstOrDefault();

            if (target != null) Console.WriteLine("Знайдено: " + target);
            else Console.WriteLine("Не знайдено.");

            Console.WriteLine("\nНаймолодший та найстарший Manager:");
            var managers = stuff.Where(s => s is Manager).ToList();
            if (managers.Count > 0)
            {
                var youngM = managers.OrderByDescending(s => s.DataNarodjennya).First();
                var oldM = managers.OrderBy(s => s.DataNarodjennya).First();
                Console.WriteLine("Молодий: " + youngM);
                Console.WriteLine("Старий: " + oldM);
            }

            Console.WriteLine("\nНародилися у жовтні (групування):");
            var octPeople = stuff.Where(s => s.DataNarodjennya.Month == 10);
            foreach (var p in octPeople)
            {
                Console.WriteLine(p.Posada + ": " + p.PIB);
            }

            Console.WriteLine("\nВсі Володимири:");
            var volodymyrs = stuff.Where(s => s.PIB.Contains("Володимир")).ToList();
            foreach (var v in volodymyrs) Console.WriteLine(v);

            if (volodymyrs.Count > 0)
            {
                var youngestVolodymyr = volodymyrs.OrderByDescending(s => s.DataNarodjennya).First();
                Console.WriteLine("Наймолодшому Володимиру (" + youngestVolodymyr.PIB + ") даємо премію!");
                decimal premia = youngestVolodymyr.Zarplata / 3;
                Console.WriteLine("Премія: " + premia + " грн.");
            }

            Console.WriteLine("\nНатисніть Enter...");
            Console.ReadLine();
        }
    }
}