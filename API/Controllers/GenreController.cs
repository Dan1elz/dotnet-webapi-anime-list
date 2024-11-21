using dotnet_anime_list.API.Services;
using dotnet_anime_list.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_anime_list.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController(GenreService service) : ControllerBase
    {
        private readonly GenreService _service = service;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllGenres(CancellationToken ct)
        {
            try
            {
                var genres = await _service.GetAllGenres(ct);
                return Ok(new { message = "Genres found successfully!", data = genres });
            } 
            catch (Exception e)
            {
                return BadRequest(new { message = "Error get all genres: " + e.Message });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenreDTO genre, CancellationToken ct)
        {
            try 
            {
                await _service.Create(genre, ct);
                return Ok(new { message = "Genre created successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error create genre: " + e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(Guid id, CancellationToken ct)
        {
            try
            {
                await _service.Delete(id, ct);
                return Ok(new { message = "Genre deleted successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error delete genre: " + e.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(Guid id, GenreDTO genre, CancellationToken ct)
        {
            try
            {
                await _service.Update(id, genre, ct);
                return Ok(new { message = "Genre updated successfully!" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = "Error update genre: " + e.Message });
            }
        }
    }
}
