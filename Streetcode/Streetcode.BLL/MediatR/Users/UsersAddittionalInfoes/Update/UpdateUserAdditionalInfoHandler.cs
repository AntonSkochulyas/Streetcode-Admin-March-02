using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Update
{
    public class UpdateUserAdditionalInfoHandler : IRequestHandler<UpdateUserAdditionalInfoCommand, Result<UserAdditionalInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateUserAdditionalInfoHandler(IMapper mapper, ILoggerService logger, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Result<UserAdditionalInfoDto>> Handle(UpdateUserAdditionalInfoCommand request, CancellationToken cancellationToken)
        {
            var updatedUserAdditionalInfo = _mapper.Map<UserAdditionalInfo>(request.UserAdditionalInfoDto);

            if (updatedUserAdditionalInfo == null)
            {
                _logger.LogError(request, "Can not map a user additional info (from request).");
                return Result.Fail("Can not map a user additional info (from request).");
            }

            var userAdditionalInfoToUpdate = await _repositoryWrapper.UserAdditionalInfoRepository.GetFirstOrDefaultAsync(x => x.Id == updatedUserAdditionalInfo.Id);

            if (userAdditionalInfoToUpdate == null)
            {
                _logger.LogError(request, $"Can not find a user additional info to update with given id: {updatedUserAdditionalInfo.Id}.");
                return Result.Fail($"Can not find a user additional info to update with given id: {updatedUserAdditionalInfo.Id}.");
            }

            Update(userAdditionalInfoToUpdate, updatedUserAdditionalInfo);

            _repositoryWrapper.UserAdditionalInfoRepository.Update(userAdditionalInfoToUpdate);

            var isUpdatedSuccessfully = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (isUpdatedSuccessfully)
            {
                var responseUserAdditionalInfo = _mapper.Map<UserAdditionalInfoDto>(updatedUserAdditionalInfo);

                if(responseUserAdditionalInfo == null)
                {
                    _logger.LogError(request, "Can not map a user additional info (to response).");
                    return Result.Fail("Can not map a user additional info (to response).");
                }

                return Result.Ok(responseUserAdditionalInfo);
            }

            return Result.Fail("Can not update a user additional info.");
        }

        private static void Update(UserAdditionalInfo userAdditionalInfoToUpdate, UserAdditionalInfo userAdditionalInfoUpdated)
        {
            userAdditionalInfoToUpdate.Phone = userAdditionalInfoUpdated.Phone;
            userAdditionalInfoToUpdate.FirstName = userAdditionalInfoUpdated.FirstName;
            userAdditionalInfoToUpdate.SecondName = userAdditionalInfoUpdated.SecondName;
            userAdditionalInfoToUpdate.ThirdName = userAdditionalInfoUpdated.ThirdName;
            userAdditionalInfoToUpdate.Email = userAdditionalInfoUpdated.Email;
        }

    }
}
