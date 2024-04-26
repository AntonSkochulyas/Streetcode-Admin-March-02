// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.Delete
{
    /// <summary>
    /// Query, that requests a handler to delete a user additional info by given id.
    /// </summary>
    /// <param name="id">
    /// User additional info id to delete.
    /// </param>
    public record DeleteUserAdditionalInfoCommand(int Id) : IRequest<Result<UserAdditionalInfoDto>>;
}
