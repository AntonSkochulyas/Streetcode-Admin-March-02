// Necesasry usings
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Dto.AdditionalContent.Tag;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.Create
{
    /// <summary>
    /// Command, that request a handler to create a tag.
    /// </summary>
    /// <param name="tag">
    /// Tag to add in database.
    /// </param>
    public record CreateTagCommand(CreateTagDto tag) : IRequest<Result<TagDto>>;
}
