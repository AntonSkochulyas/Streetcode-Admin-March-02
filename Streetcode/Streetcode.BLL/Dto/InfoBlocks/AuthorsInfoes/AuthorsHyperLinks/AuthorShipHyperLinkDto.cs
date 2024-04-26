using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorShipHyperLinkDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? URL { get; set; }
        public int AuthorShipId { get; set; }
        public AuthorShip? AuthorShip { get; set; }
    }
}
