using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes
{
    public class AuthorShip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; } = "Текст підготовлений спільно з ";
        public int AuthorShipHyperLinkId { get; set; }
        public AuthorShipHyperLink? AuthorShipHyperLink { get; set; }
    }
}
