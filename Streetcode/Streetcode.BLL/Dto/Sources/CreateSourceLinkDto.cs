﻿using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.BLL.Dto.Sources
{
    public class CreateSourceLinkDto
    {
        public string? Title { get; set; }

        public int ImageId { get; set; }
    }
}
