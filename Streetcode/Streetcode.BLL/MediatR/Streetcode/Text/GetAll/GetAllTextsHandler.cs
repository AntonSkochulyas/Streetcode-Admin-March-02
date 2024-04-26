// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Streetcode.TextContent.Text;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Streetcode.Text.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all texts from database.
    /// </summary>
    public class GetAllTextsHandler : IRequestHandler<GetAllTextsQuery, Result<IEnumerable<TextDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllTextsHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all texts from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all texts from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of TextDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<TextDto>>> Handle(GetAllTextsQuery request, CancellationToken cancellationToken)
        {
            var texts = await _repositoryWrapper.TextRepository.GetAllAsync();

            if (texts is null)
            {
                string errorMsg = StreetcodeErrors.GetAllTextsHandlerCannotFindAnyTextError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<TextDto>>(texts));
        }
    }
}