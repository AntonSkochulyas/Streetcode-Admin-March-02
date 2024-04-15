// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetByStreetcodeId;

/// <summary>
/// Query, that requests a handler to find a tag by given title.
/// </summary>
/// <param name="Title">
/// Title to find a tag.
/// </param>
public record GetTagByTitleQuery(string Title) : IRequest<Result<TagDto>>;
