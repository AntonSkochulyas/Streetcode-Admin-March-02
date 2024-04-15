using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users.UserRegisterModel;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.User.GetAll
{
    public class GetAllApplicationUserHandler : IRequestHandler<GetAllApplicationUserQuery, Result<IEnumerable<ApplicationUserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllApplicationUserHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<ApplicationUserDto>>> Handle(GetAllApplicationUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _repositoryWrapper.ApplicationUserRepository.GetAllAsync();

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
