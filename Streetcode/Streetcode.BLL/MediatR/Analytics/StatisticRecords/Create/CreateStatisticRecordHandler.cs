using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    public class CreateStatisticRecordHandler : IRequestHandler<CreateStatisticRecordCommand, Result<StatisticRecordDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateStatisticRecordHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<StatisticRecordDto>> Handle(CreateStatisticRecordCommand request, CancellationToken cancellationToken)
        {
            var streetcodeId = request.StatisticRecordDto.StreetcodeId;
            var statisticRecords = await _repositoryWrapper.StatisticRecordRepository.GetAllAsync();

            var isUniqueQrId = statisticRecords.FirstOrDefault(x => x.StreetcodeId == streetcodeId);

            if (isUniqueQrId != null)
            {
                return Result.Fail(new Error(StatisticRecordsErrors.CreateStatisticRecordHandlerQrIdShoulBeUniqueError));
            }

            var mappedStatisticRecord = _mapper.Map<StatisticRecord>(request.StatisticRecordDto);

            if (mappedStatisticRecord is null)
            {
                return Result.Fail(new Error(StatisticRecordsErrors.CreateStatisticRecordHandlerCanNotMapError));
            }

            var createdStatisticRecord = _repositoryWrapper.StatisticRecordRepository.Create(mappedStatisticRecord);

            var isCreatedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isCreatedSuccessfully)
            {
                return Result.Ok(_mapper.Map<StatisticRecordDto>(createdStatisticRecord));
            }

            return Result.Fail(new Error(StatisticRecordsErrors.CreateStatisticRecordHandlerFailedToCreateError));
        }
    }
}
