namespace dotnet_anime_list.Data.DTOs
{
    public record SeasonDTO(Guid AnimeId, string SeasonName, int QuantityEpisodes);
    public record CreateSeasonDTO(string SeasonName, int QuantityEpisodes);
    public record UpdateSeasonDTO(string SeasonName, int QuantityEpisodes);
}
