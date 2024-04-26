using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Enums;

namespace Streetcode.DAL.Entities.Timeline;

[Table("timeline_items", Schema = "streetcode")]
public class TimelineItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public DateViewPattern DateViewPattern { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
    public string? Context { get; set; }

    public int StreetcodeId { get; set; }
    public StreetcodeContent? Streetcode { get; set; }
}
