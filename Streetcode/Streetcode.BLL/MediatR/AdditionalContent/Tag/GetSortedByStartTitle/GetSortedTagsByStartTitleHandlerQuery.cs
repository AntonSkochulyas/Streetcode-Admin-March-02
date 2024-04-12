// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetSortedByStartTitle;

/// <summary>
/// Query, that requests a handler to sort a tags by start title.
/// </summary>
public record GetSortedTagsByStartTitleHandlerQuery : IRequest<Result<IEnumerable<TagDto>>>
{
    // Constructor 
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

    // Count to take
    public int Take { get; set; } = 10;

    // Start title
    public string StartsWithTitle { get; set; } = string.Empty;
}
