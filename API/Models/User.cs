using dotnet_anime_list.API.Models.Abstracts;
using dotnet_anime_list.Data.DTOs;
using System.ComponentModel.DataAnnotations;
namespace dotnet_anime_list.API.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; private set; } = string.Empty;

        [Required]
        [MaxLength(25)]
        public string LastName { get; private set; } = string.Empty;

        [MaxLength(50)]
        public string? Username { get; private set; } 

        [Required]
        [MaxLength(50)]
        public string Email { get; private set; } = string.Empty;

        [Required]
        [MaxLength(32)]
        public string Password { get; private set; } = string.Empty;

        public User() : base() { }

        public User(CreateUserDTO user) : base ()
        {
            this.Name = user.Name;
            this.LastName = user.Lastname;
            this.Username = user.Username;
            this.Email = user.Email;
            this.Password = user.Password;
        }

        public void Update(UpdateUserDTO user)
        {
            this.Name = user.Name;
            this.LastName = user.Lastname;
            this.Username = user.Username;
            base.UpdateEnity();
        }
    }
}
