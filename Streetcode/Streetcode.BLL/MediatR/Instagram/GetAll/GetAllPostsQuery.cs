// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.DAL.Entities.Instagram;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Instagram.GetAll;

/// <summary>
/// Query, that requests a handler to get all posts from database.
/// </summary>
public record GetAllPostsQuery : IRequest<Result<IEnumerable<InstagramPost>>>
{
    public GetAllPostsQuery()
    {
    }
}
