using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.Dto.InfoBlocks
{
    public class InfoBlockDto
    {
        private string? _videoURL;
        private AuthorShip? _authorShip;

        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int? AuthorShipId { get; set; }

        public string? VideoURL
        {
            get
            {
                return _videoURL;
            }
            set
            {
                if (IsValidYouTubeUrl(value))
                {
                    _videoURL = value;
                }
                else
                {
                    throw new ArgumentException("Invalid YouTube URL");
                }
            }
        }

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

        private bool IsValidYouTubeUrl(string url)
        {
            Uri? uriResult;
            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                              && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                              && uriResult.Host == "www.youtube.com";

            return isValidUrl;
        }
    }
}
