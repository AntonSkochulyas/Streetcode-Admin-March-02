using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Users.UserAdditionalInfoes;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetById
{
    public class GetByIdUserAdditionalInfoHandler : IRequestHandler<GetByIdUserAdditionalInfoQeury, Result<UserAdditionalInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetByIdUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<UserAdditionalInfoDto>> Handle(GetByIdUserAdditionalInfoQeury request, CancellationToken cancellationToken)
        {
            var foundUserAdditionalInfo = await _repositoryWrapper.UserAdditionalInfoRepository.GetItemBySpecAsync(new GetByIdUserAdditionalInfoSpec(request.Id));

            if (foundUserAdditionalInfo == null)
            {
                _logger.LogError(request, $"Can not find a user additional info with given id: {request.Id}");
                return Result.Fail($"Can not find a user additional info with given id: {request.Id}");
            }

            var responseUserAdditionalInfo = _mapper.Map<UserAdditionalInfoDto>(foundUserAdditionalInfo);

            if(responseUserAdditionalInfo == null)
            {
                _logger.LogError(request, "Can not map a user additional info (to response).");
                return Result.Fail("Can not map a user additional info (to response).");
            }

            return Result.Ok(responseUserAdditionalInfo);
        }
    }
}
