using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;

public record GetSortedTagsByStartTitleHandlerQuery : IRequest<Result<IEnumerable<TagDto>>>
{
    public GetSortedTagsByStartTitleHandlerQuery(int take, string title)
    {
        Take = take;
        Title = title;
    }

    public int Take { get; set; } = 10;
    public string Title { get; set; } = string.Empty;
}
