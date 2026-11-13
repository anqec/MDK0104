using Microsoft.EntityFrameworkCore;

public class MusicContext : DbContext
{
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<AlbumGenre> AlbumGenres => Set<AlbumGenre>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string connection = Environment.GetEnvironmentVariable("DB_CONNECTION_10")
            ?? "Host=localhost;Port=5432;Database=practice10_music;Username=postgres;Password=postgres";
        options.UseNpgsql(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().ToTable("artists");
        modelBuilder.Entity<Artist>().HasKey(a => a.Id);
        modelBuilder.Entity<Artist>().Property(a => a.Name)
            .HasColumnName("name").IsRequired().HasMaxLength(60);

        modelBuilder.Entity<Album>().ToTable("albums");
        modelBuilder.Entity<Album>().HasKey(a => a.Id);
        modelBuilder.Entity<Album>().Property(a => a.Title)
            .HasColumnName("title").IsRequired().HasMaxLength(80);
        modelBuilder.Entity<Album>().Property(a => a.Year).HasColumnName("release_year");
        modelBuilder.Entity<Album>().Property(a => a.ArtistId).HasColumnName("artist_id");
        modelBuilder.Entity<Album>()
            .HasOne(a => a.Artist)
            .WithMany(a => a.Albums)
            .HasForeignKey(a => a.ArtistId);

        modelBuilder.Entity<Genre>().ToTable("genres");
        modelBuilder.Entity<Genre>().HasKey(g => g.Id);
        modelBuilder.Entity<Genre>().Property(g => g.Name)
            .HasColumnName("name").IsRequired().HasMaxLength(40);

        modelBuilder.Entity<AlbumGenre>().ToTable("album_genres");
        modelBuilder.Entity<AlbumGenre>().HasKey(x => new { x.AlbumId, x.GenreId });
        modelBuilder.Entity<AlbumGenre>().Property(x => x.AlbumId).HasColumnName("album_id");
        modelBuilder.Entity<AlbumGenre>().Property(x => x.GenreId).HasColumnName("genre_id");
        modelBuilder.Entity<AlbumGenre>()
            .HasOne(x => x.Album)
            .WithMany(a => a.AlbumGenres)
            .HasForeignKey(x => x.AlbumId);
        modelBuilder.Entity<AlbumGenre>()
            .HasOne(x => x.Genre)
            .WithMany(g => g.AlbumGenres)
            .HasForeignKey(x => x.GenreId);
    }
}
