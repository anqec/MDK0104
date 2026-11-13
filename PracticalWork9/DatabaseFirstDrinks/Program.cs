using DatabaseFirstDrinks;
using DatabaseFirstDrinks.Models;
using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Практическая №9 — Database-First: каталог напитков");

string connection = Environment.GetEnvironmentVariable("DB_CONNECTION_9_DATABASEFIRST")
    ?? "Host=localhost;Port=5432;Database=practice9_drinks;Username=postgres;Password=postgres";
var options = new DbContextOptionsBuilder<DrinksContext>().UseNpgsql(connection).Options;
using var db = new DrinksContext(options);

// Убираем только демонстрационные записи от прошлого запуска.
var old = db.Drinks.Where(d => d.Name == "Морс (демо)" || d.Name == "Лимонад (демо)");
db.Drinks.RemoveRange(old);
db.SaveChanges();

var mors = new Drink { Name = "Морс (демо)", Price = 90 };
var lemonade = new Drink { Name = "Лимонад (демо)", Price = 110 };
db.Drinks.AddRange(mors, lemonade);
db.SaveChanges();
Console.WriteLine("Добавили два напитка.");

foreach (var drink in db.Drinks.OrderBy(d => d.Id))
{
    Console.WriteLine($"{drink.Id}: {drink.Name} — {drink.Price} руб.");
}

mors.Price = 100;
db.SaveChanges();
Console.WriteLine($"Изменили цену морса: {mors.Price} руб.");

db.Drinks.Remove(lemonade);
db.SaveChanges();
Console.WriteLine("Удалили лимонад.");
