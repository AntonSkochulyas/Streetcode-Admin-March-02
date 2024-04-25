using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Authentication;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterUser
{
    public record RegisterCommand(RegisterModelDto RegisterModelDto)
        : IRequest<Result<TokenDto>>;
}
