using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.StreetcodeCategoryContent.Create
{
    public class CreateStreetcodeCategoryContentHandler : IRequestHandler<CreateStreetcodeCategoryContentCommand, Result<CategoryContentCreatedDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public CreateStreetcodeCategoryContentHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<CategoryContentCreatedDto>> Handle(CreateStreetcodeCategoryContentCommand request, CancellationToken cancellationToken)
        {
            var newStreetcodeCategoryContent = _mapper.Map<DAL.Entities.Sources.StreetcodeCategoryContent>(request.StreetcodeCategoryContentDto);
            if (newStreetcodeCategoryContent is null)
            {
                string errorMsg = SourceErrors.CreateStreetcodeCategoryContentHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcode = await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.StreetcodeCategoryContentDto.StreetcodeId);

            if (streetcode is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.CreateStreetcodeCategoryContentCommandValidatorStreetcodeIdIsRequiredError,
                    request.StreetcodeCategoryContentDto.StreetcodeId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var sourceLink = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.StreetcodeCategoryContentDto.SourceLinkCategoryId);

            if (sourceLink is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.CreateStreetcodeCategoryContentCommandValidatorSourceLinkIdIsRequiredError,
                    request.StreetcodeCategoryContentDto.SourceLinkCategoryId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var entity = _repositoryWrapper.StreetcodeCategoryContentRepository.Create(newStreetcodeCategoryContent);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<CategoryContentCreatedDto>(entity));
            }
            else
            {
                string errorMsg = SourceErrors.CreateStreetcodeCategoryContentHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
