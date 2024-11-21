using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Repositories.SeasonRepository
{
    public interface ISeasonRepository
    {
        Task Create(Season season, CancellationToken ct);
        Task Delete(Season season, CancellationToken ct);
        Task Update(Season season, UpdateSeasonDTO update, CancellationToken ct);
        Task<Season?> GetSeason(Guid Id, CancellationToken ct);
        Task<List<Season>> GetSeasons(Guid animeId, CancellationToken ct);
        
    }
}
