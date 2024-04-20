using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.MediatR.Users.Authorize
{
    public class Response
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
