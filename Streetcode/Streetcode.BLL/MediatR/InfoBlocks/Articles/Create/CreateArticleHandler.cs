// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    /// <summary>
    /// Handler, thar handles a process of creating an article.
    /// </summary>
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Result<ArticleDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public CreateArticleHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new article.
        /// </summary>
        /// <param name="request">
        /// Request with new article.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A ArticleDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<ArticleDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = _mapper.Map<Article>(request.newArticle);

            if (newArticle is null)
            {
                const string errorMsg = "Cannot convert null to article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.ArticleRepository.Create(newArticle);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<ArticleDto>(entity));
            }
            else
            {
                const string errorMsg = "Failed to create an article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
