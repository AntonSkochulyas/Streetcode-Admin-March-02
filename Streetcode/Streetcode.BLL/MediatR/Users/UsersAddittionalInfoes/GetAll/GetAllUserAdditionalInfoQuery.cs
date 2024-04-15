using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetAll
{
    public record GetAllUserAdditionalInfoQuery
        : IRequest<Result<IEnumerable<UserAdditionalInfoDto>>>;
}
