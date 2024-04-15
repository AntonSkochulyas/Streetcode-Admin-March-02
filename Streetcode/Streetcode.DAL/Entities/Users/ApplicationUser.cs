using Microsoft.AspNetCore.Identity;

namespace Streetcode.DAL.Entities.Users
{
    public class ApplicationUser : IdentityUser<string>
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }
    }
}
