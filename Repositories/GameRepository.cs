using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineCasino.DatabaseContext;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Repositories.Interfaces;

namespace OnlineCasino.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly OnlineCasinoContext _context;

        public GameRepository(OnlineCasinoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetGamesAsync(int page, int pageSize)
        {
            return await _context.Games
                .Include(c => c.Collections)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalGamesCountAsync()
        {
            return await _context.Games.CountAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _context.Games.Include(c => c.Collections).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
