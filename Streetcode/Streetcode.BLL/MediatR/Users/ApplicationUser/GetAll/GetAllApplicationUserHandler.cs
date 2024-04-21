// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users.UserRegisterModel;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Users.ApplicationUser;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.User.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all application user from database.
    /// </summary>
    public class GetAllApplicationUserHandler : IRequestHandler<GetAllApplicationUserQuery, Result<IEnumerable<ApplicationUserDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllApplicationUserHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that get all application user from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all application user from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of ApplicationUserDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<ApplicationUserDto>>> Handle(GetAllApplicationUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _repositoryWrapper.ApplicationUserRepository.GetItemsBySpecAsync(new GetAllApplicationUsersSpec());

            if (users == null)
            {
                _logger.LogError(request, "Can not get users from database.");
                return Result.Fail("Can not get users from database.");
            }

            var responseUsers = _mapper.Map<IEnumerable<ApplicationUserDto>>(users);
            if (responseUsers == null)
            {
                _logger.LogError(request, "Can not map users (to response).");
                return Result.Fail("Can not map users (to response).");
            }

            return Result.Ok(responseUsers);
        }
    }
}
