using Streetcode.DAL.Entities.InfoBlocks;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes
{
    public class AuthorShipDto
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int AuthorShipHyperLinkId { get; set; }
        public List<AuthorShipHyperLink>? AuthorShipHyperLinks { get; set; }
        public List<InfoBlock>? InfoBlocks { get; set; }
    }
}
