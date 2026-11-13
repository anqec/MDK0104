using DatabaseFirstDrinks.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirstDrinks;

// Контекст создан по схеме таблицы drinks. Команда scaffold заменит его автоматически.
public partial class DrinksContext : DbContext
{
    public DrinksContext(DbContextOptions<DrinksContext> options) : base(options)
    {
    }

    public virtual DbSet<Drink> Drinks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("drinks_pkey");
            entity.ToTable("drinks");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });
    }
}
