using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;
using Streetcode.BLL.MediatR.Users.Authorize;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Login
{
    public record LoginCommand(LoginModelDto LoginModelDto)
        : IRequest<Result<Response>>;
}
