using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.BlobStorage;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;
using Streetcode.DAL.Specification.Sources.SourceLinkCategory;

namespace Streetcode.BLL.MediatR.Sources.SourceLink.GetCategoryById;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, Result<SourceLinkCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IBlobService _blobService;
    private readonly ILoggerService _logger;

    public GetCategoryByIdHandler(
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

    public async Task<Result<SourceLinkCategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var srcCategories = await _repositoryWrapper
            .SourceCategoryRepository
            .GetItemBySpecAsync(new GetByIdSourceLinkCategoryIncludeSpec(request.Id));

        if (srcCategories is null)
        {
            string errorMsg = string.Format(SourceErrors.GetSourceLinkCategoryByIdHandlerCanNotFindANewWithGivenIdError, request.Id);
            _logger.LogError(request, errorMsg);
            return Result.Fail(new Error(errorMsg));
        }

        var mappedSrcCategories = _mapper.Map<SourceLinkCategoryDto>(srcCategories);

        mappedSrcCategories.Image.Base64 = _blobService.FindFileInStorageAsBase64(mappedSrcCategories.Image.BlobName ?? "");

        return Result.Ok(mappedSrcCategories);
    }
}