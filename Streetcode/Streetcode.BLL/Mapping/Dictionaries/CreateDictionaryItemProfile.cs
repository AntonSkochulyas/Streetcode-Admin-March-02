using AutoMapper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.DAL.Entities.Dictionaries;

namespace Streetcode.BLL.Mapping.Dictionaries
{
    internal class CreateDictionaryItemProfile : Profile
    {
        public CreateDictionaryItemProfile()
        {
            CreateMap<DictionaryItem, CreateDictionaryItemDto>().ReverseMap();
        }
    }
}
