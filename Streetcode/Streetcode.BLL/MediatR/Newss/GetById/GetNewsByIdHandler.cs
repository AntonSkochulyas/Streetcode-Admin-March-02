// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.News;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Interfaces.Logging;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Newss.GetById
{
    /// <summary>
    /// Handler, that handles a process of getting news by given id.
    /// </summary>
    public class GetNewsByIdHandler : IRequestHandler<GetNewsByIdQuery, Result<NewsDto>>
    {
        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Blob service
        private readonly IBlobService _blobService;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor
        public GetNewsByIdHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _blobService = blobService;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets a news by given id from database.
        /// </summary>
        /// <param name="request">
        /// Request with news id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A NewsDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<NewsDto>> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.id;
            var newsDTO = _mapper.Map<NewsDto>(await _repositoryWrapper.NewsRepository.GetFirstOrDefaultAsync(
                predicate: sc => sc.Id == id,
                include: scl => scl
                    .Include(sc => sc.Image)));
            if (newsDTO is null)
            {
                string errorMsg = string.Format(NewsErrors.GetNewsByIdHandlerCanNotFindANewWithGivenIdError, request.id);
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            if (newsDTO.Image is not null)
            {
                newsDTO.Image.Base64 = _blobService.FindFileInStorageAsBase64(newsDTO.Image.BlobName);
            }

            return Result.Ok(newsDTO);
        }
    }
}