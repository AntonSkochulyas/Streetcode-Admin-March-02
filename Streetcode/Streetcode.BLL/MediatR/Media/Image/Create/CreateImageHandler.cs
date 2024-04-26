// Necessary usings.
using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

// Necessary namespaces.
namespace Streetcode.BLL.MediatR.Media.Image.Create;

/// <summary>
/// Handler, that handles a process of creating a new image.
/// </summary>
public class CreateImageHandler : IRequestHandler<CreateImageCommand, Result<ImageDto>>
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
    public CreateImageHandler(
        IBlobService blobService,
        IRepositoryWrapper repositoryWrapper,
        IMapper mapper,
        ILoggerService logger)
    {
        _blobService = blobService;
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Method, that creates a new image.
    /// </summary>
    /// <param name="request">
    /// Request with new image.
    /// </param>
    /// <param name="cancellationToken">
    /// Cancellation token, for cancelling operation, if it needed.
    /// </param>
    /// <returns>
    /// A ImageDto, or error, if it was while creating process.
    /// </returns>
    public async Task<Result<ImageDto>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        string hashBlobStorageName = _blobService.SaveFileInStorage(
            request.Image.BaseFormat,
            request.Image.Title,
            request.Image.Extension!);

        var image = _mapper.Map<DAL.Entities.Media.Images.Image>(request.Image);

        image.BlobName = hashBlobStorageName;

        await _repositoryWrapper.ImageRepository.CreateAsync(image);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        var createdImage = _mapper.Map<ImageDto>(image);

        createdImage.Base64 = _blobService.FindFileInStorageAsBase64(createdImage.BlobName);

        if(resultIsSuccess)
        {
            return Result.Ok(createdImage);
        }
        else
        {
            string errorMsg = MediaErrors.CreateImageHandlerFailedToCreateAnAudioError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}
