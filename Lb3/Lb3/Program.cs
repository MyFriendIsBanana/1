using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Company
{
    public string Name { get; set; }
    public DateTime FoundationDate { get; set; }
    public string BusinessProfile { get; set; }
    public string DirectorName { get; set; }
    public int EmployeesCount { get; set; }
    public string Address { get; set; }

    public override string ToString() =>
        $"Фірма: {Name}, Профіль: {BusinessProfile}, Директор: {DirectorName}, " +
        $"Співробітників: {EmployeesCount}, Адреса: {Address}, Заснована: {FoundationDate:yyyy-MM-dd}";
}

class Program
{
    static void Main()
    {
        var companies = new List<Company>
        {
            new Company {
                Name = "FoodCorp",
                FoundationDate = new DateTime(2020, 5, 15),
                BusinessProfile = "Food",
                DirectorName = "White John",
                EmployeesCount = 250,
                Address = "London"
            },
            new Company {
                Name = "TechGlobal",
                FoundationDate = new DateTime(2018, 3, 10),
                BusinessProfile = "IT",
                DirectorName = "Smith Alex",
                EmployeesCount = 120,
                Address = "Kyiv"
            },
            new Company {
                Name = "MarketPro",
                FoundationDate = new DateTime(2019, 11, 22),
                BusinessProfile = "маркетинг",
                DirectorName = "Black Emma",
                EmployeesCount = 80,
                Address = "London"
            },
            new Company {
                Name = "White&Co",
                FoundationDate = new DateTime(2021, 1, 30),
                BusinessProfile = "маркетинг",
                DirectorName = "Black Robert",
                EmployeesCount = 45,
                Address = "Paris"
            },
            new Company {
                Name = "SoftDev",
                FoundationDate = new DateTime(2022, 7, 12),
                BusinessProfile = "IT",
                DirectorName = "White Anna",
                EmployeesCount = 320,
                Address = "Berlin"
            }
        };

        var results = new List<string>();
        results.Add("=== РЕЗУЛЬТАТИ LINQ ЗАПИТІВ ===\n");

        // 1. Усі фірми
        var all = companies;
        results.Add("1. Усі фірми:");
        all.ToList().ForEach(c => results.Add($"   {c}"));

        // 2. Фірми з назвою "Food"
        var foodByName = companies.Where(c => c.Name == "FoodCorp");
        results.Add("\n2. Фірми з назвою 'FoodCorp':");
        foodByName.ToList().ForEach(c => results.Add($"   {c}"));

        // 3. Фірми у галузі маркетингу
        var marketing = from c in companies
                        where c.BusinessProfile == "маркетинг"
                        select c;
        results.Add("\n3. Фірми у галузі маркетингу:");
        marketing.ToList().ForEach(c => results.Add($"   {c}"));

        // 4. Фірми у галузі маркетингу або IT
        var marketingOrIT = companies.Where(c =>
            c.BusinessProfile == "маркетинг" || c.BusinessProfile == "IT");
        results.Add("\n4. Фірми у галузі маркетингу або IT:");
        marketingOrIT.ToList().ForEach(c => results.Add($"   {c}"));

        // 5. Фірми з кількістю співробітників > 100
        var bigCompanies = from c in companies
                           where c.EmployeesCount > 100
                           select c;
        results.Add("\n5. Фірми з кількістю співробітників > 100:");
        bigCompanies.ToList().ForEach(c => results.Add($"   {c}"));

        // 6. Фірми з кількістю співробітників від 100 до 300
        var midCompanies = companies.Where(c => c.EmployeesCount >= 100 && c.EmployeesCount <= 300);
        results.Add("\n6. Фірми з кількістю співробітників від 100 до 300:");
        midCompanies.ToList().ForEach(c => results.Add($"   {c}"));

        // 7. Фірми у Лондоні
        var london = from c in companies
                     where c.Address == "London"
                     select c;
        results.Add("\n7. Фірми у Лондоні:");
        london.ToList().ForEach(c => results.Add($"   {c}"));

        // 8. Фірми з прізвищем директора "White"
        var whiteDirectors = companies.Where(c => c.DirectorName.StartsWith("White"));
        results.Add("\n8. Фірми, де директор має прізвище 'White':");
        whiteDirectors.ToList().ForEach(c => results.Add($"   {c}"));

        // 9. Фірми, засновані понад 2 роки тому (на момент 2025)
        var twoYearsAgo = DateTime.Now.AddYears(-2);
        var oldFirms = from c in companies
                       where c.FoundationDate < twoYearsAgo
                       select c;
        results.Add("\n9. Фірми, засновані понад 2 роки тому:");
        oldFirms.ToList().ForEach(c => results.Add($"   {c}"));

        // 10. Фірми з директором Black та назвою, що містить "White"
        var blackAndWhite = companies.Where(c =>
            c.DirectorName.StartsWith("Black") && c.Name.Contains("White"));
        results.Add("\n10. Фірми з директором 'Black' і назвою, що містить 'White':");
        blackAndWhite.ToList().ForEach(c => results.Add($"   {c}"));

        // Виведення на екран
        foreach (var line in results)
        {
            Console.WriteLine(line);
        }

        // Збереження у файл
        File.WriteAllLines("linq_results.txt", results);
        Console.WriteLine("\n✅ Результати збережено у файл: linq_results.txt");
        Console.ReadKey();
    }
}