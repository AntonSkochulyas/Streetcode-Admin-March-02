using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Delete
{
    public class DeleteUserAdditionalInfoHandler : IRequestHandler<DeleteUserAdditionalInfoCommand, Result<UserAdditionalInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<UserAdditionalInfoDto>> Handle(DeleteUserAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            var foundUserAdditionalInfo = await _repositoryWrapper.UserAdditionalInfoRepository.GetFirstOrDefaultAsync(x => x.Id == request.Id);

            if (foundUserAdditionalInfo == null)
            {
                _logger.LogError(request, $"Can not find a user additional info with given id: {request.Id}");
                return Result.Fail($"Can not find a user additional info with given id: {request.Id}");
            }

            _repositoryWrapper.UserAdditionalInfoRepository.Delete(foundUserAdditionalInfo);

            var isDeletedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isDeletedSuccessfully)
            {
                var responseUserAdditionalInfo = _mapper.Map<UserAdditionalInfoDto>(foundUserAdditionalInfo);

                if (responseUserAdditionalInfo == null)
                {
                    _logger.LogError(request, "Can not map a user additional info (to response).");
                    return Result.Fail("Can not map a user additional info (to response).");
                }

                return Result.Ok(responseUserAdditionalInfo);
            }

            return Result.Fail("Can not delete a user additional info.");
        }
    }
}
