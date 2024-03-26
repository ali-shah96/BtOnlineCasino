using AutoMapper;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Logic.Interfaces;
using OnlineCasino.Models;
using OnlineCasino.Pagination;
using OnlineCasino.Repositories.Interfaces;

namespace OnlineCasino.Logic
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<GameModel>, PaginationMetadata)> GetGamesAsync(int page, int pageSize)
        {
            var games = await _gameRepository.GetGamesAsync(page, pageSize);
            var totalGamesCount = await _gameRepository.GetTotalGamesCountAsync();
            var paginationMetadata = new PaginationMetadata
            {
                TotalItems = totalGamesCount,
                PageSize = pageSize,
                CurrentPage = page
            };
            var gameModels = _mapper.Map<IEnumerable<GameModel>>(games);

            return (gameModels, paginationMetadata);
        }

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetGameByIdAsync(id);
            return _mapper.Map<GameModel>(game);
        }

        public async Task<GameModel> CreateGameAsync(GameModel gameModel)
        {
            var game = _mapper.Map<Game>(gameModel);
            var createdGame = await _gameRepository.CreateGameAsync(game);
            return _mapper.Map<GameModel>(createdGame);
        }

        public async Task UpdateGameAsync(int id, GameModel gameModel)
        {
            var game = _mapper.Map<Game>(gameModel);
            game.Id = id;
            await _gameRepository.UpdateGameAsync(game);
        }

        public async Task DeleteGameAsync(int id)
        {
            await _gameRepository.DeleteGameAsync(id);
        }
    }
}
