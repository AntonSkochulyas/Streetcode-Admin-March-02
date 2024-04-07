using AutoMapper;
using Streetcode.BLL.Dto.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAdditionalInfo, UserLoginDto>().ReverseMap();
            CreateMap<UserDto, UserLoginDto>().ReverseMap();
            CreateMap<UserAdditionalInfo, UserDto>().ReverseMap();
        }
    }
}
