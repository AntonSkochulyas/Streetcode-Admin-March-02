// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a fact.
    /// </summary>
    public class DeleteFactHandler : IRequestHandler<DeleteFactCommand, Result<FactDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteFactHandler(
            IRepositoryWrapper repositoryWrapper,
            ILoggerService logger,
            IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Method, that deletes a fact.
        /// </summary>
        /// <param name="request">
        /// Request with fact id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A FactDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<FactDto>> Handle(DeleteFactCommand request, CancellationToken cancellationToken)
        {
            int id = request.Id;
            var fact = await _repositoryWrapper.FactRepository.GetFirstOrDefaultAsync(n => n.Id == id);
            if (fact == null)
            {
                string errorMsg = string.Format(StreetcodeErrors.DeleteFactHandlerNoFactFoundByEnteredIdError, id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            _repositoryWrapper.FactRepository.Delete(fact);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<FactDto>(fact));
            }
            else
            {
                string errorMsg = StreetcodeErrors.DeleteFactHandlerFailedToDeleteError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}