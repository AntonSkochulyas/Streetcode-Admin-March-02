using AutoMapper;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Entities.Analytics;

namespace Streetcode.BLL.Mapping.Analytics
{
    internal class StatisticRecordProfile : Profile
    {
        public StatisticRecordProfile()
        {
            CreateMap<StatisticRecord, StatisticRecordDto>().ReverseMap();
        }
    }
}
