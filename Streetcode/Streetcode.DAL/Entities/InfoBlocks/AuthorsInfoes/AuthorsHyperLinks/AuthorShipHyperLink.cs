using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorShipHyperLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? URL { get; set; }
        public int AuthorShipId { get; set; }
        public AuthorShip? AuthorShip { get; set; }
    }
}
