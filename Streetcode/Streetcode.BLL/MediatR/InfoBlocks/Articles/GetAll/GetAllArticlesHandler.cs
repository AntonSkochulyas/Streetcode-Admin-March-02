using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll
{
    public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, Result<IEnumerable<ArticleDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllArticlesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<ArticleDto>>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            var article = await _repositoryWrapper.ArticleRepository.GetAllAsync();

            if (article is null)
            {
                const string errorMsg = $"Cannot find any article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<ArticleDto>>(article));
        }
    }
}
