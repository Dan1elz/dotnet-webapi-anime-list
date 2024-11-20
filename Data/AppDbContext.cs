using Microsoft.EntityFrameworkCore;
using dotnet_anime_list.API.Models;
using dotnet_anime_list.API.Models.Abstracts;

namespace dotnet_anime_list.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<AnimeGenres> AnimeGenres { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Genre> Genre { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Database", "api-anime-list.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information); //tirar depois 
            optionsBuilder.EnableSensitiveDataLogging(); // tirar depois

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(p => p.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
