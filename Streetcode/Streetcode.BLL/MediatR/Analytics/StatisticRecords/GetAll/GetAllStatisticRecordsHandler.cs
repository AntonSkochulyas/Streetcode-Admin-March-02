// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.GetAll
{
    /// <summary>
    /// Handler, that handles a get of all statistic records from database.
    /// </summary>
    internal class GetAllStatisticRecordsHandler : IRequestHandler<GetAllStatisticRecordsQuery, Result<IEnumerable<StatisticRecordDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public GetAllStatisticRecordsHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that get all statistic records from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all statistic records from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of StatisticRecordDto, or error, if it was, while getting process.
        /// </returns>
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
