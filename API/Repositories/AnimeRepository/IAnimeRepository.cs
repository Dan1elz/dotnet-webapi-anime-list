using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Repositories.AnimeRepository
{
    public interface IAnimeRepository
    {
        Task Create(Anime anime, CancellationToken ct);
        Task<List<Anime>> GetAnimes(Guid userId, CancellationToken ct);
        Task<Anime?> GetAnime(Guid animeId, CancellationToken ct);
        
    }
}
