using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.Sources.SourceLinkCategory.GetCategoryContentByStreetcodeId;
using Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetCategoryContentsByStreetcodeIdAndSourceLinkCategoryId;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryId
{
    public class GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandler : IRequestHandler<GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdQuery, Result<IEnumerable<StreetcodeCategoryContentDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<IEnumerable<StreetcodeCategoryContentDto>>> Handle(GetAllCategoryContentsByStreetcodeIdAndSourceLinkCategoryIdQuery request, CancellationToken cancellationToken)
        {
            if ((await _repositoryWrapper.StreetcodeRepository
                .GetFirstOrDefaultAsync(s => s.Id == request.streetcodeId)) == null)
            {
                string errorMsg = string.Format(SourceErrors.GetAllCategoryContentsBySteetcodeIdAndSourceLinkCategoryIdHandlerWithGivenStreetcodeIdError, request.streetcodeId);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var streetcodeContent = await _repositoryWrapper.StreetcodeCategoryContentRepository
                .GetAllAsync(
                    sc => sc.StreetcodeId == request.streetcodeId && sc.SourceLinkCategoryId == request.categoryId);

            if (streetcodeContent == null)
            {
                string errorMsg = string.Format(SourceErrors.GetAllCategoryContentsBySteetcodeIdAndSourceLinkCategoryIdHandlerStreetcodeIsNullError);
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            return Result.Ok(_mapper.Map<IEnumerable<StreetcodeCategoryContentDto>>(streetcodeContent));
        }
    }
}
