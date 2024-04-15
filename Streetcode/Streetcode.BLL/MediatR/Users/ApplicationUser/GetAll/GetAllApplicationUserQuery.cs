using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users.UserRegisterModel;

namespace Streetcode.BLL.MediatR.Users.User.GetAll
{
    public record GetAllApplicationUserQuery()
        : IRequest<Result<IEnumerable<ApplicationUserDto>>>;
}
