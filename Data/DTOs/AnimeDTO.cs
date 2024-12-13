using dotnet_anime_list.API.Models;

namespace dotnet_anime_list.Data.DTOs
{
    public record AnimeDTO(Guid UserId, string Title, string? AlternativeTitle, int Year, string Image, string Description, string Lenguage, float Rating, bool FavoriteState, bool WatchedState);
   public record UpdateFavoriteDTO(bool FavoriteState);
    public record UpdateWatchedDTO(bool WatchedState);
    public record AnimeGenresDTO(Guid AnimeId, Guid GenreId);

    public record GetCategoryDTO(
        string? search,
        List<Guid>? genres,
        bool? favorite,
        bool? watched,
        int? rating
    );

    public record GenreDTO(string Name);

    public record CreateAnimeDTO(
        string Title,
        string? AlternativeTitle,
        int Year,
        IFormFile Image,
        string Description,
        string Lenguage,
        float Rating,
        bool FavoriteState,
        bool WatchedState,
        List<Guid>? Genres
    );
    public record UpdateAnimeDTO(
        string Title,
        string? AlternativeTitle,
        int Year,
        IFormFile Image,
        string Description,
        string Lenguage,
        float Rating,
        bool FavoriteState,
        bool WatchedState,
        List<Guid>? Genres
    );


    public record GetAnimesDTO(
        Anime Anime,
        int Seasons,
        string AnimeUrl
    );
    public record GetAnimeDTO(
        AnimeDTO Anime,
        List<GenreDTO> Genres,
        List<Season> Seasons,
        string hostUrl
    );
}
