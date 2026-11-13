namespace DatabaseFirstDrinks.Models;

// Модель таблицы drinks. Может быть пересоздана командой dotnet ef dbcontext scaffold.
public partial class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
}
