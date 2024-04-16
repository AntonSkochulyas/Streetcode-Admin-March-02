using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Register.RegisterAdmin
{
    public record RegisterAdminCommand(RegisterModelDto RegisterModelDto)
        : IRequest<Result<Response>>;
}
