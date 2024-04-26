// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.BLL.Dto.Streetcode;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Streetcode.Delete
{
    /// <summary>
    /// Handler, that handles a process of deleting a streetcode.
    /// </summary>
    public class DeleteStreetcodeHandler : IRequestHandler<DeleteStreetcodeCommand, Result<StreetcodeDto>>
    {
        // Repository wrapper
        private readonly IRepositoryWrapper _repository;

        // Mapper
        private readonly IMapper _mapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public DeleteStreetcodeHandler(IRepositoryWrapper repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that deletes a streetcode.
        /// </summary>
        /// <param name="request">
        /// Request with streetcode id to delete.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A StreetcodeDto, or error, if it was while deleting process.
        /// </returns>
        public async Task<Result<StreetcodeDto>> Handle(DeleteStreetcodeCommand request, CancellationToken cancellationToken)
        {
            var streetcode = await _repository.StreetcodeRepository.GetFirstOrDefaultAsync(s => s.Id == request.Id);

            if (streetcode is null)
            {
                string errorMsg = $"The streetcode with id {request.Id} does not exist";
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            _repository.StreetcodeRepository.Delete(streetcode);

            var resultIsSuccess = await _repository.SaveChangesAsync() > 0;
            var streetcodeDto = _mapper.Map<StreetcodeDto>(streetcode);
            if (resultIsSuccess && streetcodeDto != null)
            {
                return Result.Ok(streetcodeDto);
            }
            else
            {
                string errorMsg = StreetcodeErrors.DeleteRelatedTermHandlerFailedToDeleteRelatedTermError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}