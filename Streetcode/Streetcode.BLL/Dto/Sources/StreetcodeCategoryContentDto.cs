﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.Dto.Sources
{
    public class StreetcodeCategoryContentDto
    {
        public string? Text { get; set; }

        public int SourceLinkCategoryId { get; set; }

        public int StreetcodeId { get; set; }
    }
}
