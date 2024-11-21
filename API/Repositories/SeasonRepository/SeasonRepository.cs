using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using dotnet_anime_list.Data.DTOs;
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
        public async Task Delete(Season season, CancellationToken ct)
        {
            _context.Season.Remove(season);
            await _context.SaveChangesAsync(ct);
        }
        public async Task Update(Season season, UpdateSeasonDTO update, CancellationToken ct)
        {
            season.Update(update);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<List<Season>> GetSeasons(Guid animeId, CancellationToken ct)
        {
            return await _context.Season.Where(s => s.AnimeId == animeId).ToListAsync(ct);
        }

        public async Task<Season?> GetSeason(Guid Id, CancellationToken ct)
        {
            return await _context.Season.FirstOrDefaultAsync(s => s.Id == Id, ct);
        }
        
    }
}
