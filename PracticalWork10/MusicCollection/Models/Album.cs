public class Album
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int Year { get; set; }
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = null!;
    public List<AlbumGenre> AlbumGenres { get; set; } = new();
}
