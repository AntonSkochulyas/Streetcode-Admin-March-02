namespace Streetcode.BLL.Dto.Sources
{
  public class CategoryContentCreatedDto
  {
    public int? Id { get; set; }
    public int SourceLinkCategoryId { get; set; }
    public string? Text { get; set; }
    public int StreetcodeId { get; set; }
  }
}
