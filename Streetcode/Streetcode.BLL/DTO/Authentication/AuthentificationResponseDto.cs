﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.Dto
{
    public class AuthentificationResponseDto
    {
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
