using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data.DTOs;

namespace dotnet_anime_list.Data.Mappers
{
    public class UserMapper
    {
        public static UserDTO MapUserToUserDTO(User user, int Amount)
        {
            return new UserDTO(user.Id, user.Name, user.LastName, user.Username, user.Email, Amount, user.Created, user.Updated);
        }
    }
}
