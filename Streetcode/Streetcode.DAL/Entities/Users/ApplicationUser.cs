using Microsoft.AspNetCore.Identity;

namespace Streetcode.DAL.Entities.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
