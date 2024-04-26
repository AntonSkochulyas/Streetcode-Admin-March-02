using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.Dto.Users.UserRegisterModel
{
    public class ApplicationUserDto
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int UserAdditionalInfoId { get; set; }
    }
}
