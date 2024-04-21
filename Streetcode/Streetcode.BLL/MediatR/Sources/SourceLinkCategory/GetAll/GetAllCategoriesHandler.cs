// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetAll
{
    /// <summary>
    /// Handler, that handles a process of getting all categories from database.
    /// </summary>
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<SourceLinkCategoryDto>>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Blob
        private readonly IBlobService _blobService;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetAllCategoriesHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _blobService = blobService;
            _logger = logger;
        }

        /// <summary>
        /// Method, that get all categories from database.
        /// </summary>
        /// <param name="request">
        /// Request to get all categories from database.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of SourceLinkCategoryDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<SourceLinkCategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var allCategories = await _repositoryWrapper.SourceCategoryRepository.GetAllAsync(
                include: cat => cat.Include(img => img.Image) !);
            if (allCategories == null)
            {
                string errorMsg = SourceErrors.GetAllCategoriesHandlerCategoriesAreNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var dtos = _mapper.Map<IEnumerable<SourceLinkCategoryDto>>(allCategories);

            foreach (var dto in dtos)
            {
                dto.Image.Base64 = _blobService.FindFileInStorageAsBase64(dto.Image.BlobName);
            }

            return Result.Ok(dtos);
        }
    }
}
