// Necessary usings.
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Instagram;
using Streetcode.DAL.Entities.Instagram;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Instagram.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all posts from database.
    /// </summary>
    public class GetAllPostsHandler : IRequestHandler<GetAllPostsQuery, Result<IEnumerable<InstagramPost>>>
    {
        // Instagram service
        private readonly IInstagramService _instagramService;

        // Parametric constructor 
        public GetAllPostsHandler(IInstagramService instagramService)
        {
            _instagramService = instagramService;
        }

        /// <summary>
        /// Method, that gets all post from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all posts from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of InstagramPost, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<InstagramPost>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var result = await _instagramService.GetPostsAsync();
            return Result.Ok(result);
        }
    }
}