﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetcode.DAL.Entities.Media.Images
{
    [Table("image_details", Schema = "media")]
    public class ImageDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string? Title { get; set; }

        [MaxLength(400)]
        public string? Alt { get; set; }

        public int? ImageId { get; set; }

        public Image? Image { get; set; }
    }
}
