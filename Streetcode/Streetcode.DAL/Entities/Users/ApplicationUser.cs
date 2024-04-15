using Microsoft.AspNetCore.Identity;

namespace Streetcode.DAL.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int UserAdditionalInfoId { get; set; }
        public UserAdditionalInfo? UserAdditionalInfo { get; set; }
    }
}
