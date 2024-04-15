using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create
{
    public class CreateUserAdditionalInfoHandler : IRequestHandler<CreateUserAdditionalInfoCommand, Result<UserAdditionalInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

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
