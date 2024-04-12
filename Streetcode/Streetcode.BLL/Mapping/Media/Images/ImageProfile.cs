using AutoMapper;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.BLL.Mapping.Media.Images;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ImageDto>().ReverseMap();

        CreateMap<ImageFileBaseCreateDto, Image>();
	}
}
