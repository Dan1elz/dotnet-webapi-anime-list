using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using dotnet_anime_list.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.AnimeRepository
{
    public class AnimeRepository(AppDbContext context) : IAnimeRepository
    {
        private readonly AppDbContext _context = context;

        public async Task Create(Anime anime, CancellationToken ct)
        {
            await _context.Anime.AddAsync(anime, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<List<Anime>> GetAnimes(Guid userId, CancellationToken ct)
        {
            return await _context.Anime.Where(a => a.UserId == userId).ToListAsync(ct);
        }
        public async Task<Anime?> GetAnime(Guid animeId, CancellationToken ct)
        {
            return await _context.Anime.FirstOrDefaultAsync(a => a.Id == animeId, ct);
        }
        public async Task Update(Anime anime, AnimeDTO updateAnime, CancellationToken ct)
        {
            anime.Update(updateAnime);
            await _context.SaveChangesAsync(ct);
        }
        public async Task UpdateWatchedAnime(Anime anime, UpdateWatchedDTO watched, CancellationToken ct)
        {
            anime.UpdateWatched(watched);
            await _context.SaveChangesAsync(ct);
        }
        public async Task UpdateFavoriteAnime(Anime anime, UpdateFavoriteDTO favorite, CancellationToken ct)
        {
            anime.UpdateFavorite(favorite);
            await _context.SaveChangesAsync(ct);
        } 
        public async Task Delete(Anime anime, CancellationToken ct)
        {
            _context.Anime.Remove(anime);
            await _context.SaveChangesAsync(ct);
        }
    }
}
