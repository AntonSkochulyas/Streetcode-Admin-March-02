using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.Dto.InfoBlocks
{
    public class InfoBlockDto
    {
        private AuthorShip? _authorShip;

        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int? AuthorShipId { get; set; }
        public string? VideoURL { get; set; }

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
