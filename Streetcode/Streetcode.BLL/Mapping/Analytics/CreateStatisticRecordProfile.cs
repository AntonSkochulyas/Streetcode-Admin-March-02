using AutoMapper;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Entities.Analytics;

namespace Streetcode.BLL.Mapping.Analytics
{
    internal class CreateStatisticRecordProfile : Profile
    {
        public CreateStatisticRecordProfile()
        {
            CreateMap<StatisticRecord, CreateStatisticRecordDto>().ReverseMap();
        }
    }
}
