using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetcode.DAL.Entities.Users
{
    public class UserRegisterModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Login is required")]
        [StringLength(32, MinimumLength = 6, ErrorMessage = "Login must be between 6 and 32 characters")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? Role { get; set; } = UserRoles.User;

        //public int UserAdditionalInfoId { get; set; }

        //public UserAdditionalInfo? UserAdditionalInfo { get; set; }
    }
}
