using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Refresh
{
    public record RefreshTokenCommand(TokenModelDto TokenModelDto)
        : IRequest<Result<TokenModelDto>>;
}
