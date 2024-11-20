using System.ComponentModel.DataAnnotations.Schema;
using dotnet_anime_list.API.Models.Abstracts;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Models
{
    public class Comment : BaseEntity
    {
        [ForeignKey("Anime")]
        public Guid AnimeId { get; private init; }

        public string CommentText { get; private set; } = string.Empty;
        public Comment() : base() { }

        public Comment(CommentDTO comment) : base()
        {
            this.AnimeId = comment.AnimeId;
            this.CommentText = comment.CommentText;
        }

        public void Update(UpdateCommentDTO comment)
        {
            this.CommentText = comment.CommentText;
            base.UpdateEnity();
        }
    }
}
