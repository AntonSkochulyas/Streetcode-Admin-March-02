using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Authentication;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeByUsername
{
    public record RevokeByToken(TokenDto TokenModelDto)
        : IRequest<Result<string>>;
}
