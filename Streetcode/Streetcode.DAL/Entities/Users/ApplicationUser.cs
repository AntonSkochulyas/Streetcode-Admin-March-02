using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Streetcode.DAL.Entities.Users
{
    [Table("application_users", Schema = "users")]
    public class ApplicationUser : IdentityUser
    {
        public int UserAdditionalInfoId { get; set; }
        public UserAdditionalInfo? UserAdditionalInfo { get; set; }
    }
}
