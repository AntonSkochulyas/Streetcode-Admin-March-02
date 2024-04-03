using AutoMapper;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;

namespace Streetcode.BLL.Mapping.InfoBlocks.AuthorsInfoes
{
    public class AuthorShipProfile : Profile
    {
        public AuthorShipProfile()
        {
            CreateMap<AuthorShip, AuthorShipDto>().ReverseMap();
        }
    }
}
