using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Dto.Partners;

namespace Streetcode.BLL.Dto.Partners;

public class PartnerSourceLinkDto
{
    public int Id { get; set; }
    public LogoTypeDto LogoType { get; set; }
    public UrlDto? TargetUrl { get; set; }
}