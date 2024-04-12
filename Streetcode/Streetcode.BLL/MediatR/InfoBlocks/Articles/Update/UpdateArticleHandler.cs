using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.Articles;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.Articles.Update
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand, Result<ArticleDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UpdateArticleHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<ArticleDto>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<Article>(request.Article);

            if (article is null)
            {
                const string errorMsg = $"Cannot convert null to article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<ArticleDto>(article);

            _repositoryWrapper.ArticleRepository.Update(article);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                const string errorMsg = $"Failed to update article";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
