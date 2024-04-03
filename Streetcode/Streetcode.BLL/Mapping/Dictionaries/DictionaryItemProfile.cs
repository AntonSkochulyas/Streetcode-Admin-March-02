using AutoMapper;
using Streetcode.BLL.Dto.Dictionaries;
using Streetcode.DAL.Entities.Dictionaries;

namespace Streetcode.BLL.Mapping.Dictionaries
{
    public class DictionaryItemProfile : Profile
    {
        public DictionaryItemProfile()
        {
            CreateMap<DictionaryItem, DictionaryItemDto>().ReverseMap();
        }
    }
}
