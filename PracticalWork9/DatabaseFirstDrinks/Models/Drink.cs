// Этот класс соответствует таблице drinks и может быть пересоздан командой scaffold.
public partial class Drink
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Price { get; set; }
}
