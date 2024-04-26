// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users.UserRegisterModel;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Users.User.GetAll
{
    /// <summary>
    /// Query, that requests a handler to get all application user from database.
    /// </summary>
    public record GetAllApplicationUserQuery()
        : IRequest<Result<IEnumerable<ApplicationUserDto>>>;
}
