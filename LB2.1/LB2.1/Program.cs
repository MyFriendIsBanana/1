using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2_Variant6
{
    // ==========================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 1 (ВАРІАНТ 6)
    // ==========================================
    public class Record
    {
        public string FullName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public DateTime Birthday { get; set; }

        public Record(string fullName, string homePhone, string workPhone, string mobilePhone, DateTime birthday)
        {
            FullName = fullName;
            HomePhone = homePhone;
            WorkPhone = workPhone;
            MobilePhone = mobilePhone;
            Birthday = birthday;
        }

        public override string ToString()
        {
            return $"ПІБ: {FullName,-20} | ДН: {Birthday.ToShortDateString()} | Моб: {MobilePhone,-12} | Дом: {HomePhone,-12} | Роб: {WorkPhone}";
        }
    }

    // ==========================================
    // КЛАСИ ДЛЯ ЗАВДАННЯ 3 (ЧЕРГА ДРУКУ)
    // ==========================================
    public class PrintJob
    {
        public string UserName { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PageCount { get; set; }

        public override string ToString()
        {
            return $"[{CreatedAt:HH:mm:ss}] Користувач: {UserName}, Файл: {FileName} ({PageCount} стор.)";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА № 2 (ВАРІАНТ 6) ===");
                Console.WriteLine("1. Завдання 1: Робота зі списком (Клас 'Запис')");
                Console.WriteLine("2. Завдання 2: Частотний аналіз тексту (Dictionary)");
                Console.WriteLine("3. Завдання 3: Черга друку (Queue)");
                Console.WriteLine("0. Вихід");
                Console.Write("\nОберіть завдання: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RunTask1();
                        break;
                    case "2":
                        RunTask2();
                        break;
                    case "3":
                        RunTask3();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Натисніть будь-яку клавішу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // ==========================================
        // ЛОГІКА ЗАВДАННЯ 1
        // ==========================================
        static void RunTask1()
        {
            List<Record> records = new List<Record>();

            // Додамо декілька тестових записів
            records.Add(new Record("Іванов І.І.", "044-111", "044-222", "050-111-11", new DateTime(1990, 5, 20)));
            records.Add(new Record("Петров П.П.", "044-333", "044-444", "067-222-22", new DateTime(1995, 10, 15)));
            records.Add(new Record("Сидоров С.С.", "-", "044-555", "063-333-33", new DateTime(1988, 1, 5)));

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- ЗАВДАННЯ 1: Список 'Запис' ---");
                Console.WriteLine("1. Переглянути всі записи");
                Console.WriteLine("2. Додати новий запис");
                Console.WriteLine("3. Редагувати запис");
                Console.WriteLine("4. Видалити запис");
                Console.WriteLine("5. Пошук іменинників (за діапазоном дат)");
                Console.WriteLine("0. Назад у головне меню");
                Console.Write("Ваш вибір: ");

                string subChoice = Console.ReadLine();

                switch (subChoice)
                {
                    case "1":
                        ShowRecords(records);
                        break;
                    case "2":
                        AddRecord(records);
                        break;
                    case "3":
                        EditRecord(records);
                        break;
                    case "4":
                        RemoveRecord(records);
                        break;
                    case "5":
                        SearchBirthdays(records);
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            }
        }

        static void ShowRecords(List<Record> list)
        {
            Console.WriteLine("\n--- Список записів ---");
            if (list.Count == 0) Console.WriteLine("Список порожній.");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i]}");
            }
            Console.WriteLine("\nНатисніть Enter для продовження...");
            Console.ReadLine();
        }

        static void AddRecord(List<Record> list)
        {
            Console.WriteLine("\n--- Додавання запису ---");
            try
            {
                Console.Write("ПІБ: ");
                string name = Console.ReadLine();
                Console.Write("Домашній телефон: ");
                string homePh = Console.ReadLine();
                Console.Write("Робочий телефон: ");
                string workPh = Console.ReadLine();
                Console.Write("Мобільний телефон: ");
                string mobPh = Console.ReadLine();
                Console.Write("Дата народження (рррр-мм-дд): ");
                DateTime bday = DateTime.Parse(Console.ReadLine());

                list.Add(new Record(name, homePh, workPh, mobPh, bday));
                Console.WriteLine("Запис додано!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка введення: {ex.Message}");
            }
            Console.ReadKey();
        }

        static void EditRecord(List<Record> list)
        {
            ShowRecords(list);
            Console.Write("Введіть номер запису для редагування: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= list.Count)
            {
                var rec = list[index - 1];
                Console.WriteLine($"Редагуємо: {rec.FullName}");
                Console.Write("Новий ПІБ (Enter щоб залишити): ");
                string val = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(val)) rec.FullName = val;

                Console.Write("Новий мобільний (Enter щоб залишити): ");
                val = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(val)) rec.MobilePhone = val;

                // Можна розширити для інших полів
                Console.WriteLine("Запис оновлено.");
            }
            else
            {
                Console.WriteLine("Невірний номер.");
            }
            Console.ReadKey();
        }

        static void RemoveRecord(List<Record> list)
        {
            ShowRecords(list);
            Console.Write("Введіть номер запису для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= list.Count)
            {
                list.RemoveAt(index - 1);
                Console.WriteLine("Запис видалено.");
            }
            else
            {
                Console.WriteLine("Невірний номер.");
            }
            Console.ReadKey();
        }

        static void SearchBirthdays(List<Record> list)
        {
            Console.WriteLine("\n--- Пошук за датою народження ---");
            try
            {
                Console.Write("Початкова дата (рррр-мм-дд): ");
                DateTime start = DateTime.Parse(Console.ReadLine());
                Console.Write("Кінцева дата (рррр-мм-дд): ");
                DateTime end = DateTime.Parse(Console.ReadLine());

                var results = list.Where(r => r.Birthday >= start && r.Birthday <= end).ToList();

                Console.WriteLine("\nРезультати пошуку:");
                if (results.Count > 0)
                {
                    foreach (var r in results) Console.WriteLine(r);

                    // Збереження у файл згідно завдання
                    string fileName = "search_results.txt";
                    using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                    {
                        sw.WriteLine($"Результати пошуку ({DateTime.Now}) для діапазону {start.ToShortDateString()} - {end.ToShortDateString()}:");
                        foreach (var r in results) sw.WriteLine(r.ToString());
                    }
                    Console.WriteLine($"\nРезультати також збережено у файл '{fileName}'");
                }
                else
                {
                    Console.WriteLine("Записів не знайдено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            Console.ReadKey();
        }

        // ==========================================
        // ЛОГІКА ЗАВДАННЯ 2 (Словники)
        // ==========================================
        static void RunTask2()
        {
            // Ініціалізація тестових файлів, якщо їх немає
            SetupTask2Files();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- ЗАВДАННЯ 2: Аналіз тексту ---");

                string indexFile = "firstFile.txt";
                if (!File.Exists(indexFile))
                {
                    Console.WriteLine("Файл 'firstFile.txt' не знайдено.");
                    Console.ReadKey();
                    return;
                }

                string[] files = File.ReadAllLines(indexFile);
                Console.WriteLine("Доступні файли для аналізу:");
                for (int i = 0; i < files.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {files[i]}");
                }
                Console.WriteLine("0. Назад");

                Console.Write("\nОберіть файл за номером: ");
                string choice = Console.ReadLine();

                if (choice == "0") return;

                if (int.TryParse(choice, out int idx) && idx > 0 && idx <= files.Length)
                {
                    string selectedFile = files[idx - 1];
                    AnalyzeFile(selectedFile);
                }
                else
                {
                    Console.WriteLine("Невірний вибір.");
                    Console.ReadKey();
                }
            }
        }

        static void SetupTask2Files()
        {
            if (!File.Exists("firstFile.txt"))
            {
                File.WriteAllLines("firstFile.txt", new[] { "text1.txt", "text2.txt" });
                File.WriteAllText("text1.txt", "Hello world. This is a test. Hello C# world.");
                File.WriteAllText("text2.txt", "Programming is fun. Collections are useful. Fun programming collections.");
                Console.WriteLine("Створено тестові файли (firstFile.txt, text1.txt, text2.txt).");
                Console.ReadKey();
            }
        }

        static void AnalyzeFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Файл {filename} не знайдено!");
                Console.ReadKey();
                return;
            }

            string text = File.ReadAllText(filename);
            // Розбиваємо текст на слова, прибираючи пунктуацію
            string[] words = Regex.Split(text.ToLower(), @"\W+");

            Dictionary<string, int> wordStats = new Dictionary<string, int>();

            foreach (string word in words)
            {
                if (!string.IsNullOrWhiteSpace(word))
                {
                    if (wordStats.ContainsKey(word))
                        wordStats[word]++;
                    else
                        wordStats[word] = 1;
                }
            }

            Console.WriteLine($"\nСтатистика для файлу '{filename}':");
            Console.WriteLine("{0,-20} {1,5}", "Слово", "К-сть");
            Console.WriteLine(new string('-', 26));

            foreach (var kvp in wordStats.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{kvp.Key,-20} {kvp.Value,5}");
            }

            // Збереження статистики
            using (StreamWriter sw = new StreamWriter($"stats_{filename}"))
            {
                foreach (var kvp in wordStats.OrderByDescending(x => x.Value))
                {
                    sw.WriteLine($"{kvp.Key}: {kvp.Value}");
                }
            }
            Console.WriteLine($"\nСтатистику збережено у файл 'stats_{filename}'");
            Console.WriteLine("Натисніть Enter...");
            Console.ReadLine();
        }

        // ==========================================
        // ЛОГІКА ЗАВДАННЯ 3 (Черга)
        // ==========================================
        static void RunTask3()
        {
            Queue<PrintJob> printQueue = new Queue<PrintJob>();
            List<PrintJob> printHistory = new List<PrintJob>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- ЗАВДАННЯ 3: Черга друку ---");
                Console.WriteLine($"Документів у черзі: {printQueue.Count}");
                Console.WriteLine("1. Додати документ на друк");
                Console.WriteLine("2. Друкувати наступний документ (обробити чергу)");
                Console.WriteLine("3. Показати історію друку");
                Console.WriteLine("4. Показати стан черги");
                Console.WriteLine("0. Назад");
                Console.Write("Вибір: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Write("Ім'я користувача: ");
                        string user = Console.ReadLine();
                        Console.Write("Назва файлу: ");
                        string file = Console.ReadLine();
                        Console.Write("Кількість сторінок: ");
                        int.TryParse(Console.ReadLine(), out int pages);

                        PrintJob job = new PrintJob
                        {
                            UserName = user,
                            FileName = file,
                            PageCount = pages,
                            CreatedAt = DateTime.Now
                        };
                        printQueue.Enqueue(job);
                        Console.WriteLine("Додано в чергу.");
                        break;
                    case "2":
                        if (printQueue.Count > 0)
                        {
                            PrintJob currentJob = printQueue.Dequeue();
                            Console.WriteLine($"Друкується: {currentJob}");
                            // Імітація часу друку
                            System.Threading.Thread.Sleep(500);
                            Console.WriteLine("Друк завершено.");
                            printHistory.Add(currentJob);
                        }
                        else
                        {
                            Console.WriteLine("Черга порожня.");
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("\n--- Історія друку ---");
                        foreach (var item in printHistory) Console.WriteLine(item);
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.WriteLine("\n--- Поточна черга ---");
                        foreach (var item in printQueue) Console.WriteLine(item);
                        Console.ReadKey();
                        break;
                    case "0":
                        return;
                }
            }
        }
    }
}