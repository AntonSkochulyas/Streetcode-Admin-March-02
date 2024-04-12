using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetAll;

public class GetAllImagesMainHandler : IRequestHandler<GetAllImagesMainQuery, Result<IEnumerable<ImageMainDto>>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;

    public GetAllImagesMainHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<ImageMainDto>>> Handle(GetAllImagesMainQuery request, CancellationToken cancellationToken)
    {
        var imagesMain = await _repositoryWrapper.ImageMainRepository.GetAllAsync();

        if (imagesMain is null)
        {
            string errorMsg = MediaErrors.GetAllImagesHandlerCanNotFindAnyImageError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var imageMainDtos = _mapper.Map<IEnumerable<ImageMainDto>>(imagesMain);

        foreach (var image in imageMainDtos)
        {
            image.Base64 = _blobService.FindFileInStorageAsBase64(image.BlobName);
        }

        return Result.Ok(imageMainDtos);
    }
}