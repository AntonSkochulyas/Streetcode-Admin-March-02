using FluentResults;
using Streetcode.BLL.Dto.Authentication;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Refresh
{
    public interface IRefreshTokenHandler
    {
        Task<Result<TokenDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken);
    }
}