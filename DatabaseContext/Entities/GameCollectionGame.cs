using Microsoft.EntityFrameworkCore;

namespace OnlineCasino.DatabaseContext.Entities
{
    [Keyless]
    public class GameCollectionGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int GameCollectionId { get; set; }
        public GameCollections GameCollection { get; set; }
    }
}
