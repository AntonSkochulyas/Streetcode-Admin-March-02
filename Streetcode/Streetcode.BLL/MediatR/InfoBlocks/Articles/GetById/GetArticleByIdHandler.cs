// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetById
{
    /// <summary>
    /// Handler, that handles a process of getting article by given id.
    /// </summary>
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Result<ArticleDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public GetArticleByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets an article by given id.
        /// </summary>
        /// <param name="request">
        /// Request with article id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A ArticleDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<ArticleDto>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _repositoryWrapper.ArticleRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

            if (article is null)
            {
                string errorMsg = $"Cannot find an article with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<ArticleDto>(article));
        }
    }
}
