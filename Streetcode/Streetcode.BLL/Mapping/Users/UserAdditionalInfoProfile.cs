using AutoMapper;
using Streetcode.BLL.Dto.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    public class UserAdditionalInfoProfile : Profile
    {
        public UserAdditionalInfoProfile()
        {
            CreateMap<UserAdditionalInfo, UserAdditionalInfoDto>().ReverseMap();
        }
    }
}
