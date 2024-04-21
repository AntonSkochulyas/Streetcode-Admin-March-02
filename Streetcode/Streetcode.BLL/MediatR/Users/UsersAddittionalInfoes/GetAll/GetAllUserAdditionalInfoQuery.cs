// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all users additional info from database.
    /// </summary>
    public record GetAllUserAdditionalInfoQuery
        : IRequest<Result<IEnumerable<UserAdditionalInfoDto>>>;
}
