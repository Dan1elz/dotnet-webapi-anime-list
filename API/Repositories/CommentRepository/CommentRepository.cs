using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using dotnet_anime_list.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.CommentRepository
{
    public class CommentRepository(AppDbContext context) : ICommentRepository
    {
        private readonly AppDbContext _context = context;

        public async Task Create(Comment comment, CancellationToken ct)
        {
            await _context.Comment.AddAsync(comment, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task Delete(Comment comment, CancellationToken ct)
        {
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync(ct);
        }
        public async Task Update(Comment comment, UpdateCommentDTO update, CancellationToken ct)
        {
            comment.Update(update);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<Comment?> GetComment(Guid Id, CancellationToken ct)
        {
            return await _context.Comment.FirstOrDefaultAsync(c => c.Id == Id, ct);
        }
        public async Task<List<Comment>> GetComments(Guid animeId, CancellationToken ct)
        {
            return await _context.Comment.Where(c => c.AnimeId == animeId).ToListAsync(ct);
        }
    }
}
