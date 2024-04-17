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
        public List<AuthorShipHyperLink>? AuthorShipHyperLinks { get; set; }
        public List<InfoBlock>? InfoBlocks { get; set; }
    }
}
