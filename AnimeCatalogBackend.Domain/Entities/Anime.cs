namespace AnimeCatalogBackend.Domain.Entities;

public class Anime
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Sinopsis { get; set; }
    public List<String> Genres { get; set; }
    public string ImageUrl { get; set; }
}