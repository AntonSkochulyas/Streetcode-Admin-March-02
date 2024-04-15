using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streetcode.DAL.Entities.Media.Images;

public class ImageBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [NotMapped]
    public string? Base64 { get; set; }

    [Required]
    [MaxLength(100)]
    public string? BlobName { get; set; }

    [Required]
    [MaxLength(10)]
    public string? MimeType { get; set; }
}