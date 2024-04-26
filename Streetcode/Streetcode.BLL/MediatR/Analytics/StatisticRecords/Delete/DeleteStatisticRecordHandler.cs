// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necssary namespaces.
namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete
{
    /// <summary>
    /// Handler, thar handler a deleting process of statistic record by given id.
    /// </summary>
    public class DeleteStatisticRecordHandler : IRequestHandler<DeleteStatisticRecordCommand, Result<StatisticRecordDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public DeleteStatisticRecordHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that deletes a statistic record by given id.
        /// </summary>
        /// <param name="request">
        /// Request with statistic record id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A StatisticRecordDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<StatisticRecordDto>> Handle(DeleteStatisticRecordCommand request, CancellationToken cancellationToken)
        {
            var findedStatisticRecordToDelete = await _repositoryWrapper.StatisticRecordRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id);

            if (findedStatisticRecordToDelete is null)
            {
                return Result.Fail(new Error(string.Format(StatisticRecordsErrors.DeleteStatisticRecordHandlerCanNotFindWithGivenIdError, request.Id)));
            }

            _repositoryWrapper.StatisticRecordRepository.Delete(findedStatisticRecordToDelete);

            var isDeletedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isDeletedSuccessfully)
            {
                return Result.Ok(_mapper.Map<StatisticRecordDto>(findedStatisticRecordToDelete));
            }

            return Result.Fail(new Error(StatisticRecordsErrors.DeleteStatisticRecordHandlerFailedToDeleteError));
        }
    }
}
