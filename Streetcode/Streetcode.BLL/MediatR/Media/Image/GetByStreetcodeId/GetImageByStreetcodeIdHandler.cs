// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.GetByStreetcodeId;

/// <summary>
/// Handler, that handles a process of getting image by streetcode id from database.
/// </summary>
public class GetImageByStreetcodeIdHandler : IRequestHandler<GetImageByStreetcodeIdQuery, Result<IEnumerable<ImageDto>>>
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
    public GetImageByStreetcodeIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets an image by streetcode id.
    /// </summary>
    /// <param name="request">
    /// Request with image id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A IEnumerable of ImageDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<IEnumerable<ImageDto>>> Handle(GetImageByStreetcodeIdQuery request, CancellationToken cancellationToken)
    {
        var images = (await _repositoryWrapper.ImageRepository
            .GetAllAsync(
            f => f.Streetcodes.Any(s => s.Id == request.StreetcodeId),
            include: q => q.Include(img => img.ImageDetails))).OrderBy(img => img.ImageDetails?.Alt);

        if (images is null || images.Count() == 0)
        {
            string errorMsg = string.Format(MediaErrors.GetImageByStreetcodeIdHandlerCanNotFindAnImageWithGivenStreetcodeIdError, request.StreetcodeId);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var imageDtos = _mapper.Map<IEnumerable<ImageDto>>(images);

        foreach (var image in imageDtos)
        {
            image.Base64 = _blobService.FindFileInStorageAsBase64(image.BlobName);
        }

        return Result.Ok(imageDtos);
    }
}