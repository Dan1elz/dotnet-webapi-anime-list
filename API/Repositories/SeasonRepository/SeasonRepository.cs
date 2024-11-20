using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.SeasonRepository
{
    public class SeasonRepository(AppDbContext context) : ISeasonRepository
    {
        private readonly AppDbContext _context = context;


        public async Task Create(Season season, CancellationToken ct)
        {
            await _context.Season.AddAsync(season, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<List<Season>> GetSeasons(Guid animeId, CancellationToken ct)
        {
            return await _context.Season.Where(s => s.AnimeId == animeId).ToListAsync(ct);
        }
    }
}
