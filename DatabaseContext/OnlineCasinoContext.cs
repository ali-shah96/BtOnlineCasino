using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnlineCasino.DatabaseContext.Entities;

namespace OnlineCasino.DatabaseContext
{
    public class OnlineCasinoContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public OnlineCasinoContext(DbContextOptions<OnlineCasinoContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameCollections> GameCollections { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Collections)
                .WithMany(gc => gc.Games)
                .UsingEntity<GameCollectionGame>(
                    j => j
                        .HasOne(gc => gc.GameCollection)
                        .WithMany()
                        .HasForeignKey(gc => gc.GameCollectionId),
                    j => j
                        .HasOne(g => g.Game)
                        .WithMany()
                        .HasForeignKey(g => g.GameId),
                    j =>
                    {
                        j.ToTable("GameCollectionsGames");
                        j.HasKey(gc => new { gc.GameId, gc.GameCollectionId });
                    }
                );

            modelBuilder.Entity<GameCollections>()
                .HasMany(gc => gc.SubCollections)
                .WithOne()
                .HasForeignKey(gc => gc.ParentCollectionId);

            #region Seed Data

            modelBuilder.Entity<GameCollections>().HasData(
                new GameCollections { Id = 1, DisplayName = "Featured Games", DisplayIndex = 1 },
                new GameCollections { Id = 2, DisplayName = "Top Rated", DisplayIndex = 2 },
                new GameCollections { Id = 3, DisplayName = "Classic Slots", DisplayIndex = 1, ParentCollectionId = 1 },
                new GameCollections { Id = 4, DisplayName = "Table Games", DisplayIndex = 2 },
                new GameCollections { Id = 5, DisplayName = "Roulette", DisplayIndex = 1, ParentCollectionId = 4 }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, DisplayName = "Game A", DisplayIndex = 1, ReleaseDate = DateTime.Now, GameCategory = GameCategory.ClassicSlots, AvailableDevices = "Desktop",Thumbnail = ""},
                new Game { Id = 2, DisplayName = "Game B", DisplayIndex = 2, ReleaseDate = DateTime.Now, GameCategory = GameCategory.ClassicSlots, AvailableDevices = "Desktop", Thumbnail = "" },
                new Game { Id = 3, DisplayName = "Game C", DisplayIndex = 1, ReleaseDate = DateTime.Now, GameCategory = GameCategory.Roulette, AvailableDevices = "Desktop", Thumbnail = "" },
                new Game { Id = 4, DisplayName = "Game D", DisplayIndex = 2, ReleaseDate = DateTime.Now, GameCategory = GameCategory.Roulette, AvailableDevices = "Desktop", Thumbnail = "" },
                new Game { Id = 5, DisplayName = "Game E", DisplayIndex = 1, ReleaseDate = DateTime.Now, GameCategory = GameCategory.ClassicSlots, AvailableDevices = "Desktop", Thumbnail = "" }
            );

            modelBuilder.Entity<GameCollectionGame>().HasData(
                new GameCollectionGame { GameId = 1, GameCollectionId = 1 }, 
                new GameCollectionGame { GameId = 2, GameCollectionId = 1 }, 
                new GameCollectionGame { GameId = 3, GameCollectionId = 2 }, 
                new GameCollectionGame { GameId = 4, GameCollectionId = 2 },
                new GameCollectionGame { GameId = 5, GameCollectionId = 3 }
            );

            modelBuilder.Entity<Users>().HasData(
                new Users { Id = 1, UserName = "user1", Password = "password1" },
                new Users { Id = 2, UserName = "user2", Password = "password2" }
            );

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}

