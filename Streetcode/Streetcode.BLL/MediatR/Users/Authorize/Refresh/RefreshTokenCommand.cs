using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Authentication;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Refresh
{
    public record RefreshTokenCommand(TokenDto TokenModelDto)
        : IRequest<Result<TokenDto>>;
}
