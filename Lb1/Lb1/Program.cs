using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Text.Json;

// Клас "Запис"
public class Record
{
    public string FullName { get; set; }
    public string HomePhone { get; set; }
    public string WorkPhone { get; set; }
    public string MobilePhone { get; set; }
    public DateTime Birthday { get; set; }

    public Record() { } // обов’язковий для XmlSerializer

    public override string ToString() =>
        $"{FullName} | ДР: {Birthday:dd.MM} | Телефони: д. {HomePhone}, р. {WorkPhone}, м. {MobilePhone}";
}

class Program
{
    static void Main()
    {
        // Приклад даних
        var records = new Record[]
        {
            new Record { FullName = "Іваненко Іван Іванович", HomePhone = "380-55-111", WorkPhone = "380-55-222", MobilePhone = "380-67-111-11-11", Birthday = new DateTime(1999, 4, 15) },
            new Record { FullName = "Петренко Олена Петрівна", HomePhone = "380-55-333", WorkPhone = "380-55-444", MobilePhone = "380-66-222-22-22", Birthday = new DateTime(2001, 7, 3) },
            new Record { FullName = "Сидоренко Андрій Степанович", HomePhone = "380-55-555", WorkPhone = "380-55-666", MobilePhone = "380-63-333-33-33", Birthday = new DateTime(1995, 5, 20) },
            new Record { FullName = "Мельник Марія Дмитрівна", HomePhone = "380-55-777", WorkPhone = "380-55-888", MobilePhone = "380-68-444-44-44", Birthday = new DateTime(2000, 12, 1) },
            new Record { FullName = "Коваль Віктор Ігорович", HomePhone = "380-55-999", WorkPhone = "380-55-000", MobilePhone = "380-93-555-55-55", Birthday = new DateTime(1998, 3, 10) }
        };

        Console.WriteLine("=== Оригінальні записи ===");
        PrintRecords(records);

        // === СЕРІАЛІЗАЦІЯ ТА ДЕСЕРІАЛІЗАЦІЯ ===
        XmlSerialize(records);
        JsonSerialize(records);

        var xmlRecords = XmlDeserialize();
        var jsonRecords = JsonDeserialize();

        Console.WriteLine("\n=== Після десеріалізації (перевірка) ===");
        PrintRecords(jsonRecords);

        // === ВВЕДЕННЯ ДІАПАЗОНУ ===
        Console.Write("\nВведіть початкову дату (дд.мм): ");
        if (!TryParseDayMonth(Console.ReadLine(), out DateTime start))
        {
            Console.WriteLine("❌ Невірний формат! Приклад: 01.03");
            return;
        }

        Console.Write("Введіть кінцеву дату (дд.мм): ");
        if (!TryParseDayMonth(Console.ReadLine(), out DateTime end))
        {
            Console.WriteLine("❌ Невірний формат! Приклад: 30.06");
            return;
        }

        // === ФІЛЬТРАЦІЯ ===
        var filtered = records.Where(r =>
        {
            var bDay = new DateTime(2000, r.Birthday.Month, r.Birthday.Day);
            var s = new DateTime(2000, start.Month, start.Day);
            var e = new DateTime(2000, end.Month, end.Day);

            if (s <= e)
                return bDay >= s && bDay <= e;
            else // діапазон через рік (наприклад: 30.12 – 15.01)
                return bDay >= s || bDay <= e;
        }).ToArray();

        // === ВИВЕДЕННЯ РЕЗУЛЬТАТУ ===
        Console.WriteLine($"\n=== Люди з ДР у діапазоні {start:dd.MM} – {end:dd.MM} ===");
        if (filtered.Length == 0)
        {
            Console.WriteLine("Немає записів у цьому діапазоні.");
        }
        else
        {
            PrintRecords(filtered);
            File.WriteAllLines("birthday_filtered.txt", filtered.Select(r => r.ToString()));
            Console.WriteLine("\n✅ Результат збережено у файл: birthday_filtered.txt");
        }

        Console.ReadKey();
    }

    static void PrintRecords(Record[] records)
    {
        foreach (var r in records)
            Console.WriteLine("  " + r);
    }

    static bool TryParseDayMonth(string input, out DateTime result)
    {
        result = DateTime.MinValue;
        return DateTime.TryParseExact(input, "dd.MM", null, System.Globalization.DateTimeStyles.None, out result);
    }

    // === XML ===
    static void XmlSerialize(Record[] records)
    {
        var serializer = new XmlSerializer(typeof(Record[]));
        using var fs = new FileStream("records.xml", FileMode.Create);
        serializer.Serialize(fs, records);
        Console.WriteLine("\n✅ XML серіалізовано у records.xml");
    }

    static Record[] XmlDeserialize()
    {
        var serializer = new XmlSerializer(typeof(Record[]));
        using var fs = new FileStream("records.xml", FileMode.Open);
        return (Record[])serializer.Deserialize(fs);
    }

    // === JSON ===
    static void JsonSerialize(Record[] records)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(records, options);
        File.WriteAllText("records.json", json);
        Console.WriteLine("\n✅ JSON серіалізовано у records.json");
    }

    static Record[] JsonDeserialize()
    {
        string json = File.ReadAllText("records.json");
        return JsonSerializer.Deserialize<Record[]>(json);
    }
}