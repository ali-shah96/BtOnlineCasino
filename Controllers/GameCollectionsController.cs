using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCasino.DatabaseContext;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Models;
using OnlineCasino.Models.Response;
using OnlineCasino.Pagination;
using Action = OnlineCasino.Models.Response.Action;

namespace OnlineCasino.Controllers
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class GameCollectionsController : ControllerBase
    {
        private readonly OnlineCasinoContext _context;
        private readonly ILogger<GameCollectionsController> _logger;
        private readonly IMapper _mapper;

        public GameCollectionsController(OnlineCasinoContext context, IMapper mapper, ILogger<GameCollectionsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameCollectionsModel>> Get(int id)
        {
            var collection = await RecursiveInclude(_context.GameCollections, id);

            if (collection == null)
            {
                return NotFound();
            }

            var mapped = _mapper.Map<GameCollectionsModel>(collection);

            return Ok(mapped);
        }

        [HttpGet]
        public async Task<ActionResult<List<GameCollectionsModel>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var totalCollections = await _context.GameCollections.CountAsync();
            var paginationModel = new PaginationModel<GameCollections>();
            var collectionsQuery = _context.GameCollections
                                        .Include(c => c.Games)
                                        .Include(c => c.SubCollections)
                                        .OrderBy(c => c.Id);

            var allCollections = await collectionsQuery.ToListAsync();

            // Paginate all collections
            var paginatedCollections = paginationModel.Paginate(allCollections, page, pageSize);

            // Map the paginated collections with their sub-collections
            var mappedCollections = await MapCollectionsWithSubCollections(paginatedCollections.ToList());

            var metadata = paginationModel.GetPaginationMetadata(totalCollections, page, pageSize);

            var response = new
            {
                Metadata = metadata,
                Collections = mappedCollections
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameCollectionsModel>> Create([FromBody] GameCollectionsModel gameCollectionsModel)
        {
            try
            {
                var collection = new GameCollections
                {
                    DisplayName = gameCollectionsModel.DisplayName,
                    DisplayIndex = gameCollectionsModel.DisplayIndex,
                    ParentCollectionId = gameCollectionsModel.ParentCollectionId
                };

                if (gameCollectionsModel?.GameIds != null && gameCollectionsModel.GameIds.Any())
                {
                    collection.Games = await _context.Games.Where(g => gameCollectionsModel.GameIds.Contains(g.Id)).ToListAsync();
                }

                if (gameCollectionsModel?.SubCollections != null && gameCollectionsModel.SubCollections.Any())
                {
                    collection.SubCollections = await CreateSubCollections(gameCollectionsModel.SubCollections);
                }

                _context.GameCollections.Add(collection);
                await _context.SaveChangesAsync();

                return Ok( new ReponseModel
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
        public async Task<IActionResult> Update(int id, [FromBody] GameCollectionsModel gameCollectionsModel)
        {
            try
            {
                var collection = await _context.GameCollections.Include(c => c.Games).Include(c => c.SubCollections).FirstOrDefaultAsync(c => c.Id == id);

                if (collection == null)
                {
                    return NotFound();
                }

                collection.DisplayName = gameCollectionsModel.DisplayName;
                collection.DisplayIndex = gameCollectionsModel.DisplayIndex;
                collection.ParentCollectionId = gameCollectionsModel.ParentCollectionId;

                if (gameCollectionsModel.GameIds != null && gameCollectionsModel.GameIds.Any())
                {
                    collection.Games = await _context.Games.Where(g => gameCollectionsModel.GameIds.Contains(g.Id)).ToListAsync();
                }
                else
                {
                    collection.Games.Clear(); 
                }

                if (gameCollectionsModel.SubCollections != null)
                {
                    await UpdateSubCollections(collection.SubCollections, gameCollectionsModel.SubCollections);
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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var collection = await _context.GameCollections.Include(c => c.Games)
                    .Include(c => c.SubCollections).FirstOrDefaultAsync(x => x.Id == id);

                if (collection == null)
                {
                    return NotFound();
                }

                await DeleteSubCollections(collection.SubCollections);

                _context.GameCollections.Remove(collection);
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

        #region Private Methods

        // Recursively loads a game collection and its sub-collections, including games
        private async Task<GameCollections> RecursiveInclude(DbSet<GameCollections> query, int id)
        {
            var collection = await query
                .Include(c => c.Games)
                .Include(c => c.SubCollections) 
                .FirstOrDefaultAsync(c => c.Id == id); 

            // Recursively load sub-collections
            if (collection != null)
            {
                foreach (var subCollection in collection.SubCollections)
                {
                    await RecursiveInclude(query, subCollection.Id);
                }
            }

            return collection;
        }

        // Recursively deletes sub-collections of a collection
        private async Task DeleteSubCollections(List<GameCollections> subCollections)
        {
            foreach (var subCollection in subCollections)
            {
                await DeleteSubCollections(subCollection.SubCollections); // Recursively delete sub-collections

                _context.GameCollections.Remove(subCollection);
            }
        }

        // Recursively updates sub-collections of a collection
        private async Task UpdateSubCollections(List<GameCollections> existingSubCollections, List<GameCollectionsModel> updatedSubCollections)
        {
            foreach (var existingSubCollection in existingSubCollections)
            {
                var updatedSubCollection = updatedSubCollections.FirstOrDefault(s => s.Id == existingSubCollection.Id);

                if (updatedSubCollection != null)
                {
                    // Update properties of the existing sub-collection
                    existingSubCollection.DisplayName = updatedSubCollection.DisplayName;
                    existingSubCollection.DisplayIndex = updatedSubCollection.DisplayIndex;

                    // Update games in the sub-collection
                    if (updatedSubCollection.GameIds != null && updatedSubCollection.GameIds.Any())
                    {
                        existingSubCollection.Games = await _context.Games.Where(g => updatedSubCollection.GameIds.Contains(g.Id)).ToListAsync();
                    }
                    else
                    {
                        existingSubCollection.Games.Clear(); // Clear existing games if GameIds is null or empty
                    }

                    // Recursively update sub-collections of the sub-collection
                    await UpdateSubCollections(existingSubCollection.SubCollections, updatedSubCollection.SubCollections);
                }
            }

            // Add new sub-collections
            foreach (var updatedSubCollection in updatedSubCollections.Where(s => s.Id == 0))
            {
                var newSubCollection = new GameCollections
                {
                    DisplayName = updatedSubCollection.DisplayName,
                    DisplayIndex = updatedSubCollection.DisplayIndex,
                };

                // Assign games to the new sub-collection
                if (updatedSubCollection.GameIds != null && updatedSubCollection.GameIds.Any())
                {
                    newSubCollection.Games = await _context.Games.Where(g => updatedSubCollection.GameIds.Contains(g.Id)).ToListAsync();
                }

                existingSubCollections.Add(newSubCollection);
            }
        }

        // Creates sub-collections recursively
        private async Task<List<GameCollections>> CreateSubCollections(List<GameCollectionsModel> subCollections)
        {
            var createdSubCollections = new List<GameCollections>();

            foreach (var subCollectionModel in subCollections)
            {
                var subCollection = new GameCollections
                {
                    DisplayName = subCollectionModel.DisplayName,
                    DisplayIndex = subCollectionModel.DisplayIndex,
                };

                // Assign games to the sub-collection
                if (subCollectionModel?.GameIds != null && subCollectionModel.GameIds.Any())
                {
                    subCollection.Games = await _context.Games.Where(g => subCollectionModel.GameIds.Contains(g.Id)).ToListAsync();
                }

                // Create sub-collections recursively
                if (subCollectionModel?.SubCollections != null && subCollectionModel.SubCollections.Any())
                {
                    subCollection.SubCollections = await CreateSubCollections(subCollectionModel.SubCollections);
                }

                _context.GameCollections.Add(subCollection);
                await _context.SaveChangesAsync();

                createdSubCollections.Add(subCollection);
            }

            return createdSubCollections;
        }

        // Maps collections with their sub-collections
        private async Task<List<GameCollectionsModel>> MapCollectionsWithSubCollections(List<GameCollections> collections)
        {
            var mappedCollections = new List<GameCollectionsModel>();

            foreach (var collection in collections)
            {
                var mappedCollection = _mapper.Map<GameCollectionsModel>(collection);
                await LoadSubCollectionsRecursively(collection, mappedCollection); // Load sub-collections recursively
                mappedCollections.Add(mappedCollection);
            }

            return mappedCollections;
        }

        // Recursively loads sub-collections
        private async Task LoadSubCollectionsRecursively(GameCollections collection, GameCollectionsModel mappedCollection)
        {
            if (collection?.SubCollections != null)
            {
                mappedCollection.SubCollections = new List<GameCollectionsModel>();

                foreach (var subCollection in collection.SubCollections)
                {
                    var mappedSubCollection = _mapper.Map<GameCollectionsModel>(subCollection);
                    await LoadSubCollectionsRecursively(subCollection, mappedSubCollection); // Recursively load sub-collections
                    mappedCollection.SubCollections.Add(mappedSubCollection);
                }
            }
        }

        #endregion

    }
}
