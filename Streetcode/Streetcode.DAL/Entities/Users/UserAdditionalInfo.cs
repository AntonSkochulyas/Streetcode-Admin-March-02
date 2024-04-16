using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetcode.DAL.Entities.Users
{
    [Table("UserAdditionalInfo", Schema = "Users")]
    public class UserAdditionalInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Phone { get; set; }

        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? ThirdName { get; set; }

        public string? Email { get; set; }

        public ushort Age { get; set; }
    }
}
