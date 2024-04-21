// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new user additional info.
    /// </summary>
    public class CreateUserAdditionalInfoHandler : IRequestHandler<CreateUserAdditionalInfoCommand, Result<UserAdditionalInfoDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Parametric constructor
        public CreateUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Method, that creates a new user additional info.
        /// </summary>
        /// <param name="request">
        /// Request with a new user additional info.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A UserAdditionalInfoDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<UserAdditionalInfoDto>> Handle(CreateUserAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            var requestUserAdditionalInfo = request.UserAdditionalInfoDto;

            var isAlreadyCreated = await IsAlreadyCreatedAsync(requestUserAdditionalInfo.Phone, requestUserAdditionalInfo.Email);

            if (isAlreadyCreated)
            {
                _logger.LogError(request, $"User additional info already exists with phone number - {requestUserAdditionalInfo.Phone}, or email - {requestUserAdditionalInfo.Email}.");
                return Result.Fail($"User additional info already exists with phone number - {requestUserAdditionalInfo.Phone}, or email - {requestUserAdditionalInfo.Email}.");
            }

            var mappedUserAdditionalInfo = _mapper.Map<UserAdditionalInfo>(requestUserAdditionalInfo);

            if(mappedUserAdditionalInfo == null)
            {
                _logger.LogError(request, "Can not map a user additional info (from request).");
                return Result.Fail("Can not map a user additional info (from request).");
            }

            await _repositoryWrapper.UserAdditionalInfoRepository.CreateAsync(mappedUserAdditionalInfo);

            var isCreatedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isCreatedSuccessfully)
            {
                var responseUserAdditionalInfo = _mapper.Map<UserAdditionalInfoDto>(mappedUserAdditionalInfo);

                if(responseUserAdditionalInfo == null)
                {
                    _logger.LogError(request, "Can not map a user additional info (to response).");
                    return Result.Fail("Can not map a user additional info (to response).");
                }

                return Result.Ok(responseUserAdditionalInfo);
            }

            _logger.LogError(request, "Can not create a user additional info.");
            return Result.Fail("Can not create a user additional info.");
        }

        /// <summary>
        /// Checks if a user additional information record with the provided phone number or email already exists.
        /// </summary>
        /// <param name="phone">The phone number to check.</param>
        /// <param name="email">The email address to check.</param>
        /// <returns>
        /// True if a user additional information record already exists with the provided phone number or email, otherwise false.
        /// </returns>
        private async Task<bool> IsAlreadyCreatedAsync(string phone, string email)
        {
            var userAdditionalInfo = await _repositoryWrapper.UserAdditionalInfoRepository.
                GetFirstOrDefaultAsync(x => x.Phone.Equals(phone) || x.Email.Equals(email));

            if (userAdditionalInfo != null)
            {
                return true;
            }

            return false;
        }
    }
}
