// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Entities.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Create
{
    /// <summary>
    /// Handler, that creates a statistic record.
    /// </summary>
    public class CreateStatisticRecordHandler : IRequestHandler<CreateStatisticRecordCommand, Result<StatisticRecordDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Constructor
        public CreateStatisticRecordHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that handles a process of creating a statistic record.
        /// </summary>
        /// <param name="request">
        /// Request with new statistic record.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A StatisticRecordDto, or error, if it was while creating process.
        /// </returns>
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
