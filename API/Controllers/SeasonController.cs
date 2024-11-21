using dotnet_anime_list.API.Services;
using dotnet_anime_list.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_anime_list.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SeasonController(SeasonService service, AnimeService animeService) : Controller
    {
        private readonly SeasonService _service = service;
        private readonly AnimeService _animeService = animeService;

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateSeason(SeasonDTO season, CancellationToken ct)
        {
            try 
            {
                await _animeService.VerifyAnime(season.AnimeId, ct);
                
                await _service.AddSeason(season, ct);
                return Ok(new { message = "Season created successfully!" });
            } catch (Exception e)
            {
                return BadRequest(new { message = "Error create season: " + e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(Guid id, CancellationToken ct)
        {
            try
            {
                await _service.DeleteSeason(id, ct);
                return Ok(new { message = "Season deleted successfully!" });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error delete season: " + e.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeason(Guid id, UpdateSeasonDTO season, CancellationToken ct)	
        {
            try
            {
                await _service.UpdateSeason(id, season, ct);
                return Ok(new { message = "Season updated successfully!" });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error update season: " + e.Message });
            }
        }
    }
}
