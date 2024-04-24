using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Login
{
    public record LoginCommand(LoginModelDto LoginModelDto)
        : IRequest<Result<TokenDto>>;
}
