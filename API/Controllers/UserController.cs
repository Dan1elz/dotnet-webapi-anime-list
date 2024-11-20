using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Services;
using dotnet_anime_list.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_anime_list.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class UserController(UserService service, AuthService authService) : ControllerBase
    {
        private readonly UserService _service = service;
        private readonly AuthService _authService = authService;

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO user, CancellationToken ct)
        {
            try
            {
                await _service.Create(user, ct);
                return Ok(new { message = "User created successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error registering user: " + e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO user, CancellationToken ct)
        {
            try
            {
                var token = await _service.Login(user, ct);
                return Ok(new { data = token, message = "User logged in successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error when logging in: " + e.Message });
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser(CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);

                var user = await _service.GetUser(token!, ct);

                return Ok(new { data = user, message = "User data returned successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "error when returning user data: " + e.Message });
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDTO user, CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);
                await _service.Update(user, token!, ct);

                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error when updating user: " + e.Message });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);

                await _service.Delete(token!, ct);

                return Ok(new { message = "User deleted successfully" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error when deleting user: " + e.Message });
            }
        }
    }
}