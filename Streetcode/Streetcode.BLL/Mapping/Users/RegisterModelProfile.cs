using AutoMapper;
using Streetcode.BLL.Dto.Users;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Mapping.Users
{
    internal class RegisterModelProfile : Profile
    {
        public RegisterModelProfile()
        {
            CreateMap<RegisterModel, RegisterModelDto>().ReverseMap();
        }
    }
}
