using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorsHyperLinks.GetAll
{
    public class GetAllAuthorShipHyperLinksHandler : IRequestHandler<GetAllAuthorShipHyperLinksQuery, Result<IEnumerable<AuthorShipHyperLinkDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllAuthorShipHyperLinksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<AuthorShipHyperLinkDto>>> Handle(GetAllAuthorShipHyperLinksQuery request, CancellationToken cancellationToken)
        {
            var authorsHyperLinks = await _repositoryWrapper.AuthorShipHyperLinkRepository.GetAllAsync();

            if (authorsHyperLinks is null)
            {
                const string errorMsg = $"Cannot find any authors hyper links";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<AuthorShipHyperLinkDto>>(authorsHyperLinks));
        }
    }
}
