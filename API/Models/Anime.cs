using dotnet_anime_list.Data.DTOs;
using dotnet_anime_list.API.Models.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_anime_list.API.Models
{
    public class Anime : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; private init; }

        [Required]
        [MaxLength(100)]
        public string Title { get; private set; } = string.Empty;

        [MaxLength(100)]
        public string? AlternativeTitle { get; private set; }

        [Required]
        public int Year { get; private set; }

        [Required]
        public string Image { get; private set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Description { get; private set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Lenguage { get; private set; } = string.Empty;

        [Required]
        public float Rating { get; private set; }

        [Required]
        public bool FavoriteState { get; private set; }

        [Required]
        public bool WatchedState { get; private set; }

        public Anime() : base() { }

        public Anime(AnimeDTO anime) : base()
        {
            this.UserId = anime.UserId;
            this.Title = anime.Title;
            this.AlternativeTitle = anime.AlternativeTitle;
            this.Year = anime.Year;
            this.Image = anime.Image;
            this.Description = anime.Description;
            this.Lenguage = anime.Lenguage;
            this.Rating = anime.Rating;
            this.FavoriteState = anime.FavoriteState;
            this.WatchedState = anime.WatchedState;
        }
        public void Update(UpdateAnimeDTO anime)
        {
            this.Title = anime.Title;
            this.AlternativeTitle = anime.AlternativeTitle;
            this.Year = anime.Year;
            this.Image = anime.Image;
            this.Description = anime.Description;
            this.Lenguage = anime.Lenguage;
            this.Rating = anime.Rating;
            this.FavoriteState = anime.FavoriteState;
            this.WatchedState = anime.WatchedState;
            base.UpdateEnity();
        }

        public void UpdateFavorite(UpdateFavoriteDTO anime)
        {
            this.FavoriteState = anime.FavoriteState;
            base.UpdateEnity();
        }

        public void UpdateWatched(UpdateWatchedDTO anime)
        {
            this.WatchedState = anime.WatchedState;
            base.UpdateEnity();
        }
    }
}
