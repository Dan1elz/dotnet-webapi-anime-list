using System.ComponentModel.DataAnnotations.Schema;
using dotnet_anime_list.API.Models.Abstracts;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;

        public Genre() { }

        public Genre(GenreDTO genre) : base()
        {
            this.Name = genre.Name;
        }
    }
}
