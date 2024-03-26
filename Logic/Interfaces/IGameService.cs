using OnlineCasino.Models;
using OnlineCasino.Pagination;

namespace OnlineCasino.Logic.Interfaces
{
    public interface IGameService
    {
        Task<(IEnumerable<GameModel>, PaginationMetadata)> GetGamesAsync(int page, int pageSize);
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> CreateGameAsync(GameModel gameModel);
        Task UpdateGameAsync(int id, GameModel gameModel);
        Task DeleteGameAsync(int id);
    }
}
