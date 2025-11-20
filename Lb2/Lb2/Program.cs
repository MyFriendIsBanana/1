using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

[Serializable]
public class Student
{
    public string FullName { get; set; }
    public string GroupNumber { get; set; }
    public int[] Grades { get; set; }

    public Student() { }

    public Student(string fullName, string groupNumber, int[] grades)
    {
        FullName = fullName;
        GroupNumber = groupNumber;
        Grades = grades;
    }

    public double AverageGrade => Grades.Length > 0 ? Grades.Average() : 0;
}

class Program
{
    static List<Student> students = new List<Student>();

    static void Main()
    {
        // Ініціалізація прикладових даних
        students.AddRange(new[]
        {
            new Student("Іваненко Іван Іванович", "КН-21", new int[] { 9, 8, 9, 10, 8 }),
            new Student("Петренко Петро Петрович", "КН-21", new int[] { 7, 6, 8, 7, 7 }),
            new Student("Сидоренко Олена Дмитрівна", "КН-22", new int[] { 10, 9, 9, 10, 10 })
        });

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== ЛАБОРАТОРНА РОБОТА №2: Колекції ===");
            Console.WriteLine("1. Додати студента");
            Console.WriteLine("2. Видалити студента (за індексом)");
            Console.WriteLine("3. Редагувати студента");
            Console.WriteLine("4. Показати всіх студентів");
            Console.WriteLine("5. Показати студентів з середнім ≥ 8 (і зберегти у файл)");
            Console.WriteLine("0. Вийти");
            Console.Write("Виберіть дію: ");

            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": EditStudent(); break;
                case "4": ShowAllStudents(); break;
                case "5": ShowTopStudents(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір. Натисніть Enter..."); Console.ReadKey(); break;
            }
        }
    }

    static void AddStudent()
    {
        Console.Write("ПІБ: ");
        string name = Console.ReadLine();
        Console.Write("Група: ");
        string group = Console.ReadLine();
        Console.Write("Оцінки (5 чисел через пробіл): ");
        var gradesInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (gradesInput.Length != 5)
        {
            Console.WriteLine("Помилка: потрібно рівно 5 оцінок.");
            Console.ReadKey();
            return;
        }

        if (int.TryParse(gradesInput[0], out int g1) &&
            int.TryParse(gradesInput[1], out int g2) &&
            int.TryParse(gradesInput[2], out int g3) &&
            int.TryParse(gradesInput[3], out int g4) &&
            int.TryParse(gradesInput[4], out int g5))
        {
            students.Add(new Student(name, group, new int[] { g1, g2, g3, g4, g5 }));
            Console.WriteLine("✅ Студента додано.");
        }
        else
        {
            Console.WriteLine("❌ Невірний формат оцінок.");
        }
        Console.ReadKey();
    }

    static void RemoveStudent()
    {
        ShowAllStudents();
        Console.Write("Введіть індекс студента для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < students.Count)
        {
            students.RemoveAt(index);
            Console.WriteLine("✅ Студента видалено.");
        }
        else
        {
            Console.WriteLine("❌ Невірний індекс.");
        }
        Console.ReadKey();
    }

    static void EditStudent()
    {
        ShowAllStudents();
        Console.Write("Введіть індекс студента для редагування: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < students.Count)
        {
            var s = students[index];
            Console.WriteLine($"Редагування: {s.FullName}");
            Console.Write($"Нове ПІБ (було: {s.FullName}): ");
            string newName = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(newName)) s.FullName = newName;

            Console.Write($"Нова група (була: {s.GroupNumber}): ");
            string newGroup = Console.ReadLine().Trim();
            if (!string.IsNullOrEmpty(newGroup)) s.GroupNumber = newGroup;

            Console.Write($"Нові оцінки (5 чисел, було: [{string.Join(", ", s.Grades)}]): ");
            var gradesInput = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (gradesInput.Length == 5 &&
                gradesInput.All(g => int.TryParse(g, out _)))
            {
                s.Grades = gradesInput.Select(int.Parse).ToArray();
            }
            Console.WriteLine("✅ Дані оновлено.");
        }
        else
        {
            Console.WriteLine("❌ Невірний індекс.");
        }
        Console.ReadKey();
    }

    static void ShowAllStudents()
    {
        Console.WriteLine("СПИСОК СТУДЕНТІВ:");
        for (int i = 0; i < students.Count; i++)
        {
            var s = students[i];
            Console.WriteLine($"{i}) {s.FullName} ({s.GroupNumber}) — Середній: {s.AverageGrade:F1}, Оцінки: [{string.Join(", ", s.Grades)}]");
        }
        Console.WriteLine("Натисніть Enter...");
        Console.ReadKey();
    }

    static void ShowTopStudents()
    {
        var top = students.Where(s => s.AverageGrade >= 8).ToList();
        Console.WriteLine("СТУДЕНТИ З СЕРЕДНІМ ≥ 8:");
        foreach (var s in top)
        {
            Console.WriteLine($"  {s.FullName} ({s.GroupNumber}) — Середній: {s.AverageGrade:F1}");
        }

        // Збереження у файл
        string output = string.Join("\n", top.Select(s => $"{s.FullName} ({s.GroupNumber}) — Середній: {s.AverageGrade:F1}"));
        File.WriteAllText("top_students.txt", output);
        Console.WriteLine("\n✅ Результат збережено у файл: top_students.txt");
        Console.ReadKey();
    }
}