using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCasino.Pagination;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using OnlineCasino.Logic.Interfaces;
using OnlineCasino.Models;
using OnlineCasino.Logic;
using OnlineCasino.Models.Response;
using Action = OnlineCasino.Models.Response.Action;

namespace OnlineCasino.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]

    public class GameController : ControllerBase
    {
        private readonly OnlineCasinoContext _context;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;

        public GameController(OnlineCasinoContext context, IMapper mapper, ILogger<GameController> logger, IGameService gameService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameModel>>> GetGamesAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            try
            {
                var (games, metadata) = await _gameService.GetGamesAsync(page, pageSize);

                var response = new
                {
                    Metadata = metadata,
                    Games = games
                };

                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<GameModel> GetGame(int id)
        {
            try
            {
                var game = _context.Games.Include(c => c.Collections).SingleOrDefault(x => x.Id == id);

                if (game == null)
                {
                    return NotFound();
                }

                var collections = _mapper.Map<GameModel>(game);

                return Ok(collections);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GameModel>> CreateGame([FromBody] GameModel gameModel)
        {
            try
            {
                var game = new Game
                {
                    DisplayName = gameModel.DisplayName,
                    DisplayIndex = gameModel.DisplayIndex,
                    ReleaseDate = gameModel.ReleaseDate,
                    GameCategory = gameModel.GameCategory,
                    Thumbnail = gameModel.Thumbnail,
                    AvailableDevices = string.Join(",", gameModel.AvailableDevices)
                };

                if (gameModel?.CollectionIds != null && gameModel.CollectionIds.Any())
                {
                    game.Collections = await _context.GameCollections.Where(c => gameModel.CollectionIds.Contains(c.Id)).ToListAsync();
                }

                _context.Games.Add(game);
                await _context.SaveChangesAsync();

                return Ok(new ReponseModel
                {
                    Message = "Sucess",
                    Action = Action.Created
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameModel gameModel)
        {
            try
            {
                var game = await _context.Games.Include(g => g.Collections).FirstOrDefaultAsync(g => g.Id == id);

                if (game == null)
                {
                    return NotFound();
                }

                game.DisplayName = gameModel.DisplayName;
                game.DisplayIndex = gameModel.DisplayIndex;
                game.ReleaseDate = gameModel.ReleaseDate;
                game.GameCategory = gameModel.GameCategory;
                game.Thumbnail = gameModel.Thumbnail;
                game.AvailableDevices = string.Join(",", gameModel.AvailableDevices);

                if (gameModel.CollectionIds != null && gameModel.CollectionIds.Any())
                {
                    game.Collections = await _context.GameCollections.Where(c => gameModel.CollectionIds.Contains(c.Id)).ToListAsync();
                }
                else
                {
                    game.Collections.Clear();
                }

                await _context.SaveChangesAsync();

                return Ok(new ReponseModel
                {
                    Message = "Sucess",
                    Action = Action.Updated
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                var game = await _context.Games.FindAsync(id);

                if (game == null)
                {
                    return NotFound();
                }

                _context.Games.Remove(game);
                await _context.SaveChangesAsync();

                return Ok(new ReponseModel
                {
                    Message = "Sucess",
                    Action = Action.Deleted
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}

