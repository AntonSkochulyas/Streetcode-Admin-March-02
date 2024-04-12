// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.AdditionalContent;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.AdditionalContent.Tag.GetById;

/// <summary>
/// Query, that requests handler to find a tag by given id.
/// </summary>
/// <param name="Id">
/// Id to finding a tag.
/// </param>
public record GetTagByIdQuery(int Id) : IRequest<Result<TagDto>>;
