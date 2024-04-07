using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Update;
using Streetcode.DAL.Entities.AdditionalContent.Coordinates.Types;
using Streetcode.DAL.Entities.Feedback;
using Streetcode.DAL.Entities.Sources;
using Streetcode.DAL.Entities.Streetcode;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Update
{
    public class UpdateSourceLinkCategoryHandler : IRequestHandler<UpdateSourceLinkCategoryCommand, Result<SourceLinkCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;

        public UpdateSourceLinkCategoryHandler(
            IRepositoryWrapper repositoryWrapper,
            IMapper mapper,
            ILoggerService logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<SourceLinkCategoryDto>> Handle(UpdateSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            var sourceLinkCategory = _mapper.Map<DAL.Entities.Sources.SourceLinkCategory>(request.SourceLinkCategoryContentDto);

            if (sourceLinkCategory is null)
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }

            var streetcode = await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.SourceLinkCategoryContentDto.StreetcodeId);

            if (streetcode is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.UpdateStreetcodeCategoryHandlerCanNotFindStreetcodeWithGivenIdError,
                    request.SourceLinkCategoryContentDto.StreetcodeId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var sourceLink = await _repositoryWrapper.SourceCategoryRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.SourceLinkCategoryContentDto.SourceLinkId);

            if (sourceLink is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.UpdateStreetcodeCategoryHandlerCanNotFindSourceLinkCategoryWithGivenIdError,
                    request.SourceLinkCategoryContentDto.SourceLinkId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcodeCategoryContent = new DAL.Entities.Sources.StreetcodeCategoryContent()
            {
                StreetcodeId = request.SourceLinkCategoryContentDto.StreetcodeId,
                SourceLinkCategoryId = sourceLinkCategory.Id,
                Text = request.SourceLinkCategoryContentDto.Text
            };

            _repositoryWrapper.StreetcodeCategoryContentRepository.Update(streetcodeCategoryContent);

            await _repositoryWrapper.SaveChangesAsync();

            _repositoryWrapper.SourceCategoryRepository.Update(sourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            var response = _mapper.Map<SourceLinkCategoryDto>(sourceLinkCategory);

            if (resultIsSuccess)
            {
                return Result.Ok(response);
            }
            else
            {
                string errorMsg = SourceErrors.UpdateSourceLinkCategoryHandlerFailedToUpdateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
