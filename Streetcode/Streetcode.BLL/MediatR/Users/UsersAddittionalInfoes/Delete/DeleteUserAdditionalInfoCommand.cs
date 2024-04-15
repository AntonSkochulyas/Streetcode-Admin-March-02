using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Delete
{
    public record DeleteUserAdditionalInfoCommand(int Id) : IRequest<Result<UserAdditionalInfoDto>>;
}
