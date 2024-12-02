using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.CommentRepository;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.API.Services
{
    public class CommentService(ICommentRepository commentRepository,  AnimeService animeService)
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly AnimeService _animeService = animeService;
        
        public async Task<Guid> Create(CommentDTO comment, CancellationToken ct)
        {
            await _animeService.VerifyAnime(comment.AnimeId, ct);

            var newComment = new Comment(comment);
            await _commentRepository.Create(newComment, ct);

            return newComment.Id;
        }
        public async Task Delete(Guid id, CancellationToken ct)
        {
            var comment = await _commentRepository.GetComment(id, ct) ?? throw new Exception("Comment not found");
            await _commentRepository.Delete(comment, ct);
        }
        public async Task Update(Guid id, UpdateCommentDTO comment, CancellationToken ct)
        {
            var commentToUpdate = await _commentRepository.GetComment(id, ct) ?? throw new Exception("Comment not found");
            await _commentRepository.Update(commentToUpdate, comment, ct);
        }
        public async Task<List<Comment>> GetComments(Guid animeId, CancellationToken ct)
        {
           return await _commentRepository.GetComments(animeId, ct) ?? throw new Exception("Comment not found");
        }
        public async Task<Comment> GetComment(Guid id, CancellationToken ct)
        {
            return await _commentRepository.GetComment(id, ct) ?? throw new Exception("Comment not found");
        }
    }
}
