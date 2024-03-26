using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineCasino.Models;

namespace OnlineCasino.DatabaseContext.Entities
{
    public class GameCollections
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int DisplayIndex { get; set; }
        public int? ParentCollectionId { get; set; }
        public List<Game> Games { get; set; }
        public List<GameCollections> SubCollections { get; set; }

    }
}
