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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateAnimeDTO anime, CancellationToken ct)
        {
            try
            {
                var token = await _authService.AuthenticationToken(_authService.GetTokenToString(HttpContext.Request.Headers["Authorization"].ToString()), ct);
                var result = await _service.Create(anime, token!, ct);
                return Ok(new { message = "Anime created successfully!", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error registering anime: " + e.Message });
            }
        }
        
        [Authorize]
        [HttpGet]
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

        [Authorize]
        [HttpGet("{animeId}")]
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

        [Authorize]
        [HttpPut("watched/{animeId}")]
        public async Task<IActionResult> UpdateWatchedAnime(Guid animeId, UpdateWatchedDTO watched, CancellationToken ct)
        {
            try
            {
                await _service.UpdateWatchedAnime(animeId, watched, ct);
                return Ok(new { message = "Anime watched successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error watching anime: " + e.Message });
            }
        }
        [Authorize]
        [HttpPut("favorite/{animeId}")]
        public async Task<IActionResult> UpdateFavoriteAnime(Guid animeId, UpdateFavoriteDTO favorite, CancellationToken ct)
        {
            try
            {
                await _service.UpdateFavoriteAnime(animeId, favorite, ct);
                return Ok(new { message = "Anime favorite successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error favoriting anime: " + e.Message });
            }
        }
        [Authorize]
        [HttpPut("{animeId}")]
        public async Task<IActionResult> UpdateAnime(Guid animeId, UpdateAnimeDTO updateAnime, CancellationToken ct)
        {
            try
            {
                await _service.UpdateAnime(animeId, updateAnime, ct);
                return Ok(new { message = "Anime updated successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error updating anime: " + e.Message });
            }
        }
        [Authorize]
        [HttpDelete("{animeId}")]
        public async Task<IActionResult> DeleteAnime(Guid animeId, CancellationToken ct)
        {
            try
            {
                await _service.DeleteAnime(animeId, ct);
                return Ok(new { message = "Anime deleted successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error deleting anime: " + e.Message });
            }
        }
        //todas as rotas de visualização personalizada de animes
    }
}
