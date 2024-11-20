using Microsoft.AspNetCore.Mvc;
using dotnet_anime_list.API.Services;
using dotnet_anime_list.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace dotnet_anime_list.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController(AnimeService service, AuthService authService) : ControllerBase
    {
       private readonly AnimeService _service = service;
       private readonly AuthService _authService = authService;

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateAnimeDTO anime, CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);
                await _service.Create(anime, token!, ct);
                return Ok(new { message = "Anime created successfully!"});
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error registering anime: " + e.Message });
            }
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAnimes(CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);
                var animes = await _service.GetAnimes(token!, ct);
                return Ok(new { message = "Animes found successfully!", data = animes });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error getting animes: " + e.Message });
            }
        }

        [HttpGet("{animeId}")]
        [Authorize]
        public async Task<IActionResult> GetAnime(Guid animeId, CancellationToken ct)
        {
            try
            {
                var anime = await _service.GetAnime(animeId, ct);
                return Ok(new { message = "Anime found successfully!", data = anime });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error getting anime: " + e.Message });
            }
        }
    }
}
