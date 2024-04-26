// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Create
{
    /// <summary>
    /// Command, that requests a handler to create a new user additional info.
    /// </summary>
    /// <param name="newUsersAddittionalInfoes">
    /// New user additional info.
    /// </param>
    public record CreateUserAdditionalInfoCommand(UserAdditionalInfoDto UserAdditionalInfoDto)
        : IRequest<Result<UserAdditionalInfoDto>>;
}
