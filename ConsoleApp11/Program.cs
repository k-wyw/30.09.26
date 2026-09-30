using System;
using System.Collections.Generic;
using System.Linq;

namespace _30._09._26
{
    // Класс для задания — вынесен из Program
    public class User
    {
        public string FullName { get; set; }
        public string Login { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> Hobbies { get; set; }

        public User(string fullName, string login, DateTime dateOfBirth, List<string> hobbies)
        {
            FullName = fullName;
            Login = login;
            DateOfBirth = dateOfBirth;
            Hobbies = hobbies;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var users = new List<User>
            {
                new User("Иванов Иван Иванович", "ivanovii", new DateTime(1990, 5, 18), new List<string> { "Чтение", "Путешествия", "Программирование" }),
                new User("Петров Петр Петрович", "petrovpp", new DateTime(1985, 10, 2), new List<string> { "Футбол", "Кулинария" }),
                new User("Сидорова Анна Михайловна", "sidorovana", new DateTime(2000, 3, 15), new List<string> { "Рисование", "Программирование" }),
                new User("Смирнов Алексей Николаевич", "smirnovan", new DateTime(1995, 7, 30), new List<string> { "Хоккей", "Фотография" }),
                new User("Кузнецова Ольга Валерьевна", "kuznetsovao", new DateTime(1992, 12, 12), new List<string> { "Йога", "Путешествия" })
            };

            Console.WriteLine("Where – выбираем всех старше 30 лет");
            var olderThan30 = users.Where(p => (DateTime.Now - p.DateOfBirth).TotalDays / 365 > 30).ToList();
            Console.WriteLine("Люди старше 30:");
            foreach (var p in olderThan30)
            {
                int age = (int)((DateTime.Now - p.DateOfBirth).TotalDays / 365);
                Console.WriteLine($"{p.FullName}, {age} лет");
            }
            Console.WriteLine(" Логины в алфавитном порядке ");
            var sortedLogins = users
                .OrderBy(p => p.Login)
                .Select(p => p.Login)
                .ToList();

            foreach (var login in sortedLogins)
                Console.WriteLine(login);
            Console.WriteLine("\n Пользователи с хобби \"Программирование\" ");
            var programmers = users
                .Where(p => p.Hobbies.Contains("Программирование"))
                .ToList();
            foreach (var p in programmers)
                Console.WriteLine(p.FullName);

            Console.WriteLine("\n Группировка по году рождения ");
            var byYear = users
                .GroupBy(p => p.DateOfBirth.Year)
                .Select(g => new { Year = g.Key, Count = g.Count() })
                .ToList();
            foreach (var item in byYear)
                Console.WriteLine($"{item.Year} - {item.Count}");

            Console.WriteLine("\n Словарь login  FullName ");
            Dictionary<string, string> loginToName = users
                .ToDictionary(p => p.Login, p => p.FullName);
            foreach (var kvp in loginToName)
                Console.WriteLine($"{kvp.Key}  {kvp.Value}");

            Console.WriteLine("\n Самый молодой и самый старый ");
            var youngest = users.OrderByDescending(p => p.DateOfBirth).First();
            var oldest = users.OrderBy(p => p.DateOfBirth).First();

            int youngestAge = (int)((DateTime.Now - youngest.DateOfBirth).TotalDays / 365);
            int oldestAge = (int)((DateTime.Now - oldest.DateOfBirth).TotalDays / 365);

            Console.WriteLine($"Самый молодой: {youngest.FullName}, дата рождения: {youngest.DateOfBirth.ToShortDateString()}, возраст: {youngestAge}");
            Console.WriteLine($"Самый старый: {oldest.FullName}, дата рождения: {oldest.DateOfBirth.ToShortDateString()}, возраст: {oldestAge}");

            Console.WriteLine("\n Уникальные хобби ");
            var uniqueHobbies = users
                .SelectMany(p => p.Hobbies)
                .Distinct()
                .ToList();
            foreach (var hobby in uniqueHobbies)
                Console.WriteLine(hobby);

            Console.WriteLine("\n Количество с хобби \"Путешествия\" ");
            int travelCount = users.Count(p => p.Hobbies.Contains("Путешествия"));
            Console.WriteLine($"Пользователей с хобби \"Путешествия\": {travelCount}");

            Console.WriteLine("\n ФИО начинается на \"С\" ");
            var startsWithS = users
                .Where(p => p.FullName.StartsWith("С"))
                .ToList();
            foreach (var p in startsWithS)
                Console.WriteLine(p.FullName);

            Console.WriteLine("\n Два пользователя с наибольшим числом хобби ");
            var topTwo = users
                .OrderByDescending(p => p.Hobbies.Count)
                .ThenBy(p => p.Login)
                .Take(2)
                .ToList();
            foreach (var p in topTwo)
                Console.WriteLine($"{p.FullName}, хобби: {p.Hobbies.Count}, логин: {p.Login}");
        }
    }
}