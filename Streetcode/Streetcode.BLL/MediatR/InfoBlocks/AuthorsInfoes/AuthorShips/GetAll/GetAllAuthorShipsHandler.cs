using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.Articles;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.InfoBlocks.Articles.GetAll;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetAll
{
    public class GetAllAuthorShipsHandler : IRequestHandler<GetAllAuthorShipsQuery, Result<IEnumerable<AuthorShipDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllAuthorShipsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<AuthorShipDto>>> Handle(GetAllAuthorShipsQuery request, CancellationToken cancellationToken)
        {
            var authorShip = await _repositoryWrapper.AuthorShipRepository.GetAllAsync();

            if (authorShip is null)
            {
                const string errorMsg = $"Cannot find any authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<AuthorShipDto>>(authorShip));
        }
    }
}
