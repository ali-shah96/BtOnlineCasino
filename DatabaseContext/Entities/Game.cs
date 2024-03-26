using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineCasino.DatabaseContext.Entities
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
        public GameCategory GameCategory { get; set; }
        public string Thumbnail { get; set; }
        public string AvailableDevices { get; set; }
        [JsonIgnore]
        public List<GameCollections> Collections { get; set; }
    }

    public enum GameCategory
    {
        ClassicSlots,
        VideoSlots,
        Roulette,
        LiveRoulette,
        Poker,
        Blackjack
    }

    public enum Devices
    {
        Desktop,
        Mobile
    }
}
