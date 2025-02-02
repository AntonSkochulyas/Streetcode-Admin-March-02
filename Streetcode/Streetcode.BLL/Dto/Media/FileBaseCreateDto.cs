﻿using System.ComponentModel.DataAnnotations;

namespace Streetcode.BLL.Dto.Media;

public class FileBaseCreateDto
{
    [MaxLength(150)]
    public string? Title { get; set; }
    public string? BaseFormat { get; set; }
    public string? MimeType { get; set; }
    public string? Extension { get; set; }
}
