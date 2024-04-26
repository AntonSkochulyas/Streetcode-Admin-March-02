using FluentResults;
using MediatR;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Media.ImageMain.GetBaseImage;

public class GetBaseImageMainHandler : IRequestHandler<GetBaseImageMainQuery, Result<MemoryStream>>
{
    private readonly IBlobService _blobStorage;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly ILoggerService _logger;

    public GetBaseImageMainHandler(IBlobService blobService, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
    {
        _blobStorage = blobService;
        _repositoryWrapper = repositoryWrapper;
        _logger = logger;
    }

    public async Task<Result<MemoryStream>> Handle(GetBaseImageMainQuery request, CancellationToken cancellationToken)
    {
        var imageMain = await _repositoryWrapper.ImageMainRepository.GetFirstOrDefaultAsync(a => a.Id == request.Id);

        if (imageMain is null)
        {
            string errorMsg = string.Format(MediaErrors.GetBaseImageHandlerCanNotFindAnAudioWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        return _blobStorage.FindFileInStorageAsMemoryStream(imageMain.BlobName ?? "");
    }
}
