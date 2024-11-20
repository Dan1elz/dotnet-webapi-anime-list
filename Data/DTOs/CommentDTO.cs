namespace dotnet_anime_list.Data.DTOs
{
   public record CommentDTO(Guid AnimeId, string CommentText);

    public record UpdateCommentDTO(string CommentText);
}
