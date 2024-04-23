using Microsoft.AspNetCore.Identity;
using Streetcode.BLL.Interfaces.Authentification;
using Streetcode.DAL.Entities.Authentication;
using Streetcode.DAL.Entities.Users;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.Services.Authorization;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IJwtService _jwtService;

    public RefreshTokenService(IRepositoryWrapper repositoryWrapper, UserManager<ApplicationUser> userManager, IJwtService jwtService)
    {
        _repositoryWrapper = repositoryWrapper;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<RefreshToken?> SaveRefreshToken(string token, string userId)
    {
        var refreshToken = new RefreshToken
        {
            RefreshTokens = token,
            RefreshTokenExpiryTime = _jwtService.GetRefreshTokenExpiryTimeFromNow(),
            ApplicationUserId = userId
        };

        refreshToken = await _repositoryWrapper.RefreshTokenRepository.CreateAsync(refreshToken);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (!resultIsSuccess)
        {
            return null;
        }

        return refreshToken;
    }

    public async Task<RefreshToken?> FindRefreshToken(string token, string userId)
    {
        var refreshToken = await _repositoryWrapper.RefreshTokenRepository
            .GetFirstOrDefaultAsync(r => r.RefreshTokens == token && r.ApplicationUserId == userId);

        return refreshToken;
    }

    public async Task<RefreshToken?> UpdateRefreshToken(string tokenOld, string tokenNew, string userId)
    {
        var refreshToken = await _repositoryWrapper.RefreshTokenRepository
            .GetFirstOrDefaultAsync(r => r.RefreshTokens == tokenOld && r.ApplicationUserId == userId);

        if (refreshToken == null)
        {
            return null;
        }

        if(refreshToken.RefreshTokenExpiryTime < DateTime.Now)
        {
            return null;
        }

        refreshToken.RefreshTokens = tokenNew;
        refreshToken.RefreshTokenExpiryTime = _jwtService.GetRefreshTokenExpiryTimeFromNow();

        _repositoryWrapper.RefreshTokenRepository.Update(refreshToken);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (!resultIsSuccess)
        {
            return null;
        }

        return refreshToken;
    }

    public async Task<RefreshToken?> DeleteRefreshToken(string token, string userId)
    {
        var refreshToken = await _repositoryWrapper.RefreshTokenRepository
            .GetFirstOrDefaultAsync(r => r.RefreshTokens == token && r.ApplicationUserId == userId);

        if (refreshToken == null)
        {
            return null;
        }

        _repositoryWrapper.RefreshTokenRepository.Delete(refreshToken);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
        if (!resultIsSuccess)
        {
            return null;
        }

        return refreshToken;
    }

    public async Task DeleteAllRefreshTokens()
    {
        _repositoryWrapper.RefreshTokenRepository.DeleteRange(await _repositoryWrapper.RefreshTokenRepository.GetAllAsync());
    }
}
