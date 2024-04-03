using AutoMapper;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.DAL.Entities.InfoBlocks.Articles;

namespace Streetcode.BLL.Mapping.InfoBlocks.Articles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
        }
    }
}
