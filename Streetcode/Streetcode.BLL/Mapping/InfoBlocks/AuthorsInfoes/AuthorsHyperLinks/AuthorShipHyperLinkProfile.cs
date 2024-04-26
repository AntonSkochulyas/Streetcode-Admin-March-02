using AutoMapper;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;

namespace Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks
{
    public class AuthorShipHyperLinkProfile : Profile
    {
        public AuthorShipHyperLinkProfile()
        {
            CreateMap<AuthorShipHyperLink, AuthorShipHyperLinkDto>().ReverseMap();

            CreateMap<AuthorShipHyperLinkCreateDto, AuthorShipHyperLink>();
        }
    }
}
