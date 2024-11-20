using dotnet_anime_list.API.Models;
using dotnet_anime_list.Data;
using dotnet_anime_list.Data.DTOs;
using Microsoft.EntityFrameworkCore;

namespace dotnet_anime_list.API.Repositories.GenreRepository
{
    public class GenreRepository(AppDbContext context) : IGenreRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Genre?> GetGenre(Guid Id, CancellationToken ct)
        {
            return await _context.Genre.Where(Genre => Genre.Id == Id).FirstOrDefaultAsync(ct);
        }
        public async Task Create(Genre genre, CancellationToken ct)
        {
            await _context.Genre.AddAsync(genre, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task Delete(Genre genre, CancellationToken ct)
        {
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync(ct);
        }
        public async Task Update(Genre genre, GenreDTO genreDTO, CancellationToken ct)
        {
            genre.Update(genreDTO);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<List<Genre>> GetAllGenres(CancellationToken ct)
        {
            return await _context.Genre.ToListAsync(ct);
        }
        
        
        public async Task AddAnimeGenre(AnimeGenres animeGenres, CancellationToken ct)
        {
            await _context.AnimeGenres.AddAsync(animeGenres, ct);
            await _context.SaveChangesAsync(ct);
        }
        public async Task RemovelAllAnimeGenre(List<AnimeGenres> animeGenres, CancellationToken ct)
        {
            _context.AnimeGenres.RemoveRange(animeGenres);
            await _context.SaveChangesAsync(ct);
        }
        public async Task<List<Genre>> GetAnimeGenresInGenre(Guid AnimeId, CancellationToken ct)
        {
            var genreIds = await _context.AnimeGenres.Where(ag => ag.AnimeId == AnimeId).Select(ag => ag.GenreId).ToListAsync(ct);
            var genres = await _context.Genre.Where(g => genreIds.Contains(g.Id)).ToListAsync(ct);

            return genres;
        }
        public async Task<List<AnimeGenres>> GetAimeGenres(Guid AnimeId, CancellationToken ct)
        {
            return await _context.AnimeGenres.Where(u => u.AnimeId == AnimeId).ToListAsync(ct);
        }
    }
}
