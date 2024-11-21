using dotnet_anime_list.API.Services;
using dotnet_anime_list.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_anime_list.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController(CommentService service) : Controller
    {
        private readonly CommentService _service = service;

        [Authorize]
        [HttpPost]	
        public async Task<IActionResult> CreateComment(CommentDTO comment, CancellationToken ct)
        {
            try
            {
                await _service.Create(comment, ct);
                return Ok(new { message = "Comment created successfully!" });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error create comment: " + e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id, CancellationToken ct)
        {
            try
            {
                await _service.Delete(id, ct);
                return Ok(new { message = "Comment deleted successfully!" });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error delete comment: " + e.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, UpdateCommentDTO comment, CancellationToken ct)
        {
            try
            {
                await _service.Update(id, comment, ct);
                return Ok(new { message = "Comment updated successfully!" });
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error update comment: " + e.Message });
            }
        }

        [Authorize]
        [HttpGet("{animeId}")]
        public async Task<IActionResult> GetComments(Guid animeId, CancellationToken ct)
        {
            try
            {
                var comments = await _service.GetComments(animeId, ct);
                return Ok(comments);
            }
            catch(Exception e)
            {
                return BadRequest(new { message = "Error get comments: " + e.Message });
            }
        }
    }
}
