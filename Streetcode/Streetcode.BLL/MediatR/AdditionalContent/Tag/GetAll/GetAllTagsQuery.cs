// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetAll;

/// <summary>
/// Query, that requests handler to get all tags from database.
/// </summary>
public record GetAllTagsQuery : IRequest<Result<IEnumerable<TagDto>>>;
