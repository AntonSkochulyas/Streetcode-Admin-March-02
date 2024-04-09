using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetSortedByStartTitle;

public record GetSortedTagsByStartTitleHandlerQuery : IRequest<Result<IEnumerable<TagDto>>>
{
    public GetSortedTagsByStartTitleHandlerQuery(string? startsWithTitle, int? take)
    {
        if(take is not null)
        {
            Take = (int)take;
        }

        if (startsWithTitle is not null)
        {
            StartsWithTitle = startsWithTitle;
        }
    }

    public int Take { get; set; } = 10;
    public string StartsWithTitle { get; set; } = string.Empty;
}
