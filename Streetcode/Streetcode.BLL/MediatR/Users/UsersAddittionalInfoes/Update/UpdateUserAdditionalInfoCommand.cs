using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Update
{
    public record UpdateUserAdditionalInfoCommand(UserAdditionalInfoDto UserAdditionalInfoDto)
        : IRequest<Result<UserAdditionalInfoDto>>;
}
