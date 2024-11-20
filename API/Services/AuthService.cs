using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.TokenRepository;

namespace dotnet_anime_list.API.Services
{
    public class AuthService(ITokenRepository repository, TokenService service)
    {
        private readonly ITokenRepository _repository = repository;
        private readonly TokenService _service = service;


        public async Task<Token?> AuthenticationToken(string _token, CancellationToken ct)
        { 
            var token = await _repository.GetToken(_token, ct);
            if(token == null)
                throw new Exception("Token not found");

            var valid = _service.ValidadeToken(token.Value);
            if (!valid)
                throw new Exception("Token expired, please log in again ");

            return token;
        }
        public async Task<string?> CreateToken(User user, CancellationToken ct)
        {
            var token = await _repository.GetTokenByID(user.Id, ct);
            if (token != null)
            {
                var valid = _service.ValidadeToken(token.Value);
                if (valid)
                {
                    return token.Value;
                }
                await _repository.Delete(token, ct);
            }

            token = _service.GenerateToken(user.Id); 
            await _repository.Create(token, ct);

            return token.Value;
        }

        public async Task DeleteToken(User user, CancellationToken ct)
        {
            var token = await _repository.GetTokenByID(user.Id, ct);
            if (token == null)
                throw new Exception("Token not found");

            await _repository.Delete(token, ct);
        }

        public string GetTokenToString(string token)
        {
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                throw new Exception("Token not found");

            var result = token.Substring("Bearer ".Length).Trim();
            return result;
        }
    }
}
