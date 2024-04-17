using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Create;

public class CreateImageMainHandler : IRequestHandler<CreateImageMainCommand, Result<ImageMainDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;

    public CreateImageMainHandler(
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

    public async Task<Result<ImageMainDto>> Handle(CreateImageMainCommand request, CancellationToken cancellationToken)
    {
        string hashBlobStorageName = _blobService.SaveFileInStorage(
            request.Image.BaseFormat ?? "",
            request.Image.Title ?? "",
            request.Image.Extension ?? "");

        var imageMain = _mapper.Map<DAL.Entities.Media.Images.ImageMain>(request.Image);

        imageMain.BlobName = $"{hashBlobStorageName}.{request.Image.Extension}";

        await _repositoryWrapper.ImageMainRepository.CreateAsync(imageMain);
        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        var createdImage = _mapper.Map<ImageMainDto>(imageMain);

        createdImage.Base64 = _blobService.FindFileInStorageAsBase64(createdImage.BlobName ?? "");

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
