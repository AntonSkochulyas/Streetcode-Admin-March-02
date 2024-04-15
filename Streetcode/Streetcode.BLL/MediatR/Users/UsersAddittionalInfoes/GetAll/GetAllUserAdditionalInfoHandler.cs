using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetAll
{
    internal class GetAllUserAdditionalInfoHandler : IRequestHandler<GetAllUserAdditionalInfoQuery, Result<IEnumerable<UserAdditionalInfoDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<IEnumerable<UserAdditionalInfoDto>>> Handle(GetAllUserAdditionalInfoQuery request, CancellationToken cancellationToken)
        {
            var userAdditionalInfoes = await _repositoryWrapper.UserAdditionalInfoRepository.GetAllAsync();

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
