using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Repositories.CommentRepository
{
    public interface ICommentRepository
    {
        Task Create(Comment comment, CancellationToken ct);
        Task Delete(Comment comment, CancellationToken ct);
        Task Update(Comment comment, UpdateCommentDTO update, CancellationToken ct);
        Task<Comment?> GetComment(Guid Id, CancellationToken ct);
        Task<List<Comment>> GetComments(Guid animeId, CancellationToken ct);
    }
}
