using FluentResults;
using MediatR;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeAll
{
    public record RevokeAllCommand()
        : IRequest<Result<string>>;
}
