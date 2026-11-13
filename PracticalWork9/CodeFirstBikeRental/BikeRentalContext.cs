using Microsoft.EntityFrameworkCore;

public class BikeRentalContext : DbContext
{
    public DbSet<Bike> Bikes => Set<Bike>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Rental> Rentals => Set<Rental>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string connection = Environment.GetEnvironmentVariable("DB_CONNECTION_9_CODEFIRST")
            ?? "Host=localhost;Port=5432;Database=practice9_codefirst;Username=postgres;Password=postgres";
        options.UseNpgsql(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bike>().Property(b => b.Model).IsRequired().HasMaxLength(60);
        modelBuilder.Entity<Customer>().Property(c => c.Name).IsRequired().HasMaxLength(60);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Bike)
            .WithMany(b => b.Rentals)
            .HasForeignKey(r => r.BikeId);

        modelBuilder.Entity<Rental>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Rentals)
            .HasForeignKey(r => r.CustomerId);
    }
}
