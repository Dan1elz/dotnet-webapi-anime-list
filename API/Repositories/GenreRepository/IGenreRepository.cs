using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Repositories.GenreRepository
{
    public interface IGenreRepository
    {
        Task Create(Genre genre, CancellationToken ct);
        Task Delete(Genre genre, CancellationToken ct);
        Task Update(Genre genre, GenreDTO genreDTO, CancellationToken ct);
        Task<Genre?> GetGenre(Guid Id, CancellationToken ct);
        Task<List<Genre>> GetAllGenres(CancellationToken ct);
        Task AddAnimeGenre(AnimeGenres animeGenres, CancellationToken ct);
        Task RemovelAllAnimeGenre(List<AnimeGenres> animeGenres, CancellationToken ct);
        Task<List<Genre>> GetAnimeGenresInGenre(Guid AnimeId, CancellationToken ct);
        Task<List<AnimeGenres>> GetAimeGenres(Guid AnimeId, CancellationToken ct);
    }
}
