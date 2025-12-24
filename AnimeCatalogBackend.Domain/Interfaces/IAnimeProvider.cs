using AnimeCatalogBackend.Domain.Entities;
namespace AnimeCatalogBackend.Domain.Interfaces;

public interface IAnimeProvider
{
    Task<Anime?> GetAnimeByIdAsync(int id);
    Task<IEnumerable<Anime>> SearchAnimeAsync(string query);
}