using AutoMapper;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.Mapping.Users
{
    internal class TokenModelProfile : Profile
    {
        public TokenModelProfile()
        {
            CreateMap<TokenModel, TokenModelDto>().ReverseMap();
        }
    }
}
