using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetById;

public class GetImageMainByIdHandler : IRequestHandler<GetImageMainByIdQuery, Result<ImageMainDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;

    public GetImageMainByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
        _blobService = blobService;
        _logger = logger;
    }

    public async Task<Result<ImageMainDto>> Handle(GetImageMainByIdQuery request, CancellationToken cancellationToken)
    {
        var image = await _repositoryWrapper.ImageMainRepository.GetFirstOrDefaultAsync(
            f => f.Id == request.Id);

        if (image is null)
        {
            string errorMsg = string.Format(MediaErrors.GetImageByIdHandlerCanNotFindAnImageWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var imageMainDto = _mapper.Map<ImageMainDto>(image);
        if(imageMainDto.BlobName != null)
        {
            imageMainDto.Base64 = _blobService.FindFileInStorageAsBase64(image.BlobName);
        }

        return Result.Ok(imageMainDto);
    }
}