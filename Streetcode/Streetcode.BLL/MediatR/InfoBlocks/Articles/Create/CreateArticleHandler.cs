using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Create
{
    public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, Result<ArticleDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public CreateArticleHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<ArticleDto>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var newArticle = _mapper.Map<Article>(request.NewArticle);

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
