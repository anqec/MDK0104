using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Практическая №9 — Code-First: прокат велосипедов");

using var db = new BikeRentalContext();
db.Database.Migrate();

var bike = new Bike { Model = "Складной велосипед" };
var customer = new Customer { Name = "Аня" };
var rental = new Rental { Bike = bike, Customer = customer, Days = 2 };

db.Rentals.Add(rental);
db.SaveChanges();
Console.WriteLine("Аренда добавлена.");

var found = db.Rentals
    .Include(r => r.Bike)
    .Include(r => r.Customer)
    .First(r => r.Id == rental.Id);
Console.WriteLine($"Просмотр: {found.Customer.Name}, {found.Bike.Model}, {found.Days} дня.");

found.Days = 3;
found.Bike.IsAvailable = false;
db.SaveChanges();
Console.WriteLine($"Изменение: аренда на {found.Days} дня, велосипед занят.");

db.Rentals.Remove(found);
found.Bike.IsAvailable = true;
db.SaveChanges();
Console.WriteLine("Удаление: аренда удалена, велосипед снова свободен.");
