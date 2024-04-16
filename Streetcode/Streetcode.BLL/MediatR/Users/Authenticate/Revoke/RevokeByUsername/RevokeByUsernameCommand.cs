using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeByUsername
{
    public record RevokeByUsernameCommand(string Username)
        : IRequest<Result<string>>;
}
