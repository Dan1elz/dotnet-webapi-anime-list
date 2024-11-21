using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.Data.Mappers
{
    public class AnimeMapper
    {
        public static AnimeDTO MapAnimeDTO(Guid Id, CreateAnimeDTO anime, string FileName)
        {
            return new AnimeDTO(Id, anime.Title, anime.AlternativeTitle, anime.Year, FileName, anime.Description, anime.Lenguage, anime.Rating, anime.FavoriteState, anime.WatchedState);
        }
        public static SeasonDTO MapSeasonDTO(Guid AnimeId, CreateSeasonDTO season)
        {
            return new SeasonDTO(AnimeId, season.SeasonName, season.QuantityEpisodes);
        }
        public static AnimeGenresDTO MapAnimeGenresDTO(Guid AnimeId, Guid GenreId)
        {
            return new AnimeGenresDTO(AnimeId, GenreId);
        }
        public static GetAnimesDTO MapToGetAnimesDTO(Anime anime, int Seasons, string animeUrl)
        {
            return new GetAnimesDTO(anime, Seasons, animeUrl);
        }
        public static GetAnimeDTO MapToGetAnimeDTO(Anime anime, List<GenreDTO> genres, List<Season> seasons, string hostUrl)
        {
            return new GetAnimeDTO(new AnimeDTO(anime.UserId, anime.Title, anime.AlternativeTitle, anime.Year, anime.Image, anime.Description, anime.Lenguage, anime.Rating, anime.FavoriteState, anime.WatchedState), genres, seasons, hostUrl);
        }
    }
}
