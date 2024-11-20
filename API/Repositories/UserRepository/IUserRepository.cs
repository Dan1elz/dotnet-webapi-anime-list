using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;
using TokenNamespace = dotnet_anime_list.API.Models;

namespace dotnet_anime_list.API.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task Create(User user, CancellationToken ct);
        Task<User?> GetUser(Guid Id, CancellationToken ct);
        Task<User?> Login(string email, string password, CancellationToken ct);
        Task Update(User user, UpdateUserDTO userDTO, CancellationToken ct);
        Task Delete(User user, CancellationToken ct);
        Task<User?> VerifyEmail(string email, CancellationToken ct);
    }
}
