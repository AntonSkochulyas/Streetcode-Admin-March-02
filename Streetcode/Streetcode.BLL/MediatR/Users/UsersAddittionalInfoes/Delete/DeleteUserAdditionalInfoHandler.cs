// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Users.UserAdditionalInfoes;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a user additional info.
    /// </summary>
    public class DeleteUserAdditionalInfoHandler : IRequestHandler<DeleteUserAdditionalInfoCommand, Result<UserAdditionalInfoDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that deletes a user additional info.
        /// </summary>
        /// <param name="request">
        /// Request with user additional info id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A UserAdditionalInfoDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<UserAdditionalInfoDto>> Handle(DeleteUserAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            var foundUserAdditionalInfo = await _repositoryWrapper.UserAdditionalInfoRepository.GetItemBySpecAsync(new GetByIdUserAdditionalInfoSpec(request.Id));

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
