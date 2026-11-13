public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<Rental> Rentals { get; set; } = new();
}
