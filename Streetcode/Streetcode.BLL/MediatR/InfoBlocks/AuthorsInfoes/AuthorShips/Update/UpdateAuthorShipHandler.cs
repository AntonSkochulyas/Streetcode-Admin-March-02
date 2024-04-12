using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update
{
    public class UpdateAuthorShipHandler : IRequestHandler<UpdateAuthorShipCommand, Result<AuthorShipDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public UpdateAuthorShipHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<AuthorShipDto>> Handle(UpdateAuthorShipCommand request, CancellationToken cancellationToken)
        {
            var authorShip = _mapper.Map<AuthorShip>(request.AuthorShip);

            if (authorShip is null)
            {
                const string errorMsg = $"Cannot convert null to authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }

            var response = _mapper.Map<AuthorShipDto>(authorShip);

            _repositoryWrapper.AuthorShipRepository.Update(authorShip);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                const string errorMsg = $"Failed to update authorship";

                _logger.LogError(request, errorMsg);

                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
