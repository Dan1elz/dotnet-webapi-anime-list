using dotnet_anime_list.API.Models.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_anime_list.API.Models
{
    public class Token : BaseEntity
    {
        [ForeignKey("User")]
        public Guid UserId { get; private init; }
        public string Value { get; private set; }

        public Token(Guid userId, string value) : base()
        {
            this.UserId = userId;
            this.Value = value;
        }
    }
}
