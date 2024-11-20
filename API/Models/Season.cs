using dotnet_anime_list.API.Models.Abstracts;
using dotnet_anime_list.Data.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_anime_list.API.Models
{
    public class Season : BaseEntity
    {
        [ForeignKey("Anime")]
        public Guid AnimeId { get; private init; }

        [Required]
        [MaxLength(100)]
        public string SeasonName { get; private set; } = string.Empty;

        [Required]
        public int QuantityEpisodes { get; private set; } 
        public Season() : base() { }

        public Season(SeasonDTO season) : base()
        {
            this.AnimeId = season.AnimeId;
            this.SeasonName = season.SeasonName;
            this.QuantityEpisodes = season.QuantityEpisodes;
        }

        public void Update(UpdateSeasonDTO season)
        {
            this.SeasonName = season.SeasonName;
            this.QuantityEpisodes = season.QuantityEpisodes;
            base.UpdateEnity();
        }
    }
}
