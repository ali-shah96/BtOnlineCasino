using OnlineCasino.DatabaseContext.Entities;

namespace OnlineCasino.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GameCategory GameCategory { get; set; }
        public string Thumbnail { get; set; }
        public List<string> AvailableDevices { get; set; }
        public List<int> CollectionIds { get; set; }
    }
}
