using System.ComponentModel.DataAnnotations;

namespace Streetcode.DAL.Entities.Users
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
