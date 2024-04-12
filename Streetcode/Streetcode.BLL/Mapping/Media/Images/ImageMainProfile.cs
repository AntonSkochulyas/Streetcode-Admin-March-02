using AutoMapper;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.BLL.Mapping.Media.Images;

public class ImageMainProfile : Profile
{
    public ImageMainProfile()
    {
        CreateMap<ImageMain, ImageMainDto>().ReverseMap();

        CreateMap<ImageFileBaseCreateDto, ImageMain>();
	}
}
