using Streetcode.DAL.Enums;

namespace Streetcode.BLL.Dto.Timeline;

public class TimelineItemDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Context { get; set; }
    public DateTime Date { get; set; }
    public DateViewPattern DateViewPattern { get; set; }
    public int StreetcodeId { get; set; }
}
