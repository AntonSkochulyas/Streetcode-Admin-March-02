// Necessary usings
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces
namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll
{
    /// <summary>
    /// Handler, that handles a process of get all articles.
    /// </summary>
    public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, Result<IEnumerable<ArticleDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public GetAllArticlesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all articles.
        /// </summary>
        /// <param name="request">
        /// Request to get all articles.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of ArticleDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<ArticleDto>>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = await _repositoryWrapper.ArticleRepository.GetAllAsync();

            if (!articles.Any())
            {
                const string errorMsg = $"Cannot find any article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<ArticleDto>>(articles));
        }
    }
}
