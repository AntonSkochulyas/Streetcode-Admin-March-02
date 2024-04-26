using AutoMapper;
using Streetcode.BLL.Dto.Sources;
using Streetcode.DAL.Entities.Sources;

namespace Streetcode.BLL.Mapping.Sources;

public class SourceLinkSubCategoryProfile : Profile
{
    public SourceLinkSubCategoryProfile()
    {
        CreateMap<CategoryContentCreatedDto, StreetcodeCategoryContent>().ReverseMap();
    }
}
