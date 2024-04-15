using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Users;

namespace Streetcode.BLL.MediatR.Users.UsersAddittionalInfoes.GetById
{
    public record GetByIdUserAdditionalInfoQeury(int Id)
        : IRequest<Result<UserAdditionalInfoDto>>;
}
