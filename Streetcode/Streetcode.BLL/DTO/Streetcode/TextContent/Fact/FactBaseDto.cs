namespace Streetcode.BLL.Dto.Streetcode.TextContent.Fact;

public class FactBaseDto
{
    public string Title { get; set; }
    public int ImageId { get; set; }
    public string? ImageDescription { get; set; }
    public string FactContent { get; set; }
    public int StreetcodeId { get; set; }
    public int? OrderNumber { get; set; }
}
