// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Users.UserAdditionalInfoes;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all users additional info from database.
    /// </summary>
    internal class GetAllUserAdditionalInfoHandler : IRequestHandler<GetAllUserAdditionalInfoQuery, Result<IEnumerable<UserAdditionalInfoDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that get all users additional info from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all users additional info from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of UserAdditionalInfoDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<UserAdditionalInfoDto>>> Handle(GetAllUserAdditionalInfoQuery request, CancellationToken cancellationToken)
        {
            var userAdditionalInfoes = await _repositoryWrapper.UserAdditionalInfoRepository.GetItemsBySpecAsync(new GetAllUserAdditionalInfoesSpec());

            if (userAdditionalInfoes == null)
            {
                _logger.LogError(request, "Can not get a user additional infoes.");
                return Result.Fail("Can not get a user additional infoes.");
            }

            var responseUserAdditionalInfoes = _mapper.Map<IEnumerable<UserAdditionalInfoDto>>(userAdditionalInfoes);

            if(responseUserAdditionalInfoes == null)
            {
                _logger.LogError(request, "Can not map a user additional infoes (to response).");
                return Result.Fail("Can not map a user additional infoes (to response).");
            }

            return Result.Ok(responseUserAdditionalInfoes);
        }
    }
}
