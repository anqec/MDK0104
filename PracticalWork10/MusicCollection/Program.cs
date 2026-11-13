using Microsoft.EntityFrameworkCore;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Практическая №10 — музыкальная коллекция");

using var db = new MusicContext();
db.Database.Migrate();

var artist = new Artist { Name = "Северный берег" };
var album = new Album { Title = "Прогулка", Year = 2023, Artist = artist };
var indie = new Genre { Name = "Инди" };
var acoustic = new Genre { Name = "Акустика" };

db.AlbumGenres.Add(new AlbumGenre { Album = album, Genre = indie });
db.AlbumGenres.Add(new AlbumGenre { Album = album, Genre = acoustic });
db.SaveChanges();
Console.WriteLine("Добавлены исполнитель, альбом и два жанра.");

var saved = db.Albums
    .Include(a => a.Artist)
    .Include(a => a.AlbumGenres)
    .ThenInclude(x => x.Genre)
    .First(a => a.Id == album.Id);
Console.WriteLine($"Альбом: {saved.Title} — {saved.Artist.Name} ({saved.Year})");
Console.WriteLine("Жанры: " + string.Join(", ", saved.AlbumGenres.Select(x => x.Genre.Name)));

saved.Year = 2024;
db.SaveChanges();
Console.WriteLine($"Год выпуска исправлен: {saved.Year}");

db.AlbumGenres.Remove(saved.AlbumGenres.First(x => x.Genre.Name == "Акустика"));
db.SaveChanges();
Console.WriteLine("У альбома удалён жанр «Акустика».");
