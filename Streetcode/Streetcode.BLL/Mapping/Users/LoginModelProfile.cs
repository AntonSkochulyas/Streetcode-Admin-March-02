using AutoMapper;
using Streetcode.BLL.Dto.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    internal class LoginModelProfile : Profile
    {
        public LoginModelProfile()
        {
            CreateMap<LoginModelDto, LoginModel>().ReverseMap();
        }
    }
}
