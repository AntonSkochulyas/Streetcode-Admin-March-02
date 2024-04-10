// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.InfoBlocks.AuthorsInfoes;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Entities.InfoBlocks.AuthorsInfoes;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.InfoBlocks.AuthorsInfoes.AuthorShips.Update
{
    /// <summary>
    /// Handler, that handles a process of updating an authorship.
    /// </summary>
    public class UpdateAuthorShipHandler : IRequestHandler<UpdateAuthorShipCommand, Result<AuthorShipDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public UpdateAuthorShipHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobSevice, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that updates and authorship.
        /// </summary>
        /// <param name="request">
        /// Request with updated authorship.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A AuthorShipDto, or error, if it was while updating process.
        /// </returns>
        public async Task<Result<AuthorShipDto>> Handle(UpdateAuthorShipCommand request, CancellationToken cancellationToken)
        {
            var authorShip = _mapper.Map<AuthorShip>(request.authorShip);

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
