using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.TokenRepository
{
    public class TokenRepository(AppDbContext context) : ITokenRepository
    {
        private readonly AppDbContext _context = context;

        public async Task Create(Token token, CancellationToken ct) 
        {
            await _context.Token.AddAsync(token, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<Token?> GetToken(string token, CancellationToken ct)
        {
           return  await _context.Token.FirstOrDefaultAsync(u => u.Value == token, ct);
        }

        public async Task<Token?> GetTokenByID(Guid Id, CancellationToken ct)
        {
            return await _context.Token.FirstOrDefaultAsync(u => u.UserId == Id, ct);
        }
        public async Task Delete(Token token, CancellationToken ct)
        {
            _context.Token.Remove(token);
            await _context.SaveChangesAsync(ct);
        }
    }
}
