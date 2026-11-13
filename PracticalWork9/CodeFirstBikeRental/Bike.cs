public class Bike
{
    public int Id { get; set; }
    public string Model { get; set; } = "";
    public bool IsAvailable { get; set; } = true;
    public List<Rental> Rentals { get; set; } = new();
}
