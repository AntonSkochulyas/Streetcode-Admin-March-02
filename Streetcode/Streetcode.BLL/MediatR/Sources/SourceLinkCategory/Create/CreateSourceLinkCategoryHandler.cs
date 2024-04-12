using AutoMapper;
using FluentResults;
using MediatR;
using Streetcode.BLL.Dto.Sources;
using Streetcode.BLL.Interfaces.Logging;
using Streetcode.DAL.Repositories.Interfaces.Base;

namespace Streetcode.BLL.MediatR.Sources.SourceLinkCategory.Create
{
    public class CreateSourceLinkCategoryHandler : IRequestHandler<CreateSourceLinkCategoryCommand, Result<SourceLinkCategoryDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly ILoggerService _logger;
        public CreateSourceLinkCategoryHandler(IMapper mapper, IRepositoryWrapper repositoryWrapper, ILoggerService logger)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task<Result<SourceLinkCategoryDto>> Handle(CreateSourceLinkCategoryCommand request, CancellationToken cancellationToken)
        {
            var newSourceLinkCategory = _mapper.Map<DAL.Entities.Sources.SourceLinkCategory>(request.SourceLinkCategoryContentDto);
            if (newSourceLinkCategory is null)
            {
                string errorMsg = SourceErrors.CreateSourceLinkCategoryHandlerCanNotConvertFromNullError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcode = await _repositoryWrapper.StreetcodeRepository.GetFirstOrDefaultAsync(
                x => x.Id == request.SourceLinkCategoryContentDto.StreetcodeId);

            if (streetcode is null)
            {
                string errorMsg = string.Format(
                    SourceErrors.UpdateSourceLinkCategoryCommandValidatorStreetcodeIdeIsRequiredError,
                    request.SourceLinkCategoryContentDto.StreetcodeId);

                _logger.LogError(request, errorMsg);
                return Result.Fail(errorMsg);
            }

            var streetcodeCategoryContent = new DAL.Entities.Sources.StreetcodeCategoryContent()
            {
                StreetcodeId = request.SourceLinkCategoryContentDto.StreetcodeId,
                SourceLinkCategoryId = newSourceLinkCategory.Id,
                Text = request.SourceLinkCategoryContentDto.Text
            };

            newSourceLinkCategory.StreetcodeCategoryContents.Add(streetcodeCategoryContent);

            var entity = _repositoryWrapper.SourceCategoryRepository.Create(newSourceLinkCategory);
            var resultIsSuccess = await _repositoryWrapper.SaveChangesAsync() > 0;

            if (resultIsSuccess)
            {
                return Result.Ok(_mapper.Map<SourceLinkCategoryDto>(entity));
            }
            else
            {
                string errorMsg = SourceErrors.CreateSourceLinkCategoryHandlerFailedToCreateError;
                _logger.LogError(request, errorMsg);
                return Result.Fail(new Error(errorMsg));
            }
        }
    }
}
