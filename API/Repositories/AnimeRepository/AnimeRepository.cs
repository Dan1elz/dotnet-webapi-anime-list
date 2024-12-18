﻿using dotnet_anime_list.API.Models;
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
        public async Task<Anime?> GetAnime(Guid animeId, CancellationToken ct)
        {
            return await _context.Anime.FirstOrDefaultAsync(a => a.Id == animeId, ct);
        }
        public async Task<(List<Anime> Animes, int TotalCount)> GetAnimes(Guid userId, int offset, int limit, CancellationToken ct)
        {
            var query =  _context.Anime.Where(a => a.UserId == userId);
            var totalCount = await query.CountAsync(ct);
             var animes = await query.Skip(offset).Take(limit).ToListAsync(ct);
            return (animes, totalCount);
        }
        public async Task<(List<Anime> Animes, int TotalCount)> GetAnimesFavorited(Guid userId, int offset, int limit, CancellationToken ct)
        {
            var query = _context.Anime.Where(a => a.UserId == userId && a.FavoriteState == true);
            var totalCount = await query.CountAsync(ct);
            var animes = await query.Skip(offset).Take(limit).ToListAsync(ct);
            return (animes, totalCount);
        }
        public async Task<(List<Anime> Animes, int TotalCount)> GetAnimesRating(Guid userId, int offset, int limit, CancellationToken ct)
        {
            var query = _context.Anime.Where(a => a.UserId == userId).OrderByDescending(a => a.Rating);
            var totalCount = await query.CountAsync(ct);
            var animes = await query.Skip(offset).Take(limit).ToListAsync(ct);
            return (animes, totalCount);
        }
        public async Task<(List<Anime> Animes, int TotalCount)> GetAnimesByCategory(Guid userId, int offset, int limit, GetCategoryDTO category, CancellationToken ct)
        {
            var query = _context.Anime.Where(a => a.UserId == userId);
            if (!string.IsNullOrEmpty(category.search))
                query = query.Where(a => a.Title.Contains(category.search) || (a.AlternativeTitle != null && a.AlternativeTitle.Contains(category.search)));

            if (category.genres != null && category.genres.Count > 0)
            {
                var AnimesId = await _context.AnimeGenres
                    .Where(ag => category.genres.Contains(ag.GenreId))
                    .Select(ag => ag.AnimeId)
                    .ToListAsync(ct);
                
                query = query.Where(a => AnimesId.Contains(a.Id));
            }

            if (category.favorite != null)
                query = query.Where(a => a.FavoriteState == category.favorite);

            if (category.watched != null)
                query = query.Where(a => a.WatchedState == category.watched);
            
            if (category.rating != null)
                query = query.Where(a => a.Rating == category.rating);

            var totalCount = await query.CountAsync(ct);
            var animes = await query.Skip(offset).Take(limit).ToListAsync(ct);
            return (animes, totalCount);
        }
        public async Task<int> GetAmountAnimesWatched(Guid userId, CancellationToken ct)
        {
            return await _context.Anime
                .Where(a => a.UserId == userId && a.WatchedState == true)
                .CountAsync(ct);
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
