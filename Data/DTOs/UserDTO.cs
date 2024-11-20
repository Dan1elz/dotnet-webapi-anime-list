﻿namespace dotnet_anime_list.Data.DTOs
{
    public record UserDTO(Guid Id,string Name, string Lastname, string? Username, string Email, DateTime Created, DateTime Updated);
    public record UpdateUserDTO(string Name, string Lastname, string? Username, string Email, string Password);
    public record CreateUserDTO(string Name, string Lastname, string? Username, string Email, string Password);
    public record LoginUserDTO(string Email, string Password);
}