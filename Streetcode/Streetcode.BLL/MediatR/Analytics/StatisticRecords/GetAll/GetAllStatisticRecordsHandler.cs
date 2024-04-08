using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.GetAll
{
    internal class GetAllStatisticRecordsHandler : IRequestHandler<GetAllStatisticRecordsQuery, Result<IEnumerable<StatisticRecordDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllStatisticRecordsHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<StatisticRecordDto>>> Handle(GetAllStatisticRecordsQuery request, CancellationToken cancellationToken)
        {
            var statisticRecords = await _repositoryWrapper.StatisticRecordRepository.GetAllAsync();

            if(statisticRecords is null)
            {
                return Result.Fail(new Error(StatisticRecordsErrors.GetAllStatisticRecordsHandlerCanNotGetAnyError));
            }

            return Result.Ok(_mapper.Map<IEnumerable<StatisticRecordDto>>(statisticRecords));
        }
    }
}
