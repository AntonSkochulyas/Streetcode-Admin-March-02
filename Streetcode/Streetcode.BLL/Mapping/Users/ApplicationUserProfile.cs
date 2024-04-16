using AutoMapper;
using Streetcode.BLL.Dto.Users.UserRegisterModel;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users.ApplicationUserProfile
{
    internal class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        }
    }
}
