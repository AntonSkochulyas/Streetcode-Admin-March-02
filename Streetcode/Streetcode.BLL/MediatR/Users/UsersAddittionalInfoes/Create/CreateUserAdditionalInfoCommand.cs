using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create
{
    public record CreateUserAdditionalInfoCommand(UserAdditionalInfoDto UserAdditionalInfoDto)
        : IRequest<Result<UserAdditionalInfoDto>>;
}
