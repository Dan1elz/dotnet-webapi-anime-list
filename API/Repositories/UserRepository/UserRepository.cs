using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using dotnet_anime_list.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.UserRepository
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task Create(User user, CancellationToken ct)
        {
            await _context.User.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task Delete(User user, CancellationToken ct)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<User?> GetUser(Guid Id, CancellationToken ct)
        {
            return await _context.User.Where(User => User.Id == Id).FirstOrDefaultAsync(ct);
        }

        public async Task<User?> Login(string email, string password, CancellationToken ct)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Email == email && u.Password == password, ct);
        }
        public async Task Update(User user, UpdateUserDTO userDTO, CancellationToken ct)
        {
            user.Update(userDTO);
            await _context.SaveChangesAsync(ct);
        }

        public async Task<User?> VerifyEmail(string email, CancellationToken ct)
        {
           return await _context.User.SingleOrDefaultAsync(u => u.Email == email, ct);
        }
    }
}
