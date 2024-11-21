using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.GenreRepository;
using dotnet_anime_list.Data.DTOs;
using dotnet_anime_list.Data.Mappers;

namespace dotnet_anime_list.API.Services
{
    public class GenreService(IGenreRepository repository)
    {
        private readonly IGenreRepository _repository = repository;
        
        public async Task Create(GenreDTO genre, CancellationToken ct)
        {
            var newGenre = new Genre(genre);
            await _repository.Create(newGenre, ct);
        }
        public async Task Delete(Guid Id, CancellationToken ct)
        {
            Genre? genre = await _repository.GetGenre(Id, ct) ?? throw new Exception("Genre not found");
            await _repository.Delete(genre, ct);
        }
        public async Task Update(Guid Id, GenreDTO genre, CancellationToken ct)
        {
            Genre? genreToUpdate = await _repository.GetGenre(Id, ct) ?? throw new Exception("Genre not found");
            await _repository.Update(genreToUpdate, genre, ct);
        }
        public async Task<List<Genre>> GetAllGenres(CancellationToken ct)
        {
            return await _repository.GetAllGenres(ct);
        }
        public async Task<List<Genre>> GetAllAnimeGenres(Guid AnimeId, CancellationToken ct)
        {
            return await _repository.GetAnimeGenresInGenre(AnimeId, ct);
        }
        public async Task AddAnimeGenre(List<Guid> Genres, Guid AnimeId, CancellationToken ct)
        {
            var animeGenres = await _repository.GetAimeGenres(AnimeId, ct);
            await _repository.RemovelAllAnimeGenre(animeGenres, ct);

            foreach (var genreId in Genres)
            {
                Genre? genre = await _repository.GetGenre(genreId, ct);
                if (genre != null)
                {
                    AnimeGenres? animeGenre = new AnimeGenres(AnimeMapper.MapAnimeGenresDTO(genre.Id, AnimeId));
                    await _repository.AddAnimeGenre(animeGenre, ct);
                }
            }
        }
    }
}
