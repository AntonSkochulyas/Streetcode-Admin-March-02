using System.ComponentModel.DataAnnotations.Schema;
using Streetcode.DAL.Entities.Users;

namespace Streetcode.DAL.Entities.Authentication
{
    // TODO: Develop configuration
    [Table("refresh_tokens", Schema = "users")]
    public class RefreshToken
    {
        public string? RefreshTokens { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
