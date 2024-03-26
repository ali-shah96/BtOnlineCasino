using OnlineCasino.DatabaseContext.Entities;

namespace OnlineCasino.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGamesAsync(int page, int pageSize);
        Task<int> GetTotalGamesCountAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<Game> CreateGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(int id);
    }
}
