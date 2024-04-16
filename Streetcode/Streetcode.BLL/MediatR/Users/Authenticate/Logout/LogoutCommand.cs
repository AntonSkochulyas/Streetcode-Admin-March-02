using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Logout
{
    public record LogoutCommand()
        : IRequest<Result<string>>;
}
