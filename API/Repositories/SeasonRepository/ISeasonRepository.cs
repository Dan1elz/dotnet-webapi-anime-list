using dotnet_anime_list.API.Models;

namespace dotnet_anime_list.API.Repositories.SeasonRepository
{
    public interface ISeasonRepository
    {
        Task Create(Season season, CancellationToken ct);
        Task<List<Season>> GetSeasons(Guid animeId, CancellationToken ct);
    }
}
