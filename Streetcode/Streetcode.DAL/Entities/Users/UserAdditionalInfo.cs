using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetcode.DAL.Entities.Users
{
    [Table("UserAdditionalInfo", Schema = "UserAdditionalInfo")]
    public class UserAdditionalInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\+?(\d{10,13})$", ErrorMessage = "Invalid phone number format")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(32, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 32 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Second name is required")]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Second name must be between 5 and 32 characters")]
        public string? SecondName { get; set; }

        [Required(ErrorMessage = "Third name is required")]
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Third name must be between 5 and 32 characters")]
        public string? ThirdName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(32, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 32 characters")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(14, 99, ErrorMessage = "Age must be between 14 and 99")]
        public ushort Age { get; set; }
    }
}
