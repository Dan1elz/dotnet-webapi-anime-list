using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Repositories.UserRepository;
using dotnet_anime_list.Data.DTOs;
using dotnet_anime_list.Data.Mappers;
 
namespace dotnet_anime_list.API.Services
{
    public class UserService(IUserRepository repository, AuthService service)
    {
        private readonly IUserRepository _repository = repository;
        private readonly AuthService _service = service;

        public async Task Create(CreateUserDTO user, CancellationToken ct)
        {
            var verifyUser = await _repository.VerifyEmail(user.Email, ct);
            if(verifyUser != null)
                throw new Exception("Email already exists");

            var newUser = new User(user);
            await _repository.Create(newUser, ct);
        }
        public async Task<string?> Login(LoginUserDTO user, CancellationToken ct)
        {
            var login = await _repository.Login(user.Email, user.Password, ct) ?? throw new Exception("Email or password incorrect");
            var token = await _service.CreateToken(login, ct) ?? throw new Exception("Error to create token");
            return token;
        }
        public async Task<UserDTO> GetUser(Token token, CancellationToken ct)
        {
            var user = await _repository.GetUser(token.UserId, ct) ?? throw new Exception("User not found");
            return UserMapper.MapUserToUserDTO(user);
        }
        public async Task Update(UpdateUserDTO user, Token token, CancellationToken ct)
        {
            var userToUpdate = await _repository.GetUser(token.UserId, ct) ?? throw new Exception("User not found");
            if (userToUpdate.Email != user.Email || user.Password != user.Password)
                throw new Exception("Email or password incorrect");

            await _repository.Update(userToUpdate, user, ct);
        }
        public async Task Delete(Token token, CancellationToken ct)
        {
            var user = await _repository.GetUser(token.UserId, ct) ?? throw new Exception("User not found");
            await _repository.Delete(user, ct);
            await _service.DeleteToken(user, ct);
        }
    }
}
