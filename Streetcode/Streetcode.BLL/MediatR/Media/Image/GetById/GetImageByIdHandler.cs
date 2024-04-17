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
namespace Streetcode.BLL.MediatR.Media.Image.GetById;

/// <summary>
/// Handler, that handles a process of getting an image by given id.
/// </summary>
public class GetImageByIdHandler : IRequestHandler<GetImageByIdQuery, Result<ImageDto>>
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
    public GetImageByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>
    /// Method, that gets an image from database by given id.
    /// </summary>
    /// <param name="request">
    /// Image id to get.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A ImageDto, or error, if it was while getting process.
    /// </returns>
    public async Task<Result<ImageDto>> Handle(GetImageByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await _repositoryWrapper.ImageRepository.GetFirstOrDefaultAsync(
            f => f.Id == request.Id,
            include: q => q.Include(i => i.ImageDetails)!);

        if (image is null)
        {
            string errorMsg = string.Format(MediaErrors.GetImageByIdHandlerCanNotFindAnImageWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var imageDto = _mapper.Map<ImageDto>(image);
        if (imageDto.BlobName != null)
        {
            imageDto.Base64 = _blobService.FindFileInStorageAsBase64(image.BlobName ?? "");
        }

        return Result.Ok(imageDto);
    }
}