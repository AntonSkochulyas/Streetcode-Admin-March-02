using Streetcode.DAL.Entities.Users;

namespace Streetcode.BLL.Dto.Users
{
    public class RegisterModelDto
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; } = UserRoles.User;

        public int UserAdditionalInfoId { get; set; }
    }
}
