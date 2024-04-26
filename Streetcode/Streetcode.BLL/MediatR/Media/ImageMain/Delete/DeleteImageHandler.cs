using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Media.Images;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Delete;

public class DeleteImageMainHandler : IRequestHandler<DeleteImageMainCommand, Result<ImageMainDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public DeleteImageMainHandler(IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _blobService = blobService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ImageMainDto>> Handle(DeleteImageMainCommand request, CancellationToken cancellationToken)
    {
        var imageMain = await _repositoryWrapper.ImageMainRepository
            .GetFirstOrDefaultAsync(i => i.Id == request.Id);

        if (imageMain is null)
        {
            string errorMsg = string.Format(MediaErrors.DeleteImageHandlerCanNotFindAnImageWithGivenCategoryIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        _repositoryWrapper.ImageMainRepository.Delete(imageMain);

        var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

        if (resultIsSuccess)
        {
            _blobService.DeleteFileInStorage(imageMain.BlobName);
        }

        if(resultIsSuccess)
        {
            return Result.Ok(_mapper.Map<ImageMainDto>(imageMain));
        }
        else
        {
            string errorMsg = MediaErrors.DeleteImageHandlerFailedToDeleteAnImageError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}