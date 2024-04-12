// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.Dto.Media.Art;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Art.GetByStreetcodeId
{
    /// <summary>
    /// Handler, that handles a process of getting art by streetcode id.
    /// </summary>
    public class GetArtsByStreetcodeIdHandler : IRequestHandler<GetArtsByStreetcodeIdQuery, Result<IEnumerable<ArtDto>>>
    {
        // Blob service
        private readonly IBlobService _blobService;

        // Mapper
        private readonly IMapper _mapper;

        // Repository wrapper
        private readonly IRepositoryWrapper _repositoryWrapper;

        // Logger
        private readonly ILoggerService _logger;

        // Parametric constructor 
        public GetArtsByStreetcodeIdHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            IBlobService blobService,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _blobService = blobService;
            _logger = logger;
        }

        /// <summary>
        /// Method, that gets an art by given streetcode id.
        /// </summary>
        /// <param name="request">
        /// Request with art streetcode id to get.
        /// </param>
        /// <param name="cancellationToken">
        /// Cancellation token, for cancelling operation, if it needed.
        /// </param>
        /// <returns>
        /// A IEnumerable of ArtDto, or error, if it was while getting process.
        /// </returns>
        public async Task<Result<IEnumerable<ArtDto>>> Handle(GetArtsByStreetcodeIdQuery request, CancellationToken cancellationToken)
        {
            var arts = await _repositoryWrapper.ArtRepository
                .GetAllAsync(
                predicate: sc => sc.StreetcodeArts.Any(s => s.StreetcodeId == request.StreetcodeId),
                include: scl => scl
                    .Include(sc => sc.Image)!);

            if (arts is null)
            {
                string errorMsg = string.Format(MediaErrors.GetArtsByStreetcodeIdHandlerCanNotFindArtWithGivenStreetcodeIdError, request.StreetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var imageIds = arts.Where(a => a.Image != null).Select(a => a.Image!.Id);

            var artsDto = _mapper.Map<IEnumerable<ArtDto>>(arts);
            foreach (var artDto in artsDto)
            {
                if (artDto.Image != null && artDto.Image.BlobName != null)
                {
                    artDto.Image.Base64 = _blobService.FindFileInStorageAsBase64(artDto.Image.BlobName);
                }
            }

            return Result.Ok(artsDto);
        }
    }
}
