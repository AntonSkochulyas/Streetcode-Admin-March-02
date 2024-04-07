using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Analytics;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Analytics.StatisticRecords.Delete
{
    public class DeleteStatisticRecordHandler : IRequestHandler<DeleteStatisticRecordCommand, Result<StatisticRecordDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;


        public DeleteStatisticRecordHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

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
