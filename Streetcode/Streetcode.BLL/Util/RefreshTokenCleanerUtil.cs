using Streetcode.BLL.Interfaces.Authentification;

namespace Streetcode.BLL.Util;

public class RefreshTokenCleanerUtil
{
    private readonly IRefreshTokenService _refreshTokenService;

    public RefreshTokenCleanerUtil(IRefreshTokenService refreshTokenService)
    {
        _refreshTokenService = refreshTokenService;
    }

    public async Task CleanExpiredRefreshTokens()
    {
        await _refreshTokenService.DeleteExpiredRefreshTokens();
    }
}
