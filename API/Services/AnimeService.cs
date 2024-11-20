using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.AnimeRepository;
using dotnet_anime_list.Data.DTOs;
using dotnet_anime_list.Data.Mappers;
using Newtonsoft.Json;


namespace dotnet_anime_list.API.Services
{
    public class AnimeService(IAnimeRepository repository, UtilsService utilsService, GenreService genreService, SeasonService seasonService)
    {
        private readonly IAnimeRepository _repository = repository;
        private readonly UtilsService _utilsService = utilsService;
        private readonly GenreService _genreService = genreService;
        private readonly SeasonService _seasonService = seasonService;

        public async Task Create(CreateAnimeDTO anime, Token token, CancellationToken ct)
        {
            var filePath = "Uploads/Images/Animes";
            var image = await _utilsService.SaveImg(anime.Image, filePath, ct);

            var newAnime = new Anime(AnimeMapper.MapAnimeDTO(token.UserId, anime, image));
            await _repository.Create(newAnime, ct);

            if (anime.Genres != null)
                await _genreService.AddAnimeGenre(anime.Genres, newAnime.Id, ct);

        }

        public async Task<List<GetAnimesDTO>> GetAnimes(Token token, CancellationToken ct)
        {
            List<Anime> animes = await _repository.GetAnimes(token.UserId, ct);
            var animeDTOs = new List<GetAnimesDTO>();

            foreach (var anime in animes)
            {
                var quantitySeasons = await _seasonService.GetQuantitySeasons(anime.Id, ct);
                quantitySeasons = quantitySeasons == 0 ? 0 : quantitySeasons;
                var animeDTO = AnimeMapper.MapToGetAnimesDTO(anime, quantitySeasons);
                animeDTOs.Add(animeDTO);
            }

            return animeDTOs;
        }
        public async Task<GetAnimeDTO> GetAnime(Guid animeId, CancellationToken ct)
        {
            var anime = await _repository.GetAnime(animeId, ct) ?? throw new Exception("Anime not found");
            List<Season> seasons = await _seasonService.GetSeasons(anime.Id, ct) ?? [];
            List<Genre> genres = await _genreService.GetGenres(anime.Id, ct) ?? [];
            
            var genreDTOs = genres.Select(genre => new GenreDTO(genre.Name)).ToList();
            var animeDTO = AnimeMapper.MapToGetAnimeDTO(anime, genreDTOs, seasons);
            return animeDTO;
        }
    }
}
