using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.SeasonRepository;
using dotnet_anime_list.Data.DTOs;
using dotnet_anime_list.Data.Mappers;

namespace dotnet_anime_list.API.Services
{
    public class SeasonService(ISeasonRepository repository)
    {
        private readonly ISeasonRepository _repository = repository;

        public async Task AddSeasons(Guid AnimeId, List<CreateSeasonDTO> seasons, CancellationToken ct)
        {
            foreach (var season in seasons)
            {
                var newSeason = new Season(AnimeMapper.MapSeasonDTO(AnimeId, season));
                await _repository.Create(newSeason, ct);
            }
        }
        public async Task AddSeason(SeasonDTO season, CancellationToken ct)
        {
            var newSeason = new Season(season);
            await _repository.Create(newSeason, ct);
        }
        public async Task<List<Season>> GetSeasons(Guid animeId, CancellationToken ct)
        {
            var result = await _repository.GetSeasons(animeId, ct) ?? throw new Exception("Season not found");
            return result;
        }
        public async Task<int> GetQuantitySeasons(Guid animeId, CancellationToken ct)
        {
            var result = await _repository.GetSeasons(animeId, ct) ?? throw new Exception("Season not found");
            return result.Count;
        }

    }
}
