using Streetcode.BLL.Dto.AdditionalContent.Tag;
using Streetcode.DAL.Enums;

namespace Streetcode.BLL.Dto.Streetcode;

public class BaseStreetcodeDto
{
    public int Index { get; set; }
    public string? Title { get; set; }
    public string? DateString { get; set; }
    public string? ShortDescription { get; set; }
    public string? Alias { get; set; }
    public string? TransliterationUrl { get; set; }
    public int ImageBlackAndWhiteId { get; set; }
    public int ImageAnimatedId { get; set; }
    public int ImageForLinkId { get; set; }
    public string? InstagramARLink { get; set; }
    public string? InvolvedPeople { get; set; }
    public StreetcodeStatus Status { get; set; }
    public DateTime EventStartOrPersonBirthDate { get; set; }
    public DateTime? EventEndOrPersonDeathDate { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<StreetcodeTagDto>? Tags { get; set; }
    public string? Teaser { get; set; }
    public StreetcodeType StreetcodeType { get; set; }
}
