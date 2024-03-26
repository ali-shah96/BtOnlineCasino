using OnlineCasino.DatabaseContext.Entities;

namespace OnlineCasino.Models
{
    public class GameCollectionsModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public int? ParentCollectionId { get; set; }

        public List<int> GameIds { get; set; }
        public List<GameCollectionsModel> SubCollections { get; set; }
    }
}
