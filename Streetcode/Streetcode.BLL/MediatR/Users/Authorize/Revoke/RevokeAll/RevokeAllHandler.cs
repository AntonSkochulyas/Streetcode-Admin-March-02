using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Authentification;

namespace Streetcode.BLL.MediatR.Users.Authenticate.Revoke.RevokeAll
{
    public class RevokeAllHandler : IRequestHandler<RevokeAllCommand, Result<string>>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RevokeAllHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Result<string>> Handle(RevokeAllCommand request, CancellationToken cancellationToken)
        {
            await _refreshTokenService.DeleteAllRefreshTokens();

            return Result.Ok("Revoked successfully.");
        }
    }
}
