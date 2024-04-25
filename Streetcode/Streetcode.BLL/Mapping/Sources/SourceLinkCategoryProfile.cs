using AutoMapper;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;

namespace Streetcode.BLL.Mapping.Sources;

public class SourceLinkCategoryProfile : Profile
{
    public SourceLinkCategoryProfile()
    {
        CreateMap<SourceLinkCategory, SourceLinkCategoryDto>()
            .ForMember(dto => dto.Image, c => c.MapFrom(b => b.Image))
            .ReverseMap();
        CreateMap<SourceLinkCategory, CategoryWithNameDto>().ReverseMap();
        CreateMap<SourceLinkCategory, ImageDto>()
            .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.Image.MimeType))
            .ForMember(dest => dest.BlobName, opt => opt.MapFrom(src => src.Image.BlobName));
        CreateMap<SourceLinkCategoryDto, SourceLinkCategory>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(dto => dto.Title))
            .ForMember(dest => dest.ImageId, opt => opt.MapFrom(dto => dto.ImageId));

        CreateMap<StreetcodeCategoryContent, StreetcodeCategoryContentDto>().ReverseMap();
        CreateMap<StreetcodeContent, StreetcodeDto>().ReverseMap();
        CreateMap<CreateSourceLinkDto, SourceLinkCategory>().ReverseMap();
        CreateMap<SourceLinkResponseDto, SourceLinkCategory>().ReverseMap();
    }
}
