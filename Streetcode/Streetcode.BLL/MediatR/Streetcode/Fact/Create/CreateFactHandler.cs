// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Fact;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessaru namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Fact.Create
{
    /// <summary>
    /// Handler, that handles a process of creating a new fact.
    /// </summary>
    public class CreateFactHandler : IRequestHandler<CreateFactCommand, Result<FactDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public CreateFactHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that creates a new fact.
        /// </summary>
        /// <param name="request">
        /// Request with a new fact.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A FactDto, or error, if it was while creating process.
        /// </returns>
        public async Task<Result<FactDto>> Handle(CreateFactCommand request, CancellationToken cancellationToken)
        {
            if (await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.StreetcodeId) is null)
            {
                return LogAndReturnError(string.Format(StreetcodeErrors.CreateFactHandlerStreetcodeWithIdDoesNotExistError, request.Fact.FactContent), request);
            }

            if (await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(x => x.Id == request.Fact.ImageId) is null)
            {
                return LogAndReturnError(string.Format(StreetcodeErrors.CreateFactHandlerImageWithIdDoesNotExistError, request.Fact.ImageId), request);
            }

            var fact = _mapper.Map<DAL.Entities.Streetcode.TextContent.Fact>(request.Fact);

            if (fact is null)
            {
                return LogAndReturnError(StreetcodeErrors.CreateFactHandlerCannotConvertNullToFactError, request);
            }

            _repositoryWrapper.FactRepository.Create(fact);

            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;
            if (resultIsSuccess)
            {
                var factDto = _mapper.Map<FactDto>(fact);
                return Result.Ok(factDto);
            }
            else
            {
                return LogAndReturnError(StreetcodeErrors.CreateFactHandlerFailedToCreateFactError, request);
            }
        }

        private Result<FactDto> LogAndReturnError(string errorMsg, CreateFactCommand request)
        {
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}