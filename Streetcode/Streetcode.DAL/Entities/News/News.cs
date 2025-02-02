﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Streetcode.DAL.Entities.Media.Images;

namespace Streetcode.DAL.Entities.News
{
    [Table("news", Schema = "news")]
    [Microsoft.EntityFrameworkCore.Index(nameof(URL), IsUnique = true)]
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Text { get; set; }

        public string? URL { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
