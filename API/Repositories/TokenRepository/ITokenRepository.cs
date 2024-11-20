using dotnet_anime_list.API.Models;

namespace dotnet_anime_list.API.Repositories.TokenRepository
{
    public interface ITokenRepository
    {
        Task Create(Token token, CancellationToken ct);
        Task Delete(Token token, CancellationToken ct);
        Task<Token?> GetToken(string token, CancellationToken ct);
        Task<Token?> GetTokenByID(Guid Id, CancellationToken ct);
    }
}
