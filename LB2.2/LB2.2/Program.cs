using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab2_Variant6
{
    // Клас для першого завдання (Запис)
    // Зробив полями, бо так простіше
    class Zapis
    {
        public string PIB;
        public string DimPhone;
        public string RobPhone;
        public string MobPhone;
        public DateTime DataNarodjennya;

        // Конструктор
        public Zapis(string pib, string dim, string rob, string mob, DateTime data)
        {
            PIB = pib;
            DimPhone = dim;
            RobPhone = rob;
            MobPhone = mob;
            DataNarodjennya = data;
        }
    }

    // Клас для черги друку
    class Document
    {
        public string User;
        public string FileName;
        public int Pages;

        public Document(string user, string file, int pages)
        {
            User = user;
            FileName = file;
            Pages = pages;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Щоб укр мова нормально відображалась
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\n--- МЕНЮ ---");
                Console.WriteLine("1. Завдання 1 (Список)");
                Console.WriteLine("2. Завдання 2 (Словник)");
                Console.WriteLine("3. Завдання 3 (Черга)");
                Console.WriteLine("0. Вихід");
                Console.Write("Виберіть пункт: ");

                string vybor = Console.ReadLine();

                if (vybor == "1")
                {
                    Task1();
                }
                else if (vybor == "2")
                {
                    Task2();
                }
                else if (vybor == "3")
                {
                    Task3();
                }
                else if (vybor == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Невірний вибір.");
                }
            }
        }

        static void Task1()
        {
            // Ось тут переробив ініціалізацію, як в завданні
            // Створюємо список і зразу наповнюємо його даними
            List<Zapis> spysok = new List<Zapis>()
            {
                new Zapis("Іванов І.І.", "22-33", "44-55", "050-111-22-33", new DateTime(1990, 5, 20)),
                new Zapis("Петров П.П.", "11-00", "55-66", "067-999-88-77", new DateTime(1995, 10, 15)),
                new Zapis("Сидоров С.С.", "нема", "77-88", "063-555-44-33", new DateTime(2000, 1, 5))
            };

            while (true)
            {
                Console.WriteLine("\n--- ЗАВДАННЯ 1 ---");
                Console.WriteLine("1. Показати всі записи");
                Console.WriteLine("2. Додати новий");
                Console.WriteLine("3. Видалити");
                Console.WriteLine("4. Пошук іменинників");
                Console.WriteLine("0. Назад");
                Console.Write("-> ");
                string op = Console.ReadLine();

                if (op == "1")
                {
                    Console.WriteLine("\nВсі люди в списку:");
                    for (int i = 0; i < spysok.Count; i++)
                    {
                        Console.WriteLine((i + 1) + ". " + spysok[i].PIB + " | ДН: " + spysok[i].DataNarodjennya.ToShortDateString() + " | Моб: " + spysok[i].MobPhone);
                    }
                }
                else if (op == "2")
                {
                    Console.Write("Введіть ПІБ: ");
                    string name = Console.ReadLine();
                    Console.Write("Дом. телефон: ");
                    string home = Console.ReadLine();
                    Console.Write("Роб. телефон: ");
                    string work = Console.ReadLine();
                    Console.Write("Моб. телефон: ");
                    string mob = Console.ReadLine();
                    Console.Write("Дата (рік-місяць-день): ");

                    try
                    {
                        DateTime date = Convert.ToDateTime(Console.ReadLine());
                        // Додаємо в кінець списку
                        spysok.Add(new Zapis(name, home, work, mob, date));
                        Console.WriteLine("Запис додано!");
                    }
                    catch
                    {
                        Console.WriteLine("Ви ввели неправильну дату!");
                    }
                }
                else if (op == "3")
                {
                    Console.Write("Введіть номер, який видалити: ");
                    try
                    {
                        int num = Convert.ToInt32(Console.ReadLine());
                        if (num > 0 && num <= spysok.Count)
                        {
                            spysok.RemoveAt(num - 1);
                            Console.WriteLine("Видалили.");
                        }
                        else
                        {
                            Console.WriteLine("Такого номера немає.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Це не число.");
                    }
                }
                else if (op == "4")
                {
                    Console.WriteLine("Пошук по датах");
                    Console.Write("З якої дати: ");
                    DateTime d1 = Convert.ToDateTime(Console.ReadLine());
                    Console.Write("По яку дату: ");
                    DateTime d2 = Convert.ToDateTime(Console.ReadLine());

                    Console.WriteLine("Результат:");

                    // Запис у файл
                    StreamWriter sw = new StreamWriter("result.txt");
                    sw.WriteLine("Результати пошуку:");

                    bool found = false;
                    foreach (Zapis z in spysok)
                    {
                        if (z.DataNarodjennya >= d1 && z.DataNarodjennya <= d2)
                        {
                            string info = z.PIB + " - " + z.DataNarodjennya.ToShortDateString();
                            Console.WriteLine(info);
                            sw.WriteLine(info);
                            found = true;
                        }
                    }
                    sw.Close(); // закриваємо файл

                    if (found) Console.WriteLine("Результат записано у файл result.txt");
                    else Console.WriteLine("Нікого не знайдено.");
                }
                else if (op == "0")
                {
                    break;
                }
            }
        }

        static void Task2()
        {
            // Створюємо файли для тесту, якщо їх нема
            if (!File.Exists("firstFile.txt"))
            {
                File.WriteAllText("firstFile.txt", "text1.txt");
                File.WriteAllText("text1.txt", "Hello world hello C# world programming is fun");
            }

            Console.WriteLine("Читаємо назву файлу з firstFile.txt...");
            string nazvaFaylu = File.ReadAllText("firstFile.txt");

            if (File.Exists(nazvaFaylu))
            {
                string text = File.ReadAllText(nazvaFaylu);
                text = text.ToLower(); // щоб не плутати великі і малі літери

                // Розбиваємо текст на слова по пробілах
                string[] slova = text.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);

                Dictionary<string, int> slovnik = new Dictionary<string, int>();

                // Рахуємо скільки разів зустрічається слово
                foreach (string s in slova)
                {
                    if (slovnik.ContainsKey(s))
                    {
                        slovnik[s] = slovnik[s] + 1;
                    }
                    else
                    {
                        slovnik.Add(s, 1);
                    }
                }

                Console.WriteLine("\nСтатистика:");
                foreach (var item in slovnik)
                {
                    Console.WriteLine("Слово: " + item.Key + " | Кількість: " + item.Value);
                }
            }
            else
            {
                Console.WriteLine("Файлу з текстом не існує.");
            }
            Console.WriteLine("Натисніть Enter...");
            Console.ReadLine();
        }

        static void Task3()
        {
            Queue<Document> cherga = new Queue<Document>();

            while (true)
            {
                Console.WriteLine("\n--- ЧЕРГА ДРУКУ ---");
                Console.WriteLine("Документів у черзі: " + cherga.Count);
                Console.WriteLine("1. Додати файл");
                Console.WriteLine("2. Друкувати (видалити з черги)");
                Console.WriteLine("0. Назад");
                Console.Write("-> ");
                string k = Console.ReadLine();

                if (k == "1")
                {
                    Console.Write("Ім'я користувача: ");
                    string u = Console.ReadLine();
                    Console.Write("Назва файлу: ");
                    string f = Console.ReadLine();
                    Console.Write("Сторінок: ");
                    int p = Convert.ToInt32(Console.ReadLine());

                    Document doc = new Document(u, f, p);
                    cherga.Enqueue(doc);
                    Console.WriteLine("Додали в чергу.");
                }
                else if (k == "2")
                {
                    if (cherga.Count > 0)
                    {
                        Document d = cherga.Dequeue();
                        Console.WriteLine("Друкується файл: " + d.FileName);
                        Console.WriteLine("Користувач: " + d.User);
                        Console.WriteLine("Готово!");
                    }
                    else
                    {
                        Console.WriteLine("Черга пуста, нічого друкувати.");
                    }
                }
                else if (k == "0")
                {
                    break;
                }
            }
        }
    }
}