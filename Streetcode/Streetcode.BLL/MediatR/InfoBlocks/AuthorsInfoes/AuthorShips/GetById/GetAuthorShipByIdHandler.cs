using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.GetById
{
    public class GetAuthorShipByIdHandler : IRequestHandler<GetAuthorShipByIdQuery, Result<AuthorShipDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAuthorShipByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<AuthorShipDto>> Handle(GetAuthorShipByIdQuery request, CancellationToken cancellationToken)
        {
            var authorShip = await _repositoryWrapper.AuthorShipRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

            if (authorShip is null)
            {
                string errorMsg = $"Cannot find an authorship with corresponding id: {request.Id}";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<AuthorShipDto>(authorShip));
        }
    }
}
