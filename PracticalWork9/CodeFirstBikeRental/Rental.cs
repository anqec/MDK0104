public class Rental
{
    public int Id { get; set; }
    public int BikeId { get; set; }
    public Bike Bike { get; set; } = null!;
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int Days { get; set; }
}
