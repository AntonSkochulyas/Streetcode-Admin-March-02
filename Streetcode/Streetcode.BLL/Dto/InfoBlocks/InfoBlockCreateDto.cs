using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.Streetcode.TextContent;

namespace Streetcode.BLL.Dto.InfoBlocks
{
    public class InfoBlockCreateDto
    {
        private AuthorShip? _authorShip;

        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int? AuthorShipId { get; set; }
        public string? VideoURL { get; set; }
        public int TermId { get; set; }
        public Term? Term { get; set; }

        public AuthorShip? AuthorShip
        {
            get
            {
                return _authorShip;
            }
            set
            {
                if (value?.Text == "Текст підготовлений спільно з ")
                {
                    _authorShip = null;
                }
            }
        }
    }
}
