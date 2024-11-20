using System.ComponentModel.DataAnnotations.Schema;
using dotnet_anime_list.API.Models.Abstracts;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Models
{
    public class AnimeGenres : BaseEntity
    {
        [ForeignKey("Anime")]
        public Guid AnimeId { get; private init; }
        public Guid GenreId { get; private init; }

        public AnimeGenres() : base() { }

        public AnimeGenres(AnimeGenresDTO animeGenres) : base()
        {
            this.AnimeId = animeGenres.AnimeId;
            this.GenreId = animeGenres.GenreId;
        }
    }
}
