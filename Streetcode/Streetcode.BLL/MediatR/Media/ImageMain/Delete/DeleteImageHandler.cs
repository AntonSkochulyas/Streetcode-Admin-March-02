using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.Delete;

public class DeleteImageMainHandler : IRequestHandler<DeleteImageMainCommand, Result<Unit>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;

    public DeleteImageMainHandler(IRepositoryWrapper repositoryWrapper, IBlobService blobService, ILoggerService logger)
    {
        _repositoryWrapper = repositoryWrapper;
        _blobService = blobService;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteImageMainCommand request, CancellationToken cancellationToken)
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
            return Result.Ok(Unit.Value);
        }
        else
        {
            string errorMsg = MediaErrors.DeleteImageHandlerFailedToDeleteAnImageError;
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }
    }
}