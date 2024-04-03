using AutoMapper;
using Streetcode.BLL.Dto.InfoBlocks;
using Streetcode.DAL.Entities.InfoBlocks;

namespace Streetcode.BLL.Mapping.InfoBlocks
{
    public class InfoBlockProfile : Profile
    {
        public InfoBlockProfile()
        {
            CreateMap<InfoBlock, InfoBlockDto>().ReverseMap();
        }
    }
}
