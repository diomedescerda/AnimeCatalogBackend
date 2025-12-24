using System.Net.Http.Json;
using AnimeCatalogBackend.Domain.Entities;
using AnimeCatalogBackend.Domain.Interfaces;

namespace AnimeCatalogBackend.Infrastructure.ExternalApis;

public class JikanAnimeProvider : IAnimeProvider
{
   private readonly HttpClient _httpClient;

   public JikanAnimeProvider(HttpClient httpClient)
   {
      _httpClient = httpClient;
   }

   public async Task<Anime?> GetAnimeByIdAsync(int id)
   {
      var response = await _httpClient.GetFromJsonAsync<JikanResponse>($"https://api.jikan.moe/v4/anime/{id}");
      return response?.Data != null ? Map(response.Data) : null;
}

   public async Task<IEnumerable<Anime>> SearchAnimeAsync(string query)
   {
      var response =
         await _httpClient.GetFromJsonAsync<JikanSearchResponse>($"https://api.jikan.moe/v4/anime?q={query}");
         return response?.Data?.Select(Map) ?? Enumerable.Empty<Anime>();
   }

   private Anime Map(JikanAnimeDto data)
   {
      return new Anime
      {
         Id = data.MalId,
         Title = data.Title,
         Sinopsis = data.Sinopsis,
         Genres = data.Genres.Select(g => g.Name).ToList(),
         ImageUrl = data.Images?.Jpg?.ImageUrl
      };
   }
}

public class JikanResponse { public JikanAnimeDto? Data { get; set; } }

public class JikanSearchResponse { public List<JikanAnimeDto>? Data { get; set; } }

public class JikanAnimeDto
{
   public int MalId { get; set; }
   public string? Title { get; set; }
   public string? Sinopsis { get; set; }
   public List<JikanGenreDto>? Genres { get; set; }
   public JikanImageSetDto Images { get; set; }
}

public class JikanGenreDto { public string? Name { get; set; } }

public class JikanImageSetDto { public JikanImageDto Jpg { get; set; } }

public class JikanImageDto { public string? ImageUrl { get; set; } }