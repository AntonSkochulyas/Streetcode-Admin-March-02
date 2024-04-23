// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all category names from database.
    /// </summary>
    public class GetAllCategoryNamesHandler : IRequestHandler<GetAllCategoryNamesQuery, Result<IEnumerable<CategoryWithNameDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllCategoryNamesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all category names from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all category names from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of CategoryWithNameDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<CategoryWithNameDto>>> Handle(GetAllCategoryNamesQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _repositoryWrapper.SourceCategoryRepository.GetItemsBySpecAsync(new GetAllSourceLinkCategorySpec());

            if (allCategories == null)
            {
                string errorMsg = SourceErrors.GetAllCategoryNamesHandlerCategoriesAreNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<CategoryWithNameDto>>(allCategories));
        }
    }
}
