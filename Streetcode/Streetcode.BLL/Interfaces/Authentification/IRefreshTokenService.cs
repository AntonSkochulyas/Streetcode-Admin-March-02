using Streetcode.DAL.Entities.Authentication;

namespace Streetcode.BLL.Interfaces.Authentification;

public interface IRefreshTokenService
{
    public Task<RefreshToken?> SaveRefreshToken(string token, string userId);

    public Task<RefreshToken?> FindRefreshToken(string token, string userId);

    public Task<RefreshToken?> UpdateRefreshToken(string tokenOld, string tokenNew, string userId);

    public Task<RefreshToken?> DeleteRefreshToken(string token, string userId);

    public Task DeleteAllRefreshTokens();
}
